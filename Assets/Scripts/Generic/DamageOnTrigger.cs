using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DamageOnTrigger : MonoBehaviour
{
    [SerializeField] private int _damagePower = 10;
    [SerializeField] private string _tagToCompare;
    [SerializeField] private UnityEvent OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damagePower);
                OnTrigger?.Invoke();
            }
            {
                Debug.LogWarning("Component not found");
            }
            
        }
    }
}
