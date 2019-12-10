#pragma once
class UserErrorCallback : public PxErrorCallback
{
public:
	UserErrorCallback();
	~UserErrorCallback();

	virtual void reportError(PxErrorCode::Enum code, const char* message, const char* file, int line);

	void SetErrorCallback(OnErrorCallback cb);

	void Release();
private:
	OnErrorCallback errorCallback;
};

