using System;
using UnityEngine;

namespace _Project.Scripts
{
    /// <summary>
    /// Provide Gyroscopic Event.
    /// </summary>
    [DefaultExecutionOrder(-1)]
    public class GyroscopeHandler : MonoBehaviour
    {
        private bool _gyroscopeSupported;
        public static event Action<Vector3> OnGyroscopeUpdate;

        private void Awake()
        {
            _gyroscopeSupported = SystemInfo.supportsGyroscope;
            if (!_gyroscopeSupported)
            {
                Debug.LogError("Gyroscope is not supported on this device.");
            }
        }

        private void Start()
        {
            if (_gyroscopeSupported)
            {
                Input.gyro.enabled = true;
            }
        }

        private void Update()
        {
            if (_gyroscopeSupported)
            {
                OnGyroscopeUpdate?.Invoke(Input.gyro.attitude.eulerAngles);
            }
        }


        public static float GetTilt()
        {
            var xTilt = Input.acceleration.x;
            var deadArea = new Vector2(-0.15f, 0.15f);
            if (xTilt < deadArea.x || xTilt > deadArea.y)
            {
                xTilt = 0;
            }

            return xTilt;
        }
    }
}