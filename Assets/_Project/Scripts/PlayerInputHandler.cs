using Cinemachine;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerInputHandler : MonoBehaviour
    {
        private KartMovementController _kartMovementController;
        public InputMode inputMode = InputMode.Keyboard;
        public CinemachineVirtualCamera virtualCamera;
    
        private float _acceleration;
        private float _brake;
        private bool _isBrakePressed;
        private float _turn;
    
        private void Awake()
        {
            _kartMovementController = GetComponent<KartMovementController>();
#if !UNITY_EDITOR
     inputMode = InputMode.Touch;
#endif
        }
    
        private void Update()
        {
            if (inputMode == InputMode.Keyboard)
            {
                _acceleration = Input.GetAxis("Vertical");
                _turn = Input.GetAxis("Horizontal")* 0.5f;
                _brake = Input.GetKey(KeyCode.Space) ? 1 : 0;
            }
            else
            {
                _turn = Mathf.Abs(Input.acceleration.x) <0.2f ? 0 : Input.acceleration.x * 0.5f;
                _brake = _isBrakePressed ? 1 : 0;
                
                
                // var targetFOV = 40 + Mathf.Abs(_acceleration) * 7;
                var targetFOV = 33 + _kartMovementController.CurrentSpeed / _kartMovementController._topSpeed * 17;
                virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(virtualCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime*3);
                
                var targetDutch= -Input.acceleration.x * 30;
                virtualCamera.m_Lens.Dutch = Mathf.Lerp(virtualCamera.m_Lens.Dutch, targetDutch, Time.deltaTime * 2);
            }
        }
    
        private void FixedUpdate()
        {
            _kartMovementController.Move(_turn,_acceleration,_acceleration,_brake);
        }
        
        
        #region Touch Input

        public void OnAccelerateButtonPressed()
        {
            _acceleration = 1;
        }

        public void OnAccelerateButtonReleased()
        {
            _acceleration = 0;
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
    }
}