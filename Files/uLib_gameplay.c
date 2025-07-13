#include <uLib.h>

extern void OvlMessage_Update(PlayState* play);

void z64rom_PrePlayUpdate(PlayState* play) {
	
}

void z64rom_PostPlayUpdate(PlayState* play) {
	OvlMessage_Update(play);
}

void z64rom_PrePlayDraw(PlayState* play) {
	
}

void z64rom_PostPlayDraw(PlayState* play) {
}
