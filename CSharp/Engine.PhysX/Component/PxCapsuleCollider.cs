using System;

namespace Engine.PhysX
{
	public partial class PxCapsuleCollider : PxCollider
	{
		public CapsuleColliderData ColliderData { get; }

		public override int Layer => this.ColliderData.layer;

		public PxCapsuleCollider(PhysicsScene scene, CapsuleColliderData data, bool isDynamic, object userData = null) : base(scene, isDynamic, userData)
		{
			NLog.Log.Assert(data.IsValid);
			this.ColliderData = data;

			this.position = data.position;
			this.rotation = data.rotation;
			this.ptr = scene.AddCapsuleCollider(this);
		}

		public override void SetTransform(PxVec3 p, PxVec4 r)
		{
			Physics.SetActorTransform(this.ptr, p, r);
			base.SetTransform(p, r);
		}
	}
}
