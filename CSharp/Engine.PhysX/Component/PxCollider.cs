using System;


namespace Engine.PhysX
{
	public abstract partial class PxCollider
	{
		protected PhysicsScene scene;
		public Action<PxVec3, PxVec4> OnTransUpdateAction { get; set; }

		protected IntPtr ptr = IntPtr.Zero;
		public IntPtr Ptr
		{
			get => this.ptr;
			protected set => this.ptr = value;
		}

		protected PxVec3 position;
		public virtual PxVec3 Position
		{
			get => this.position;
			set => this.position = value;
		}

		protected PxVec4 rotation;
		public PxVec4 Rotation
		{
			get => this.rotation;
			set => this.rotation = value;
		}

		public object UserData { get; private set; }

		public abstract int Layer { get; }

		public bool IsDynamic{ get; }

		/// <summary>
		/// 是否可能被碰撞查询到
		/// </summary>
		public bool CanQuery { get; protected set; }

		protected PxCollider(PhysicsScene scene, bool isDynamic, object userData = null)
		{
			this.scene = scene;
			this.IsDynamic = isDynamic;
			this.UserData = userData;
			this.Rotation = PxVec4.Identity;
			this.CanQuery = true;
		}

		public virtual void SetTransform(PxVec3 pos, PxVec4 rot)
		{
			this.Position = pos;
			this.Rotation = rot;
			this.OnTransUpdateAction?.Invoke(pos, rot);
		}

		/// <summary>
		/// 是否关闭场景查询碰撞
		/// </summary>
		public virtual void SetQueryEnabled(bool val)
		{
			this.CanQuery = val;
			Physics.SetShapeInSceneQueryEnabled(this.Ptr, val);
		}

		public virtual void SetGravityEnabled(bool val)
		{
			if(this.IsDynamic)
			{
				Physics.SetActorGravity(this.Ptr, val);
			}
		}

		public void Release()
		{
			this.scene.RemoveCollider(this.Ptr);
			this.OnTransUpdateAction = null;
			this.UserData = null;
		}
	}
}


