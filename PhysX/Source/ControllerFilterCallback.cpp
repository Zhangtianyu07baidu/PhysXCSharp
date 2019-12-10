#include "stdafx.h"


ControllerFilterCallback::ControllerFilterCallback(OnFilterCallback callback)
{
	this->collideCallback = callback;
}


ControllerFilterCallback::~ControllerFilterCallback()
{
	this->collideCallback = NULL;
}

bool ControllerFilterCallback::filter(const PxController& a, const PxController& b)
{
	CapsuleController* controllerA = static_cast<CapsuleController*>(a.getActor()->userData);
	CapsuleController* controllerB = static_cast<CapsuleController*>(b.getActor()->userData);
	if (controllerA == NULL || controllerB == NULL)
	{
		return false;
	}
	if (controllerA->IsFilterEnabled())
	{
		std::set<int>& ignoreLayers = PhysicsManager::GetInstance().GetIgnoreLayers();

		set<int>::iterator it = ignoreLayers.begin();
		set<int>::iterator end = ignoreLayers.end();
		for (; it != end; ++it)
		{
			const int layerSum = *it;

			const int resultA = (1 << controllerA->GetLayer()) & layerSum;
			const int resultB = (1 << controllerB->GetLayer()) & layerSum;
			if (resultA != 0 && resultB != 0)
			{
				return false;
			}
		}
	}

	if (this->collideCallback != NULL)
	{
		return this->collideCallback(controllerA->GetScene(), a.getActor(), b.getActor());
	}

	return true;
}