#pragma once

class PhysicsScene;

class CapsuleController : public ColliderBase
{
	PX_NOCOPY(CapsuleController)

public:
	CapsuleController(CapsuleControllerData& data, PhysicsScene* scene, 
		OnFilterCallback withWorldCb, OnFilterCallback withControllerCb,
		OnHitCallback hitShapeCb, OnHitCallback hitControllerCb);
	~CapsuleController();

	PxRigidDynamic* CreateController(PxMaterial* material);
	virtual int GetLayer();
	virtual void Release();
	virtual ColliderType::Enum GetType();

	void SetPosition(const PxExtendedVec3& position) const;
	const PxExtendedVec3& GetPosition() const;
	void SetFootPosition(const PxExtendedVec3& position) const;
	const PxExtendedVec3 GetFootPosition() const;

	//bool CanStandUp(PxScene* scene);
	PxControllerCollisionFlags Move(PxVec3& motion) const;

	void SetFilterEnabled(bool enabled);
	bool IsFilterEnabled() const;
	void SetRadius(PxF32 radius) const;
	void SetHeight(PxF32 height) const;
	void SetClimbingMode(PxCapsuleClimbingMode::Enum mode) const;
	PxCapsuleClimbingMode::Enum	GetClimbingMode() const;
	void InvalidateCache() const;
	PxRigidDynamic* GetActor() const;

private:
	PxCapsuleController * controller;
	CapsuleControllerData controllerData;
	QueryFilterCallback* queryFilterCallback;
	ControllerFilterCallback* controllerFilterCallback;
	class UserControllerHitReportCallback* hitReportCallback;
	PxControllerFilters filters;
	bool filterEnabled;
};

