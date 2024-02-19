using UnityEngine;
using UnityEditor;

public class UncompressedAssetBundle
{
    [MenuItem("Assets/Extract AssetsBundles")]
    static void ExtractAssetBundles()
    {
        // สร้าง AssetBundles
        BuildPipeline.BuildAssetBundles("Assets/AssetsBundles", BuildAssetBundleOptions.UncompressedAssetBundle, BuildTarget.StandaloneWindows);
        Debug.Log("AssetsBundles Extracted!");

        // ระบุชื่อ AssetBundle ที่สร้างมา
        string assetBundlePath = "Assets/AssetsBundles/myassetbundle";

        // ระบุชื่อไฟล์ที่ต้องการแตกไฟล์
        string assetName = "myassetbundle";

        // โหลด AssetBundle
        AssetBundle assetBundle = AssetBundle.LoadFromFile(assetBundlePath);

        // ดึงไฟล์จาก AssetBundle
        GameObject prefab = assetBundle.LoadAsset<GameObject>(assetName);

        // ทำตามต้องการของคุณ
        // ... ต่อไปตามต้องการ
    }
}
