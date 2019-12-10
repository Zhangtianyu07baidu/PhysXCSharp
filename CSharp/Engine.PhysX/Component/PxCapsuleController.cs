using System;

namespace Engine.PhysX
{
	[Flags]
	public enum PxCapsuleClimbingMode : byte
	{
		/// <summary>
		/// 不受步进偏移值的约束。他们可能会爬过高于此值的障碍物
		/// </summary>
		EASY = 0,

		/// <summary>
		/// 确保胶囊不能越过高于步进偏移的障碍物
		/// </summary>
		CONSTRAINED = 1,
	}

	public partial class PxCapsuleController : PxCollider
	{
		public CapsuleControllerData ControllerData;
		private bool useGravity = true;

		public ControllerFilterCallback Filter { get; }

		public ControllerHitReportCallback HitReport { get; }

		public override int Layer => this.ControllerData.layer;

		public override PxVec3 Position
		{
			get
			{
				if (this.Ptr != IntPtr.Zero)
				{
					PhysXDll.GetControllerPosition(this.Ptr, ref this.position);
				}
				else
				{
					NLog.Log.Warn("PxCapsuleController Ptr is zero!");
				}
				return this.position;
			}
			set
			{
				if (this.Ptr != IntPtr.Zero)
				{
					PhysXDll.SetControllerPosition(this.Ptr, value);
				}
				else
				{
					NLog.Log.Warn("PxCapsuleController Ptr is zero!");
				}
			}
		}

		private PxVec3 footPosition;
		public PxVec3 FootPosition
		{
			get => this.footPosition;
			set
			{
				this.footPosition = value;
				PhysXDll.SetControllerFootPosition(this.ptr, this.footPosition);
			}
		}

		public PxCapsuleClimbingMode ClimbingMode { get; } = PxCapsuleClimbingMode.CONSTRAINED;

		public bool IsFilterEnabled
		{
			get;
			private set;
		}

		public PxCapsuleController(PhysicsScene scene, CapsuleControllerData data, object userData = null, 
			ControllerFilterCallback filter = null, ControllerHitReportCallback hitReport = null)
			: base(scene, true, userData)
		{
			NLog.Log.Assert(data.IsValid);
			this.ControllerData = data;
			this.Filter = filter;
			this.HitReport = hitReport;
			this.ptr = scene.AddCharacterController(this);
			this.position = data.position;
			this.IsFilterEnabled = true;
		}

		public override void SetTransform(PxVec3 pos, PxVec4 rot)
		{
			PhysXDll.SetControllerPosition(this.Ptr, pos);
			base.SetTransform(pos, rot);
		}

		public CollisionFlags Move(PxVec3 motion)
		{
			CollisionFlags flag = Physics.Move(this.Ptr, motion, ref this.footPosition);
			return flag;
		}

		public void SetRadius(float radius)
		{
			this.ControllerData.radius = radius;
			NLog.Log.Assert(this.ControllerData.IsValid);
			PhysXDll.SetControllerRadius(this.Ptr, radius);
		}

		public void SetHeight(float height)
		{
			this.ControllerData.height = height;
			NLog.Log.Assert(this.ControllerData.IsValid);
			PhysXDll.SetControllerHeight(this.Ptr, this.ControllerData.height);
		}

		/// <summary>
		/// 用来设置过滤器CollideFilter和IgnoreControllerLayerCollision的开关
		/// </summary>
		public void SetFilterEnabled(bool enabled)
		{
			this.IsFilterEnabled = enabled;
			Physics.SetFilterEnabled(this.Ptr, enabled);
		}

		public override void SetQueryEnabled(bool val)
		{
			this.CanQuery = val;
		}

		/// <summary>
		/// controller的重力不受物理引擎控制，需要自己实现
		/// </summary>
		public override void SetGravityEnabled(bool val)
		{
			this.useGravity = val;
		}
	}
}
