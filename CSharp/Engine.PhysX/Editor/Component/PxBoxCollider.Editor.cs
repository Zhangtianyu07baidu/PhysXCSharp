#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial class PxBoxCollider
	{
		public PxBoxCollider(PhysicsScene scene, BoxColliderData data, bool isDynamic, string name, object userData = null) : 
			this(scene, data, isDynamic, userData)
		{
			this.Name = name;
		}

		public override void DrawGizmos()
		{
			GizmosUtil.DrawCube(this.position, this.rotation, new Vector3(this.ColliderData.x_extents, 
				this.ColliderData.y_extents, 
				this.ColliderData.z_extents), Color.green, new Color());
		}
	}
}

#endif