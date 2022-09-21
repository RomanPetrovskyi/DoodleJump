using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Editor
{
    public class Builder : MonoBehaviour
    {
        public static void BuildApk()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();

            buildPlayerOptions.scenes = new[]
            {
                "Assets/Scenes/Start.unity",
                "Assets/Scenes/Game.unity",
                "Assets/Scenes/SampleScene.unity"
            };

            buildPlayerOptions.locationPathName = "builds/Team2DoodleTest.apk";
            buildPlayerOptions.target = BuildTarget.Android;
            buildPlayerOptions.options = BuildOptions.None;

            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log("BUILD IS DONE");
            }

            if (summary.result == BuildResult.Failed)
            {
                Debug.Log("BUILD FAILED");
            }
        }
    }
}
