using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class KartWheel : MonoBehaviour
    {
        public GameObject wheelObject;
        public WheelCollider wheelCollider;
        public KartWheelType wheelType;

        private KartMovementController _kartMovementController;

        private void Awake()
        {
            _kartMovementController = GetComponentInParent<KartMovementController>();
        }
        
        public void Move(float movementInput, float topSpeed)
        {
            wheelCollider.motorTorque = movementInput * topSpeed * 750 * Time.deltaTime;
            // Reduce motor torque when kart is turning.
            wheelCollider.motorTorque *= 1 - Mathf.Abs(wheelCollider.steerAngle) / 30;
        }

        public void Turn(float turnInput, float turnSensitivity, float maxTurnAngle)
        {
            if (wheelType != KartWheelType.Front) return;
            var turnAngle = turnInput * turnSensitivity * maxTurnAngle;
            wheelCollider.steerAngle = Mathf.Lerp(wheelCollider.steerAngle, turnAngle, Time.deltaTime*2);
        }

        public void Brake(bool isBrakePressed)
        {
            if (isBrakePressed)
            {
                wheelCollider.brakeTorque = _kartMovementController.BrakeDeceleration * 300 * Time.deltaTime;
            }
            else
            {
                wheelCollider.brakeTorque = 0;
            }
        }

        private void Update()
        {
            wheelCollider.GetWorldPose(out var position, out var rotation);
            wheelObject.transform.position = position;
            wheelObject.transform.rotation = rotation;
        }
    }
}