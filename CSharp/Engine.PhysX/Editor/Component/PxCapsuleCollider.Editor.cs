
#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial class PxCapsuleCollider
	{
		public PxCapsuleCollider(PhysicsScene scene, CapsuleColliderData data, bool isDynamic, string name, object userData = null) : 
			this(scene, data, isDynamic, userData)
		{
			this.Name = name;
		}

		public override void DrawGizmos()
		{
			PxVec4 drawRot = this.rotation;
			if (this.ColliderData.direction == 0)
			{
				//X-Axis
				PxVec4 rotationDelta = PxVec4.AngleAxis(90f, PxVec3.forward);
				drawRot *= rotationDelta;
			}
			else if (this.ColliderData.direction == 2)
			{
				//Z-Axis
				PxVec4 rotationDelta = PxVec4.AngleAxis(90f, PxVec3.right);
				drawRot *= rotationDelta;
			}

			GizmosUtil.DrawCapsule(this.position, drawRot, 
				this.ColliderData.halfHeight * 2 + this.ColliderData.radius * 2, 
				this.ColliderData.radius, Color.green, new Color());
		}
	}
}

#endif