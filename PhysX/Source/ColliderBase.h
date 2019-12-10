#pragma once
class PhysicsScene;

class ColliderBase
{
public:
	ColliderBase(PhysicsScene* scene);
	virtual ~ColliderBase();

	PhysicsScene* GetScene() const;

	virtual int GetLayer() = 0;
	virtual void Release() = 0;
	virtual ColliderType::Enum GetType() = 0;

protected:
	PhysicsScene* scene;
};

