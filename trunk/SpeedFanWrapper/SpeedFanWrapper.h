// The following ifdef block is the standard way of creating macros which make exporting 
// from a DLL simpler. All files within this DLL are compiled with the SPEEDFANWRAPPER_EXPORTS
// symbol defined on the command line. this symbol should not be defined on any project
// that uses this DLL. This way any other project whose source files include this file see 
// SPEEDFANWRAPPER_API functions as being imported from a DLL, whereas this DLL sees symbols
// defined with this macro as being exported.

#ifdef SPEEDFANWRAPPER_EXPORTS
#define SPEEDFANWRAPPER_API __declspec(dllexport)
#else
#define SPEEDFANWRAPPER_API __declspec(dllimport)
#endif

extern "C" {
	SPEEDFANWRAPPER_API WORD getVersion();

	SPEEDFANWRAPPER_API WORD getNumFans();

	SPEEDFANWRAPPER_API WORD getNumTemps();

	SPEEDFANWRAPPER_API WORD getNumVolts();

	SPEEDFANWRAPPER_API int getTemp(int id);

	SPEEDFANWRAPPER_API int getFan(int id);

	SPEEDFANWRAPPER_API int getVolt(int id);
}

/*
// This class is exported from the SpeedFanWrapper.dll
class SPEEDFANWRAPPER_API CSpeedFanWrapper {
public:
	CSpeedFanWrapper(void);
	// TODO: add your methods here.
};

extern SPEEDFANWRAPPER_API int nSpeedFanWrapper;

SPEEDFANWRAPPER_API int fnSpeedFanWrapper(void);
*/

