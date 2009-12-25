// SpeedFanWrapper.cpp : Defines the exported functions for the DLL application.
//

#include "stdafx.h"
#include "SpeedFanWrapper.h"

SFSensors sensors;

extern "C" {
	SPEEDFANWRAPPER_API WORD /*__stdcall*/ getVersion() {
		return sensors.getVersion();
	}

	SPEEDFANWRAPPER_API WORD /*__stdcall*/ getNumFans() {
		return sensors.getNumFans();
	}

	SPEEDFANWRAPPER_API WORD /*__stdcall*/ getNumTemps() {
		return sensors.getNumTemps();
	}

	SPEEDFANWRAPPER_API WORD /*__stdcall*/ getNumVolts() {
		return sensors.getNumVolts();
	}

	SPEEDFANWRAPPER_API int /*__stdcall*/ getTemp(int id) {
		return sensors.getTemp(id);
	}

	SPEEDFANWRAPPER_API int /*__stdcall*/ getFan(int id) {
		return sensors.getFan(id);
	}

	SPEEDFANWRAPPER_API int /*__stdcall*/ getVolt(int id) {
		return sensors.getVolt(id);
	}
}

/*
// This is an example of an exported variable
SPEEDFANWRAPPER_API int nSpeedFanWrapper=0;

// This is an example of an exported function.
SPEEDFANWRAPPER_API int fnSpeedFanWrapper(void)
{
	return 42;
}

// This is the constructor of a class that has been exported.
// see SpeedFanWrapper.h for the class definition
CSpeedFanWrapper::CSpeedFanWrapper()
{
	return;
}
*/