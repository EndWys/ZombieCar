using Assets._Project.Scripts.Core.EnemiesLogic;
using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesCount = 10;

    private float _spawnWidth = 6f;
    private float _startZ;
    private float _endZ;

    private EnemyPool _pool;
    private List<Enemy> _activeEnemies = new();

    private void Awake()
    {
        _pool = GetComponent<EnemyPool>();

        TryFindRoadGeneratorData();
    }

    private void TryFindRoadGeneratorData()
    {
        var generator = FindObjectOfType<LevelGenerator>();

        if (generator != null)
        {
            _spawnWidth = generator.SpawnWidth;
            _startZ = generator.SpawnStartZ;
            _endZ = generator.SpawnEndZ;
        }
        else
        {
            Debug.LogWarning("RoadGenerator not found! EnemySpawner will use default values.");
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = _pool.GetObject();

            float x = Random.Range(-_spawnWidth / 2f, _spawnWidth / 2f);
            float z = Random.Range(_startZ, _endZ);

            enemy.CachedTrasform.position = new Vector3(x, 0, z);

            _activeEnemies.Add(enemy);

            enemy.Activate();
        }
    }

    public void DeactivateAll()
    {
        foreach (var enemy in _activeEnemies)
            enemy.Deactivate();

        _activeEnemies.Clear();
    }
}