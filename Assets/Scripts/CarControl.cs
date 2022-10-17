using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CarControl : MonoBehaviour
{
    [SerializeField] public float force = 10;

    [SerializeField] public float _maxAngle = 60;

    [FormerlySerializedAs("SpeesCar")]
    [SerializeField]
    public TextMeshProUGUI speesCar;

    private float _vertical, _horizontal;

    [SerializeField] private FixedJoystick _fixedJoystick;

    [SerializeField] private Transform _transformFL;
    [SerializeField] private Transform _transformFR;
    [SerializeField] private Transform _transformBL;
    [SerializeField] private Transform _transformBR;

    [SerializeField] private WheelCollider _colliderFL;
    [SerializeField] private WheelCollider _colliderFR;
    [SerializeField] private WheelCollider _colliderBL;
    [SerializeField] private WheelCollider _colliderBR;

    public void FixedUpdate()
    {
        _colliderBL.motorTorque = _fixedJoystick.Vertical * force;
        _colliderBR.motorTorque = _fixedJoystick.Vertical * force;
        
        if (Input.GetKey(KeyCode.Space))
        {
            _colliderFL.brakeTorque = 300000f;
            _colliderFR.brakeTorque = 300000f;
            _colliderBL.brakeTorque = 300000f;
            _colliderBR.brakeTorque = 300000f;
        }
        else
        {
            _colliderFL.brakeTorque = 0;
            _colliderFR.brakeTorque = 0;
            _colliderBL.brakeTorque = 0;
            _colliderBR.brakeTorque = 0;
        }

        _colliderFL.steerAngle = _maxAngle * _fixedJoystick.Horizontal;
        _colliderFR.steerAngle = _maxAngle * _fixedJoystick.Horizontal;

        RotateWheel(_colliderFL, _transformFL);
        RotateWheel(_colliderFR, _transformFR);
        RotateWheel(_colliderBL, _transformBL);
        RotateWheel(_colliderBR, _transformBR);

        if (Input.GetKey("w") && force != 40)
        {
            force++;
        }
        else
        {
            force--;
        }

        speesCar.text = force.ToString(CultureInfo.InvariantCulture);
    }

    private void RotateWheel(WheelCollider collider, Transform transform)
    {
        collider.GetWorldPose(out Vector3 position, out Quaternion rotation);
        transform.rotation = rotation;
        transform.position = position;
    }
}