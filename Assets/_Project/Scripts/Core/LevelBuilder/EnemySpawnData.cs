using UnityEngine;

namespace Assets._Project.Scripts.Core.LevelBuilder
{
    [CreateAssetMenu(fileName = "EnemySpawnData", menuName = "GameData/Enemy Spawn Data")]
    public class EnemySpawnData : ScriptableObject
    {
        public float SpawnStartZ;
        public float SpawnEndZ;
        public float SpawnWidth;
    }
}