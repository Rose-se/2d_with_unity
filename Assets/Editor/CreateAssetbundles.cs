using System;
using UnityEditor;
using UnityEngine;

public class CreateAssetsBundles
{
    [MenuItem("Assets/Create Assets Bundles")]
    private static void BuildAllAssetsBundles()
    {
        string assetBundlesDirectoryPath = "Assets/AssetsBundles";
        try
        {
            BuildPipeline.BuildAssetBundles(assetBundlesDirectoryPath, BuildAssetBundleOptions.None, EditorUserBuildSettings.activeBuildTarget);
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }
}
