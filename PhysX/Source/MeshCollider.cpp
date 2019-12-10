#include "stdafx.h"

MeshCollider::MeshCollider(MeshColliderData& data, PhysicsScene* scene, float* vertices, int* triangles) : Collider(scene)
{
	this->colliderData = data;

	for (int i = 0; i < data.verticesNum; i++)
	{
		const int index = i * 3;
		this->pxVec3Array.push_back(PxVec3(vertices[index], vertices[index + 1], vertices[index + 2]));
	}

	for (int i = 0; i < data.trianglesNum; i++)
	{
		this->intArray.push_back(triangles[i]);
	}
}

MeshCollider::~MeshCollider()
{
	
}

PxRigidActor* MeshCollider::CreateActor(PxPhysics* physics, PxMaterial* material, bool isDynamic)
{
	MeshColliderData& data = this->colliderData;

	int vertexCount = this->pxVec3Array.size();

	//world pos
	/*std::vector<PxVec3> convexVerts;
	for (int i = 0; i < vertexCount; ++i)
	{
		convexVerts.push_back(extPxVec3Array[i]);
	}*/

	int triCount = this->intArray.size();
	/*std::vector<int> indices32;
	for (int i = 0; i < triCount; ++i)
	{
		indices32.push_back(extIntArray[i]);
	}*/
	
	/*PxConvexMeshDesc convex_desc;
	convex_desc.points.count = vertexCount;
	convex_desc.points.stride = sizeof(PxVec3);
	convex_desc.points.data = &convexVerts[0];
	convex_desc.flags = PxConvexFlag::eCOMPUTE_CONVEX;

	PxDefaultMemoryOutputStream buf;
	if (!this->sceneManager->GetCooking()->cookConvexMesh(convex_desc, buf))
		return NULL;

	PxDefaultMemoryInputData input(buf.getData(), buf.getSize());
	PxConvexMesh *convex_mesh = physics->createConvexMesh(input);

	this->shape = physics->createShape(PxConvexMeshGeometry(convex_mesh), *material, true);*/

	

	PxTriangleMeshDesc meshDesc;
	meshDesc.points.count = vertexCount;
	meshDesc.points.stride = sizeof(PxVec3);
	meshDesc.points.data = &this->pxVec3Array[0];

	meshDesc.triangles.count = triCount / 3;
	meshDesc.triangles.stride = 3 * sizeof(PxU32);
	meshDesc.triangles.data = &this->intArray[0];
	if (data.isReverseSide)
	{
		meshDesc.flags = PxMeshFlag::eFLIPNORMALS;
	}

	PxDefaultMemoryOutputStream writeBuffer;
	bool status = PhysicsManager::GetInstance().GetCooking()->cookTriangleMesh(meshDesc, writeBuffer);
	if (!status)
		return NULL;

	PxDefaultMemoryInputData readBuffer(writeBuffer.getData(), writeBuffer.getSize());
	
	PxTransform localTrans(data.position.ToPxVec3(), data.rotation.ToPxQuat());
	
	/*if (isDynamic)
		this->actor = physics->createRigidDynamic(localTrans);
	else*/
		this->actor = physics->createRigidStatic(localTrans);
	
	PxTriangleMesh* triangleMesh = physics->createTriangleMesh(readBuffer);
	PxMeshScale scale(data.scale.ToPxVec3(), PxQuat(PxIdentity));
	this->actor->createShape(PxTriangleMeshGeometry(triangleMesh, scale), *material);
	triangleMesh->release();
	return this->actor;
}

ColliderType::Enum MeshCollider::GetType()
{
	return ColliderType::MESH;
}

/*void* MeshCollider::GetId()
{
	return this->colliderData.id;
}*/

int MeshCollider::GetLayer()
{
	return this->colliderData.layer;
}