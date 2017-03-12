// Wrapper.h

#pragma once
#define external extern "C" __declspec(dllexport)

using namespace System;
using namespace WCFSharp::External;

external unsigned int __stdcall PluginGetData(unsigned int ID, void* InBuffer, unsigned int InBufferSize, void* OutBuffer, unsigned int OutBufferSize)
{
	return CommfortPlugin::GetData(ID, InBuffer, InBufferSize, OutBuffer, OutBufferSize);
}

external void __stdcall PluginProcess(unsigned int ID, void* InBuffer, unsigned int InBufferSize)
{
	CommfortPlugin::Process(ID, InBuffer, InBufferSize);
}

external bool __stdcall PluginStart(unsigned int PluginID, void* Func1, void* Func2)
{
	return CommfortPlugin::Start(PluginID, Func1, Func2);
}

external void __stdcall PluginStop()
{
	CommfortPlugin::Stop();
}

external void __stdcall PluginShowOptions()
{
	CommfortPlugin::ShowOptions();
}

external void __stdcall PluginShowAbout()
{
	CommfortPlugin::ShowAbout();
}
