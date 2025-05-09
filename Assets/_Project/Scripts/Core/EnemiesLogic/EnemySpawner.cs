using Assets._Project.Scripts.Core.EnemiesLogic;
using Assets._Project.Scripts.Core.LevelBuilder;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int enemiesCount = 10;
    [SerializeField] private EnemyPool pool;

    private float _spawnWidth = 6f;
    private float _startZ;
    private float _endZ;

    private List<Enemy> _activeEnemies = new();

    private void Awake()
    {
        TryFindSpawnZoneData();
    }

    private void TryFindSpawnZoneData()
    {
        var data = Resources.Load<EnemySpawnData>("EnemySpawnData");
        if (data != null)
        {
            _spawnWidth = data.SpawnWidth;
            _startZ = data.SpawnStartZ;
            _endZ = data.SpawnEndZ;
        }
        else
        {
            Debug.LogWarning("EnemySpawnData not found in Resources!");
        }
    }

    public void Spawn()
    {
        for (int i = 0; i < enemiesCount; i++)
        {
            Enemy enemy = pool.GetObject();

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

    #region Draw gizmos


    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        var data = Resources.Load<EnemySpawnData>("EnemySpawnData");
        if (data == null) return;

        float centerZ = (data.SpawnStartZ + data.SpawnEndZ) / 2f;
        float sizeZ = Mathf.Abs(data.SpawnEndZ - data.SpawnStartZ);

        Vector3 center = new Vector3(0, 0.1f, centerZ);
        Vector3 size = new Vector3(data.SpawnWidth, 0.1f, sizeZ);

        Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.3f);
        Gizmos.DrawCube(center, size);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(center, size);
    }
    #endif

    #endregion
}