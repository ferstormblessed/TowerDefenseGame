using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ApplyRotation : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _forceVector;
    [SerializeField] private ForceMode _forceMode;
    [SerializeField] private float _xTorque = 1;
    [SerializeField] private float _minYTorque = 1;
    [SerializeField] private float _maxYTorque = 1;
    [SerializeField] private float _zTorque = 1;
    [SerializeField] private bool _applyTorqueOnAwake;

    private void Awake()
    {
        _forceVector.x = Random.Range(-_xTorque, _xTorque);
        _forceVector.y = Random.Range(_minYTorque, _maxYTorque);
        _forceVector.z = Random.Range(-_zTorque, _zTorque);

        if (_applyTorqueOnAwake)
        {
            ApplyTorqueVector();
        }
    }

    public void ApplyTorqueVector()
    {
        _rigidbody.AddTorque(_forceVector, _forceMode);
    }
}
