/*
 * SpeedFan Information Tool 1.0
 *
 * Retrieves temperature information from SpeedFan.
 *
 * Original code (c) 2008, Christopher Vagnetoft
 *
 * C++ wrapper class (c) 2009, Aaron Maslen
 *
 * Free to use and reuse under the GNU Public License (GPL) v2.
 *
 * Note: This is probably the only sample available on the internet on how to
 * access SpeedFans shared memory using C++. Disturbing, isn't it? 
 */


//SFClass.h
#ifndef SFCLASS
#define SFCLASS


#include <windows.h>

// pragma pack is included here because the struct is a pascal Packed Record,
// meaning that fields aren't aligned on a 4-byte boundary. 4 bytes fit 2
// 2-byte records.
#pragma pack(1)

class SFSensors {
	// This is the struct we're using to access the shared memory.
	struct SFMemory {
		WORD version;
		WORD flags;
		int MemSize;
		int handle;
		WORD NumTemps;
		WORD NumFans;
		WORD NumVolts;
		signed int temps[32];
		signed int fans[32];
		signed int volts[32];
	};

	private:
		HANDLE file;
		HANDLE filemap;
		SFMemory* sfmemory;

	public:
		SFSensors() {
		const char* filename = "SFSharedMemory_ALM";
		const char* mapname  = "SFSharedMemory_ALM";
		UINT nSize = sizeof(SFMemory);

		// Open file
		file = (HANDLE)CreateFileA(filename,GENERIC_READ | GENERIC_WRITE, 0,
			NULL, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);
		// Create file mapping
		filemap = (HANDLE)CreateFileMappingA(file,NULL,PAGE_READWRITE,0,nSize,mapname);
		// Get pointer
		sfmemory = (SFMemory*)MapViewOfFile(filemap, FILE_MAP_READ, 0, 0, nSize);
		
		//if (!sfmemory)
			//printf("Failed to open shared memory.");
	}

	~SFSensors() {
		// Close the handles we opened.
		CloseHandle(filemap);
		CloseHandle(file);
	}

	WORD getVersion() {
		if (sfmemory)
			return (WORD)(sfmemory->version);
		else
			return NULL;
	}

	WORD getNumFans() {
		if (sfmemory)
			return (WORD)(sfmemory->NumFans);
		else
			return NULL;
	}

	WORD getNumTemps() {
		if (sfmemory)
			return (WORD)(sfmemory->NumTemps);
		else
			return NULL;
	}

	WORD getNumVolts() {
		if (sfmemory)
			return (WORD)(sfmemory->NumVolts);
		else
			return NULL;
	}

	int getTemp(int id) {
		if (sfmemory)
			return (int)(sfmemory->temps[id]);
		else
			return NULL;
	}

	int getFan(int id) {
		if (sfmemory)
			return (int)(sfmemory->fans[id]);
		else
			return NULL;
	}

	int getVolt(int id) {
		if (sfmemory)
			return (int)(sfmemory->volts[id]);
		else
			return NULL;
	}

};

#endif
