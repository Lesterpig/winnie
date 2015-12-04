#pragma once

#if defined(_MSC_VER)
	#define EXPORTCDECL extern "C" __declspec(dllexport)
#else
	#define EXPORTCDECL extern "C"
#endif