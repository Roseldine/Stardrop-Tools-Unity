
using UnityEngine;

namespace StardropTools
{
    public class TransformObject : CoreObject
    {
        // Position
        public float PosX { get => Position.x; set => Position.SetX(value); }
        public float PosY { get => Position.y; set => Position.SetY(value); }
        public float PosZ { get => Position.z; set => Position.SetZ(value); }

        public Vector2 PosXY { get => Position.GetXY(); set => Position.SetXY(value.x, value.y); }
        public Vector2 PosXZ { get => Position.GetXZ(); set => Position.SetXZ(value.x, value.y); }
        public Vector2 PosYZ { get => Position.GetYZ(); set => Position.SetYZ(value.x, value.y); }


        // Rotation
        public float RotX { get => Rotation.x; }
        public float RotY { get => Rotation.y; }
        public float RotZ { get => Rotation.z; }
        public float RotW { get => Rotation.w; }

        public float LocalRotX { get => LocalRotation.x; }
        public float LocalRotY { get => LocalRotation.y; }
        public float LocalRotZ { get => LocalRotation.z; }
        public float LocalRotW { get => LocalRotation.w; }

        public float EulerRotX { get => EulerRotation.x; set => EulerRotation.SetX(value); }
        public float EulerRotY { get => EulerRotation.y; set => EulerRotation.SetY(value); }
        public float EulerRotZ { get => EulerRotation.z; set => EulerRotation.SetZ(value); }

        public float LocalEulerRotX { get => LocalRotation.x; set => EulerRotation.SetX(value); }
        public float LocalEulerRotY { get => LocalRotation.y; set => EulerRotation.SetY(value); }
        public float LocalEulerRotZ { get => LocalRotation.z; set => EulerRotation.SetZ(value); }


        // Scale
        public float ScaleX { get => Scale.x; set => Scale.SetX(value); }
        public float ScaleY { get => Scale.y; set => Scale.SetY(value); }
        public float ScaleZ { get => Scale.z; set => Scale.SetZ(value); }

        // position
        public void SetPosition(Vector3 position)
            => Position = position;

        public void SetLocalPosition(Vector3 localPosition)
            => LocalPosition = localPosition;


        // Rotation
        public void SetRotation(Quaternion rotation)
            => Rotation = rotation;

        public void SetLocalRotation(Quaternion localRotation)
            => LocalRotation = localRotation;

        public void SetRotation(Vector3 rotation)
            => EulerRotation = rotation;
        public void SetLocalRotation(Vector3 rotation)
            => LocalEulerRotation = rotation;


        // scale
        public void SetScale(Vector3 scale)
            => Scale = scale;


        public Vector3 DirectionTo(Vector3 target) => target - Position;
        public Vector3 DirectionTo(Transform target) => target.position - Position;

        public Vector3 DirectionFrom(Vector3 target) => Position - target;
        public Vector3 DirectionFrom(Transform target) => Position - target.position;


        public float DistanceFrom(Vector3 target) => DirectionTo(target).magnitude;
        public float DistanceFrom(Transform target) => DirectionTo(target).magnitude;


        public Quaternion SmoothLookAt(Vector3 direction, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            if (direction == Vector3.zero)
                return Quaternion.identity;

            Quaternion lookRot = Quaternion.LookRotation(direction);
            Quaternion targetRot = Quaternion.Slerp(Rotation, lookRot, Time.deltaTime * lookSpeed);

            if (lockX) lookRot.x = 0;
            if (lockY) lookRot.y = 0;
            if (lockZ) lookRot.z = 0;

            SetRotation(targetRot);

            return targetRot;
        }

        public Quaternion SmoothLookAt(Transform target, float lookSpeed, bool lockX = false, bool lockY = true, bool lockZ = false)
        {
            Vector3 lookDir = DirectionTo(target.position);
            Quaternion targetRot = SmoothLookAt(lookDir, lookSpeed, lockX, lockY, lockZ);

            return targetRot;
        }
    }
}
