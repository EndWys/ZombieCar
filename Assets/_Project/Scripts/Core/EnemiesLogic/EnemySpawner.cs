using Assets._Project.Scripts.Core.EnemiesLogic;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPool))]
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int count;
    [SerializeField] private Vector2 xRange;
    [SerializeField] private float startZ;
    [SerializeField] private float endZ;

    private EnemyPool _pool;
    private List<Enemy> _activeEnemies = new();

    private void Awake()
    {
        _pool = GetComponent<EnemyPool>();
    }

    public void Spawn()
    {
        for (int i = 0; i < count; i++)
        {
            var enemy = _pool.Get();
            float x = Random.Range(xRange.x, xRange.y);
            float z = Random.Range(startZ, endZ);
            enemy.CachedTrasform.position = new Vector3(x, 0, z);
            enemy.Activate();

            _activeEnemies.Add(enemy);
        }
    }

    public void DeactivateAll()
    {
        foreach (var enemy in _activeEnemies)
            enemy.Deactivate();

        _activeEnemies.Clear();
    }
}
