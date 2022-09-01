using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemySpawnController : MonoBehaviour
{
    [SerializeField] private WavesData _wavesData;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private GameObject _weakEnemyPrefab;
    [SerializeField] private GameObject _midEnemyPrefab;
    [SerializeField] private GameObject _heavyEnemyPrefab;
    [SerializeField] private float _minimumSpawnDelay = 1.0f;
    [SerializeField] private float _maximumSpawnDelay = 5.0f;
    [SerializeField] private float _waitTimeBetweenWaves = 5;
    private int _waveNumber;
    [SerializeField] private UnityEvent OnFinishWaves;

    private void Start()
    {
        StartCoroutine(CreateNewEnemyWave());
    }

    IEnumerator CreateNewEnemyWave()
    {
        while (_waveNumber < _wavesData.Waves.Length && GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
        {
            yield return new WaitForSeconds(_waitTimeBetweenWaves);
            StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].WeakEnemies, _weakEnemyPrefab));
            StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].MidEnemies, _midEnemyPrefab));
            StartCoroutine(SpawnEnemies(_wavesData.Waves[_waveNumber].HeavyEnemies, _heavyEnemyPrefab));

            while (GameManager.Instance.EnemyCount > 0)
            {
                yield return null;
            }
            
            _waveNumber++;
        }

        //winner
        if (!GameManager.Instance.GameOver)
        {
            OnFinishWaves?.Invoke();
        }
    }

    IEnumerator SpawnEnemies(int enemyAmount, GameObject enemyPrefab)
    {
        for (int i = 0; i < enemyAmount; i++)
        {
            Instantiate(enemyPrefab, _spawnPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(_minimumSpawnDelay, _maximumSpawnDelay));
        }
    }

}
