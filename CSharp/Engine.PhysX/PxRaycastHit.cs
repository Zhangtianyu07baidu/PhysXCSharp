using System;
using System.Runtime.InteropServices;

namespace Engine.PhysX
{
	[StructLayout(LayoutKind.Sequential)]
	public struct PxRaycastHit
	{
		public static PxRaycastHit Empty = default(PxRaycastHit);
		public IntPtr ptr;
		public int layer;
		public PxVec3 point;
		public PxVec3 normal;
		public float distance;
	}
}
