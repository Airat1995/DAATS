using DAATS.Initializer.Level;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DAATS.Editor
{
    public class LevelCreationWindow : EditorWindow
    {
        [MenuItem("Levels/Level")]
        private static void ShowWindow()
        {
            var window = GetWindow<LevelCreationWindow>();
            window.titleContent = new GUIContent("Level Creation Window");
            window.minSize = new Vector2(250, 250);
        }

        private void OnEnable()
        {
            var root = rootVisualElement;
            var window = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Editor/EditorWindows/LevelCreation/Resources/LevelCreation_window.uxml");
            window.CloneTree(root);

            TextInputBaseField<string> nameField = (TextInputBaseField<string>)root.Q("levelNameField");
            TextInputBaseField<int> numField = (TextInputBaseField<int>)root.Q("levelNumField");
            Toggle hideToggle = (Toggle)root.Q("hideToggle");
            Button createButton = (Button)root.Q("createButton");


            createButton.clicked += () => CreateObject(nameField, numField, hideToggle);
        }

        private void CreateObject(TextInputBaseField<string> nameField, TextInputBaseField<int> numField, Toggle hideToggle)
        {
            string levelName = nameField.value;
            int levelNum = numField.value;
            bool hide = hideToggle.value;

            if (string.IsNullOrWhiteSpace(levelName))
            {
                Debug.LogError("Unable to create level with empty name!");
                return;
            }

            var rootGO = new GameObject(levelName);
            var levelDescriptor = rootGO.AddComponent<LevelDescriptor>();
            levelDescriptor.FillInfo(levelName, levelNum, hide);
            PrefabUtility.SaveAsPrefabAsset(rootGO, "Assets/Resources/Level/" + levelName + ".prefab");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            DestroyImmediate(rootGO);
        }
    }
}
