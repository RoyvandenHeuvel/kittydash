using UnityEngine;

namespace Assets.Scripts
{
    public static class Vector3Utilities
    {
        public static string ToString(this Vector3 vector3)
        {
            return string.Format("Value of X: {0}, value of Y: {1}, value of Z: {2}", vector3.x, vector3.y, vector3.z);
        }

        public static Vector3 SetZ(this Vector3 vector3, float value)
        {
            vector3.z = value;
            return vector3;
        }
    }
}
