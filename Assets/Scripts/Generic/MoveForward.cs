using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;

    private void Update()
    {
        transform.position += transform.forward * (Time.deltaTime * _moveSpeed);
    }
}
