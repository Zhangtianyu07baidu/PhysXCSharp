
using System;
using System.Collections.Generic;
using Engine.Base;


namespace Engine.PhysX
{
	public partial class PhysicsScene
	{
		private readonly IntPtr scenePtr;

		public Dictionary<IntPtr, PxCollider> Colliders { get; } = new Dictionary<IntPtr, PxCollider>(IntPtrEqualityComparer.Instance);

		private const int MAX_HIT = 20;
		public PxRaycastHit[] RaycastHits { get; } = new PxRaycastHit[MAX_HIT];

		public PhysicsScene(IntPtr scenePtr)
		{
			this.scenePtr = scenePtr;
			for (int i = 0; i < MAX_HIT; i++)
			{
				this.RaycastHits[i] = new PxRaycastHit();
			}
		}

		public IntPtr AddBoxCollider(PxBoxCollider box)
		{
			IntPtr boxPtr = PhysXDll.AddBoxCollider(this.scenePtr, box.ColliderData, box.IsDynamic);
			this.Colliders.Add(boxPtr, box);
			return boxPtr;
		}

		public IntPtr AddCapsuleCollider(PxCapsuleCollider capsule)
		{
			IntPtr capsulePtr = PhysXDll.AddCapsuleCollider(this.scenePtr, capsule.ColliderData, capsule.IsDynamic);
			this.Colliders.Add(capsulePtr, capsule);
			return capsulePtr;
		}

		public IntPtr AddCharacterController(PxCapsuleController controller)
		{
			IntPtr controllerPtr = Physics.AddCharacterController(this.scenePtr, controller);
			this.Colliders.Add(controllerPtr, controller);
			return controllerPtr;
		}

		public IntPtr AddMeshCollider(PxMeshCollider mesh)
		{
			IntPtr meshPtr = PhysXDll.AddMeshCollider(this.scenePtr, mesh.ColliderData, mesh.ColliderData.vertices, mesh.ColliderData.triangles);
			this.Colliders.Add(meshPtr, mesh);
			return meshPtr;
		}

		public IntPtr AddSphereCollider(PxSphereCollider sphere)
		{
			IntPtr spherePtr = PhysXDll.AddSphereCollider(this.scenePtr, sphere.ColliderData, sphere.IsDynamic);
			this.Colliders.Add(spherePtr, sphere);
			return spherePtr;
		}

		public PxCollider GetCollider(IntPtr colliderPtr)
		{
			NLog.Log.Assert(this.Colliders.TryGetValue(colliderPtr, out PxCollider collider));
			return collider;
		}

		public void RemoveCollider(IntPtr colliderPtr)
		{
			NLog.Log.Assert(this.Colliders.Remove(colliderPtr));
			PhysXDll.RemoveCollider(this.scenePtr, colliderPtr);
		}

		public void Update(float deltaTime)
		{
			PhysXDll.Update(this.scenePtr, deltaTime);
		}

		public bool OnFilterWorld(IntPtr p1, IntPtr p2)
		{
			this.Colliders.TryGetValue(p1, out PxCollider collider1);
			this.Colliders.TryGetValue(p2, out PxCollider collider2);
			PxCapsuleController controller1 = collider1 as PxCapsuleController;
			if (controller1.IsFilterEnabled && controller1.Filter != null)
			{
				return controller1.Filter.OnFilterWorld(collider2);
			}
			return true;
		}

		public bool OnFilterController(IntPtr p1, IntPtr p2)
		{
			this.Colliders.TryGetValue(p1, out PxCollider collider1);
			this.Colliders.TryGetValue(p2, out PxCollider collider2);
			PxCapsuleController controller1 = collider1 as PxCapsuleController;
			PxCapsuleController controller2 = collider2 as PxCapsuleController;

			if (!controller2.CanQuery)
				return false;

			if (controller1.IsFilterEnabled && controller1.Filter != null)
			{
				return controller1.Filter.OnFilterController(controller2);
			}
			return true;
		}

		public void OnHitShape(IntPtr controllerPtr, IntPtr colliderPtr)
		{
			NLog.Log.Assert(this.Colliders.TryGetValue(controllerPtr, out PxCollider collider));
			NLog.Log.Assert(this.Colliders.TryGetValue(colliderPtr, out PxCollider targetCollider));
			PxCapsuleController controller = collider as PxCapsuleController;
			controller.HitReport.OnHitShape(targetCollider);
		}

		public void OnHitController(IntPtr controllerPtr, IntPtr colliderPtr)
		{
			NLog.Log.Assert(this.Colliders.TryGetValue(controllerPtr, out PxCollider collider));
			NLog.Log.Assert(this.Colliders.TryGetValue(colliderPtr, out PxCollider targetCollider));
			PxCapsuleController controller = collider as PxCapsuleController;
			PxCapsuleController targetController = targetCollider as PxCapsuleController;
			controller.HitReport.OnHitController(targetController);
		}

		public void OnUpdate(IntPtr colliderPtr, PxVec3 position, PxVec4 rotation)
		{
			if (this.Colliders.TryGetValue(colliderPtr, out PxCollider collider))
			{
				collider.Position = position;
				collider.Rotation = rotation;
				collider.OnTransUpdateAction?.Invoke(position, rotation);
			}
		}

		public void Release()
		{
			this.Colliders.Clear();
			Physics.ReleaseScene(this.scenePtr);
		}
	}
}
