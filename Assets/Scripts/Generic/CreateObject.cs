using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class CreateObject : MonoBehaviour
{
    [SerializeField] private GameObject _objectToCreate;
    [Range(0, 1)] [SerializeField] private float _chance = 1;

    public void CreateNewObject()
    {
        if (Random.value < _chance)
        {
            Instantiate(_objectToCreate, transform.position, Quaternion.identity);
        }
    }
}
