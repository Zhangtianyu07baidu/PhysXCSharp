#pragma once
class UserAssertHandler : public PxAssertHandler
{
public:
	UserAssertHandler();
	~UserAssertHandler();

	virtual void operator()(const char* exp, const char* file, int line, bool& ignore);
};