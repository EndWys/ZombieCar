using UnityEngine;

namespace Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic
{
    public class LevelGenerator : MonoBehaviour
    {
        private const int ADDITION_VISUAL_ROADS = 2;

        [Header("Road Settings")]
        [SerializeField] private Transform roadParent;
        [SerializeField] private RoadFinish finish;
        [SerializeField] private GameObject roadPrefab;

        [HideInInspector] public int RoadCount = 10;

        [Header("Enemy Spawn Settings")]
        [HideInInspector] public float SpawnOffsetStart = 5f;
        [HideInInspector] public float SpawnOffsetEnd = 10f;
        [HideInInspector] public float SpawnWidth = 6f;

        [HideInInspector] public float SpawnStartZ;
        [HideInInspector] public float SpawnEndZ;

        private Vector3 _startPosition = Vector3.zero;

        public void GenerateLevelRoad()
        {
            ClearRoad();

            float length = GetRoadPiecePrefabLength();
            PaveRoad(length);
            SetFinishZone(length);
            CalculateSpawnZone(length);
        }

        public void ClearRoad()
        {
            for (int i = roadParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(roadParent.GetChild(i).gameObject);
            }
        }

        private void PaveRoad(float onePieceLength)
        {
            Vector3 currentRoadPosition = _startPosition;

            for (int i = 0; i < RoadCount + ADDITION_VISUAL_ROADS; i++)
            {
                Instantiate(roadPrefab, currentRoadPosition, roadPrefab.transform.rotation, roadParent);
                currentRoadPosition += new Vector3(0, 0, onePieceLength);
            }
        }

        private void SetFinishZone(float onePieceLength)
        {
            int lastPieceOffset = 1;
            Vector3 finishPosition = new Vector3(_startPosition.x, _startPosition.y, onePieceLength * (RoadCount - lastPieceOffset));
            finish.CachedTrasform.position = finishPosition;
        }

        private void CalculateSpawnZone(float onePieceLength)
        {
            float levelStart = _startPosition.z;
            float levelEnd = onePieceLength * RoadCount;

            SpawnStartZ = levelStart + SpawnOffsetStart;
            SpawnEndZ = levelEnd - SpawnOffsetEnd;
        }

        private void OnDrawGizmosSelected()
        {
            if (roadPrefab == null) return;

            float onePieceLength = GetRoadPiecePrefabLength();
            float levelEnd = onePieceLength * (RoadCount - 1);

            float startZ = _startPosition.z + SpawnOffsetStart;
            float endZ = levelEnd - SpawnOffsetEnd;

            Vector3 center = new Vector3(0, 0.1f, (startZ + endZ) / 2f);
            Vector3 size = new Vector3(SpawnWidth, 0.1f, Mathf.Abs(endZ - startZ));

            Gizmos.color = new Color(1f, 0.3f, 0.3f, 0.3f);
            Gizmos.DrawCube(center, size);
        }

        private float GetRoadPiecePrefabLength()
        {
            if (roadPrefab.TryGetComponent(out MeshRenderer mesh))
            {
                return mesh.bounds.size.z;
            }

            Debug.LogError("Wrong road type");
            return 0;
        }
    }
}