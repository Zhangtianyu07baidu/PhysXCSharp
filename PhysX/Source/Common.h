#pragma once
class PhysicsManager;

static const PxVec3 FORWARD = PxVec3(0, 0, 1);
static const PxVec3 RIGHT = PxVec3(1, 0, 0);
static const PxVec3 UP = PxVec3(0, 1, 0);
//static const PxReal Rad2Deg = 180.0f / PxPi;
//static const PxReal Deg2Rad = PxPi / 180.0f;

struct ColliderType
{
	enum Enum
	{
		BOX = 1,
		SPHERE = 2,
		CAPSULE = 4,
		MESH = 8,
		Controller = 16,
	};
};

/*enum RigidbodyFreezeType
{
	None = 0,
	FreezePositionX = 1,
	FreezePositionY = 2,
	FreezePositionZ = 4,
	FreezePosition = 7,

	FreezeRotationX = 8,
	FreezeRotationY = 16,
	FreezeRotationZ = 32,
	FreezeRotation = 56,

	FreezeAll = 63,
};*/

struct Vector3
{
	float x, y, z;

	Vector3() { }
	Vector3(const PxVec3& vec3);
	Vector3(float x, float y, float z);
	PxVec3 ToPxVec3();
	Vector3& operator=(PxVec3& p);
};

struct Vector4
{
	float x, y, z, w;
	Vector4() {}
	Vector4(const PxQuat& q);
	Vector4(float x, float y, float z, float w);
	PxQuat ToPxQuat();
	Vector4 Clone();
};

struct RaycastHit
{
	void* actor;
	int layer;
	Vector3 point;
	Vector3 normal;
	float distance;
};

struct CapsuleControllerData
{
	int layer;
	float slopeLimit;
	float stepOffset;
	float skinWidth;
	float minMoveDistance;
	float radius;
	float height;
	Vector3 position;
};

struct BoxColliderData
{
	//int id;
	int layer;
	Vector3 position;
	Vector4 rotation;
	float x_extents;
	float y_extents;
	float z_extents;
};

struct SphereColliderData
{
	//void* id;
	int layer;
	float radius;
	Vector3 position;
	Vector4 rotation;
};

struct CapsuleColliderData
{
	int layer;
	float radius;
	float halfHeight;
	Vector3 position;
	Vector4 rotation;
	int direction; //0£ºX-Axis 1£ºY-Axis 2£ºZ-Axis
};

struct MeshColliderData
{
	int layer;
	Vector3 position;
	Vector4 rotation;
	Vector3 scale;
	bool isReverseSide;
	int verticesNum;
	int trianglesNum;
};

typedef void(*OnSceneUpdateCallback)(class PhysicsScene* scene, PxRigidDynamic* p, Vector3* position, Vector4* rotation);

typedef void(*OnErrorCallback)(int type, const char* message);

typedef bool(*OnFilterCallback)(class PhysicsScene* scene, PxRigidDynamic* p1, const PxRigidActor* p2);

typedef void(*OnHitCallback)(class PhysicsScene* scene, PxRigidDynamic* p1, const PxRigidActor* p2);

class Common
{
public:
	Common();
	~Common();
};


