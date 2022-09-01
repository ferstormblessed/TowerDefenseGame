using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponAttack : MonoBehaviour, IWillPlayAudio
{
    [SerializeField] private Transform _weaponBarrel;
    [SerializeField] private float _maxRayDistance = 20;
    [SerializeField] private int _damagePower = 10;
    [SerializeField] private float _shotCooldown = 1;

    enum  ShotTypeEnum
    {
        Ray,
        Instantiate
    }

    [SerializeField] private ShotTypeEnum _shotType;
    [SerializeField] private GameObject _cannonballPrefab;
    [SerializeField] private Transform _cannonballSpawnPosition;
    [SerializeField] private UnityEvent OnShot;
    [SerializeField] private LayerMask _enemyLayerMask;
    private AudioController _audioController;
    //[SerializeField] private AudioSfx _cannonShot;
    //[SerializeField] private GameState _gameState;

    private void Awake()
    {
        _audioController = FindObjectOfType<AudioController>();
    }

    public void StartWeaponAttack(){
        StartCoroutine(FireRoutine());
    }

    IEnumerator FireRoutine()
    {
        while (GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
        {
            Ray ray = new Ray(_weaponBarrel.position, _weaponBarrel.forward);
            if (Physics.Raycast(ray, out RaycastHit hitInfo, _maxRayDistance, _enemyLayerMask))
            {
                //print(hitInfo.collider.name);
                
                if (hitInfo.collider.CompareTag("Enemy"))
                {
                    if (_shotType == ShotTypeEnum.Instantiate)
                    {
                        //instantiate cannonball
                        Instantiate(_cannonballPrefab, _cannonballSpawnPosition.position,
                            _cannonballSpawnPosition.rotation);
                        //GameManager.Instance._audioController.PlayAudio("CannonShot");
                        //_cannonShot.PlayAudio();
                    }
                    else
                    {
                        Health enemyHealth = hitInfo.collider.GetComponent<Health>();
                        if (enemyHealth != null)
                        {
                            enemyHealth.TakeDamage(_damagePower);
                        }
                    }
                    OnShot?.Invoke();
                }
                Debug.DrawRay(ray.origin, ray.direction * _maxRayDistance, Color.red);
                yield return new WaitForSeconds(_shotCooldown); 
            }
            else
            {
                yield return null; 
                Debug.DrawRay(ray.origin, ray.direction * _maxRayDistance, Color.yellow);
            }
        }
    }

    public void PlaySfx(string audioName)
    {
        _audioController.PlayAudio(audioName);
    }
}
