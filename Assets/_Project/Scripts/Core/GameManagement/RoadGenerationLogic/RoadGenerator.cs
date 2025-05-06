using UnityEngine;

namespace Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic
{
    public class RoadGenerator : MonoBehaviour
    {
        [SerializeField] private RoadFinish finish;

        [SerializeField] private GameObject roadPrefab;
        [SerializeField] private int roadCount = 10;

        private Vector3 _startPosition = Vector3.zero;

        private void Start()
        {
            GenerateLevelRoad();
        }

        private void GenerateLevelRoad()
        {
            float length = GetRoadPiecePrefabLength();

            PaveRoad(length);
            SetFinishZone(length);
        }

        private void PaveRoad(float onePieceLenth)
        {
            Vector3 currentRoadPosition = _startPosition;

            for (int i = 0; i < roadCount; i++)
            {
                Instantiate(roadPrefab, currentRoadPosition, roadPrefab.transform.rotation, transform);

                currentRoadPosition += new Vector3(0, 0, onePieceLenth);
            }
        }


        private void SetFinishZone(float onePieceLenth)
        {
            int lastPieceOffset = 1;
            Vector3 finishPosition = new Vector3(_startPosition.x, _startPosition.y, onePieceLenth * (roadCount - lastPieceOffset));

            finish.CachedTrasform.position = finishPosition;
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