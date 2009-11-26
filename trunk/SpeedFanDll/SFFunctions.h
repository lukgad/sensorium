//SFFunctions.h

#include "windows.h"


extern "C" {
	__declspec(dllexport) WORD /*__stdcall*/ getVersion();

	__declspec(dllexport) WORD /*__stdcall*/ getNumFans();

	__declspec(dllexport) WORD /*__stdcall*/ getNumTemps();

	__declspec(dllexport) WORD /*__stdcall*/ getNumVolts();

	__declspec(dllexport) int /*__stdcall*/ getTemp(int id);

	__declspec(dllexport) int /*__stdcall*/ getFan(int id);

	__declspec(dllexport) int /*__stdcall*/ getVolt(int id);
}
