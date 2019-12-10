using System;
using System.Runtime.InteropServices;
using Engine.Base;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PxVec3
	{
		public float x;
		public float y;
		public float z;

		public static readonly PxVec3 zero;
		public static readonly PxVec3 one = new PxVec3(1f, 1f, 1f);
		/// <summary>
		/// PxVec3(0, 0, 1)
		/// </summary>
		public static readonly PxVec3 forward = new PxVec3(0f, 0f, 1f);
		public static readonly PxVec3 back = new PxVec3(0f, 0f, -1f);
		public static readonly PxVec3 right = new PxVec3(1f, 0f, 0f);
		public static readonly PxVec3 left = new PxVec3(-1f, 0f, 0f);
		/// <summary>
		/// PxVec3(0, 1, 0)
		/// </summary>
		public static readonly PxVec3 up = new PxVec3(0f, 1f, 0f);
		/// <summary>
		/// PxVec3(0, -1, 0)
		/// </summary>
		public static readonly PxVec3 down = new PxVec3(0f, -1f, 0f);

		public PxVec3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}
		public PxVec3 normalized
		{
			get
			{
				return Normalize(this);
			}
		}

		public float magnitude
		{
			get
			{
				return (float)Math.Sqrt(x * x + y * y + z * z);
			}
		}

		public PxVec3 Abs => new PxVec3(Mathf.Abs(this.x), Mathf.Abs(this.y), Mathf.Abs(this.z));

		public float MaxElement => Mathf.Max(x, Mathf.Max(y, z));

		public float MinElement => Mathf.Min(x, Mathf.Min(y, z));

		public override string ToString()
		{
			return string.Format("({0:F3}, {1:F3}, {2:F3})", x, y, z);
		}

		public static PxVec3 Normalize(PxVec3 value)
		{
			var num = value.magnitude;
			if (num > 9.99999974737875E-06)
			{
				return value / num;
			}
			return zero;
		}

		public bool IsFinite()
		{
			return this.x.IsFinite() && this.y.IsFinite() && this.z.IsFinite();
		}

		public static PxVec3 operator -(PxVec3 c)
		{
			return new PxVec3(-c.x, -c.y, -c.z);
		}

		public static PxVec3 operator +(PxVec3 c1, PxVec3 c2)
		{
			return new PxVec3(c1.x + c2.x, c1.y + c2.y, c1.z + c2.z);
		}

		public static PxVec3 operator -(PxVec3 c1, PxVec3 c2)
		{
			return new PxVec3(c1.x - c2.x, c1.y - c2.y, c1.z - c2.z);
		}

		public static PxVec3 operator *(PxVec3 c1, float f)
		{
			return new PxVec3(c1.x * f, c1.y * f, c1.z * f);
		}

		public static PxVec3 operator *(float f, PxVec3 c1)
		{
			return new PxVec3(c1.x * f, c1.y * f, c1.z * f);
		}

		public static PxVec3 operator /(PxVec3 c1, float f)
		{
			return new PxVec3(c1.x / f, c1.y / f, c1.z / f);
		}

		public static implicit operator PxVec3(Engine.Base.Vector3 vec)
		{
			return new PxVec3(vec.x, vec.y, vec.z);
		}

		public static implicit operator Engine.Base.Vector3(PxVec3 px)
		{
			return new Engine.Base.Vector3(px.x, px.y, px.z);
		}

		public static implicit operator PxVec3(UnityEngine.Vector3 vec)
		{
			return new PxVec3(vec.x, vec.y, vec.z);
		}

		public static implicit operator UnityEngine.Vector3(PxVec3 px)
		{
			return new UnityEngine.Vector3(px.x, px.y, px.z);
		}
	}
}
