using System.IO;
using UnityEngine;

public class LoadFromFile : MonoBehaviour
{
    void Start() 
    {
        string assetBundlePath = Path.Combine(Application.streamingAssetsPath, "AssetsBundles");
        var myLoadedAssetBundle = AssetBundle.LoadFromFile(assetBundlePath);
        if (myLoadedAssetBundle == null) {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }
        var prefab = myLoadedAssetBundle.LoadAsset<GameObject>("myassetbundle");
        Instantiate(prefab);
    }
}