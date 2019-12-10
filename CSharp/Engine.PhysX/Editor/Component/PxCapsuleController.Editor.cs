#if UNITY_EDITOR

using System;
using UnityEngine;

namespace Engine.PhysX
{
	public partial class PxCapsuleController
	{
		public PxCapsuleController(PhysicsScene scene, CapsuleControllerData ColliderData, string name, object userData = null,
			ControllerFilterCallback filter = null, ControllerHitReportCallback hitReport = null)
			: this(scene, ColliderData, userData, filter, hitReport)
		{
			this.Name = name;
		}

		public override void DrawGizmos()
		{
			GizmosUtil.DrawCapsule(this.Position, this.rotation, 
				this.ControllerData.height + this.ControllerData.radius * 2 + this.ControllerData.skinWidth * 2, 
				this.ControllerData.radius + this.ControllerData.skinWidth, Color.green, new Color());
		}
	}
}

#endif