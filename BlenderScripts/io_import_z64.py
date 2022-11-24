# zelda64-import-blender
# Import models from Zelda64 files into Blender
# Copyright (C) 2013 SoulofDeity
# Copyright (C) 2020 Dragorn421
#
# This program is free software: you can redistribute it and/or modify
# it under the terms of the GNU Lesser General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
#
# This program is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU Lesser General Public License for more details.
#
# You should have received a copy of the GNU Lesser General Public License
# along with this program.  If not, see <https://www.gnu.org/licenses/>

bl_info = {
    "name":        "Zelda64 Importer",
    "version":     (2, 3),
    "author":      "SoulofDeity",
    "blender":     (2, 6, 0),
    "location":    "File > Import-Export",
    "description": "Import Zelda64 - updated in 2020",
    "warning":     "",
    "wiki_url":    "https://github.com/Dragorn421/zelda64-import-blender",
    "tracker_url": "https://github.com/Dragorn421/zelda64-import-blender",
    "support":     'COMMUNITY',
    "category":    "Import-Export"}

"""Anim stuff: RodLima http://www.facebook.com/rod.lima.96?ref=tn_tnmn"""

import bpy, os, struct, time
import mathutils
import re

from bpy import ops
from bpy.props import *
from bpy_extras.image_utils import load_image
from bpy_extras.io_utils import ExportHelper, ImportHelper
from math import *
from mathutils import *
from struct import pack, unpack_from

from mathutils import Vector, Euler, Matrix

# logging stuff, code mostly uses getLogger()
import logging

# https://stackoverflow.com/questions/2183233/how-to-add-a-custom-loglevel-to-pythons-logging-facility
logging_trace_level = 5
logging.addLevelName(logging_trace_level, 'TRACE')

def getLogger(name):
    global root_logger
    log = root_logger.getChild(name)
    def trace(message, *args, **kws):
        if log.isEnabledFor(logging_trace_level):
            log._log(logging_trace_level, message, args, **kws)
    log.trace = trace
    return log

def registerLogging(level=logging.INFO):
    global root_logger, root_logger_formatter, root_logger_stream_handler, root_logger_file_handler, root_logger_operator_report_handler
    root_logger = logging.getLogger('z64import')
    root_logger_stream_handler = logging.StreamHandler()
    root_logger_file_handler = None
    root_logger_operator_report_handler = None
    root_logger_formatter = logging.Formatter('%(levelname)s:%(name)s: %(message)s')
    root_logger_stream_handler.setFormatter(root_logger_formatter)
    root_logger.addHandler(root_logger_stream_handler)
    root_logger.setLevel(1) # actual level filtering is left to handlers
    root_logger_stream_handler.setLevel(level)
    getLogger('setupLogging').debug('Logging OK')

def setLoggingLevel(level):
    global root_logger_stream_handler
    root_logger_stream_handler.setLevel(level)

def setLogFile(path):
    global root_logger, root_logger_formatter, root_logger_file_handler
    if root_logger_file_handler:
        root_logger.removeHandler(root_logger_file_handler)
        root_logger_file_handler = None
    if path:
        root_logger_file_handler = logging.FileHandler(path, mode='w')
        root_logger_file_handler.setFormatter(root_logger_formatter)
        root_logger.addHandler(root_logger_file_handler)
        root_logger_file_handler.setLevel(1)

class OperatorReportLogHandler(logging.Handler):
    def __init__(self, operator):
        super().__init__()
        self.operator = operator

    def flush(self):
        pass

    def emit(self, record):
        try:
            type = 'DEBUG'
            for levelType,  minLevel in (
                ('ERROR',   logging.WARNING),
                ('WARNING', logging.INFO),
                ('INFO',    logging.DEBUG)
            ):
                if record.levelno > minLevel:
                    type = levelType
                    break
            msg = self.format(record)
            self.operator.report({type}, msg)
        except Exception:
            self.handleError(record)

def setLogOperator(operator, level=logging.INFO):
    global root_logger, root_logger_formatter, root_logger_operator_report_handler
    if root_logger_operator_report_handler:
        root_logger.removeHandler(root_logger_operator_report_handler)
        root_logger_operator_report_handler = None
    if operator:
        root_logger_operator_report_handler = OperatorReportLogHandler(operator)
        root_logger_operator_report_handler.setFormatter(root_logger_formatter)
        root_logger_operator_report_handler.setLevel(logging.WARNING)
        root_logger.addHandler(root_logger_operator_report_handler)

def unregisterLogging():
    global root_logger, root_logger_stream_handler
    setLogFile(None)
    setLogOperator(None)
    root_logger.removeHandler(root_logger_stream_handler)
#

def splitOffset(offset):
    return offset >> 24, offset & 0x00FFFFFF

def translateRotation(rot):
    """ axis, angle """
    return Matrix.Rotation(rot[3], 4, Vector(rot[:3]))

def validOffset(segment, offset):
    seg, offset = splitOffset(offset)
    if seg > 15:
        return False
    if offset >= len(segment[seg]):
        return False
    return True

def pow2(val):
    i = 1
    while i < val:
        i <<= 1
    return int(i)

def powof(val):
    num, i = 1, 0
    while num < val:
        num <<= 1
        i += 1
    return int(i)

def checkUseVertexAlpha():
    global useVertexAlpha
    return useVertexAlpha

class Tile:
    def __init__(self):
        self.current_texture_file_path = None
        self.texFmt, self.texBytes = 0x00, 0
        self.width, self.height = 0, 0
        self.rWidth, self.rHeight = 0, 0
        self.texSiz = 0
        self.lineSize = 0
        self.rect = Vector([0, 0, 0, 0])
        self.scale = Vector([1, 1])
        self.ratio = Vector([1, 1])
        self.clip = Vector([0, 0])
        self.mask = Vector([0, 0])
        self.shift = Vector([0, 0])
        self.tshift = Vector([0, 0])
        self.offset = Vector([0, 0])
        self.data = 0x00000000
        self.palette = 0x00000000

    def getFormatName(self):
        fmt = ['RGBA','YUV','CI','IA','I']
        siz = ['4','8','16','32']
        return '%s%s' % (
            fmt[self.texFmt] if self.texFmt < len(fmt) else 'UnkFmt',
            siz[self.texSiz] if self.texSiz < len(siz) else '_UnkSiz'
        )

    def create(self, segment, use_transparency):
        # todo texture files are written several times, at each usage
        log = getLogger('Tile.create')
        fmtName = self.getFormatName()
        #Noka here
        extrastring = ""
        w = self.rWidth
        if int(self.clip.x) & 1 != 0:
            if replicateTexMirrorBlender:
                w <<= 1
            if enableTexMirrorSharpOcarinaTags:
                extrastring += "#MirrorX"
        h = self.rHeight
        if int(self.clip.y) & 1 != 0:
            if replicateTexMirrorBlender:
                h <<= 1
            if enableTexMirrorSharpOcarinaTags:
                extrastring += "#MirrorY"
        if int(self.clip.x) & 2 != 0 and enableTexClampSharpOcarinaTags:
            extrastring += "#ClampX"
        if int(self.clip.y) & 2 != 0 and enableTexClampSharpOcarinaTags:
            extrastring += "#ClampY"
        self.current_texture_file_path = '%s/textures/%s_%08X%s.tga' % (fpath, fmtName, self.data, extrastring)
        if exportTextures: # fixme exportTextures == False breaks the script
            try:
                os.mkdir(fpath + "/textures")
            except FileExistsError:
                pass
            except:
                log.exception('Could not create textures directory %s' % (fpath + "/textures"))
                pass
            if not os.path.isfile(self.current_texture_file_path):
                log.debug('Writing texture %s (format 0x%02X)' % (self.current_texture_file_path, self.texFmt))
                file = open(self.current_texture_file_path, 'wb')
                self.write_error_encountered = False
                if self.texFmt == 2:
                    if self.texSiz not in (0, 1):
                        log.error('Unknown texture format %d with pixel size %d', self.texFmt, self.texSiz)
                    p = 16 if self.texSiz == 0 else 256
                    file.write(pack("<BBBHHBHHHHBB",
                        0,  # image comment length
                        1,  # 1 = paletted
                        1,  # 1 = indexed uncompressed colors
                        0,  # index of first palette entry (?)
                        p,  # amount of entries in palette
                        32, # bits per pixel
                        0,  # bottom left X (?)
                        0,  # bottom left Y (?)
                        w,  # width
                        h,  # height
                        8,  # pixel depth
                        8   # 8 bits alpha hopefully?
                    ))
                    self.writePalette(file, segment, p)
                else:
                    file.write(pack("<BBBHHBHHHHBB",
                        0, # image comment length
                        0, # no palette
                        2, # uncompressed Truecolor (24-32 bits)
                        0, # irrelevant, no palette
                        0, # irrelevant, no palette
                        0, # irrelevant, no palette
                        0, # bottom left X (?)
                        0, # bottom left Y (?)
                        w, # width
                        h, # height
                        32,# pixel depth
                        8  # 8 bits alpha (?)
                    ))
                if int(self.clip.y) & 1 != 0 and replicateTexMirrorBlender:
                    self.writeImageData(file, segment, True)
                else:
                    self.writeImageData(file, segment)
                file.close()
                if self.write_error_encountered:
                    oldName = self.current_texture_file_path
                    oldNameDir, oldNameBase = os.path.split(oldName)
                    newName = '%s/fallback_%s' % (oldNameDir, oldNameBase)
                    log.warning('Moving failed texture file import from %s to %s', oldName, newName)
                    if os.path.isfile(newName):
                        os.remove(newName)
                    os.rename(oldName, newName)
                    self.current_texture_file_path = newName
        try:
            tex_name = 'tex_%s_%08X' % (fmtName,self.data)
            tex = bpy.data.textures.new(name=tex_name, type='IMAGE')
            img = load_image(self.current_texture_file_path)
            if img:
                tex.image = img
                if int(self.clip.x) & 2 != 0 and enableTexClampBlender:
                    img.use_clamp_x = True
                if int(self.clip.y) & 2 != 0 and enableTexClampBlender:
                    img.use_clamp_y = True
            mtl_name = 'mtl_%08X' % self.data
            mtl = bpy.data.materials.new(name=mtl_name)
            if enableShadelessMaterials:
                mtl.use_shadeless = True
            mt = mtl.texture_slots.add()
            mt.texture = tex
            mt.texture_coords = 'UV'
            mt.use_map_color_diffuse = True
            if use_transparency:
                mt.use_map_alpha = True
                tex.use_mipmap = True
                tex.use_interpolation = True
                tex.use_alpha = True
                mtl.use_transparency = True
                mtl.alpha = 0.0
                mtl.game_settings.alpha_blend = 'ALPHA'
            return mtl
        except:
            log.exception('Failed to create material mtl_%08X %r', self.data)
            return None

    def calculateSize(self):
        log = getLogger('Tile.calculateSize')
        maxTxl, lineShift = 0, 0
        # fixme what is maxTxl? this whole function is rather mysterious, not sure how/why it works
        #texFmt 0 2 texSiz 0
        # RGBA CI 4b
        if (self.texFmt == 0 or self.texFmt == 2) and self.texSiz == 0:
            maxTxl = 4096
            lineShift = 4
        # texFmt 3 4 texSiz 0
        # IA I 4b
        elif (self.texFmt == 3 or self.texFmt == 4) and self.texSiz == 0:
            maxTxl = 8192
            lineShift = 4
        # texFmt 0 2 texSiz 1
        # RGBA CI 8b
        elif (self.texFmt == 0 or self.texFmt == 2) and self.texSiz == 1:
            maxTxl = 2048
            lineShift = 3
        # texFmt 3 4 texSiz 1
        # IA I 8b
        elif (self.texFmt == 3 or self.texFmt == 4) and self.texSiz == 1:
            maxTxl = 4096
            lineShift = 3
        # texFmt 0 3 texSiz 2
        # RGBA IA 16b
        elif (self.texFmt == 0 or self.texFmt == 3) and self.texSiz == 2:
            maxTxl = 2048
            lineShift = 2
        # texFmt 2 4 texSiz 2
        # CI I 16b
        elif (self.texFmt == 2 or self.texFmt == 4) and self.texSiz == 2:
            maxTxl = 2048
            lineShift = 0
        # texFmt 0 texSiz 3
        # RGBA 32b
        elif self.texFmt == 0 and self.texSiz == 3:
            maxTxl = 1024
            lineShift = 2
        else:
            log.warning('Unknown format for texture %s texFmt %d texSiz %d', self.current_texture_file_path, self.texFmt, self.texSiz)
        lineWidth = self.lineSize << lineShift
        self.lineSize = lineWidth
        tileWidth = self.rect.z - self.rect.x + 1
        tileHeight = self.rect.w - self.rect.y + 1
        maskWidth = 1 << int(self.mask.x)
        maskHeight = 1 << int(self.mask.y)
        lineHeight = 0
        if lineWidth > 0:
            lineHeight = min(int(maxTxl / lineWidth), tileHeight)
        if self.mask.x > 0 and (maskWidth * maskHeight) <= maxTxl:
            self.width = maskWidth
        elif (tileWidth * tileHeight) <= maxTxl:
            self.width = tileWidth
        else:
            self.width = lineWidth
        if self.mask.y > 0 and (maskWidth * maskHeight) <= maxTxl:
            self.height = maskHeight
        elif (tileWidth * tileHeight) <= maxTxl:
            self.height = tileHeight
        else:
            self.height = lineHeight
        clampWidth, clampHeight = 0, 0
        if self.clip.x == 1:
            clampWidth = tileWidth
        else:
            clampWidth = self.width
        if self.clip.y == 1:
            clampHeight = tileHeight
        else:
            clampHeight = self.height
        if maskWidth > self.width:
            self.mask.x = powof(self.width)
            maskWidth = 1 << int(self.mask.x)
        if maskHeight > self.height:
            self.mask.y = powof(self.height)
            maskHeight = 1 << int(self.mask.y)
        if int(self.clip.x) & 2 != 0:
            self.rWidth = pow2(clampWidth)
        elif int(self.clip.x) & 1 != 0:
            self.rWidth = pow2(maskWidth)
        else:
            self.rWidth = pow2(self.width)
        if int(self.clip.y) & 2 != 0:
            self.rHeight = pow2(clampHeight)
        elif int(self.clip.y) & 1 != 0:
            self.rHeight = pow2(maskHeight)
        else:
            self.rHeight = pow2(self.height)
        self.shift.x, self.shift.y = 1.0, 1.0
        if self.tshift.x > 10:
            self.shift.x = 1 << int(16 - self.tshift.x)
        elif self.tshift.x > 0:
            self.shift.x /= 1 << int(self.tshift.x)
        if self.tshift.y > 10:
            self.shift.y = 1 << int(16 - self.tshift.y)
        elif self.tshift.y > 0:
            self.shift.y /= 1 << int(self.tshift.y)
        self.ratio.x = (self.scale.x * self.shift.x) / self.rWidth
        if not enableToon:
            self.ratio.x /= 32;
        if int(self.clip.x) & 1 != 0 and replicateTexMirrorBlender:
            self.ratio.x /= 2
        self.offset.x = self.rect.x
        self.ratio.y = (self.scale.y * self.shift.y) / self.rHeight
        if not enableToon:
            self.ratio.y /= 32;
        if int(self.clip.y) & 1 != 0 and replicateTexMirrorBlender:
            self.ratio.y /= 2
        self.offset.y = 1.0 + self.rect.y

    def writePalette(self, file, segment, palSize):
        log = getLogger('Tile.writePalette')
        if not validOffset(segment, self.palette + palSize * 2 - 1):
            log.error('Segment offsets 0x%X-0x%X are invalid, writing black palette to %s (has the segment data been loaded?)' % (self.palette, self.palette + palSize * 2 - 1, self.current_texture_file_path))
            for i in range(palSize):
                file.write(pack("L", 0))
            self.write_error_encountered = True
            return
        seg, offset = splitOffset(self.palette)
        for i in range(palSize):
            color = unpack_from(">H", segment[seg], offset + i * 2)[0]
            r = int(255/31 * ((color >> 11) & 0b11111))
            g = int(255/31 * ((color >> 6) & 0b11111))
            b = int(255/31 * ((color >> 1) & 0b11111))
            a = 255 * (color & 1)
            file.write(pack("BBBB", b, g, r, a))

    def writeImageData(self, file, segment, fy=False, df=False):
        log = getLogger('Tile.writeImageData')
        if fy == True:
            dir = (0, self.rHeight, 1)
        else:
            dir = (self.rHeight - 1, -1, -1)
        if self.texSiz <= 3:
            bpp = (0.5,1,2,4)[self.texSiz] # bytes (not bits) per pixel
        else:
            log.warning('Unknown texSiz %d for texture %s, defaulting to 4 bytes per pixel' % (self.texSiz, self.current_texture_file_path))
            bpp = 4
        lineSize = self.rWidth * bpp
        writeFallbackData = False
        if not validOffset(segment, self.data + int(self.rHeight * lineSize) - 1):
            log.error('Segment offsets 0x%X-0x%X are invalid, writing default fallback colors to %s (has the segment data been loaded?)' % (self.data, self.data + int(self.rHeight * lineSize) - 1, self.current_texture_file_path))
            writeFallbackData = True
        if (self.texFmt,self.texSiz) not in (
            (0,2), (0,3), # RGBA16, RGBA32
            #(1,-1), # YUV ? "not used in z64 games"
            (2,0), (2,1), # CI4, CI8
            (3,0), (3,1), (3,2), # IA4, IA8, IA16
            (4,0), (4,1), # I4, I8
        ):
            log.error('Unknown fmt/siz combination %d/%d (%s?)', self.texFmt, self.texSiz, self.getFormatName())
            writeFallbackData = True
        if writeFallbackData:
            size = self.rWidth * self.rHeight
            if int(self.clip.x) & 1 != 0 and replicateTexMirrorBlender:
                size *= 2
            if int(self.clip.y) & 1 != 0 and replicateTexMirrorBlender:
                size *= 2
            for i in range(size):
                if self.texFmt == 2: # CI (paletted)
                    file.write(pack("B", 0))
                else:
                    file.write(pack(">L", 0x000000FF))
            self.write_error_encountered = True
            return
        seg, offset = splitOffset(self.data)
        for i in range(dir[0], dir[1], dir[2]):
            off = offset + int(i * lineSize)
            line = []
            j = 0
            while j < int(self.rWidth * bpp):
                if bpp < 2: # 0.5, 1
                    color = unpack_from("B", segment[seg], off + int(floor(j)))[0]
                    if bpp == 0.5:
                        color = ((color >> 4) if j % 1 == 0 else color) & 0xF
                elif bpp == 2:
                    color = unpack_from(">H", segment[seg], off + j)[0]
                else: # 4
                    color = unpack_from(">L", segment[seg], off + j)[0]
                if self.texFmt == 0: # RGBA
                    if self.texSiz == 2: # RGBA16
                        r = ((color >> 11) & 0b11111) * 255 // 31
                        g = ((color >> 6) & 0b11111) * 255 // 31
                        b = ((color >> 1) & 0b11111) * 255 // 31
                        a = (color & 1) * 255
                    elif self.texSiz == 3: # RGBA32
                        r = (color >> 24) & 0xFF
                        g = (color >> 16) & 0xFF
                        b = (color >> 8) & 0xFF
                        a = color & 0xFF
                elif self.texFmt == 2: # CI
                    if self.texSiz == 0: # CI4
                        p = color
                    elif self.texSiz == 1: # CI8
                        p = color
                elif self.texFmt == 3: # IA
                    if self.texSiz == 0: # IA4
                        r = g = b = (color >> 1) * 255 // 7
                        a = (color & 1) * 255
                    elif self.texSiz == 1: # IA8
                        r = g = b = (color >> 4) * 255 // 15
                        a = (color & 0xF) * 255 // 15
                    elif self.texSiz == 2: # IA16
                        r = g = b = color >> 8
                        a = color & 0xFF
                elif self.texFmt == 4: # I
                    if self.texSiz == 0: # I4
                        r = g = b = a = color * 255 // 15
                    elif self.texSiz == 1: # I8
                        r = g = b = a = color
                try:
                    if self.texFmt == 2: # CI
                        line.append(p)
                    else:
                        line.append((b << 24) | (g << 16) | (r << 8) | a)
                except UnboundLocalError:
                    log.error('Unknown format texFmt %d texSiz %d', self.texFmt, self.texSiz)
                    raise
                """
                if self.texFmt == 0x40 or self.texFmt == 0x48 or self.texFmt == 0x50:
                    line.append(a)
                else:
                    line.append((b << 24) | (g << 16) | (r << 8) | a)
                """
                j += bpp
            if self.texFmt == 2: # CI # in (0x40, 0x48, 0x50):
                file.write(pack("B" * len(line), *line))
            else:
                file.write(pack(">" + "L" * len(line), *line))
            if int(self.clip.x) & 1 != 0 and replicateTexMirrorBlender:
                line.reverse()
                if self.texFmt == 2: # CI # in (0x40, 0x48, 0x50):
                    file.write(pack("B" * len(line), *line))
                else:
                    file.write(pack(">" + "L" * len(line), *line))
        if int(self.clip.y) & 1 != 0 and df == False and replicateTexMirrorBlender:
            if fy == True:
                self.writeImageData(file, segment, False, True)
            else:
                self.writeImageData(file, segment, True, True)


class Vertex:
    def __init__(self):
        self.pos = Vector([0, 0, 0])
        self.uv = Vector([0, 0])
        self.normal = Vector([0, 0, 0])
        self.color = [0, 0, 0, 0]
        self.limb = None

    def read(self, segment, offset):
        log = getLogger('Vertex.read')
        if not validOffset(segment, offset + 16):
            log.warning('Invalid segmented offset 0x%X for vertex' % (offset + 16))
            return
        seg, offset = splitOffset(offset)
        self.pos.x = unpack_from(">h", segment[seg], offset)[0]
        self.pos.z = unpack_from(">h", segment[seg], offset + 2)[0]
        self.pos.y = -unpack_from(">h", segment[seg], offset + 4)[0]
        global scaleFactor
        self.pos *= scaleFactor
        self.uv.x = float(unpack_from(">h", segment[seg], offset + 8)[0])
        self.uv.y = float(unpack_from(">h", segment[seg], offset + 10)[0])
        self.normal.x = unpack_from("b", segment[seg], offset + 12)[0] / 128
        self.normal.z = unpack_from("b", segment[seg], offset + 13)[0] / 128
        self.normal.y = -unpack_from("b", segment[seg], offset + 14)[0] / 128
        self.color[0] = min(segment[seg][offset + 12] / 255, 1.0)
        self.color[1] = min(segment[seg][offset + 13] / 255, 1.0)
        self.color[2] = min(segment[seg][offset + 14] / 255, 1.0)
        if checkUseVertexAlpha():
            self.color[3] = min(segment[seg][offset + 15] / 255, 1.0)


class Mesh:
    def __init__(self):
        self.verts, self.uvs, self.colors, self.faces = [], [], [], []
        self.faces_use_smooth = []
        self.vgroups = {}
        # import normals
        self.normals = []

    def create(self, name_format, hierarchy, offset, use_normals):
        log = getLogger('Mesh.create')
        if len(self.faces) == 0:
            log.trace('Skipping empty mesh %08X', offset)
            if self.verts:
                log.warning('Discarding unused vertices, no faces')
            return
        log.trace('Creating mesh %08X', offset)

        me_name = name_format % ('me_%08X' % offset)
        me = bpy.data.meshes.new(me_name)
        ob = bpy.data.objects.new(name_format % ('ob_%08X' % offset), me)
        bpy.context.scene.objects.link(ob)
        bpy.context.scene.objects.active = ob
        me.vertices.add(len(self.verts))

        for i in range(len(self.verts)):
            me.vertices[i].co = self.verts[i]
        me.tessfaces.add(len(self.faces))
        vcd = me.tessface_vertex_colors.new().data
        for i in range(len(self.faces)):
            me.tessfaces[i].vertices = self.faces[i]
            me.tessfaces[i].use_smooth = self.faces_use_smooth[i]

            vcd[i].color1 = self.colors[i * 3]
            vcd[i].color2 = self.colors[i * 3 + 1]
            vcd[i].color3 = self.colors[i * 3 + 2]
        uvd = me.tessface_uv_textures.new().data
        for i in range(len(self.faces)):
            material = self.uvs[i * 4]
            if material:
                if not material.name in me.materials:
                    me.materials.append(material)
                uvd[i].image = material.texture_slots[0].texture.image
            uvd[i].uv[0] = self.uvs[i * 4 + 1]
            uvd[i].uv[1] = self.uvs[i * 4 + 2]
            uvd[i].uv[2] = self.uvs[i * 4 + 3]
        me.calc_normals()
        me.validate()
        me.update()

        log.debug('me =\n%r', me)
        log.debug('verts =\n%r', self.verts)
        log.debug('faces =\n%r', self.faces)
        log.debug('normals =\n%r', self.normals)

        if use_normals:
            # fixme make sure normals are set in the right order
            # fixme duplicate faces make normal count not the loop count
            loop_normals = []
            for face_normals in self.normals:
                loop_normals.extend(n for vi,n in face_normals)
            me.use_auto_smooth = True
            try:
                me.normals_split_custom_set(loop_normals)
            except:
                log.exception('normals_split_custom_set failed, known issue due to duplicate faces')

        if hierarchy:
            for name, vgroup in self.vgroups.items():
                grp = ob.vertex_groups.new(name)
                for v in vgroup:
                    grp.add([v], 1.0, 'REPLACE')
            ob.parent = hierarchy.armature
            mod = ob.modifiers.new(hierarchy.name, 'ARMATURE')
            mod.object = hierarchy.armature
            mod.use_bone_envelopes = False
            mod.use_vertex_groups = True
            mod.show_in_editmode = True
            mod.show_on_cage = True


class Limb:
    def __init__(self):
        self.parent, self.child, self.sibling = -1, -1, -1
        self.pos = Vector([0, 0, 0])
        self.near, self.far = 0x00000000, 0x00000000
        self.poseBone = None
        self.poseLocPath, self.poseRotPath = None, None
        self.poseLoc, self.poseRot = Vector([0, 0, 0]), None

    def read(self, segment, offset, actuallimb, BoneCount):
        seg, offset = splitOffset(offset)

        rot_offset = offset & 0xFFFFFF
        rot_offset += (0 * (BoneCount * 6 + 8));

        self.pos.x = unpack_from(">h", segment[seg], offset)[0]
        self.pos.z = unpack_from(">h", segment[seg], offset + 2)[0]
        self.pos.y = -unpack_from(">h", segment[seg], offset + 4)[0]
        global scaleFactor
        self.pos *= scaleFactor
        self.child = unpack_from("b", segment[seg], offset + 6)[0]
        self.sibling = unpack_from("b", segment[seg], offset + 7)[0]
        self.near = unpack_from(">L", segment[seg], offset + 8)[0]
        self.far = unpack_from(">L", segment[seg], offset + 12)[0]

        self.poseLoc.x = unpack_from(">h", segment[seg], rot_offset)[0]
        self.poseLoc.z = unpack_from(">h", segment[seg], rot_offset + 2)[0]
        self.poseLoc.y = unpack_from(">h", segment[seg], rot_offset + 4)[0]
        getLogger('Limb.read').trace("      Limb %r: %f,%f,%f", actuallimb, self.poseLoc.x, self.poseLoc.z, self.poseLoc.y)

class Hierarchy:
    def __init__(self):
        self.name, self.offset = "", 0x00000000
        self.limbCount, self.dlistCount = 0x00, 0x00
        self.limb = []
        self.armature = None

    def read(self, segment, offset):
        log = getLogger('Hierarchy.read')
        self.dlistCount = None
        if not validOffset(segment, offset + 5):
            log.error('Invalid segmented offset 0x%X for hierarchy' % (offset + 5))
            return False
        if not validOffset(segment, offset + 9):
            log.warning('Invalid segmented offset 0x%X for hierarchy (incomplete header), still trying to import ignoring dlistCount' % (offset + 9))
            self.dlistCount = 1
        self.name = "sk_%08X" % offset
        self.offset = offset
        seg, offset = splitOffset(offset)
        limbIndex_offset = unpack_from(">L", segment[seg], offset)[0]
        if not validOffset(segment, limbIndex_offset):
            log.error("        ERROR:  Limb index table 0x%08X out of range" % limbIndex_offset)
            return False
        limbIndex_seg, limbIndex_offset = splitOffset(limbIndex_offset)
        self.limbCount = segment[seg][offset + 4]
        if not self.dlistCount:
            self.dlistCount = segment[seg][offset + 8]
        for i in range(self.limbCount):
            limb_offset = unpack_from(">L", segment[limbIndex_seg], limbIndex_offset + 4 * i)[0]
            limb = Limb()
            limb.index = i
            self.limb.append(limb)
            if validOffset(segment, limb_offset + 12):
                limb.read(segment, limb_offset, i, self.limbCount)
            else:
                log.error("        ERROR:  Limb 0x%02X offset 0x%08X out of range" % (i, limb_offset))[0]
        self.limb[0].pos = Vector([0, 0, 0])
        self.initLimbs(0x00)
        return True

    def create(self):
        rx, ry, rz = 90,0,0
        if (bpy.context.active_object):
            bpy.ops.object.mode_set(mode='OBJECT', toggle=False)
        for i in bpy.context.selected_objects:
            i.select = False
        self.armature = bpy.data.objects.new(self.name, bpy.data.armatures.new("%s_armature" % self.name))
        self.armature.show_x_ray = True
        self.armature.data.draw_type = 'STICK'
        bpy.context.scene.objects.link(self.armature)
        bpy.context.scene.objects.active = self.armature
        bpy.ops.object.mode_set(mode='EDIT', toggle=False)
        for i in range(self.limbCount):
            bone = self.armature.data.edit_bones.new("limb_%02i" % i)
            bone.use_deform = True
            bone.head = self.limb[i].pos

        for i in range(self.limbCount):
            bone = self.armature.data.edit_bones["limb_%02i" % i]
            if (self.limb[i].parent != -1):
                bone.parent = self.armature.data.edit_bones["limb_%02i" % self.limb[i].parent]
                bone.use_connect = False
            bone.tail = bone.head + Vector([0, 0, 0.0001])
        bpy.ops.object.mode_set(mode='OBJECT')

    def initLimbs(self, i):
        if (self.limb[i].child > -1 and self.limb[i].child != i):
            self.limb[self.limb[i].child].parent = i
            self.limb[self.limb[i].child].pos += self.limb[i].pos
            self.initLimbs(self.limb[i].child)
        if (self.limb[i].sibling > -1 and self.limb[i].sibling != i):
            self.limb[self.limb[i].sibling].parent = self.limb[i].parent
            self.limb[self.limb[i].sibling].pos += self.limb[self.limb[i].parent].pos
            self.initLimbs(self.limb[i].sibling)

    def getMatrixLimb(self, offset):
        j = 0
        index = (offset & 0x00FFFFFF) / 0x40
        for i in range(self.limbCount):
            if self.limb[i].near != 0:
                if (j == index):
                    return self.limb[i]
                j += 1
        return self.limb[0]


class F3DZEX:
    def __init__(self):
        self.use_transparency = detectedDisplayLists_use_transparency
        self.alreadyRead = []
        self.segment, self.vbuf, self.tile  = [], [], []
        self.geometryModeFlags = set()

        self.animTotal = 0
        self.TimeLine = 0
        self.TimeLinePosition = 0
        self.displaylists = []

        for i in range(16):
            self.alreadyRead.append([])
            self.segment.append([])
            self.vbuf.append(Vertex())
        for i in range(2):
            self.tile.append(Tile())
            self.vbuf.append(Vertex())
        for i in range(14 + 32):
            self.vbuf.append(Vertex())
        self.curTile = 0
        self.material = []
        self.hierarchy = []
        self.resetCombiner()

    def loaddisplaylists(self, path):
        log = getLogger('F3DZEX.loaddisplaylists')
        if not os.path.isfile(path):
            log.info('Did not find %s (use to manually set offsets of display lists to import)', path)
            self.displaylists = []
            return
        try:
            file = open(path)
            self.displaylists = file.readlines()
            file.close()
            log.info("Loaded the display list list successfully!")
        except:
            log.exception('Could not read displaylists.txt')

    def loadSegment(self, seg, path):
        try:
            file = open(path, 'rb')
            self.segment[seg] = file.read()
            file.close()
        except:
            getLogger('F3DZEX.loadSegment').error('Could not load segment 0x%02X data from %s' % (seg, path))
            pass

    def locateHierarchies(self):
        log = getLogger('F3DZEX.locateHierarchies')
        data = self.segment[0x06]
        for i in range(0, len(data), 4):
            # test for header "bboooooo pp000000 xx000000": if segment bb=0x06 and offset oooooo 4-aligned and not zero parts (pp!=0)
            if data[i] == 0x06 and (data[i+3] & 3) == 0 and data[i+4] != 0:
                offset = unpack_from(">L", data, i)[0] & 0x00FFFFFF
                if offset < len(data):
                    # each Limb index entry is 4 bytes starting at offset
                    offset_end = offset + (data[i+4] << 2)
                    if offset_end <= len(data):
                        j = offset
                        while j < offset_end:
                            # test for limb entry "bboooooo": valid limb entry table as long as segment bb=0x06 and offset oooooo 4-aligned and offset is valid
                            if data[j] != 0x06 or (data[j+3] & 3) != 0 or (unpack_from(">L", data, j)[0] & 0x00FFFFFF) > len(data):
                                break
                            j += 4
                        if (j == i):
                            j |= 0x06000000
                            log.info("    hierarchy found at 0x%08X", j)
                            h = Hierarchy()
                            if h.read(self.segment, j):
                                self.hierarchy.append(h)
                            else:
                                log.warning('Skipping hierarchy at 0x%08X', j)

    def locateAnimations(self):
        log = getLogger('F3DZEX.locateAnimations')
        data = self.segment[0x06]
        self.animation = []
        self.offsetAnims = []
        self.durationAnims = []
        for i in range(0, len(data), 4):
            if ((data[i] == 0) and (data[i+1] > 1) and
                 (data[i+2] == 0) and (data[i+3] == 0) and
                 (data[i+4] == 0x06) and
                 (((data[i+5] << 16)|(data[i+6] << 8)|data[i+7]) < len(data)) and
                 (data[i+8] == 0x06) and
                 (((data[i+9] << 16)|(data[i+10] << 8)|data[i+11]) < len(data)) and
                 (data[i+14] == 0) and (data[i+15] == 0)):
                log.info("          Anims found at %08X Frames: %d", i, data[i+1] & 0x00FFFFFF)
                self.animation.append(i)
                self.offsetAnims.append(i)
                self.offsetAnims[self.animTotal] = (0x06 << 24) | i
                self.durationAnims.append(data[i+1] & 0x00FFFFFF)
                self.animTotal += 1
        if(self.animTotal > 0):
                log.info("          Total Anims                         : %d", self.animTotal)

    def locateExternAnimations(self):
        log = getLogger('F3DZEX.locateExternAnimations')
        data = self.segment[0x0F]
        self.animation = []
        self.offsetAnims = []
        for i in range(0, len(data), 4):
            if ((data[i] == 0) and (data[i+1] > 1) and
                 (data[i+2] == 0) and (data[i+3] == 0) and
                 (data[i+4] == 0x06) and
                 (((data[i+5] << 16)|(data[i+6] << 8)|data[i+7]) < len(data)) and
                 (data[i+8] == 0x06) and
                 (((data[i+9] << 16)|(data[i+10] << 8)|data[i+11]) < len(data)) and
                 (data[i+14] == 0) and (data[i+15] == 0)):
                log.info("          Ext Anims found at %08X" % i, "Frames:", data[i+1] & 0x00FFFFFF)
                self.animation.append(i)
                self.offsetAnims.append(i)
                self.offsetAnims[self.animTotal] = (0x0F << 24) | i
                self.animTotal += 1
        if(self.animTotal > 0):
            log.info("        Total Anims                   :", self.animTotal)

    def locateLinkAnimations(self):
        log = getLogger('F3DZEX.locateLinkAnimations')
        data = self.segment[0x04]
        self.animation = []
        self.offsetAnims = []
        self.animFrames = []
        self.animTotal = -1
        if (len( self.segment[0x04] ) > 0):
            if (MajorasAnims):
                for i in range(0xD000, 0xE4F8, 8):
                    self.animTotal += 1
                    self.animation.append(self.animTotal)
                    self.animFrames.append(self.animTotal)
                    self.offsetAnims.append(self.animTotal)
                    self.offsetAnims[self.animTotal]     = unpack_from(">L", data, i + 4)[0]
                    self.animFrames[self.animTotal] = unpack_from(">h", data, i)[0]
                    log.debug('- Animation #%d offset: %07X frames: %d', self.animTotal+1, self.offsetAnims[self.animTotal], self.animFrames[self.animTotal])
            else:
                for i in range(0x2310, 0x34F8, 8):
                    self.animTotal += 1
                    self.animation.append(self.animTotal)
                    self.animFrames.append(self.animTotal)
                    self.offsetAnims.append(self.animTotal)
                    self.offsetAnims[self.animTotal]     = unpack_from(">L", data, i + 4)[0]
                    self.animFrames[self.animTotal] = unpack_from(">h", data, i)[0]
                    log.debug('- Animation #%d offset: %07X frames: %d', self.animTotal+1, self.offsetAnims[self.animTotal], self.animFrames[self.animTotal])
        log.info("         Link has come to town!!!!")
        if ( (len( self.segment[0x07] ) > 0) and (self.animTotal > 0)):
            self.buildLinkAnimations(self.hierarchy[0], 0)

    def importMap(self):
        if importStrategy == 'NO_DETECTION':
            self.importMapWithHeaders()
        elif importStrategy == 'BRUTEFORCE':
            self.searchAndImport(3, False)
        elif importStrategy == 'SMART':
            self.importMapWithHeaders()
            self.searchAndImport(3, True)
        elif importStrategy == 'TRY_EVERYTHING':
            self.importMapWithHeaders()
            self.searchAndImport(3, False)

    def importMapWithHeaders(self):
        log = getLogger('F3DZEX.importMapWithHeaders')
        data = self.segment[0x03]
        for i in range(0, len(data), 8):
            if data[i] == 0x0A:
                mapHeaderSegment = data[i+4]
                if mapHeaderSegment != 0x03:
                    log.warning('Skipping map header located in segment 0x%02X, referenced by command at 0x%X', mapHeaderSegment, i)
                    continue
                # mesh header offset 
                mho = (data[i+5] << 16) | (data[i+6] << 8) | data[i+7]
                if not mho < len(data):
                    log.error('Mesh header offset 0x%X is past the zmap file size, skipping', mho)
                    continue
                type = data[mho]
                log.info("            Mesh Type: %d" % type)
                if type == 0:
                    if mho + 12 > len(data):
                        log.error('Mesh header at 0x%X of type %d extends past the zmap file size, skipping', mho, type)
                        continue
                    count = data[mho+1]
                    startSeg = data[mho+4]
                    start = (data[mho+5] << 16) | (data[mho+6] << 8) | data[mho+7]
                    endSeg = data[mho+8]
                    end = (data[mho+9] << 16) | (data[mho+10] << 8) | data[mho+11]
                    if startSeg != endSeg:
                        log.error('Mesh header at 0x%X of type %d has start and end in different segments 0x%02X and 0x%02X, skipping', mho, type, startSeg, endSeg)
                        continue
                    if startSeg != 0x03:
                        log.error('Skipping mesh header at 0x%X of type %d: entries are in segment 0x%02X', mho, type, startSeg)
                        continue
                    log.info('Reading %d display lists from 0x%X to 0x%X', count, start, end)
                    for j in range(start, end, 8):
                        opa = unpack_from(">L", data, j)[0]
                        if opa:
                            self.use_transparency = False
                            self.buildDisplayList(None, [None], opa, mesh_name_format='%s_opa')
                        xlu = unpack_from(">L", data, j+4)[0]
                        if xlu:
                            self.use_transparency = True
                            self.buildDisplayList(None, [None], xlu, mesh_name_format='%s_xlu')
                elif type == 1:
                   count = data[mho+1]
                   seg = data[mho+4]
                   start = (data[mho+5] << 16) | (data[mho+6] << 8) | data[mho+7]
                   end = (data[mho+9] << 16) | (data[mho+10] << 8) | data[mho+11]
                   print("         Mesh Type: %02X" % type)
                   
                   end = start + 8
                   for j in range(start, end, 4):
                        self.buildDisplayList(None, [None], unpack_from(">L", data, j)[0])
                    
                elif type == 2:
                    if mho + 12 > len(data):
                        log.error('Mesh header at 0x%X of type %d extends past the zmap file size, skipping', mho, type)
                        continue
                    count = data[mho+1]
                    startSeg = data[mho+4]
                    start = (data[mho+5] << 16) | (data[mho+6] << 8) | data[mho+7]
                    endSeg = data[mho+8]
                    end = (data[mho+9] << 16) | (data[mho+10] << 8) | data[mho+11]
                    if startSeg != endSeg:
                        log.error('Mesh header at 0x%X of type %d has start and end in different segments 0x%02X and 0x%02X, skipping', mho, type, startSeg, endSeg)
                        continue
                    if startSeg != 0x03:
                        log.error('Skipping mesh header at 0x%X of type %d: entries are in segment 0x%02X', mho, type, startSeg)
                        continue
                    log.info('Reading %d display lists from 0x%X to 0x%X', count, start, end)
                    for j in range(start, end, 16):
                        opa = unpack_from(">L", data, j+8)[0]
                        if opa:
                            self.use_transparency = False
                            self.buildDisplayList(None, [None], opa, mesh_name_format='%s_opa')
                        xlu = unpack_from(">L", data, j+12)[0]
                        if xlu:
                            self.use_transparency = True
                            self.buildDisplayList(None, [None], xlu, mesh_name_format='%s_xlu')
                else:
                    log.error('Unknown mesh type %d in mesh header at 0x%X', type, mho)
            elif (data[i] == 0x14):
                return
        log.warning('Map headers ended unexpectedly')

    def importObj(self):
        log = getLogger('F3DZEX.importObj')
        log.info("Locating hierarchies...")
        self.locateHierarchies()

        if len(self.hierarchy) == 0:
            log.info("Found zilch. Using display lists, then...")
            if len(self.displaylists) == 0:
                log.info("...but none were found...")
            for offsetStr in self.displaylists:
                while offsetStr and offsetStr[-1] in ('\r','\n'):
                    offsetStr = offsetStr[:-1]
                if re.match(r'^[0-9]+$', offsetStr):
                    log.warning('Reading offset %s as hexadecimal, NOT decimal', offsetStr)
                if len(offsetStr) > 2 and offsetStr[:2] == '0x':
                    offsetStr = offsetStr[2:]
                try:
                    offset = int(offsetStr, 16)
                except ValueError:
                    log.error('Could not parse %s from displaylists.txt as hexadecimal, skipping entry', offsetStr)
                    continue
                if (offset & 0xFF000000) == 0:
                    log.info('Defaulting segment for offset 0x%X to 6', offset)
                    offset |= 0x06000000
                self.buildDisplayList(None, 0, offset)

        for hierarchy in self.hierarchy:
            log.info("Building hierarchy '%s'..." % hierarchy.name)
            hierarchy.create()
            for i in range(hierarchy.limbCount):
                limb = hierarchy.limb[i]
                if limb.near != 0:
                    if validOffset(self.segment, limb.near):
                        log.info("    0x%02X : building display lists..." % i)
                        self.resetCombiner()
                        self.buildDisplayList(hierarchy, limb, limb.near)
                    else:
                        log.info("    0x%02X : out of range" % i)
                else:
                    log.info("    0x%02X : n/a" % i)
        if len(self.hierarchy) > 0:
            bpy.context.scene.objects.active = self.hierarchy[0].armature
            self.hierarchy[0].armature.select = True
            bpy.ops.object.mode_set(mode='POSE', toggle=False)
            global AnimtoPlay
            if (AnimtoPlay > 0):
                bpy.context.scene.frame_end = 1
                if(ExternalAnimes and len(self.segment[0x0F]) > 0):
                    self.locateExternAnimations()
                else:
                    self.locateAnimations()
                if len(self.animation) > 0:
                    for h in self.hierarchy:
                        if h.armature.animation_data is None:
                            h.armature.animation_data_create()
                    # use the hierarchy with most bones
                    # this works for building any animation regardless of its target skeleton (bone positions) because all limbs are named limb_XX, so the hierarchy with most bones has bones with same names as every other armature
                    # and the rotation and root location animated values don't rely on the actual armature used
                    # and in blender each action can be used for any armature, vertex groups/bone names just have to match
                    # this is useful for iron knuckles and anything with several hierarchies, although an unedited iron kunckles zobj won't work
                    hierarchy = max(self.hierarchy, key=lambda h:h.limbCount)
                    armature = hierarchy.armature
                    log.info('Building animations using armature %s in %s', armature.data.name, armature.name)
                    for i in range(len(self.animation)):
                        AnimtoPlay = i + 1
                        log.info("   Loading animation %d/%d", AnimtoPlay, len(self.animation))
                        action = bpy.data.actions.new('anim%d_%d' % (AnimtoPlay, self.durationAnims[i]))
                        # not sure what users an action is supposed to have, or what it should be linked to
                        action.use_fake_user = True
                        armature.animation_data.action = action
                        self.buildAnimations(hierarchy, 0)
                    for h in self.hierarchy:
                        h.armature.animation_data.action = action
                    bpy.context.scene.frame_end = max(self.durationAnims)
                else:
                    self.locateLinkAnimations()
            else:
                log.info("    Load anims OFF.")

        if importStrategy == 'NO_DETECTION':
            pass
        elif importStrategy == 'BRUTEFORCE':
            self.searchAndImport(6, False)
        elif importStrategy == 'SMART':
            self.searchAndImport(6, True)
        elif importStrategy == 'TRY_EVERYTHING':
            self.searchAndImport(6, False)

    def searchAndImport(self, segment, skipAlreadyRead):
        log = getLogger('F3DZEX.searchAndImport')
        data = self.segment[segment]
        self.use_transparency = detectedDisplayLists_use_transparency
        log.info(
            'Searching for %s display lists in segment 0x%02X (materials with transparency: %s)',
            'non-read' if skipAlreadyRead else 'any', segment, 'yes' if self.use_transparency else 'no')
        validOpcodesStartIndex = 0
        for i in range(0, len(data), 8):
            opcode = data[i]
            # valid commands are 0x00-0x07 and 0xD3-0xFF
            # however, could be not considered valid:
            # 0x07 G_QUAD
            # 0xEC G_SETCONVERT (YUV-related)
            # 0xE4 G_TEXRECT, 0xF6 G_FILLRECT (2d overlay)
            # 0xEB, 0xEE, 0xEF, 0xF1 ("unimplemented -> rarely used" being the reasoning)
            # but filtering out those hurts the resulting import
            isValid = (opcode <= 0x07 or opcode >= 0xD3) #and opcode not in (0x07,0xEC,0xE4,0xF6,0xEB,0xEE,0xEF,0xF1)
            if not isValid:
                validOpcodesStartIndex = None
            elif validOpcodesStartIndex is None:
                validOpcodesStartIndex = i
            # if this command means "end of dlist"
            if (opcode == 0xDE and data[i+1] != 0) or opcode == 0xDF:
                # build starting at earliest valid opcode
                log.debug('Found opcode 0x%X at 0x%X, building display list from 0x%X', opcode, i, validOpcodesStartIndex)
                self.buildDisplayList(
                    None, [None], (segment << 24) | validOpcodesStartIndex,
                    mesh_name_format = '%s_detect',
                    skipAlreadyRead = skipAlreadyRead,
                    extraLenient = True
                )
                validOpcodesStartIndex = None

    def resetCombiner(self):
        self.primColor = Vector([1.0, 1.0, 1.0, 1.0])
        self.envColor = Vector([1.0, 1.0, 1.0, 1.0])
        if checkUseVertexAlpha():
            self.vertexColor = Vector([1.0, 1.0, 1.0, 1.0])
        else:
            self.vertexColor = Vector([1.0, 1.0, 1.0])
        self.shadeColor = Vector([1.0, 1.0, 1.0])

    def checkUseNormals(self):
        return vertexMode == 'NORMALS' or (vertexMode == 'AUTO' and 'G_LIGHTING' in self.geometryModeFlags)

    def getCombinerColor(self):
        def mult4d(v1, v2):
            return Vector([v1[i] * v2[i] for i in range(4)])
        cc = Vector([1.0, 1.0, 1.0, 1.0])
        # todo these have an effect even if vertexMode == 'NONE' ?
        if enablePrimColor:
            cc = mult4d(cc, self.primColor)
        if enableEnvColor:
            cc = mult4d(cc, self.envColor)
        # todo assume G_LIGHTING means normals if set, and colors if clear, but G_SHADE may play a role too?
        if vertexMode == 'COLORS' or (vertexMode == 'AUTO' and 'G_LIGHTING' not in self.geometryModeFlags):
            cc = mult4d(cc, self.vertexColor.to_4d())
        elif self.checkUseNormals():
            cc = mult4d(cc, self.shadeColor.to_4d())
        if checkUseVertexAlpha():
            return cc
        else:
            return cc.xyz

    def buildDisplayList(self, hierarchy, limb, offset, mesh_name_format='%s', skipAlreadyRead=False, extraLenient=False):
        log = getLogger('F3DZEX.buildDisplayList')
        segment = offset >> 24
        segmentMask = segment << 24
        data = self.segment[segment]

        startOffset = offset & 0x00FFFFFF
        endOffset = len(data)
        if skipAlreadyRead:
            log.trace('is 0x%X in %r ?', startOffset, self.alreadyRead[segment])
            for fromOffset,toOffset in self.alreadyRead[segment]:
                if fromOffset <= startOffset and startOffset <= toOffset:
                    log.debug('Skipping already read dlist at 0x%X', startOffset)
                    return
                if startOffset <= fromOffset:
                    if endOffset > fromOffset:
                        endOffset = fromOffset
                        log.debug('Shortening dlist to end at most at 0x%X, at which point it was read already', endOffset)
            log.trace('no it is not')

        def buildRec(offset):
            self.buildDisplayList(hierarchy, limb, offset, mesh_name_format=mesh_name_format, skipAlreadyRead=skipAlreadyRead)

        mesh = Mesh()
        has_tex = False
        material = None
        if hierarchy:
            matrix = [limb]
        else:
            matrix = [None]

        log.debug('Reading dlists from 0x%08X', segmentMask | startOffset)
        for i in range(startOffset, endOffset, 8):
            w0 = unpack_from(">L", data, i)[0]
            w1 = unpack_from(">L", data, i + 4)[0]
            # G_NOOP
            if data[i] == 0x00:
                pass
            elif data[i] == 0x01:
                count = (w0 >> 12) & 0xFF
                index = ((w0 & 0xFF) >> 1) - count
                vaddr = w1
                if validOffset(self.segment, vaddr + int(16 * count) - 1):
                    for j in range(count):
                        self.vbuf[index + j].read(self.segment, vaddr + 16 * j)
                        if hierarchy:
                            self.vbuf[index + j].limb = matrix[len(matrix) - 1]
                            if self.vbuf[index + j].limb:
                                self.vbuf[index + j].pos += self.vbuf[index + j].limb.pos
            elif data[i] == 0x02:
                try:
                    index = ((data[i + 2] & 0x0F) << 3) | (data[i + 3] >> 1)
                    if data[i + 1] == 0x10:
                        self.vbuf[index].normal.x = unpack_from("b", data, i + 4)[0] / 128
                        self.vbuf[index].normal.z = unpack_from("b", data, i + 5)[0] / 128
                        self.vbuf[index].normal.y = -unpack_from("b", data, i + 6)[0] / 128
                        self.vbuf[index].color = unpack_from("BBBB", data, i + 4)[0] / 255
                    elif data[i + 1] == 0x14:
                        self.vbuf[index].uv.x = float(unpack_from(">h", data, i + 4)[0])
                        self.vbuf[index].uv.y = float(unpack_from(">h", data, i + 6)[0])
                except IndexError:
                    if not extraLenient:
                        log.exception('Bad vertex indices in 0x02 at 0x%X %08X %08X', i, w0, w1)
            elif data[i] == 0x05 or data[i] == 0x06:
                if has_tex:
                    material = None
                    for j in range(len(self.material)):
                        if self.material[j].name == "mtl_%08X" % self.tile[0].data:
                            material = self.material[j]
                            break
                    if material == None:
                        material = self.tile[0].create(self.segment, self.use_transparency)
                        if material:
                            self.material.append(material)
                    has_tex = False
                v1, v2 = None, None
                vi1, vi2 = -1, -1
                if not importTextures:
                    material = None
                nbefore_props = ['verts','uvs','colors','vgroups','faces','faces_use_smooth','normals']
                nbefore_lengths = [(nbefore_prop, len(getattr(mesh, nbefore_prop))) for nbefore_prop in nbefore_props]
                # a1 a2 a3 are microcode values
                def addTri(a1, a2, a3):
                    try:
                        verts = [self.vbuf[a >> 1] for a in (a1,a2,a3)]
                    except IndexError:
                        if extraLenient:
                            return False
                        raise
                    verts_pos = [(v.pos.x, v.pos.y, v.pos.z) for v in verts]
                    verts_index = [mesh.verts.index(pos) if pos in mesh.verts else None for pos in verts_pos]
                    for j in range(3):
                        if verts_index[j] is None:
                            mesh.verts.append(verts_pos[j])
                            verts_index[j] = len(mesh.verts) - 1
                    mesh.uvs.append(material)
                    face_normals = []
                    for j in range(3):
                        v = verts[j]
                        vi = verts_index[j]
                        # todo is this computation of shadeColor correct?
                        sc = (((v.normal.x + v.normal.y + v.normal.z) / 3) + 1.0) / 2
                        if checkUseVertexAlpha():
                            self.vertexColor = Vector([v.color[0], v.color[1], v.color[2], v.color[3]])
                        else:
                            self.vertexColor = Vector([v.color[0], v.color[1], v.color[2]])
                        self.shadeColor = Vector([sc, sc, sc])
                        mesh.colors.append(self.getCombinerColor())
                        mesh.uvs.append((self.tile[0].offset.x + v.uv.x * self.tile[0].ratio.x, self.tile[0].offset.y - v.uv.y * self.tile[0].ratio.y))
                        if hierarchy:
                            if v.limb:
                                limb_name = 'limb_%02i' % v.limb.index
                                if not (limb_name in mesh.vgroups):
                                    mesh.vgroups[limb_name] = []
                                mesh.vgroups[limb_name].append(vi)
                        face_normals.append((vi, (v.normal.x, v.normal.y, v.normal.z)))
                    mesh.faces.append(tuple(verts_index))
                    mesh.faces_use_smooth.append('G_SHADE' in self.geometryModeFlags and 'G_SHADING_SMOOTH' in self.geometryModeFlags)
                    mesh.normals.append(tuple(face_normals))
                    if len(set(verts_index)) < 3 and not extraLenient:
                        log.warning('Found empty tri! %d %d %d' % tuple(verts_index))
                    return True

                try:
                    revert = not addTri(data[i+1], data[i+2], data[i+3])
                    if data[i] == 0x06:
                        revert = revert or not addTri(data[i+4+1], data[i+4+2], data[i+4+3])
                except:
                    log.exception('Failed to import vertices and/or their data from 0x%X', i)
                    revert = True
                if revert:
                    # revert any change
                    for nbefore_prop, nbefore in nbefore_lengths:
                        val_prop = getattr(mesh, nbefore_prop)
                        while len(val_prop) > nbefore:
                            val_prop.pop()
            # G_TEXTURE
            elif data[i] == 0xD7:
                log.debug('0xD7 G_TEXTURE used, but unimplemented')
                # fixme ?
#                for i in range(2):
#                    if ((w1 >> 16) & 0xFFFF) < 0xFFFF:
#                        self.tile[i].scale.x = ((w1 >> 16) & 0xFFFF) * 0.0000152587891
#                    else:
#                        self.tile[i].scale.x = 1.0
#                    if (w1 & 0xFFFF) < 0xFFFF:
#                        self.tile[i].scale.y = (w1 & 0xFFFF) * 0.0000152587891
#                    else:
#                        self.tile[i].scale.y = 1.0
                pass
            # G_POPMTX
            elif data[i] == 0xD8 and enableMatrices:
                if hierarchy and len(matrix) > 1:
                    matrix.pop()
            # G_MTX
            elif data[i] == 0xDA and enableMatrices:
                log.debug('0xDA G_MTX used, but implementation may be faulty')
                # fixme this looks super weird, not sure what it's doing either
                if hierarchy and data[i + 4] == 0x0D:
                    if (data[i + 3] & 0x04) == 0:
                        matrixLimb = hierarchy.getMatrixLimb(unpack_from(">L", data, i + 4)[0])
                        if (data[i + 3] & 0x02) == 0:
                            newMatrixLimb = Limb()
                            newMatrixLimb.index = matrixLimb.index
                            newMatrixLimb.pos = (Vector([matrixLimb.pos.x, matrixLimb.pos.y, matrixLimb.pos.z]) + matrix[len(matrix) - 1].pos) / 2
                            matrixLimb = newMatrixLimb
                        if (data[i + 3] & 0x01) == 0:
                            matrix.append(matrixLimb)
                        else:
                            matrix[len(matrix) - 1] = matrixLimb
                    else:
                        matrix.append(matrix[len(matrix) - 1])
                elif hierarchy:
                    log.error("unknown limb %08X %08X" % (w0, w1))
            # G_DL
            elif data[i] == 0xDE:
                log.trace('G_DE at 0x%X %08X%08X', segmentMask | i, w0, w1)
                mesh.create(mesh_name_format, hierarchy, offset, self.checkUseNormals())
                mesh.__init__()
                offset = segmentMask | i
                if validOffset(self.segment, w1):
                    buildRec(w1)
                if data[i + 1] != 0x00:
                    self.alreadyRead[segment].append((startOffset,i))
                    return
            # G_ENDDL
            elif data[i] == 0xDF:
                log.trace('G_ENDDL at 0x%X %08X%08X', segmentMask | i, w0, w1)
                mesh.create(mesh_name_format, hierarchy, offset, self.checkUseNormals())
                self.alreadyRead[segment].append((startOffset,i))
                return
            # handle "LOD dlists"
            elif data[i] == 0xE1:
                # 4 bytes starting at data[i+8+4] is a distance to check for displaying this dlist
                mesh.create(mesh_name_format, hierarchy, offset, self.checkUseNormals())
                mesh.__init__()
                offset = segmentMask | i
                if validOffset(self.segment, w1):
                    buildRec(w1)
                else:
                    log.warning('Invalid 0xE1 offset 0x%04X, skipping', w1)
            elif data[i] == 0xE7:
                mesh.create(mesh_name_format, hierarchy, offset, self.checkUseNormals())
                mesh.__init__()
                offset = segmentMask | i
            elif data[i] == 0xF0:
                self.palSize = ((w1 & 0x00FFF000) >> 13) + 1
            elif data[i] == 0xF2:
                self.tile[self.curTile].rect.x = (w0 & 0x00FFF000) >> 14
                self.tile[self.curTile].rect.y = (w0 & 0x00000FFF) >> 2
                self.tile[self.curTile].rect.z = (w1 & 0x00FFF000) >> 14
                self.tile[self.curTile].rect.w = (w1 & 0x00000FFF) >> 2
                self.tile[self.curTile].width = (self.tile[self.curTile].rect.z - self.tile[self.curTile].rect.x) + 1
                self.tile[self.curTile].height = (self.tile[self.curTile].rect.w - self.tile[self.curTile].rect.y) + 1
                self.tile[self.curTile].texBytes = int(self.tile[self.curTile].width * self.tile[self.curTile].height) << 1
                if (self.tile[self.curTile].texBytes >> 16) == 0xFFFF:
                    self.tile[self.curTile].texBytes = self.tile[self.curTile].size << 16 >> 15
                self.tile[self.curTile].calculateSize()
            # G_LOADTILE, G_TEXRECT, G_SETZIMG, G_SETCIMG (2d "direct" drawing?)
            elif data[i] == 0xF4 or data[i] == 0xE4 or data[i] == 0xFE or data[i] == 0xFF:
                log.debug('0x%X %08X : %08X', data[i], w0, w1)
            # G_SETTILE
            elif data[i] == 0xF5:
                self.tile[self.curTile].texFmt = (w0 >> 21) & 0b111
                self.tile[self.curTile].texSiz = (w0 >> 19) & 0b11
                self.tile[self.curTile].lineSize = (w0 >> 9) & 0x1FF
                self.tile[self.curTile].clip.x = (w1 >> 8) & 0x03
                self.tile[self.curTile].clip.y = (w1 >> 18) & 0x03
                self.tile[self.curTile].mask.x = (w1 >> 4) & 0x0F
                self.tile[self.curTile].mask.y = (w1 >> 14) & 0x0F
                self.tile[self.curTile].tshift.x = w1 & 0x0F
                self.tile[self.curTile].tshift.y = (w1 >> 10) & 0x0F
            elif data[i] == 0xFA:
                self.primColor = Vector([((w1 >> (8*(3-i))) & 0xFF) / 255 for i in range(4)])
                log.debug('new primColor -> %r', self.primColor)
                if enablePrimColor and self.primColor.w != 1 and not checkUseVertexAlpha():
                    log.warning('primColor %r has non-opaque alpha, merging it into vertex colors may produce unexpected results' % self.primColor)
                #self.primColor = Vector([min(((w1 >> 24) & 0xFF) / 255, 1.0), min(0.003922 * ((w1 >> 16) & 0xFF), 1.0), min(0.003922 * ((w1 >> 8) & 0xFF), 1.0), min(0.003922 * ((w1) & 0xFF), 1.0)])
            elif data[i] == 0xFB:
                self.envColor = Vector([((w1 >> (8*(3-i))) & 0xFF) / 255 for i in range(4)])
                log.debug('new envColor -> %r', self.envColor)
                if enableEnvColor and self.envColor.w != 1 and not checkUseVertexAlpha():
                    log.warning('envColor %r has non-opaque alpha, merging it into vertex colors may produce unexpected results' % self.envColor)
                #self.envColor = Vector([min(0.003922 * ((w1 >> 24) & 0xFF), 1.0), min(0.003922 * ((w1 >> 16) & 0xFF), 1.0), min(0.003922 * ((w1 >> 8) & 0xFF), 1.0)])
                if invertEnvColor:
                    self.envColor = Vector([1 - c for c in self.envColor])
            elif data[i] == 0xFD:
                try:
                    if data[i - 8] == 0xF2:
                        self.curTile = 1
                    else:
                        self.curTile = 0
                except:
                    log.exception('Failed to switch texel? at 0x%X', i)
                    pass
                try:
                    if data[i + 8] == 0xE8:
                        self.tile[0].palette = w1
                    else:
                        self.tile[self.curTile].data = w1
                except:
                    log.exception('Failed to switch texel data? at 0x%X', i)
                    pass
                has_tex = True
            # G_CULLDL, G_BRANCH_Z, G_SETOTHERMODE_L, G_SETOTHERMODE_H, G_RDPLOADSYNC, G_RDPTILESYNC, G_LOADBLOCK,
            elif data[i] in (0x03,0x04,0xE2,0xE3,0xE6,0xE8,0xF3,):
                # not relevant for importing
                pass
            # G_GEOMETRYMODE
            elif data[i] == 0xD9:
                # todo do not push mesh if geometry mode doesnt actually change?
                mesh.create(mesh_name_format, hierarchy, offset, self.checkUseNormals())
                mesh.__init__()
                offset = segmentMask | i
                # https://wiki.cloudmodding.com/oot/F3DZEX#RSP_Geometry_Mode
                # todo SharpOcarina tags
                geometryModeMasks = {
                    'G_ZBUFFER':            0b00000000000000000000000000000001,
                    'G_SHADE':              0b00000000000000000000000000000100, # used by 0x05/0x06 for mesh.faces_use_smooth
                    'G_CULL_FRONT':         0b00000000000000000000001000000000, # todo set culling (not possible per-face or per-material or even per-object apparently) / SharpOcarina tags
                    'G_CULL_BACK':          0b00000000000000000000010000000000, # todo same
                    'G_FOG':                0b00000000000000010000000000000000,
                    'G_LIGHTING':           0b00000000000000100000000000000000,
                    'G_TEXTURE_GEN':        0b00000000000001000000000000000000, # todo billboarding?
                    'G_TEXTURE_GEN_LINEAR': 0b00000000000010000000000000000000, # todo billboarding?
                    'G_SHADING_SMOOTH':     0b00000000001000000000000000000000, # used by 0x05/0x06 for mesh.faces_use_smooth
                    'G_CLIPPING':           0b00000000100000000000000000000000,
                }
                clearbits = ~w0 & 0x00FFFFFF
                setbits = w1
                for flagName, flagMask in geometryModeMasks.items():
                    if clearbits & flagMask:
                        self.geometryModeFlags.discard(flagName)
                        clearbits = clearbits & ~flagMask
                    if setbits & flagMask:
                        self.geometryModeFlags.add(flagName)
                        setbits = setbits & ~flagMask
                log.debug('Geometry mode flags as of 0x%X: %r', i, self.geometryModeFlags)
                """
                # many unknown flags. keeping this commented out for any further research
                if clearbits:
                    log.warning('Unknown geometry mode flag at 0x%X in clearbits %s', i, bin(clearbits))
                if setbits:
                    log.warning('Unknown geometry mode flag at 0x%X in setbits %s', i, bin(setbits))
                """
            # G_SETCOMBINE
            elif data[i] == 0xFC:
                # https://wiki.cloudmodding.com/oot/F3DZEX/Opcode_Details#0xFC_.E2.80.94_G_SETCOMBINE
                pass # todo
            else:
                log.warning('Skipped (unimplemented) opcode 0x%02X' % data[i])
        log.warning('Reached end of dlist started at 0x%X', startOffset)
        self.alreadyRead[segment].append((startOffset,endOffset))

    def LinkTpose(self, hierarchy):
        log = getLogger('F3DZEX.LinkTpose')
        segment = []
        data = self.segment[0x06]
        segment = self.segment
        RX, RY, RZ = 0,0,0
        BoneCount  = hierarchy.limbCount
        bpy.context.scene.tool_settings.use_keyframe_insert_auto = True
        bonesIndx = [0,-90,0,0,0,0,0,0,0,90,0,0,0,180,0,0,-180,0,0,0,0]
        bonesIndy = [0,90,0,0,0,90,0,0,90,-90,-90,-90,0,0,0,90,0,0,90,0,0]
        bonesIndz = [0,0,0,0,0,0,0,0,0,0,0,0,0,-90,0,0,90,0,0,0,0]

        log.info("Link T Pose...")
        for i in range(BoneCount):
            bIndx = ((BoneCount-1) - i)
            if (i > -1):
                bone = hierarchy.armature.bones["limb_%02i" % (bIndx)]
                bone.select = True
                bpy.ops.transform.rotate(value = radians(bonesIndx[bIndx]), axis=(0, 0, 0), constraint_axis=(True, False, False))
                bpy.ops.transform.rotate(value = radians(bonesIndz[bIndx]), axis=(0, 0, 0), constraint_axis=(False, False, True))
                bpy.ops.transform.rotate(value = radians(bonesIndy[bIndx]), axis=(0, 0, 0), constraint_axis=(False, True, False))
                bpy.ops.pose.select_all(action="DESELECT")

        hierarchy.armature.bones["limb_00"].select = True ## Translations
        bpy.ops.transform.translate(value =(0, 0, 0), constraint_axis=(True, False, False))
        bpy.ops.transform.translate(value = (0, 0, 50), constraint_axis=(False, False, True))
        bpy.ops.transform.translate(value = (0, 0, 0), constraint_axis=(False, True, False))
        bpy.ops.pose.select_all(action="DESELECT")
        bpy.context.scene.tool_settings.use_keyframe_insert_auto = False

        for i in range(BoneCount):
            bIndx = i
            if (i > -1):
                bone = hierarchy.armature.bones["limb_%02i" % (bIndx)]
                bone.select = True
                bpy.ops.transform.rotate(value = radians(-bonesIndy[bIndx]), axis=(0, 0, 0), constraint_axis=(False, True, False))
                bpy.ops.transform.rotate(value = radians(-bonesIndz[bIndx]), axis=(0, 0, 0), constraint_axis=(False, False, True))
                bpy.ops.transform.rotate(value = radians(-bonesIndx[bIndx]), axis=(0, 0, 0), constraint_axis=(True, False, False))
                bpy.ops.pose.select_all(action="DESELECT")

        hierarchy.armature.bones["limb_00"].select = True ## Translations
        bpy.ops.transform.translate(value =(0, 0, 0), constraint_axis=(True, False, False))
        bpy.ops.transform.translate(value = (0, 0, -50), constraint_axis=(False, False, True))
        bpy.ops.transform.translate(value = (0, 0, 0), constraint_axis=(False, True, False))
        bpy.ops.pose.select_all(action="DESELECT")

    def buildLinkAnimations(self, hierarchy, newframe):
        global AnimtoPlay
        global Animscount
        log = getLogger('F3DZEX.buildLinkAnimations')
        # todo buildLinkAnimations hasn't been rewritten/improved like buildAnimations has
        log.warning('The code to build link animations has not been improved/tested for a while, not sure what features it lacks compared to regular animations, pretty sure it will not import all animations')
        segment = []
        rot_indx = 0
        rot_indy = 0
        rot_indz = 0
        data = self.segment[0x06]
        segment = self.segment
        n_anims = self.animTotal
        seg, offset = splitOffset(hierarchy.offset)
        BoneCount  = hierarchy.limbCount
        RX, RY, RZ = 0,0,0
        frameCurrent = newframe

        if (AnimtoPlay > 0 and AnimtoPlay <= n_anims):
          currentanim = AnimtoPlay - 1
        else:
          currentanim = 0

        log.info("currentanim: %d frameCurrent: %d", currentanim+1, frameCurrent+1)
        AnimationOffset = self.offsetAnims[currentanim]
        TAnimationOffset = self.offsetAnims[currentanim]
        AniSeg = AnimationOffset >> 24
        AnimationOffset &= 0xFFFFFF
        rot_offset = AnimationOffset
        rot_offset += (frameCurrent * (BoneCount * 6 + 8))
        frameTotal = self.animFrames[currentanim]
        rot_offset += BoneCount * 6

        Trot_offset = TAnimationOffset & 0xFFFFFF
        Trot_offset += (frameCurrent * (BoneCount * 6 + 8))
        TRX = unpack_from(">h", segment[AniSeg], Trot_offset)[0]
        Trot_offset += 2
        TRZ = unpack_from(">h", segment[AniSeg], Trot_offset)[0]
        Trot_offset += 2
        TRY = -unpack_from(">h", segment[AniSeg], Trot_offset)[0]
        Trot_offset += 2
        BoneListListOffset = unpack_from(">L", segment[seg], offset)[0]
        BoneListListOffset &= 0xFFFFFF

        BoneOffset = unpack_from(">L", segment[seg], BoneListListOffset + (0 << 2))[0]
        S_Seg = (BoneOffset >> 24) & 0xFF
        BoneOffset &= 0xFFFFFF
        TRX += unpack_from(">h", segment[S_Seg], BoneOffset)[0]
        TRZ += unpack_from(">h", segment[S_Seg], BoneOffset + 2)[0]
        TRY += -unpack_from(">h", segment[S_Seg], BoneOffset + 4)[0]
        newLocx = TRX / 79
        newLocz = -25.5
        newLocz += TRZ / 79
        newLocy = TRY / 79

        bpy.context.scene.tool_settings.use_keyframe_insert_auto = True

        for i in range(BoneCount):
            bIndx = ((BoneCount-1) - i) # Had to reverse here, cuz didn't find a way to rotate bones on LOCAL space, start rotating from last to first bone on hierarchy GLOBAL.
            RX = unpack_from(">h", segment[AniSeg], rot_offset)[0]
            rot_offset -= 2
            RY = unpack_from(">h", segment[AniSeg], rot_offset + 4)[0]
            rot_offset -= 2
            RZ = unpack_from(">h", segment[AniSeg], rot_offset + 8)[0]
            rot_offset -= 2

            RX /= (182.04444444444444444444)
            RY /= (182.04444444444444444444)
            RZ /= (182.04444444444444444444)

            RXX = (RX)
            RYY = (-RZ)
            RZZ = (RY)

            log.trace('limb: %d RX %d RZ %d RY %d anim: %d frame: %d', bIndx, int(RXX), int(RZZ), int(RYY), currentanim+1, frameCurrent+1)
            if (i > -1):
                bone = hierarchy.armature.bones["limb_%02i" % (bIndx)]
                bone.select = True
                bpy.ops.transform.rotate(value = radians(RXX), axis=(0, 0, 0), constraint_axis=(True, False, False))
                bpy.ops.transform.rotate(value = radians(RZZ), axis=(0, 0, 0), constraint_axis=(False, False, True))
                bpy.ops.transform.rotate(value = radians(RYY), axis=(0, 0, 0), constraint_axis=(False, True, False))
                bpy.ops.pose.select_all(action="DESELECT")

        hierarchy.armature.bones["limb_00"].select = True ## Translations
        bpy.ops.transform.translate(value =(newLocx, 0, 0), constraint_axis=(True, False, False))
        bpy.ops.transform.translate(value = (0, 0, newLocz), constraint_axis=(False, False, True))
        bpy.ops.transform.translate(value = (0, newLocy, 0), constraint_axis=(False, True, False))
        bpy.ops.pose.select_all(action="DESELECT")

        if (frameCurrent < (frameTotal - 1)):## Next Frame ### Could have done some math here but... just reverse previus frame, so it just repose.
            bpy.context.scene.tool_settings.use_keyframe_insert_auto = False

            hierarchy.armature.bones["limb_00"].select = True ## Translations
            bpy.ops.transform.translate(value = (-newLocx, 0, 0), constraint_axis=(True, False, False))
            bpy.ops.transform.translate(value = (0, 0, -newLocz), constraint_axis=(False, False, True))
            bpy.ops.transform.translate(value = (0, -newLocy, 0), constraint_axis=(False, True, False))
            bpy.ops.pose.select_all(action="DESELECT")

            rot_offset = AnimationOffset
            rot_offset += (frameCurrent * (BoneCount * 6 + 8))
            rot_offset += 6
            for i in range(BoneCount):
                RX = unpack_from(">h", segment[AniSeg], rot_offset)[0]
                rot_offset += 2
                RY = unpack_from(">h", segment[AniSeg], rot_offset)[0]
                rot_offset += 2
                RZ = unpack_from(">h", segment[AniSeg], rot_offset)[0]
                rot_offset += 2

                RX /= (182.04444444444444444444)
                RY /= (182.04444444444444444444)
                RZ /= (182.04444444444444444444)

                RXX = (-RX)
                RYY = (RZ)
                RZZ = (-RY)

                log.trace("limb: %d RX %d RZ %d RY %d anim: %d frame: %d", i, int(RXX), int(RZZ), int(RYY), currentanim+1, frameCurrent+1)
                if (i > -1):
                    bone = hierarchy.armature.bones["limb_%02i" % (i)]
                    bone.select = True
                    bpy.ops.transform.rotate(value = radians(RYY), axis=(0, 0, 0), constraint_axis=(False, True, False))
                    bpy.ops.transform.rotate(value = radians(RZZ), axis=(0, 0, 0), constraint_axis=(False, False, True))
                    bpy.ops.transform.rotate(value = radians(RXX), axis=(0, 0, 0), constraint_axis=(True, False, False))
                    bpy.ops.pose.select_all(action="DESELECT")

            bpy.context.scene.frame_end += 1
            bpy.context.scene.frame_current += 1
            frameCurrent += 1
            self.buildLinkAnimations(hierarchy, frameCurrent)
        else:
            bpy.context.scene.tool_settings.use_keyframe_insert_auto = False
            bpy.context.scene.frame_current = 1

    def buildAnimations(self, hierarchyMostBones, newframe):
        log = getLogger('F3DZEX.buildAnimations')
        rot_indx = 0
        rot_indy = 0
        rot_indz = 0
        Trot_indx = 0
        Trot_indy = 0
        Trot_indz = 0
        segment = self.segment
        RX, RY, RZ = 0,0,0
        n_anims = self.animTotal
        if (AnimtoPlay > 0 and AnimtoPlay <= n_anims):
            currentanim = AnimtoPlay - 1
        else:
            currentanim = 0

        AnimationOffset = self.offsetAnims[currentanim]
        #seg, offset = splitOffset(hierarchy.offset) # not used, MUST be not relevant because we use hierarchyMostBones (its armature) as placeholder
        BoneCountMax = hierarchyMostBones.limbCount
        armature = hierarchyMostBones.armature
        frameCurrent = newframe

        if not validOffset(segment, AnimationOffset):
            log.warning('Skipping invalid animation offset 0x%X', AnimationOffset)
            return

        AniSeg = AnimationOffset >> 24
        AnimationOffset &= 0xFFFFFF

        frameTotal = unpack_from(">h", segment[AniSeg], (AnimationOffset))[0]
        rot_vals_addr = unpack_from(">L", segment[AniSeg], (AnimationOffset + 4))[0]
        RotIndexoffset = unpack_from(">L", segment[AniSeg], (AnimationOffset + 8))[0]
        Limit = unpack_from(">h", segment[AniSeg], (AnimationOffset + 12))[0] # todo no idea what this is

        rot_vals_addr  &= 0xFFFFFF
        RotIndexoffset &= 0xFFFFFF

        rot_vals_max_length = int ((RotIndexoffset - rot_vals_addr) / 2)
        if rot_vals_max_length < 0:
            log.info('rotation indices (animation data) is located before indexed rotation values, this is weird but fine')
            rot_vals_max_length = (len(segment[AniSeg]) - rot_vals_addr) // 2
        rot_vals_cache = []
        def rot_vals(index, errorDefault=0):
            if index < 0 or (rot_vals_max_length and index >= rot_vals_max_length):
                log.trace('index in rotations table %d is out of bounds (rotations table is <= %d long)', index, rot_vals_max_length)
                return errorDefault
            if index >= len(rot_vals_cache):
                rot_vals_cache.extend(unpack_from(">h", segment[AniSeg], (rot_vals_addr) + (j * 2))[0] for j in range(len(rot_vals_cache),index+1))
                log.trace('Computed rot_vals_cache up to %d %r', index, rot_vals_cache)
            return rot_vals_cache[index]

        bpy.context.scene.tool_settings.use_keyframe_insert_auto = True
        bpy.context.scene.frame_end = frameTotal
        bpy.context.scene.frame_current = frameCurrent + 1

        log.log(
            logging.INFO if (frameCurrent + 1) % min(20, max(min(10, frameTotal), frameTotal // 3)) == 0 else logging.DEBUG,
            "anim: %d/%d frame: %d/%d", currentanim+1, self.animTotal, frameCurrent+1, frameTotal)

        ## Translations
        Trot_indx = unpack_from(">h", segment[AniSeg], RotIndexoffset)[0]
        Trot_indy = unpack_from(">h", segment[AniSeg], RotIndexoffset + 2)[0]
        Trot_indz = unpack_from(">h", segment[AniSeg], RotIndexoffset + 4)[0]

        if (Trot_indx >= Limit):
            Trot_indx += frameCurrent
        if (Trot_indz >= Limit):
            Trot_indz += frameCurrent
        if (Trot_indy >= Limit):
            Trot_indy += frameCurrent

        TRX = rot_vals(Trot_indx)
        TRZ = rot_vals(Trot_indy)
        TRY = rot_vals(Trot_indz)

        global scaleFactor
        newLocx =  TRX * scaleFactor
        newLocz =  TRZ * scaleFactor
        newLocy = -TRY * scaleFactor
        log.trace("X %d Y %d Z %d", int(TRX), int(TRY), int(TRZ))

        log.trace("       %d Frames %d still values %f tracks",frameTotal, Limit, ((rot_vals_max_length - Limit) / frameTotal)) # what is this debug message?
        for i in range(BoneCountMax):
            bIndx = ((BoneCountMax-1) - i) # Had to reverse here, cuz didn't find a way to rotate bones on LOCAL space, start rotating from last to first bone on hierarchy GLOBAL.

            if RotIndexoffset + (bIndx * 6) + 10 + 2 > len(segment[AniSeg]):
                log.trace('Ignoring bone %d in animation %d, rotation table does not have that many entries', bIndx, AnimtoPlay)
                continue

            rot_indexx = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 6)[0]
            rot_indexy = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 8)[0]
            rot_indexz = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 10)[0]

            rot_indx = rot_indexx
            rot_indy = rot_indexy
            rot_indz = rot_indexz

            if (rot_indx >= Limit):
                rot_indx += frameCurrent
            if (rot_indy >= Limit):
                rot_indy += frameCurrent
            if (rot_indz >= Limit):
                rot_indz += frameCurrent

            RX = rot_vals(rot_indx, False)
            RY = rot_vals(rot_indz, False)
            RZ = rot_vals(rot_indy, False)

            if RX is False or RY is False or RZ is False:
                log.trace('Ignoring bone %d in animation %d, rotation table did not have the entry', bIndx, AnimtoPlay)
                continue

            RX /= 182.04444444444444444444
            RY /= -182.04444444444444444444
            RZ /= 182.04444444444444444444

            RXX = radians(RX)
            RYY = radians(RY)
            RZZ = radians(RZ)

            log.trace("limb: %d XIdx: %d YIdx: %d ZIdx: %d frameTotal: %d", bIndx, rot_indexx, rot_indexy, rot_indexz, frameTotal)
            log.trace("limb: %d RX %d RZ %d RY %d anim: %d frame: %d frameTotal: %d", bIndx, int(RX), int(RZ), int(RY), currentanim+1, frameCurrent+1, frameTotal)
            if (bIndx > -1):
                bone = armature.data.bones["limb_%02i" % (bIndx)]
                bone.select = True
                bpy.ops.transform.rotate(value = RXX, axis=(0, 0, 0), constraint_axis=(True, False, False))
                bpy.ops.transform.rotate(value = RZZ, axis=(0, 0, 0), constraint_axis=(False, False, True))
                bpy.ops.transform.rotate(value = RYY, axis=(0, 0, 0), constraint_axis=(False, True, False))
                bpy.ops.pose.select_all(action="DESELECT")

        bone = armature.pose.bones["limb_00"]
        bone.location += mathutils.Vector((newLocx,newLocz,-newLocy))
        bone.keyframe_insert(data_path='location')

        ### Could have done some math here but... just reverse previus frame, so it just repose.
        bpy.context.scene.tool_settings.use_keyframe_insert_auto = False

        bone = armature.pose.bones["limb_00"]
        bone.location -= mathutils.Vector((newLocx,newLocz,-newLocy))

        for i in range(BoneCountMax):
            bIndx = i

            if RotIndexoffset + (bIndx * 6) + 10 + 2 > len(segment[AniSeg]):
                log.trace('Ignoring bone %d in animation %d, rotation table does not have that many entries', bIndx, AnimtoPlay)
                continue

            rot_indexx = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 6)[0]
            rot_indexy = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 8)[0]
            rot_indexz = unpack_from(">h", segment[AniSeg], RotIndexoffset + (bIndx * 6) + 10)[0]

            rot_indx = rot_indexx
            rot_indy = rot_indexy
            rot_indz = rot_indexz

            if (rot_indx > Limit):
                rot_indx += frameCurrent
            if (rot_indy > Limit):
                rot_indy += frameCurrent
            if (rot_indz > Limit):
                rot_indz += frameCurrent

            RX = rot_vals(rot_indx, False)
            RY = rot_vals(rot_indz, False)
            RZ = rot_vals(rot_indy, False)

            if RX is False or RY is False or RZ is False:
                log.trace('Ignoring bone %d in animation %d, rotation table did not have the entry', bIndx, AnimtoPlay)
                continue

            RX /= -182.04444444444444444444
            RY /= 182.04444444444444444444
            RZ /= -182.04444444444444444444

            RXX = radians(RX)
            RYY = radians(RY)
            RZZ = radians(RZ)

            log.trace("limb: %d XIdx: %d YIdx: %d ZIdx: %d frameTotal: %d", i, rot_indexx, rot_indexy, rot_indexz, frameTotal)
            log.trace("limb: %d RX %d RZ %d RY %d anim: %d frame: %d frameTotal: %d", bIndx, int(RX), int(RZ), int(RY), currentanim+1, frameCurrent+1, frameTotal)
            if (bIndx > -1):
                bone = armature.data.bones["limb_%02i" % (bIndx)]
                bone.select = True
                bpy.ops.transform.rotate(value = RYY, axis=(0, 0, 0), constraint_axis=(False, True, False))
                bpy.ops.transform.rotate(value = RZZ, axis=(0, 0, 0), constraint_axis=(False, False, True))
                bpy.ops.transform.rotate(value = RXX, axis=(0, 0, 0), constraint_axis=(True, False, False))
                bpy.ops.pose.select_all(action="DESELECT")

        if (frameCurrent < (frameTotal - 1)):## Next Frame
            frameCurrent += 1
            self.buildAnimations(hierarchyMostBones, frameCurrent)
        else:
            bpy.context.scene.frame_current = 1

global Animscount
Animscount = 1

class ImportZ64(bpy.types.Operator, ImportHelper):
    """Load a Zelda64 File"""
    bl_idname    = "file.zobj2020"
    bl_label     = "Import Zelda64"
    bl_options   = {'PRESET', 'UNDO'}
    filename_ext = ".zobj"
    filter_glob  = StringProperty(default="*.zobj;*.zmap", options={'HIDDEN'})
    loadOtherSegments = BoolProperty(name="Load Data From Other Segments",
                                    description="Load data from other segments",
                                    default=True,)
    importStrategy = EnumProperty(name='Detect DLists',
                                 items=(('NO_DETECTION', 'Minimum', 'Maps: only use headers\nObjects: only use hierarchies\nOnly this option will not create unexpected geometry'),
                                        ('BRUTEFORCE', 'Bruteforce', 'Try to import everything that looks like a display list\n(ignores header for maps)'),
                                        ('SMART', 'Smart-ish', 'Minimum + Bruteforce but avoids reading the same display lists several times'),
                                        ('TRY_EVERYTHING', 'Try everything', 'Minimum + Bruteforce'),),
                                 description='How to find display lists to import (try this if there is missing geometry)',
                                 default='NO_DETECTION',)
    vertexMode = EnumProperty(name="Vtx Mode",
                             items=(('COLORS', "COLORS", "Use vertex colors"),
                                    ('NORMALS', "NORMALS", "Use vertex normals as shading"),
                                    ('NONE', "NONE", "Don't use vertex colors or normals"),
                                    ('AUTO', "AUTO", "Switch between normals and vertex colors automatically according to 0xD9 G_GEOMETRYMODE flags"),),
                             description="Legacy option, shouldn't be useful",
                             default='AUTO',)
    useVertexAlpha = BoolProperty(name="Use vertex alpha",
                                 description="Only enable if your version of blender has native support",
                                 default=(bpy.app.version == (2,79,7) and bpy.app.build_hash == b'10f724cec5e3'),)
    enableMatrices = BoolProperty(name="Matrices",
                                 description="Use 0xDA G_MTX and 0xD8 G_POPMTX commands",
                                 default=True,)
    detectedDisplayLists_use_transparency = BoolProperty(name="Default to transparency",
                                                         description='Set material to use transparency or not for display lists that were detected',
                                                         default=False,)
    enablePrimColor = BoolProperty(name="Prim Color",
                                  description="Enable blending with primitive color",
                                  default=False,) # this may be nice for strictly importing but exporting again will then not be exact
    enableEnvColor = BoolProperty(name="Env Color",
                                 description="Enable blending with environment color",
                                 default=False,) # same as primColor above
    invertEnvColor = BoolProperty(name="Invert Env Color",
                                 description="Invert environment color (temporary fix)",
                                 default=False,) # todo what is this?
    exportTextures = BoolProperty(name="Export Textures",
                                 description="Export textures for the model",
                                 default=True,)
    importTextures = BoolProperty(name="Import Textures",
                                 description="Import textures for the model",
                                 default=True,)
    enableTexClampBlender = BoolProperty(name="Texture Clamp",
                                 description="Enable texture clamping in Blender, used by Blender in the 3d viewport and by zzconvert",
                                 default=False,)
    replicateTexMirrorBlender = BoolProperty(name="Texture Mirror",
                                  description="Replicate texture mirroring by writing the textures with the mirrored parts (with double width/height) instead of the initial texture",
                                  default=False,)
    enableTexClampSharpOcarinaTags = BoolProperty(name="Texture Clamp SO Tags",
                                 description="Add #ClampX and #ClampY tags where necessary in the texture filename, used by SharpOcarina",
                                 default=False,)
    enableTexMirrorSharpOcarinaTags = BoolProperty(name="Texture Mirror SO Tags",
                                  description="Add #MirrorX and #MirrorY tags where necessary in the texture filename, used by SharpOcarina",
                                  default=False,)
    enableShadelessMaterials = BoolProperty(name="Shadeless Materials",
                                  description="Set materials to be shadeless, prevents using environment colors in-game",
                                  default=False,)
    enableToon = BoolProperty(name="Toony UVs",
                             description="Obtain a toony effect by not scaling down the uv coords",
                             default=False,)
    originalObjectScale = IntProperty(name="File Scale",
                             description="Scale of imported object, blender model will be scaled 1/(file scale) (use 1 for maps, actors are usually 100, 10 or 1) (0 defaults to 1 for maps and 100 for actors)",
                             default=0, min=0, soft_max=1000)
    loadAnimations = BoolProperty(name="Load animations",
                             description="For animated actors, load all animations or none",
                             default=True,)
    MajorasAnims = BoolProperty(name="MajorasAnims",
                             description="Majora's Mask Link's Anims.",
                             default=False,)
    ExternalAnimes = BoolProperty(name="ExternalAnimes",
                             description="Load External Animes.",
                             default=False,)
    setView3dParameters = BoolProperty(name="Set 3D View parameters",
                             description="For maps, use a more appropriate grid size and clip distance",
                             default=True,)
    logging_level = IntProperty(name="Log level",
                             description="(logs in the system console) The lower, the more logs. trace=%d debug=%d info=%d" % (logging_trace_level,logging.DEBUG,logging.INFO),
                             default=logging.INFO, min=1, max=51)
    report_logging_level = IntProperty(name='Report level',
                             description='What logs to report to Blender. When the import is done, warnings and errors are shown, if any. trace=%d debug=%d info=%d' % (logging_trace_level,logging.DEBUG,logging.INFO),
                             default=logging.INFO, min=1, max=51)
    logging_logfile_enable = BoolProperty(name='Log to file',
                             description='Log everything (all levels) to a file',
                             default=False,)
    logging_logfile_path = StringProperty(name='Log file path',
                             #subtype='FILE_PATH', # cannot use two FILE_PATH at the same time
                             description='File to write logs to\nPath can be relative (to imported file) or absolute',
                             default='log_io_import_z64.txt',)

    def execute(self, context):
        global fpath
        fpath, fext = os.path.splitext(self.filepath)
        fpath, fname = os.path.split(fpath)
        global importStrategy
        global vertexMode, enableMatrices
        global detectedDisplayLists_use_transparency
        global useVertexAlpha
        global enablePrimColor, enableEnvColor, invertEnvColor
        global importTextures, exportTextures
        global enableTexClampBlender, replicateTexMirrorBlender
        global enableTexClampSharpOcarinaTags, enableTexMirrorSharpOcarinaTags
        global enableMatrices, enableToon
        global AnimtoPlay, MajorasAnims, ExternalAnimes
        importStrategy = self.importStrategy
        vertexMode = self.vertexMode
        useVertexAlpha = self.useVertexAlpha
        enableMatrices = self.enableMatrices
        detectedDisplayLists_use_transparency = self.detectedDisplayLists_use_transparency
        enablePrimColor = self.enablePrimColor
        enableEnvColor = self.enableEnvColor
        invertEnvColor = self.invertEnvColor
        importTextures = self.importTextures
        exportTextures = self.exportTextures
        enableTexClampBlender = self.enableTexClampBlender
        replicateTexMirrorBlender = self.replicateTexMirrorBlender
        enableTexClampSharpOcarinaTags = self.enableTexClampSharpOcarinaTags
        enableTexMirrorSharpOcarinaTags = self.enableTexMirrorSharpOcarinaTags
        enableToon = self.enableToon
        AnimtoPlay = 1 if self.loadAnimations else 0
        MajorasAnims = self.MajorasAnims
        ExternalAnimes = self.ExternalAnimes
        global enableShadelessMaterials
        enableShadelessMaterials = self.enableShadelessMaterials
        global scaleFactor
        if self.originalObjectScale == 0:
            if fext.lower() == '.zmap':
                scaleFactor = 1 # maps are actually stored 1:1
            else:
                scaleFactor = 1 / 100
        else:
            scaleFactor = 1 / self.originalObjectScale
        setLoggingLevel(self.logging_level)
        log = getLogger('ImportZ64.execute')
        if self.logging_logfile_enable:
            logfile_path = self.logging_logfile_path
            if not os.path.isabs(logfile_path):
                logfile_path = '%s/%s' % (fpath, logfile_path)
            log.info('Writing logs to %s' % logfile_path)
            setLogFile(logfile_path)
        setLogOperator(self, self.report_logging_level)
        try:
            log.info("Importing '%s'..." % fname)
            time_start = time.time()
            self.run_import(fpath, fname, fext)
            log.info("SUCCESS:  Elapsed time %.4f sec" % (time.time() - time_start))
            bpy.context.scene.update()
        finally:
            setLogFile(None)
            setLogOperator(None)
        return {'FINISHED'}

    def run_import(self, fpath, fname, fext):
        log = getLogger('ImportZ64.run_import')
        f3dzex = F3DZEX()
        f3dzex.loaddisplaylists(os.path.join(fpath, "displaylists.txt"))
        if self.loadOtherSegments:
            log.debug('Loading other segments')
            # for segment 2, use [zmap prefix]_scene.zscene then segment_02.zdata then fallback to any .zscene
            scene_file = None
            if "_room" in fname:
                scene_file = fpath + "/" + fname[:fname.index("_room")] + "_scene.zscene"
            if not scene_file or not os.path.isfile(scene_file):
                scene_file = fpath + "/segment_02.zdata"
            if not scene_file or not os.path.isfile(scene_file):
                scene_file = None
                for f in os.listdir(fpath):
                    if f.endswith('.zscene'):
                        if scene_file:
                            log.warning('Found another .zscene file %s, keeping %s' % (f, scene_file))
                        else:
                            scene_file = fpath + '/' + f
            if scene_file and os.path.isfile(scene_file):
                log.info('Loading scene segment 0x02 from %s' % scene_file)
                f3dzex.loadSegment(2, scene_file)
            else:
                log.debug('No file found to load scene segment 0x02 from')
            for i in range(16):
                if i == 2:
                    continue
                # I was told this is "ZRE" naming?
                segment_data_file = fpath + "/segment_%02X.zdata" % i
                if os.path.isfile(segment_data_file):
                    log.info('Loading segment 0x%02X from %s' % (i, segment_data_file))
                    f3dzex.loadSegment(i, segment_data_file)
                else:
                    log.debug('No file found to load segment 0x%02X from', i)

        if fext.lower() == '.zmap':
            log.debug('Importing map')
            f3dzex.loadSegment(0x03, self.filepath)
            f3dzex.importMap()
        else:
            log.debug('Importing object')
            f3dzex.loadSegment(0x06, self.filepath)
            f3dzex.importObj()

        #Noka here
        if fext.lower() == '.zmap' and self.setView3dParameters:
            for screen in bpy.data.screens:
                for area in screen.areas:
                    if area.type == 'VIEW_3D':
                        area.spaces.active.grid_lines = 500
                        area.spaces.active.grid_scale = 10
                        area.spaces.active.grid_subdivisions = 10
                        area.spaces.active.clip_end = 900000
        #

    def draw(self, context):
        l = self.layout
        l.prop(self, 'importStrategy', text='Strategy')
        if self.importStrategy != 'NO_DETECTION':
            l.prop(self, 'detectedDisplayLists_use_transparency')
        l.prop(self, "vertexMode")
        l.prop(self, "useVertexAlpha")
        l.prop(self, "loadOtherSegments")
        l.prop(self, "originalObjectScale")
        box = l.box()
        box.prop(self, "enableTexClampBlender")
        box.prop(self, "replicateTexMirrorBlender")
        if self.replicateTexMirrorBlender:
            wBox = box.box()
            wBox.label(text='Enabling texture mirroring', icon='ERROR')
            wBox.label(text='will break exporting with', icon='ERROR')
            wBox.label(text='SharpOcarina, and may break', icon='ERROR')
            wBox.label(text='exporting in general with', icon='ERROR')
            wBox.label(text='other tools.', icon='ERROR')
        box.prop(self, "enableTexClampSharpOcarinaTags")
        box.prop(self, "enableTexMirrorSharpOcarinaTags")
        l.prop(self, "enableMatrices")
        l.prop(self, "enablePrimColor")
        l.prop(self, "enableEnvColor")
        l.prop(self, "invertEnvColor")
        l.prop(self, "exportTextures")
        l.prop(self, "importTextures")
        l.prop(self, "enableShadelessMaterials")
        l.prop(self, "enableToon")
        l.separator()
        l.prop(self, "loadAnimations")
        l.prop(self, "MajorasAnims")
        l.prop(self, "ExternalAnimes")
        l.prop(self, "setView3dParameters")
        l.separator()
        l.prop(self, "logging_level")
        l.prop(self, 'logging_logfile_enable')
        if self.logging_logfile_enable:
            l.prop(self, 'logging_logfile_path')

def menu_func_import(self, context):
    self.layout.operator(ImportZ64.bl_idname, text="Zelda64 (.zobj;.zmap)")


def register():
    registerLogging()
    bpy.utils.register_module(__name__)
    bpy.types.INFO_MT_file_import.append(menu_func_import)

def unregister():
    bpy.utils.unregister_module(__name__)
    bpy.types.INFO_MT_file_import.remove(menu_func_import)
    unregisterLogging()


if __name__ == "__main__":
    register()
