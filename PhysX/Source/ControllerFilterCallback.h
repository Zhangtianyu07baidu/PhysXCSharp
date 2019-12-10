#pragma once

class ControllerFilterCallback : public PxControllerFilterCallback
{
public:
	ControllerFilterCallback(OnFilterCallback callback);
	~ControllerFilterCallback();
	
	virtual bool filter(const PxController& a, const PxController& b);

private:
	OnFilterCallback collideCallback;
};