using System;
using System.Collections.Generic;
using _Project.Scripts.Collectables;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Rigidbody))]
    public class KartMovementController : MonoBehaviour
    {
        public InputMode inputMode = InputMode.Keyboard;
        [SerializeField] private float topSpeed = 30f;
        [SerializeField] private float brakeDeceleration = 50f;
        public float BrakeDeceleration => brakeDeceleration;

        [SerializeField] private List<KartWheel> wheels;
        [SerializeField] private ParticleSystem coinCollectEffect;

        private Rigidbody _rigidbody;
        private float _movementInput;
        private float _turnInput;
        private bool _isBrakePressed;

        public float turnSensitivity = 1f;
        public float maxTurnAngle = 30f;

        public Vector3 centerOfMass;
        private readonly Vector3 _resetGyroRotation = new Vector3(-9999.999f, -9999.999f, -9999.999f);
        private Vector3 _initialGyroRotation;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.centerOfMass = centerOfMass;
#if !UNITY_EDITOR
     inputMode = InputMode.Touch;
#endif
            _initialGyroRotation = _resetGyroRotation;
        }

        private void Update()
        {
            if (inputMode == InputMode.Keyboard)
            {
                GetKeyboardInput();
            }
            else
            {
                _turnInput = Mathf.Abs(Input.acceleration.x) <0.2f ? 0 : Input.acceleration.x * 0.5f;
            }
        }

        private void LateUpdate()
        {
            Move();
            Turn();
            Brake();
        }

        private void GetKeyboardInput()
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
            if (inputMode == InputMode.Keyboard)
            {
                var isBrakePressed = Input.GetKey(KeyCode.Space);
                foreach (var wheel in wheels)
                {
                    wheel.Brake(isBrakePressed);
                }
            }
            else
            {
                foreach (var wheel in wheels)
                {
                    wheel.Brake(_isBrakePressed);
                }
            }
        }

        #region Touch Input

        public void OnAccelerateButtonPressed()
        {
            _movementInput = 1;
        }

        public void OnAccelerateButtonReleased()
        {
            _movementInput = 0;
        }

        public void OnBrakeButtonPressed()
        {
            _isBrakePressed = true;
        }

        public void OnBrakeButtonReleased()
        {
            _isBrakePressed = false;
        }

        #endregion

        private void OnEnable()
        {
            //GyroscopeHandler.OnGyroscopeUpdate += OnGyroscopeUpdate;
        }
        
        private void OnDisable()
        {
            //GyroscopeHandler.OnGyroscopeUpdate -= OnGyroscopeUpdate;
        }

        private void OnGyroscopeUpdate(Vector3 obj)
        {
            //Debug.Log("1: "+ obj);
            if (_initialGyroRotation == _resetGyroRotation)
            {
                _initialGyroRotation = obj;
            }
            //Debug.Log("2: initial: "+ _initialGyroRotation);
            obj -= _initialGyroRotation;
            _turnInput = obj.y / 90;
            _turnInput = Mathf.Clamp(-_turnInput, -1f, 1f);
            //Debug.Log("3: turnInput: "+ _turnInput);
            
            Debug.Log("Acceleration: "+ Input.acceleration);
            _turnInput = Mathf.Abs(Input.acceleration.x) <0.2f ? 0 : Input.acceleration.x * 0.5f;
        }

        public void CollectCoin(Coin coin)
        {
            coinCollectEffect.Play();
        }
    }
}

public enum InputMode
{
    Keyboard,
    Touch
}