using System;

namespace Engine.PhysX
{
	public partial class PxBoxCollider : PxCollider
	{
		public BoxColliderData ColliderData { get; }

		public override int Layer => this.ColliderData.layer;

		public PxBoxCollider(PhysicsScene scene, BoxColliderData data, bool isDynamic, object userData = null) : base(scene, isDynamic, userData)
		{
			NLog.Log.Assert(data.IsValid);
			this.ColliderData = data;

			this.position = data.position;
			this.rotation = data.rotation;
			this.ptr = scene.AddBoxCollider(this);
		}

		public override void SetTransform(PxVec3 p, PxVec4 r)
		{
			Physics.SetActorTransform(this.ptr, p, r);
			base.SetTransform(p, r);
		}
	}
}
