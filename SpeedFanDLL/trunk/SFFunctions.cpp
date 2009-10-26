//SFFunctions.cpp

#include "SFFunctions.h"
#include "SFclass.h"

SFSensors sensors;

//namespace SpeedFanDll
//{
extern "C" {
	WORD /*__stdcall*/ getVersion() {
		return sensors.getVersion();
	}

	WORD /*__stdcall*/ getNumFans() {
		return sensors.getNumFans();
	}

	WORD /*__stdcall*/ getNumTemps() {
		return sensors.getNumTemps();
	}

	WORD /*__stdcall*/ getNumVolts() {
		return sensors.getNumVolts();
	}

	int /*__stdcall*/ getTemp(int id) {
		return sensors.getTemp(id);
	}

	int /*__stdcall*/ getFan(int id) {
		return sensors.getFan(id);
	}

	int /*__stdcall*/ getVolt(int id) {
		return sensors.getVolt(id);
	}
}
//}
