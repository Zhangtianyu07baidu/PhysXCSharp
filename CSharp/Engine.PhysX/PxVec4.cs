using System.Runtime.InteropServices;
using Engine.Base;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PxVec4
	{
		public static PxVec4 Identity = new PxVec4(0, 0, 0, 1);
		public static PxVec4 Zero = new PxVec4(0, 0, 0, 0);

		public float x;
		public float y;
		public float z;
		public float w;

		public PxVec4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		public static PxVec4 AngleAxis(float degree, PxVec3 axis)
		{
			if (axis.magnitude < 0.000001f)
			{
				return Identity;
			}

			axis = axis.normalized;
			var cos = Mathf.Cos(Mathf.Deg2Rad(degree) / 2);
			var sin = Mathf.Sin(Mathf.Deg2Rad(degree) / 2);

			return new PxVec4(sin * axis.x, sin * axis.y, sin * axis.z, cos);
		}

		public static PxVec4 operator *(PxVec4 lhs, PxVec4 rhs)
		{
			return new PxVec4(
				lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y,
				lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z,
				lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x,
				lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		public static bool operator == (PxVec4 c1, PxVec4 c2)
		{
			return c1.Equals(c2);
		}

		public static bool operator != (PxVec4 c1, PxVec4 c2)
		{
			return !(c1 == c2);
		}

		public bool Equals(PxVec4 other)
		{
			return w.EqualsEx(other.w) && x.EqualsEx(other.x) && y.EqualsEx(other.y) && z.EqualsEx(other.z);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}
			return obj is PxVec4 && this.Equals((PxVec4)obj);
		}

		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = w.GetHashCode();
				hashCode = (hashCode * 397) ^ x.GetHashCode();
				hashCode = (hashCode * 397) ^ y.GetHashCode();
				hashCode = (hashCode * 397) ^ z.GetHashCode();
				return hashCode;
			}
		}

		public static implicit operator PxVec4(UnityEngine.Quaternion q)
		{
			return new PxVec4(q.x, q.y, q.z, q.w);
		}

		public static implicit operator UnityEngine.Quaternion(PxVec4 vec4)
		{
			return new UnityEngine.Quaternion(vec4.x, vec4.y, vec4.z, vec4.w);
		}
	}
}
