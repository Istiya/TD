using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

#nullable enable

public class WaveManager : MonoBehaviour
{
    public List<WaveModel> _waveModels = new List<WaveModel>();
    int _waveIndex = 0;

    public float timeBetweensEnemies = 1f;
    public float timeBetweensWaves = 30f;
    private float _deafaultTimeBetweensWaves = 30f;

    public event Action<float>? OnTimeChanges;

    int numberEnemies;

    private void Start()
    {
        InvokeRepeating("DoWaves", _deafaultTimeBetweensWaves, _deafaultTimeBetweensWaves);
        StartCoroutine(DecriseTimeBetweensWaves());
    }

    void EnemyInstantiate(GameObject enemyPrefab, Vector3 spawnPoint)
    {
        EnemyModel enemyModel = Instantiate(enemyPrefab, spawnPoint, enemyPrefab.transform.rotation, GameObject.FindGameObjectWithTag("Map").transform.Find("Enemies")).GetComponent<EnemyModel>();
        enemyModel.gameObject.AddComponent<EnemyController>();
    }

    void SpawnEnemy(EnemyType enemyType, Vector3 spawnPoint)
    {
        EnemyInstantiate(PrefabManager.instance.Get(enemyType.GetPrefabType()), spawnPoint);
    }

    IEnumerator SpawnWave()
    {
        WaveModel waveModel = FindWaveModel();

        List<EnemyType> enemyTypes = waveModel.enemyTypes;
        System.Random rand = new System.Random();

        Vector3 spawnPoint = Waypoints.GetStartPosition();

        numberEnemies = waveModel.enemyCount;
        for (int i = 0; i < numberEnemies; i++)
        {
            EnemyType enemyType = enemyTypes[rand.Next(0, enemyTypes.Count)];
            SpawnEnemy(enemyType, spawnPoint);
            yield return new WaitForSeconds(timeBetweensEnemies);
        }

        _waveIndex++;
    }

    void DoWaves()
    {
        timeBetweensWaves = _deafaultTimeBetweensWaves;
        StartCoroutine(SpawnWave());
        StartCoroutine(DecriseTimeBetweensWaves());
    }

    IEnumerator DecriseTimeBetweensWaves()
    {
        for (int i = 0; i < 30; i++)
        {
            timeBetweensWaves -= 1f;
            OnTimeChanges?.Invoke(timeBetweensWaves);
            yield return new WaitForSeconds(1f);
        }
    }

    WaveModel FindWaveModel()
    {
        WaveModel waveModel = _waveModels.FindAll(w => _waveIndex >= w.waveIndex).Max();
        return waveModel;
    }
}
