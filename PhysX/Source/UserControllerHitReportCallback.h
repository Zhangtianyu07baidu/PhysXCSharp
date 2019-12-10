#pragma once
class UserControllerHitReportCallback : public PxUserControllerHitReport
{
public:
	UserControllerHitReportCallback(class CapsuleController* controller, OnHitCallback onHitShape, OnHitCallback onHitController);
	~UserControllerHitReportCallback();

	/**
	\brief Called when current controller hits a shape.

	This is called when the CCT moves and hits a shape. This will not be called when a moving shape hits a non-moving CCT.

	\param[in] hit Provides information about the hit.

	@see PxControllerShapeHit
	*/
	virtual void onShapeHit(const PxControllerShapeHit& hit);

	/**
	\brief Called when current controller hits another controller.

	\param[in] hit Provides information about the hit.

	@see PxControllersHit
	*/
	virtual void onControllerHit(const PxControllersHit& hit);

	/**
	\brief Called when current controller hits a user-defined obstacle.

	\param[in] hit Provides information about the hit.

	@see PxControllerObstacleHit PxObstacleContext
	*/
	virtual void onObstacleHit(const PxControllerObstacleHit& hit);

private:
	CapsuleController* controller;
	OnHitCallback onHitShape;
	OnHitCallback onHitController;
};

