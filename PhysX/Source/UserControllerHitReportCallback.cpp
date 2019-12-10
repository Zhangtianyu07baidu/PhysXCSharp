#include "stdafx.h"
#include "UserControllerHitReportCallback.h"



UserControllerHitReportCallback::UserControllerHitReportCallback(CapsuleController* controller, OnHitCallback onHitShape, OnHitCallback onHitController)
{
	this->controller = controller;
	this->onHitShape = onHitShape;
	this->onHitController = onHitController;
}


UserControllerHitReportCallback::~UserControllerHitReportCallback()
{
	this->controller = NULL;
	this->onHitShape = NULL;
	this->onHitController = NULL;
}

void UserControllerHitReportCallback::onShapeHit(const PxControllerShapeHit& hit)
{
	if (this->onHitShape != NULL)
	{
		this->onHitShape(this->controller->GetScene(), this->controller->GetActor(), hit.actor);
	}
}

void UserControllerHitReportCallback::onControllerHit(const PxControllersHit& hit)
{
	if (this->onHitController != NULL)
	{
		this->onHitController(this->controller->GetScene(), this->controller->GetActor(), hit.other->getActor());
	}
}

void UserControllerHitReportCallback::onObstacleHit(const PxControllerObstacleHit& hit)
{

}
