using UnityEngine;

namespace _Project.Scripts.Utility
{
    [ExecuteAlways]
    public class ContinuousRotation : MonoBehaviour
    {
        public Vector3 rotationSpeed;
        public bool x, y, z;

        private void Update()
        {
            if (x)
                transform.Rotate(rotationSpeed.x * Time.deltaTime, 0, 0);
            if (y)
                transform.Rotate(0, rotationSpeed.y * Time.deltaTime, 0);
            if (z)
                transform.Rotate(0, 0, rotationSpeed.z * Time.deltaTime);
        }
    }
}