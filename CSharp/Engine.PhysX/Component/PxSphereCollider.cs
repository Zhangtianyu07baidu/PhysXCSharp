using System;

namespace Engine.PhysX
{
	public partial class PxSphereCollider : PxCollider
	{
		public SphereColliderData ColliderData { get; }

		public override int Layer
		{
			get;
		}

		public PxSphereCollider(PhysicsScene scene, SphereColliderData data, bool isDynamic, object userData = null) : base(scene, isDynamic, userData)
		{
			NLog.Log.Assert(data.IsValid);
			this.ColliderData = data;
			this.Layer = data.layer;
			this.ptr = scene.AddSphereCollider(this);
		}

		public override void SetTransform(PxVec3 pos, PxVec4 rot)
		{
			Physics.SetActorTransform(this.Ptr, pos, rot);
			base.SetTransform(pos, rot);
		}
	}
}
