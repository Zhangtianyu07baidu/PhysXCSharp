using System;
using System.Collections.Generic;
using Engine.Base;


namespace Engine.PhysX
{
	/// <summary>
	///	CollisionFlags is a bitmask returned by CharacterController.Move.
	/// </summary>
	[Flags]
	public enum CollisionFlags
	{
		NONE = 0,
		COLLIDED_SIDES = 1,
		COLLIDED_ABOVE = 2,
		COLLIDED_BELOW = 4
	}

	[Flags]
	public enum CapsuleAxis
	{
		X_AXIS = 0,
		Y_AXIS = 1,
		Z_AXIS = 2
	}

	public static partial class Physics
	{
		[Flags]
		public enum ErrorCode
		{
			ERROR = 0,
			INFO = 1,
			WARNING = 2
		}

		private static bool isInited;
		private static readonly Dictionary<IntPtr, PhysicsScene> scenes = new Dictionary<IntPtr, PhysicsScene>(IntPtrEqualityComparer.Instance);

		// 先创建Delegate并引用，否则调试时会报Delegate被回收的错误
		private static readonly PhysXDll.OnFilterCallback onFilterController = OnFilterController;
		private static readonly PhysXDll.OnFilterCallback onFilterWorld = OnFilterWorld;
		private static readonly PhysXDll.OnHitCallback onHitShape = OnHitShape;
		private static readonly PhysXDll.OnHitCallback onHitController = OnHitController;
		private static readonly PhysXDll.OnSceneUpdateCallback onSceneUpdate = OnSceneUpdate;
		private static readonly PhysXDll.OnErrorCallback onErrorOutput = OnErrorOutput;

		public static void Initialize()
		{
			if (!isInited)
			{
				PhysXDll.SetErrorCallback(onErrorOutput);
				PhysXDll.Initialize();
				PhysXDll.RegistSceneUpdateCallback(onSceneUpdate);
				isInited = true;
			}
		}

		public static PhysicsScene CreateScene()
		{
			IntPtr p = PhysXDll.CreateScene();
			PhysicsScene scene = new PhysicsScene(p);
			scenes[p] = scene;
			return scene;
		}

		public static PhysicsScene GetScene(IntPtr scenePtr)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			return scene;
		}

		public static PxVec3 GetControllerPosition(IntPtr p)
		{
			var position = new PxVec3();
			PhysXDll.GetControllerPosition(p, ref position);
			return position;
		}

		public static CollisionFlags Move(IntPtr p, PxVec3 motion, ref PxVec3 footPos)
		{
			return (CollisionFlags)PhysXDll.MoveController(p, motion, ref footPos);
		}

		public static void IgnoreControllerLayerCollision(int layer1, int layer2, bool ignore = true)
		{
			PhysXDll.IgnoreControllerLayerCollision(layer1, layer2, ignore);
		}

		public static void SetFilterEnabled(IntPtr p, bool enabled)
		{
			PhysXDll.SetFilterEnabled(p, enabled);
		}

		public static void SetActorTransform(IntPtr p, PxVec3 position, PxVec4 rotation)
		{
			PhysXDll.SetActorTransform(p, position, rotation);
		}

		public static void SetActorGravity(IntPtr p, bool useGravity)
		{
			PhysXDll.SetActorGravity(p, useGravity);
		}

		public static void SetShapeInSceneQueryEnabled(IntPtr p, bool enabled)
		{
			PhysXDll.SetShapeInSceneQueryEnabled(p, enabled);
		}

		public static void ReleaseScene(IntPtr scenePtr)
		{
			NLog.Log.Assert(scenes.Remove(scenePtr));
			PhysXDll.ReleaseScene(scenePtr);
		}

		public static IntPtr AddCharacterController(IntPtr scene, PxCapsuleController controller)
		{
			PhysXDll.OnFilterCallback filterWorld = null;
			PhysXDll.OnFilterCallback filterController = null;

			PhysXDll.OnHitCallback hitShape = null;
			PhysXDll.OnHitCallback hitController = null;

			if (controller.Filter != null)
			{
				filterWorld = onFilterWorld;
				filterController = onFilterController;
			}

			if (controller.HitReport != null)
			{
				hitShape = onHitShape;
				hitController = onHitController;
			}

			return PhysXDll.AddCharacterController(scene, controller.ControllerData, 
				filterWorld, filterController, hitShape, hitController);
		}

		public static void Release()
		{
			if(isInited)
			{
				PhysXDll.Release();
			}
			isInited = false;
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnFilterCallback))]
#endif
		private static bool OnFilterWorld(IntPtr scenePtr, IntPtr p1, IntPtr p2)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			return scene.OnFilterWorld(p1, p2);
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnFilterCallback))]
#endif
		private static bool OnFilterController(IntPtr scenePtr, IntPtr p1, IntPtr p2)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			return scene.OnFilterController(p1, p2);
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnHitCallback))]
#endif
		private static void OnHitShape(IntPtr scenePtr, IntPtr controllerPtr, IntPtr colliderPtr)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			scene.OnHitShape(controllerPtr, colliderPtr);
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnHitCallback))]
#endif
		private static void OnHitController(IntPtr scenePtr, IntPtr controllerPtr, IntPtr colliderPtr)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			scene.OnHitController(controllerPtr, colliderPtr);
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnSceneUpdateCallback))]
#endif
		private static void OnSceneUpdate(IntPtr scenePtr, IntPtr colliderPtr, PxVec3 position, PxVec4 rotation)
		{
			NLog.Log.Assert(scenes.TryGetValue(scenePtr, out PhysicsScene scene));
			scene.OnUpdate(colliderPtr, position, rotation);
		}

#if !FIGHT_SERVER
		[AOT.MonoPInvokeCallback(typeof(PhysXDll.OnErrorCallback))]
#endif
		private static void OnErrorOutput(int type, string message)
		{
			switch ((ErrorCode)type)
			{
				case ErrorCode.INFO:
					NLog.Log.Info("PhysX: " + message);
					break;
				case ErrorCode.WARNING:
					NLog.Log.Warn("PhysX: " + message);
					break;
				case ErrorCode.ERROR:
					NLog.Log.Error("PhysX: " + message);
					break;
				default:
					NLog.Log.Error("No Error Code in PhysX " + type);
					break;
			}
		}

		/*private static T[] GetArray<T>(int length, Action<IntPtr> action)
		{
			var array = new T[length];
			var size = Marshal.SizeOf(typeof(T)) * length;
			var ptr = Marshal.AllocHGlobal(size);
			action(ptr);
			for(var i = 0; i < length; i++)
			{
				var p = new IntPtr(ptr.ToInt64() + Marshal.SizeOf(typeof(T)) * i);
				array[i] = (T)Marshal.PtrToStructure(p, typeof(T));
			}
			Marshal.FreeHGlobal(ptr); // 释放内存  
			return array;
		}*/
	}
}