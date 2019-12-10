using System;

namespace Engine.PhysX
{
	public partial class PhysicsScene
	{
		public bool Linecast(PxVec3 start, PxVec3 end)
		{
			return Raycast(start, end - start);
		}

		public bool Linecast(PxVec3 start, PxVec3 end, int layerMask)
		{
			return Raycast(start, end -start, layerMask);
		}

		public bool Linecast(PxVec3 start, PxVec3 end, out PxRaycastHit hitInfo)
		{
			return Raycast(start, end - start, out hitInfo);
		}

		public bool Linecast(PxVec3 start, PxVec3 end, out PxRaycastHit hitInfo, int layerMask)
		{
			return Raycast(start, end - start, out hitInfo, layerMask);
		}

		public bool Raycast(PxRay ray)
		{
			return Raycast(ray.origin, ray.direction);
		}

		public bool Raycast(PxRay ray, float maxDistance)
		{
			return Raycast(ray.origin, ray.direction, maxDistance);
		}

		public bool Raycast(PxRay ray, out PxRaycastHit hitInfo)
		{
			return Raycast(ray.origin, ray.direction, out hitInfo);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction)
		{
			return PhysXDll.Raycast(this.scenePtr, origin, direction, -1);
		}

		public bool Raycast(PxRay ray, float maxDistance, int layerMask)
		{
			return Raycast(ray.origin, ray.direction, maxDistance, layerMask);
		}

		public bool Raycast(PxRay ray, out PxRaycastHit hitInfo, float maxDistance)
		{
			return Raycast(ray.origin, ray.direction, out hitInfo, maxDistance);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, float maxDistance)
		{
			return PhysXDll.Raycast(this.scenePtr, origin, direction, maxDistance);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, int layerMask)
		{
			return Raycast(origin, direction, -1, layerMask);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, out PxRaycastHit hitInfo)
		{
			return Raycast(origin, direction, out hitInfo, -1);
		}

		public bool Raycast(PxRay ray, out PxRaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return Raycast(ray.origin, ray.direction, out hitInfo, maxDistance, layerMask);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, float maxDistance, int layerMask)
		{
			return PhysXDll.Raycast(this.scenePtr, origin, direction, maxDistance, layerMask);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance)
		{
			return PhysXDll.Raycast(this.scenePtr, origin, direction, maxDistance, out hitInfo);
		}

		public bool Raycast(PxVec3 origin, PxVec3 direction, out PxRaycastHit hitInfo, float maxDistance, int layerMask)
		{
			return PhysXDll.Raycast(this.scenePtr, origin, direction, maxDistance, layerMask, out hitInfo);
		}

		public int RaycastAll(PxRay ray)
		{
			return RaycastAll(ray.origin, ray.direction);
		}

		public int RaycastAll(PxRay ray, float maxDistance)
		{
			return RaycastAll(ray.origin, ray.direction, maxDistance);
		}

		public int RaycastAll(PxVec3 origin, PxVec3 direction)
		{
			return RaycastAll(origin, direction, -1);
		}

		public int RaycastAll(PxRay ray, float maxDistance, int layerMask)
		{
			return RaycastAll(ray.origin, ray.direction, maxDistance, layerMask);
		}

		public int RaycastAll(PxVec3 origin, PxVec3 direction, float maxDistance)
		{
			return PhysXDll.RaycastAll(this.scenePtr, origin, direction, maxDistance, this.RaycastHits);
		}

		public int RaycastAll(PxVec3 origin, PxVec3 direction, float maxDistance, int layerMask)
		{
			return PhysXDll.RaycastAll(this.scenePtr, origin, direction, maxDistance, layerMask, this.RaycastHits);
		}
	}
}
