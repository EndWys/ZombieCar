using Assets._Project.Scripts.Core.GameManagement;
using UnityEditor;
using UnityEngine;

namespace Assets._Project.Scripts.Core.LevelBuilder.Editor
{
    public class RoadGeneratorEditor : EditorWindow
    {
        private const int _ADDITIONAL_VISUAL_ROADS = 2;

        private Transform _roadParent;
        private GameObject _roadPrefab;
        private RoadFinish _finish;

        private Vector3 _startPosition = Vector3.zero;

        private int _roadCount = 10;
        private float _spawnOffsetStart = 5f;
        private float _spawnOffsetEnd = 10f;
        private float _spawnWidth = 6f;

        private string _savePath = "Assets/_Project/Data/Resources/EnemySpawnData.asset";

        private int _lastRoadIndex => _roadCount - 1;

        [MenuItem("Tools/Road Generator")]
        public static void ShowWindow() => GetWindow<RoadGeneratorEditor>("Road Generator");

        private void OnGUI()
        {
            GUILayout.Label("Road Generator", EditorStyles.boldLabel);

            _roadCount = EditorGUILayout.IntField("Road Count", _roadCount);
            _roadParent = (Transform)EditorGUILayout.ObjectField("Road Parent", _roadParent, typeof(Transform), true);
            _roadPrefab = (GameObject)EditorGUILayout.ObjectField("Road Prefab", _roadPrefab, typeof(GameObject), false);
            _finish = (RoadFinish)EditorGUILayout.ObjectField("Finish", _finish, typeof(RoadFinish), true);

            GUILayout.Space(10);
            GUILayout.Label("Enemy Spawn Settings", EditorStyles.boldLabel);

            _spawnOffsetStart = EditorGUILayout.FloatField("Spawn Offset Start", _spawnOffsetStart);
            _spawnOffsetEnd = EditorGUILayout.FloatField("Spawn Offset End", _spawnOffsetEnd);
            _spawnWidth = EditorGUILayout.FloatField("Spawn Width", _spawnWidth);

            GUILayout.Space(10);

            if (GUILayout.Button("Generate Road"))
                GenerateRoad();

            if (GUILayout.Button("Clear Road"))
                ClearRoad();
        }

        private void GenerateRoad()
        {
            if (_roadParent == null || _roadPrefab == null || _finish == null)
            {
                Debug.LogError("Set all required fields!");
                return;
            }

            ClearRoad();

            float pieceLength = GetPieceLength();
            Vector3 currentPos = _startPosition;

            for (int i = 0; i < _roadCount + _ADDITIONAL_VISUAL_ROADS; i++)
            {
                GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(_roadPrefab, _roadParent);
                obj.transform.position = currentPos;
                currentPos += new Vector3(0, 0, pieceLength);
            }

            _finish.CachedTrasform.position = _startPosition + new Vector3(0, 0, pieceLength * _lastRoadIndex);

            SaveSpawnData(_startPosition.z + _spawnOffsetStart, pieceLength * _lastRoadIndex - _spawnOffsetEnd, _spawnWidth);
        }

        private void ClearRoad()
        {
            for (int i = _roadParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(_roadParent.GetChild(i).gameObject);
            }
        }

        private float GetPieceLength()
        {
            if (_roadPrefab.TryGetComponent(out MeshRenderer mesh))
                return mesh.bounds.size.z;

            Debug.LogError("Road prefab missing MeshRenderer");
            return 10f;
        }

        private void SaveSpawnData(float startZ, float endZ, float width)
        {
            var data = AssetDatabase.LoadAssetAtPath<EnemySpawnData>(_savePath);
            if (data == null)
            {
                data = CreateInstance<EnemySpawnData>();
                AssetDatabase.CreateAsset(data, _savePath);
            }

            data.SpawnStartZ = startZ;
            data.SpawnEndZ = endZ;
            data.SpawnWidth = width;

            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();

            Debug.Log("Saved spawn data to Resources.");
        }
    }
}