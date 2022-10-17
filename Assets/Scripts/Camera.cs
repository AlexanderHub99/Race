using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _car;

    [SerializeField]private float _offsetX = 10f;
    [SerializeField]private float _offsetY = 10f;
    [SerializeField]private float _offsetZ = 10f;

    private float _speed = 10f;

    private void FixedUpdate()
    {
        Vector3 _offset = new Vector3(_offsetX,_offsetY,_offsetZ);
        var targetPosition = _car.TransformPoint(_offset);
        transform.position = Vector3.Lerp(transform.position, targetPosition, _speed * Time.deltaTime);

        var dirction = _car.position - transform.position;
        var rotation = Quaternion.LookRotation(dirction, Vector3.up);

        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _speed * Time.deltaTime);
    }
}
