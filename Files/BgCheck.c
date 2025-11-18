#include <uLib.h>
#include "code/z_bgcheck.h"
#include <uLib_vector.h>
//Version: 1.1
/*This version value is used by SharpOcarina to determine if it needs to update the BgCheck.c of an old project
to use newly added features. Put a high value like 99 to stop SharpOcarina from ever asking to update it again.
*/

static bool Col3D_LineVsTriangle(Vec3f tri[3], Vec3f start, Vec3f end, Vec3f* outPos, Vec3f* outNor, bool cullBackface, bool infinite) {
    Vec3f vertex0 = tri[0];
    Vec3f vertex1 = tri[1];
    Vec3f vertex2 = tri[2];
    Vec3f edge1, edge2, h, s, q;
    f32 a, f, u, v;
    Vec3f dir = Vec3f_LineSegDir(start, end);
    f32 dist = Vec3f_DistXYZ(start, end);
    
    edge1 = Vec3f_Sub(vertex1, vertex0);
    edge2 = Vec3f_Sub(vertex2, vertex0);
    h = Vec3f_Cross(dir, edge2);
    a = Vec3f_Dot(edge1, h);
    if ((cullBackface && a < 0.0f))
        return false;
    if (a > -EPSILON && a < EPSILON)
        return false;          // This ray is parallel to this triangle.
    f = 1.0f / a;
    s = Vec3f_Sub(start, vertex0);
    u = f * Vec3f_Dot(s, h);
    if (u < 0.0f || u > 1.0f)
        return false;
    q = Vec3f_Cross(s, edge1);
    v = f * Vec3f_Dot(dir, q);
    if (v < 0.0f || u + v > 1.0f)
        return false;
    // At this stage we can compute t to find out where the intersection point is on the line.
    f32 t = f * Vec3f_Dot(edge2, q);
    
    if (t > EPSILON && (infinite || t < dist)) { // ray intersection
        if (outPos)
            *outPos = Vec3f_Add(start, Vec3f_MulVal(dir, t));
        if (outNor)
            *outNor = h;
        
        return true;
    } else                     // This means that there is a line intersection but not a ray intersection.
        return false;
}

Asm_VanillaHook(SSNodeList_Alloc);
void SSNodeList_Alloc(PlayState* play, SSNodeList* this, s32 tblMax, s32 numPolys) {
    this->max = tblMax;
    this->count = 0;
    this->tbl = THA_AllocEndAlign(&play->state.tha, tblMax * sizeof(SSNode), -2);
    
    ASSERT(this->tbl != NULL, "this->short_slist_node_tbl != NULL", "../z_bgcheck.c", 5975);
    
    // this->polyCheckTbl = GameState_Alloc(&play->state, numPolys, "../z_bgcheck.c", 5979);
    // ASSERT(this->polyCheckTbl != NULL, "this->polygon_check != NULL", "../z_bgcheck.c", 5981);
}

Asm_VanillaHook(BgCheck_PosErrorCheck);
s32 BgCheck_PosErrorCheck(Vec3f* pos, char* file, s32 line) {
#if 0
    if (
        pos->x >= BGCHECK_XYZ_ABSMAX || pos->x <= -BGCHECK_XYZ_ABSMAX || pos->y >= BGCHECK_XYZ_ABSMAX ||
        pos->y <= -BGCHECK_XYZ_ABSMAX || pos->z >= BGCHECK_XYZ_ABSMAX || pos->z <= -BGCHECK_XYZ_ABSMAX
    ) {
        osSyncPrintf(VT_FGCOL(RED));
        // "Position is invalid."
        osSyncPrintf(
            "T_BGCheck_PosErrorCheck():位置が妥当ではありません。pos (%f,%f,%f) file:%s line:%d\n", pos->x,
            pos->y, pos->z, file, line
        );
        osSyncPrintf(VT_RST);
        
        return true;
    }
#endif
    
    return false;
}

Asm_VanillaHook(BgCheck_ResetPolyCheckTbl);
void BgCheck_ResetPolyCheckTbl(SSNodeList* nodeList, s32 numPolys) {
    nodeList->polyCheckTbl = NULL;
    // bzero(nodeList->polyCheckTbl, numPolys);
}

Asm_VanillaHook(BgCheck_CheckLineAgainstSSList);
s32 BgCheck_CheckLineAgainstSSList(SSList* ssList, CollisionContext* colCtx, u16 xpFlags1, u16 xpFlags2, Vec3f* posA, Vec3f* posB, Vec3f* outPos, CollisionPoly** outPoly, f32* outDistSq, f32 chkDist, s32 bccFlags) {
    SSNode* curNode;
    // u8* checkedPoly;
    Vec3f polyIntersect;
    CollisionPoly* polyList;
    CollisionPoly* curPoly;
    s32 result;
    f32 minY;
    f32 distSq;
    s16 polyId;
    
    result = false;
    polyList = colCtx->colHeader->polyList;
    if (ssList->head == SS_NULL) {
        return result;
    }
    
    curNode = &colCtx->polyNodes.tbl[ssList->head];
    while (true) {
        polyId = curNode->polyId;
        // checkedPoly = &colCtx->polyNodes.polyCheckTbl[polyId];
        
        if (
            /* *checkedPoly == true || */ COLPOLY_VIA_FLAG_TEST(polyList[polyId].flags_vIA, xpFlags1) ||
            !(xpFlags2 == 0 || COLPOLY_VIA_FLAG_TEST(polyList[polyId].flags_vIA, xpFlags2))
        ) {
            
            if (curNode->next == SS_NULL) {
                break;
            } else {
                curNode = &colCtx->polyNodes.tbl[curNode->next];
                continue;
            }
        }
        // *checkedPoly = true;
        curPoly = &polyList[polyId];
        minY = CollisionPoly_GetMinY(curPoly, colCtx->colHeader->vtxList);
        if (posA->y < minY && posB->y < minY) {
            break;
        }
        if (
            CollisionPoly_LineVsPoly(
                curPoly, colCtx->colHeader->vtxList, posA, posB, &polyIntersect,
                (bccFlags & BGCHECK_CHECK_ONE_FACE) != 0, chkDist
            )
        ) {
            distSq = Math3D_Vec3fDistSq(posA, &polyIntersect);
            if (distSq < *outDistSq) {
                
                *outDistSq = distSq;
                *outPos = polyIntersect;
                *posB = polyIntersect;
                *outPoly = curPoly;
                result = true;
            }
        }
        if (curNode->next == SS_NULL) {
            break;
        }
        curNode = &colCtx->polyNodes.tbl[curNode->next];
    }
    
    return result;
}

Asm_VanillaHook(CollisionPoly_CheckYIntersect);
s32 CollisionPoly_CheckYIntersect(CollisionPoly* poly, Vec3s* vtxList, f32 x, f32 z, f32* yIntersect, f32 chkDist) {
    static Vec3f polyVerts[3];
    Vec3f out;
    Vec3f start = { x, BGCHECK_XYZ_ABSMAX, z };
    Vec3f end = { x, 0, z };
    
    CollisionPoly_GetVertices(poly, vtxList, polyVerts);
    if (Col3D_LineVsTriangle(polyVerts, start, end, &out, NULL, false, true)) {
        *yIntersect = out.y;
        
        return true;
    }
    
    return false;
}

Asm_VanillaHook(BgCheck_Allocate);
void BgCheck_Allocate(CollisionContext* colCtx, PlayState* play, CollisionHeader* colHeader) {
    u32 tblMax;
    u32 memSize;
    
    colCtx->colHeader = colHeader;
    
    colCtx->memSize = 0xF000 * 8;
    colCtx->dyna.polyNodesMax = 1000 * 8;
    colCtx->dyna.polyListMax = 512 * 8;
    colCtx->dyna.vtxListMax = 512 * 8;

    if (colCtx->subdivAmount.x == 0 ||  colCtx->subdivAmount.y == 0 ||  colCtx->subdivAmount.z == 0)
    {
        colCtx->subdivAmount.x = 16;
        colCtx->subdivAmount.y = 4;
        colCtx->subdivAmount.z = 16;
    }
    
    colCtx->lookupTbl = THA_AllocEndAlign(
        &play->state.tha,
        colCtx->subdivAmount.x * sizeof(StaticLookup) * colCtx->subdivAmount.y * colCtx->subdivAmount.z,
        ~1
    );
    if (colCtx->lookupTbl == NULL) {
        LogUtils_HungupThread(0, 0);
    }
    colCtx->minBounds.x = colCtx->colHeader->minBounds.x;
    colCtx->minBounds.y = colCtx->colHeader->minBounds.y;
    colCtx->minBounds.z = colCtx->colHeader->minBounds.z;
    colCtx->maxBounds.x = colCtx->colHeader->maxBounds.x;
    colCtx->maxBounds.y = colCtx->colHeader->maxBounds.y;
    colCtx->maxBounds.z = colCtx->colHeader->maxBounds.z;
    BgCheck_SetSubdivisionDimension(
        colCtx->minBounds.x,
        colCtx->subdivAmount.x,
        &colCtx->maxBounds.x,
        &colCtx->subdivLength.x,
        &colCtx->subdivLengthInv.x
    );
    BgCheck_SetSubdivisionDimension(
        colCtx->minBounds.y,
        colCtx->subdivAmount.y,
        &colCtx->maxBounds.y,
        &colCtx->subdivLength.y,
        &colCtx->subdivLengthInv.y
    );
    BgCheck_SetSubdivisionDimension(
        colCtx->minBounds.z,
        colCtx->subdivAmount.z,
        &colCtx->maxBounds.z,
        &colCtx->subdivLength.z,
        &colCtx->subdivLengthInv.z
    );
    memSize = colCtx->subdivAmount.x * sizeof(StaticLookup) * colCtx->subdivAmount.y * colCtx->subdivAmount.z +
        colCtx->colHeader->numPolygons * sizeof(u8) + colCtx->dyna.polyNodesMax * sizeof(SSNode) +
        colCtx->dyna.polyListMax * sizeof(CollisionPoly) + colCtx->dyna.vtxListMax * sizeof(Vec3s) +
        sizeof(CollisionContext);
    
    if (colCtx->memSize < memSize) {
        LogUtils_HungupThread(0, 0);
    }
    tblMax = (colCtx->memSize - memSize) / sizeof(SSNode);
    
    SSNodeList_Initialize(&colCtx->polyNodes);
    SSNodeList_Alloc(play, &colCtx->polyNodes, tblMax, colCtx->colHeader->numPolygons);
    
    BgCheck_InitializeStaticLookup(colCtx, play, colCtx->lookupTbl);
    
    DynaPoly_Init(play, &colCtx->dyna);
    DynaPoly_Alloc(play, &colCtx->dyna);
}

Asm_VanillaHook(Scene_CommandCollisionHeader);
void Scene_CommandCollisionHeader(PlayState* play, SceneCmd* cmd) {
    CollisionHeader* colHeader = SEGMENTED_TO_VIRTUAL(cmd->colHeader.data);

    play->colCtx.subdivAmount.x = *AVAL(&cmd->colHeader,u8,1);
    play->colCtx.subdivAmount.y = *AVAL(&cmd->colHeader,u8,2);
    play->colCtx.subdivAmount.z = *AVAL(&cmd->colHeader,u8,3);

    colHeader->vtxList = SEGMENTED_TO_VIRTUAL(colHeader->vtxList);
    colHeader->polyList = SEGMENTED_TO_VIRTUAL(colHeader->polyList);
    colHeader->surfaceTypeList = SEGMENTED_TO_VIRTUAL(colHeader->surfaceTypeList);
    colHeader->bgCamList = SEGMENTED_TO_VIRTUAL(colHeader->bgCamList);
    colHeader->waterBoxes = SEGMENTED_TO_VIRTUAL(colHeader->waterBoxes);

    BgCheck_Allocate(&play->colCtx, play, colHeader);
}