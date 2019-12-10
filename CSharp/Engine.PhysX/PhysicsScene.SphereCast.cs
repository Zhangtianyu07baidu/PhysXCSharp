using System;

namespace Engine.PhysX
{
	public partial class PhysicsScene
	{
		public bool SphereCast(PxRay ray, float radius)
		{
			return SphereCast(ray.origin, ray.direction, radius);
		}

		public bool SphereCast(PxVec3 origin, PxVec3 direction, float radius)
		{
			return PhysXDll.SphereCast(this.scenePtr, origin, direction, radius, -1);
		}

		public bool SphereCast(PxRay ray, float radius, out PxRaycastHit hitInfo)
		{
			return SphereCast(ray.origin, radius, ray.direction, out hitInfo);
		}

		public bool SphereCast(PxRay ray, float radius, float maxDistance, int layerMask)
		{
			return SphereCast(ray.origin, radius, ray.direction, maxDistance, layerMask);
		}

		public bool SphereCast(PxRay ray, float radius, out PxRaycastHit hitInfo, float maxDistance)
		{
			return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance);
		}
		
		public bool SphereCast(PxVec3 origin, float radius, PxVec3 direction, out PxRaycastHit hitInfo)
		{
			return PhysXDll.SphereCast(this.scenePtr, origin, direction, radius, -1, out hitInfo);
		}

		public bool SphereCast(PxVec3 origin, float radius, PxVec3 direction, float maxDistance, int layerMask)
		{
			return PhysXDll.SphereCast(this.scenePtr, origin, direction, radius, maxDistance, layerMask);
		}

		public bool SphereCast(PxRay ray, float radius, out PxRaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return SphereCast(ray.origin, radius, ray.direction, out hitInfo, maxDistance, layerMask);
		}

		public bool SphereCast(PxVec3 origin, float radius, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance)
		{
			return PhysXDll.SphereCast(this.scenePtr, origin, direction, radius, maxDistance, out hitInfo);
		}

		public bool SphereCast(PxVec3 origin, float radius, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return PhysXDll.SphereCast(this.scenePtr, origin, direction, radius, maxDistance, layerMask, out hitInfo);
		}

		public int SphereCastAll(PxRay ray, float radius)
		{
			return SphereCastAll(ray.origin, radius, ray.direction);
		}

		public int SphereCastAll(PxRay ray, float radius, float maxDistance)
		{
			return SphereCastAll(ray.origin, radius, ray.direction, maxDistance);
		}

		public int SphereCastAll(PxVec3 origin, float radius, PxVec3 direction)
		{
			return SphereCastAll(origin, radius, direction, -1);
		}

		public int SphereCastAll(PxRay ray, float radius, float maxDistance, int layerMask)
		{
			return SphereCastAll(ray.origin, radius, ray.direction, maxDistance, layerMask);
		}

		public int SphereCastAll(PxVec3 origin, float radius, PxVec3 direction, float maxDistance)
		{
			return PhysXDll.SphereCastAll(this.scenePtr, origin, direction, radius, maxDistance, this.RaycastHits);
		}

		public int SphereCastAll(PxVec3 origin, float radius, PxVec3 direction, float maxDistance,
			int layerMask)
		{
			return PhysXDll.SphereCastAll(this.scenePtr, origin, direction, radius, maxDistance, layerMask, this.RaycastHits);
		}
	}
}
