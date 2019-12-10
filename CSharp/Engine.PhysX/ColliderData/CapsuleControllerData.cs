using System.Runtime.InteropServices;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public partial struct CapsuleControllerData : IColliderData
	{
		public int layer;

		public float slopeLimit;

		/// <summary>
		/// 可攀爬的高度
		/// </summary>
		public float stepOffset;

		public float skinWidth;

		public float minMoveDistance;

		public float radius;

		public float height;

		public PxVec3 position;

		public bool IsValid
		{
			get
			{
				if (this.slopeLimit < 0f)
				{
					NLog.Log.Error("CapsuleControllerData slopeLimit is less then zero!");
					return false;
				}

				if (this.stepOffset < 0f)
				{
					NLog.Log.Error("CapsuleControllerData stepOffset is less then zero!");
					return false;
				}

				if (this.skinWidth < 0f)
				{
					NLog.Log.Error("CapsuleControllerData skinWidth is less then zero!");
					return false;
				}

				if (this.radius < 0f)
				{
					NLog.Log.Error("CapsuleControllerData radius is less then zero!");
					return false;
				}

				if (this.height < 0f)
				{
					NLog.Log.Error("CapsuleControllerData height is less then zero!");
					return false;
				}

				if (this.stepOffset > this.height + this.radius * 2.0f)
				{
					NLog.Log.Error("CapsuleControllerData stepOffset is invalid!");
					return false;
				}

				return true;
			}
		}

	}
}
