#include "stdafx.h"



UserErrorCallback::UserErrorCallback()
{
	this->errorCallback = NULL;
}


UserErrorCallback::~UserErrorCallback()
{
}

void UserErrorCallback::SetErrorCallback(OnErrorCallback cb)
{
	this->errorCallback = cb;
}

void UserErrorCallback::reportError(PxErrorCode::Enum code, const char* message, const char* file, int line)
{
	if (this->errorCallback != NULL)
	{
		if(code <= 1)
		{
			this->errorCallback(1, message);
		}
		else
		{
			char buffer[1024];
#if PX_WINDOWS_FAMILY
			sprintf_s(buffer, "%s(%d) : %s\n", file, line, message);
#else
			sprintf(buffer, "%s(%d) : %s\n", file, line, message);
#endif
			this->errorCallback(code & 3, buffer);
		}
	}
}

void UserErrorCallback::Release()
{
	this->errorCallback = NULL;
}