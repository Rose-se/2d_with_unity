using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class BundleWebLoader : MonoBehaviour
{
    public string bundleUrl = "https://dev-meta-horse-general.oss-ap-northeast-1.aliyuncs.com/_item/_unity_config/_assetbundles/_windows/skin_zebra";
    public string assetName = "skin_zebra";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(bundleUrl);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to download AssetBundle: " + www.error);
            yield break;
        }

        AssetBundle remoteAssetBundle = DownloadHandlerAssetBundle.GetContent(www);

        if (remoteAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            yield break;
        }

        Instantiate(remoteAssetBundle.LoadAsset(assetName));

        // Unload the AssetBundle after using it
        remoteAssetBundle.Unload(false);
    }
}
