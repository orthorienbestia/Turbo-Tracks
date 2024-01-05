using System;
using _Project.Scripts.Collectables;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class KartMovementController : MonoBehaviour
{
    [SerializeField] private GameObject[] _wheelMeshes = new GameObject[4];
    public WheelCollider[] wheelColliders = new WheelCollider[4];
    [SerializeField] internal float _topSpeed = 200.0f;

    private readonly Quaternion[] _wheelMeshLocalRotations = new Quaternion[4];
    private float _currentTorque;
    private Rigidbody _rigidbody;
    private Vector2 _currentInputVector;
    private Vector2 _smoothInputVelocity;
    private float _currentMaxSteerAngle;

    public float CurrentSpeed => _rigidbody.velocity.magnitude * 3.6f;
    public float AccelInput { get; private set; }

    public Vector3 centerOfMass;

    [SerializeField] private ParticleSystem coinCollectEffect;

    private void Awake()
    {
        for (var i = 0; i < 4; i++)
        {
            _wheelMeshLocalRotations[i] = _wheelMeshes[i].transform.localRotation;
        }

        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass;
    }

    private const float DeadSlowing = float.MaxValue * 1f;

    public void Move(float steering, float accel, float footBrake, float handBrake)
    {
        var input = new Vector2(steering, accel);
        _currentInputVector = Vector2.SmoothDamp(_currentInputVector, input, ref _smoothInputVelocity, 0.2f);
        accel = _currentInputVector.y;
        steering = _currentInputVector.x;

        for (var i = 0; i < 4; i++)
        {
            wheelColliders[i].GetWorldPose(out Vector3 position, out Quaternion quat);
            _wheelMeshes[i].transform.SetPositionAndRotation(position, quat);
        }

        steering = Mathf.Clamp(steering, -1, 1);
        AccelInput = accel = Mathf.Clamp(accel, 0, 1);
        footBrake = -1 * Mathf.Clamp(footBrake, -1, 0);
        handBrake = Mathf.Clamp(handBrake, 0, 1);

        var steerAngle = steering * _currentMaxSteerAngle;
        wheelColliders[0].steerAngle = steerAngle;
        wheelColliders[1].steerAngle = steerAngle;

        ApplyDrive(accel, footBrake);
        CapSpeed();

        if (handBrake > 0f)
        {
            var handBrakeTorque = handBrake * float.MaxValue;
            wheelColliders[2].brakeTorque = handBrakeTorque;
            wheelColliders[3].brakeTorque = handBrakeTorque;
        }
        else
        {
            wheelColliders[2].brakeTorque =  0f;
            wheelColliders[3].brakeTorque =  0f;
            // wheelColliders[2].brakeTorque = accel == 0 ? DeadSlowing : 0f;
            // wheelColliders[3].brakeTorque = accel == 0 ? DeadSlowing : 0f;
        }

        _rigidbody.AddForce(100 * _rigidbody.velocity.magnitude * -transform.up);
        TractionControl();
        AntiRoll();

        _currentMaxSteerAngle = CurrentSpeed switch
        {
            < 20f => Mathf.MoveTowards(_currentMaxSteerAngle, 25, 0.5f),
            > 20f and < 60f => Mathf.MoveTowards(_currentMaxSteerAngle, 25 / 1.5f, 0.5f),
            > 50 => Mathf.MoveTowards(_currentMaxSteerAngle, 25 / 2f, 0.5f),
            _ => _currentMaxSteerAngle
        };
    }

    private void CapSpeed()
    {
        var speed = _rigidbody.velocity.magnitude;
        speed *= 3.6f;
        if (speed > _topSpeed)
            _rigidbody.velocity = _topSpeed / 3.6f * _rigidbody.velocity.normalized;
    }

    private void ApplyDrive(float accel, float footBrake)
    {
        var thrustTorque = accel * (_currentTorque / 4f);
        for (var i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = thrustTorque;

            if (CurrentSpeed > 5 && Vector3.Angle(transform.forward, _rigidbody.velocity) < 50f)
            {
                wheelColliders[i].brakeTorque = 3000 * footBrake;
            }
            else if (footBrake > 0)
            {
                wheelColliders[i].brakeTorque = 0f;
                wheelColliders[i].motorTorque = -450f * footBrake;
            }
        }
    }

    private void AntiRoll()
    {
        for (var i = 0; i < wheelColliders.Length; i += 2)
        {
            var travelL = GetWheelTravel(wheelColliders[i]);
            var travelR = GetWheelTravel(wheelColliders[i + 1]);

            var antiRollForce = (travelL - travelR) * 5000;

            ApplyAntiRollForce(wheelColliders[i], -antiRollForce);
            ApplyAntiRollForce(wheelColliders[i + 1], antiRollForce);
        }
    }

    private static float GetWheelTravel(WheelCollider wheelCollider) => wheelCollider.GetGroundHit(out var wheelHit)
        ? (-wheelCollider.transform.InverseTransformPoint(wheelHit.point).y - wheelCollider.radius) /
          wheelCollider.suspensionDistance
        : 1.0f;

    private void ApplyAntiRollForce(WheelCollider wheelCollider, float force)
    {
        if (wheelCollider.GetGroundHit(out _))
            _rigidbody.AddForceAtPosition(wheelCollider.transform.up * force, wheelCollider.transform.position);
    }

    private void TractionControl()
    {
        for (var i = 0; i < 4; i++)
        {
            wheelColliders[i].GetGroundHit(out var wheelHit);
            AdjustTorque(wheelHit.forwardSlip);
        }
    }

    private void AdjustTorque(float forwardSlip)
    {
        if (forwardSlip >= 0.44f && _currentTorque >= 0)
        {
            _currentTorque -= 10;
        }
        else
        {
            _currentTorque += 10;
            if (_currentTorque > 2000)
            {
                _currentTorque = 2000;
            }
        }
    }

    public void CollectCoin(Coin coin)
    {
        coinCollectEffect.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            Debug.Log("Waypoint: " + other.gameObject.name);
            GameplayManager.Instance.CrossedWayPoint(other.gameObject);
        }
    }
}