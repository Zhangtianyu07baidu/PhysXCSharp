#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial class PxSphereCollider
	{
		public PxSphereCollider(PhysicsScene scene, SphereColliderData data, bool isDynamic, string name, object userData) : 
			this(scene, data, isDynamic, userData)
		{
			this.Name = name;
		}

		public override void DrawGizmos()
		{
			Gizmos.DrawSphere(this.ColliderData.position, this.ColliderData.radius);
		}
	}
}


#endif