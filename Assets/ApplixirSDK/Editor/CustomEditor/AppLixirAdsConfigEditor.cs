using System.IO;
using ApplixirSDK.Runtime;
using UnityEditor;
using UnityEngine;

namespace ApplixirSDK.Editor.CustomEditor
{
    [UnityEditor.CustomEditor(typeof(AppLixirAdsConfig))]
    public class AppLixirAdsConfigEditor : UnityEditor.Editor
    {
        [MenuItem("Tools/ApplixirSDK/Applixir Ads Config")]
        private static void CreateApplixirAdsConfig()
        {
            ScriptableObjectUtility.CreateAssetAtPath<AppLixirAdsConfig>(
                "ApplixirSDK/Resources/ApplixirAdsConfig.asset");
        }

        public override void OnInspectorGUI()
        {
            AppLixirAdsConfig cfg = (AppLixirAdsConfig)target;
            GUILayout.Label("Ads configuration", EditorStyles.boldLabel);

            cfg.accountId = EditorGUILayout.IntField(
                new GUIContent("Account Id", "Account Id from your applixir.com account information"),
                cfg.accountId);
            cfg.siteId = EditorGUILayout.IntField(
                new GUIContent("Site Id", "Site Id from the applixir.com site settings in your account"),
                cfg.siteId);
            cfg.zoneId = EditorGUILayout.IntField(
                new GUIContent("Zone Id", "Zone Id from the applixir.com site settings in your account. " +
                                          "Use default zone 2050 for testing purposes"),
                cfg.zoneId);
            cfg.logLevel = (LogLevel)EditorGUILayout.EnumPopup(
                new GUIContent("Log Level", "level of logs to use for the SDK. Remember to set to None before " +
                                            "release"),
                cfg.logLevel);
        }
    }

    public static class ScriptableObjectUtility
    {
        /// <summary>
        /// Creates and saves a new ScriptableObject of the specified type at a given path.
        /// </summary>
        public static T CreateAssetAtPath<T>(string relativePath) where T : ScriptableObject
        {
            T asset;
            var path = Path.Combine(Application.dataPath, relativePath);
            Debug.Log(relativePath);
            if (!File.Exists(path))
            {
                asset = ScriptableObject.CreateInstance<T>();
                string directory = Path.GetDirectoryName(path);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(path);
                }

                Debug.Log($"Assets/{relativePath}");
                AssetDatabase.CreateAsset(asset, $"Assets/{relativePath}");
                AssetDatabase.SaveAssets();
            }
            else
            {
                asset = AssetDatabase.LoadAssetAtPath<T>($"Assets/{relativePath}");
            }

            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;

            return asset;
        }
    }
}