#include "KaleidoScope.h"

static const Vtx sMarkBossVtx[] = {
    VTX(-4, 4,  0, 0,   0,   255, 255, 255, 255),
    VTX(-4, -4, 0, 0,   256, 255, 255, 255, 255),
    VTX(4,  4,  0, 256, 0,   255, 255, 255, 255),
    VTX(4,  -4, 0, 256, 256, 255, 255, 255, 255),
};

static const Vtx sMarkChestVtx[] = {
    VTX(-4, 4,  0, 0,   0,   255, 255, 255, 255),
    VTX(-4, -4, 0, 0,   256, 255, 255, 255, 255),
    VTX(4,  4,  0, 256, 0,   255, 255, 255, 255),
    VTX(4,  -4, 0, 256, 256, 255, 255, 255, 255),
};

PauseMapMarksData gPauseMapMarkDataTable[] = {
