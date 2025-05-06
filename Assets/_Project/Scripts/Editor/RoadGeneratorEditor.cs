using UnityEditor;
using UnityEngine;
using Assets._Project.Scripts.Core.GameManagement.RoadGenerationLogic;

namespace Assets._Project.Scripts.Editor
{
    public class RoadGeneratorEditor : EditorWindow
    {
        private LevelGenerator _generator;

        [MenuItem("Tools/Road Generator")]
        public static void ShowWindow()
        {
            GetWindow<RoadGeneratorEditor>("Road Generator");
        }

        private void OnEnable()
        {
            FindGenerator();
        }

        private void OnHierarchyChange()
        {
            if (_generator == null)
            {
                FindGenerator();
                Repaint();
            }
        }

        private void FindGenerator()
        {
            _generator = FindObjectOfType<LevelGenerator>();
        }

        private void OnGUI()
        {
            GUILayout.Label("Road Generator", EditorStyles.boldLabel);

            if (_generator == null)
            {
                EditorGUILayout.HelpBox("RoadGenerator not found in the scene.", MessageType.Warning);
                if (GUILayout.Button("Try Find Again"))
                {
                    FindGenerator();
                }
                return;
            }

            EditorGUILayout.LabelField("Found:", _generator.name);
            _generator.RoadCount = EditorGUILayout.IntField("Road Count", _generator.RoadCount);

            if (GUILayout.Button("Generate Road"))
            {
                _generator.GenerateLevelRoad();
            }

            if (GUILayout.Button("Clear Road"))
            {
                _generator.ClearRoad();
            }

            GUILayout.Space(10);
            GUILayout.Label("Enemy Spawn Settings", EditorStyles.boldLabel);

            _generator.SpawnOffsetStart = EditorGUILayout.FloatField("Spawn Offset Start", _generator.SpawnOffsetStart);
            _generator.SpawnOffsetEnd = EditorGUILayout.FloatField("Spawn Offset End", _generator.SpawnOffsetEnd);
            _generator.SpawnWidth = EditorGUILayout.FloatField("Spawn Width", _generator.SpawnWidth);
        }
    }
}