using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    [SerializeField] private List<Transform> _damageList = new List<Transform>();
    [SerializeField] private int _damagePower;
    [SerializeField] private UnityEvent OnDamageDealt;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private int _delayBeforeExplotion = 1;
    [SerializeField] private float _blinkDuration = 0.1f;
    [SerializeField] private float _blinkDelayDecreaseRate = 0.1f;
    [SerializeField] private string _tagToCompare = "Weapon";
    
    private void Start()
    {
        StartCoroutine(ExplodeRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_tagToCompare))
        {
            _damageList.Add(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            _damageList.Remove(other.transform);
        }
    }

    IEnumerator ExplodeRoutine()
    {
        float blinkDelay = 1;
        while (blinkDelay > 0)
        {
            yield return new WaitForSeconds(blinkDelay);
            _renderer.material.SetColor("_EmissionColor", Color.white);
            yield return new WaitForSeconds(_blinkDuration);
            _renderer.material.SetColor("_EmissionColor", Color.red);
            blinkDelay -= _blinkDelayDecreaseRate;
        }
        _renderer.material.SetColor("_EmissionColor", Color.white);

        yield return new WaitForSeconds(_delayBeforeExplotion);
        foreach (var weapon in _damageList)
        {
            if(weapon == null)
                continue;

            if (weapon.TryGetComponent(out Health health))
            {
                health.TakeDamage(_damagePower);
            }
        }
        
        OnDamageDealt?.Invoke();
    }
}
