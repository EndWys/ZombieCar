using Assets._Project.Scripts.Core.GameManagement;
using Assets._Project.Scripts.Core.LevelBuilder;
using UnityEditor;
using UnityEngine;

namespace Assets._Project.Scripts.Core.LevelBuilder.Editor
{
    public class RoadGeneratorEditor : EditorWindow
    {
        private int roadCount = 10;
        private float spawnOffsetStart = 5f;
        private float spawnOffsetEnd = 10f;
        private float spawnWidth = 6f;

        private Transform roadParent;
        private GameObject roadPrefab;
        private RoadFinish finish;

        private Vector3 startPosition = Vector3.zero;
        private const int ADDITIONAL_VISUAL_ROADS = 2;

        private string savePath = "Assets/_Project/Data/Resources/EnemySpawnData.asset";

        [MenuItem("Tools/Road Generator")]
        public static void ShowWindow() => GetWindow<RoadGeneratorEditor>("Road Generator");

        private void OnGUI()
        {
            GUILayout.Label("Road Generator", EditorStyles.boldLabel);

            roadCount = EditorGUILayout.IntField("Road Count", roadCount);
            roadParent = (Transform)EditorGUILayout.ObjectField("Road Parent", roadParent, typeof(Transform), true);
            roadPrefab = (GameObject)EditorGUILayout.ObjectField("Road Prefab", roadPrefab, typeof(GameObject), false);
            finish = (RoadFinish)EditorGUILayout.ObjectField("Finish", finish, typeof(RoadFinish), true);

            GUILayout.Space(10);
            GUILayout.Label("Enemy Spawn Settings", EditorStyles.boldLabel);

            spawnOffsetStart = EditorGUILayout.FloatField("Spawn Offset Start", spawnOffsetStart);
            spawnOffsetEnd = EditorGUILayout.FloatField("Spawn Offset End", spawnOffsetEnd);
            spawnWidth = EditorGUILayout.FloatField("Spawn Width", spawnWidth);

            GUILayout.Space(10);

            if (GUILayout.Button("Generate Road"))
                GenerateRoad();

            if (GUILayout.Button("Clear Road"))
                ClearRoad();
        }

        private void GenerateRoad()
        {
            if (roadParent == null || roadPrefab == null || finish == null)
            {
                Debug.LogError("Set all required fields!");
                return;
            }

            ClearRoad();

            float pieceLength = GetPieceLength();
            Vector3 currentPos = startPosition;

            for (int i = 0; i < roadCount + ADDITIONAL_VISUAL_ROADS; i++)
            {
                GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(roadPrefab, roadParent);
                obj.transform.position = currentPos;
                currentPos += new Vector3(0, 0, pieceLength);
            }

            finish.transform.position = startPosition + new Vector3(0, 0, pieceLength * (roadCount - 1));

            SaveSpawnData(startPosition.z + spawnOffsetStart, pieceLength * (roadCount - 1) - spawnOffsetEnd, spawnWidth);
        }

        private void ClearRoad()
        {
            for (int i = roadParent.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(roadParent.GetChild(i).gameObject);
            }
        }

        private float GetPieceLength()
        {
            if (roadPrefab.TryGetComponent(out MeshRenderer mesh))
                return mesh.bounds.size.z;

            Debug.LogError("Road prefab missing MeshRenderer");
            return 10f;
        }

        private void SaveSpawnData(float startZ, float endZ, float width)
        {
            var data = AssetDatabase.LoadAssetAtPath<EnemySpawnData>(savePath);
            if (data == null)
            {
                data = CreateInstance<EnemySpawnData>();
                AssetDatabase.CreateAsset(data, savePath);
            }

            data.spawnStartZ = startZ;
            data.spawnEndZ = endZ;
            data.spawnWidth = width;

            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();

            Debug.Log("Saved spawn data to Resources.");
        }
    }
}