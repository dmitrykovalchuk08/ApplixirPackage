using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ApplixirSDK.Editor
{
    public class WebGLPostBuild : MonoBehaviour
    {
        [PostProcessBuild]
        public static void OnPostBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target == BuildTarget.WebGL)
            {
                Log("Post-build step for WebGL");
                PerformPostBuildSteps(pathToBuiltProject);
                Log("Post-build step for WebGL Done");
            }
        }

        private static void PerformPostBuildSteps(string pathToBuiltProject)
        {
            string copyfrom = Path.Combine(Application.dataPath, "ApplixirSDK/Editor/res/app-ads.txt");
            if (!File.Exists(copyfrom))
            {
                Debug.LogError("ApplixirSDK could not find app-ads.txt file. " +
                               "If you moved SDK files - please set new path to app-ads.txt file here.");
                return;
            }

            string filePath = Path.Combine(pathToBuiltProject, "app-ads.txt");
            File.Copy(copyfrom, filePath, true);
        }

        private const string Tag = "ApplixirWebGL";

        private static void Log(string message)
        {
            Debug.Log($"[{Tag}] {message}");
        }
    }
}