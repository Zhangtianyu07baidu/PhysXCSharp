#include "stdafx.h"
#include "UserAssertHandler.h"


UserAssertHandler::UserAssertHandler()
{
}

UserAssertHandler::~UserAssertHandler()
{
}

void UserAssertHandler::operator()(const char* exp, const char* file, int line, bool& ignore)
{
	PX_UNUSED(ignore); // is used only in debug windows config
	char buffer[1024];
#if PX_WINDOWS_FAMILY
	sprintf_s(buffer, "Assertion failed: %s\n", exp);
#else
	sprintf(buffer, "Assertion failed: %s\n", exp);
#endif
	PhysicsManager::GetInstance().Print(PxErrorCode::eINTERNAL_ERROR, buffer, file, line);
}
