using System;


namespace Engine.PhysX
{
	public partial class PxMeshCollider : PxCollider
	{
		public MeshColliderData ColliderData { get; }

		public override int Layer
		{
			get;
		}

		public PxMeshCollider(PhysicsScene scene, MeshColliderData data) : base(scene, false)
		{
			NLog.Log.Assert(data.IsValid);
			this.ColliderData = data;
			this.Layer = data.layer;

			this.ptr = scene.AddMeshCollider(this);
			this.Position = data.position;
			this.Rotation = data.rotation;
		}

		public override void SetTransform(PxVec3 pos, PxVec4 rot)
		{
			Physics.SetActorTransform(this.Ptr, pos, rot);
			base.SetTransform(pos, rot);
		}
	}
}
