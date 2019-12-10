#include "stdafx.h"
#include "UserControllerHitReportCallback.h"

//指针初始化不能省略，否则android上易出现错误
CapsuleController::CapsuleController(CapsuleControllerData& data, PhysicsScene* scene, 
	OnFilterCallback withWorldCb, OnFilterCallback withControllerCb,
	OnHitCallback hitShapeCb, OnHitCallback hitControllerCb) : ColliderBase(scene), controller(NULL),
	queryFilterCallback(NULL), controllerFilterCallback(NULL), hitReportCallback(NULL)
{
	this->controllerData = data;
	if(withWorldCb != NULL && withControllerCb != NULL)
	{
		this->queryFilterCallback = new QueryFilterCallback(this, withWorldCb);
		this->controllerFilterCallback = new ControllerFilterCallback(withControllerCb);
	}

	if (hitShapeCb != NULL && hitControllerCb != NULL)
	{
		this->hitReportCallback = new UserControllerHitReportCallback(this, hitShapeCb, hitControllerCb);
	}

	this->filterEnabled = true;
}

CapsuleController::~CapsuleController()
{
	if (this->queryFilterCallback != NULL)
	{
		delete this->queryFilterCallback;
		this->queryFilterCallback = NULL;
	}
	
	if (this->controllerFilterCallback != NULL)
	{
		delete this->controllerFilterCallback;
		this->controllerFilterCallback = NULL;
	}
	
	if (this->hitReportCallback != NULL)
	{
		delete this->hitReportCallback;
		this->hitReportCallback = NULL;
	}
}

PxRigidDynamic* CapsuleController::CreateController(PxMaterial* material)
{
	this->filters.mCCTFilterCallback = this->controllerFilterCallback;
	this->filters.mFilterCallback = this->queryFilterCallback;

	PxCapsuleControllerDesc capsuleDesc;
	capsuleDesc.radius = this->controllerData.radius;
	capsuleDesc.height = this->controllerData.height;
	capsuleDesc.material = material;
	capsuleDesc.slopeLimit = cosf(this->controllerData.slopeLimit * PxPi / 180);
	capsuleDesc.stepOffset = this->controllerData.stepOffset;
	capsuleDesc.contactOffset = this->controllerData.skinWidth;
	capsuleDesc.climbingMode = PxCapsuleClimbingMode::eCONSTRAINED;
	
	capsuleDesc.position = PxExtendedVec3(this->controllerData.position.x, this->controllerData.position.y, this->controllerData.position.z);

	capsuleDesc.reportCallback = this->hitReportCallback;

	PxController* c = this->scene->controllerMgr->createController(capsuleDesc);
	this->controller = static_cast<PxCapsuleController*>(c);
	PxRigidDynamic* actor = this->controller->getActor();
	return actor;
}

int CapsuleController::GetLayer()
{
	return this->controllerData.layer;
}

ColliderType::Enum CapsuleController::GetType()
{
	return ColliderType::Controller;
}

void CapsuleController::Release()
{
	this->controller->getActor()->userData = NULL;
	this->controller->release();
}

void CapsuleController::SetPosition(const PxExtendedVec3& position) const
{
	this->controller->setPosition(position);
}

const PxExtendedVec3& CapsuleController::GetPosition() const
{
	return this->controller->getPosition();
}

void CapsuleController::SetFootPosition(const PxExtendedVec3& position) const
{
	this->controller->setFootPosition(position);
}

const PxExtendedVec3 CapsuleController::GetFootPosition() const
{
	return this->controller->getFootPosition();
}

PxControllerCollisionFlags CapsuleController::Move(PxVec3& motion) const
{
	return this->controller->move(motion, this->controllerData.minMoveDistance, 0, this->filters);
}

void CapsuleController::SetFilterEnabled(bool enabled)
{
	filterEnabled = enabled;
}

bool CapsuleController::IsFilterEnabled() const
{
	return filterEnabled;
}

void CapsuleController::SetRadius(PxF32 radius) const
{
	this->controller->setRadius(radius);
}

void CapsuleController::SetHeight(PxF32 height) const
{
	this->controller->setHeight(height);
	this->controller->resize(height);
}

void CapsuleController::SetClimbingMode(PxCapsuleClimbingMode::Enum mode) const
{
	this->controller->setClimbingMode(mode);
}

PxCapsuleClimbingMode::Enum CapsuleController::GetClimbingMode() const
{
	return this->controller->getClimbingMode();
}

void CapsuleController::InvalidateCache() const
{
	this->controller->invalidateCache();
}

PxRigidDynamic* CapsuleController::GetActor() const
{
	return this->controller->getActor();
}

/*bool CapsuleController::CanStandUp(PxScene* scene)
{
	PxOverlapBuffer hit;
	PxCapsuleGeometry geom(this->controller->getRadius(), this->controller->getHeight() * 0.5f);
	PxExtendedVec3 position = this->controller->getPosition();
	PxVec3 pos((PxReal)position.x, (PxReal)position.y, (PxReal)position.z);
	PxQuat orientation(PxHalfPi, PxVec3(0.0f, 0.0f, 1.0f));
	return scene->overlap(geom, PxTransform(pos, orientation), hit, PxQueryFilterData(PxQueryFlag::eANY_HIT | PxQueryFlag::eSTATIC | PxQueryFlag::eDYNAMIC));
}*/