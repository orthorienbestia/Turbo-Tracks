using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class KartMovementController : MonoBehaviour
    {
        [SerializeField] private float topSpeed = 30f;
        [SerializeField] private float brakeDeceleration = 50f;
        public float BrakeDeceleration => brakeDeceleration;

        [SerializeField] private List<KartWheel> wheels;

        private Rigidbody _rigidbody;
        private float _movementInput;
        private float _turnInput;

        public float turnSensitivity = 1f;
        public float maxTurnAngle = 30f;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = Vector3.zero;
        }

        private void Update()
        {
            GetInput();
        }

        private void LateUpdate()
        {
            Move();
            Turn();
            Brake();
        }

        private void GetInput()
        {
            _movementInput = Input.GetAxis("Vertical");
            _turnInput = Input.GetAxis("Horizontal");
        }

        private void Move()
        {
            foreach (var wheel in wheels)
            {
                wheel.Move(_movementInput, topSpeed);
            }
        }

        private void Turn()
        {
            foreach (var wheel in wheels)
            {
                wheel.Turn(_turnInput, turnSensitivity, maxTurnAngle);
            }
        }

        private void Brake()
        {
            var isBrakePressed = Input.GetKey(KeyCode.Space);
            foreach (var wheel in wheels)
            {
                wheel.Brake(isBrakePressed);
            }
        }
    }
}