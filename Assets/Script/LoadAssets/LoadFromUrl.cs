using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class LoadFromUrl : MonoBehaviour
{
    public string assetBundleName;
    public string url = "http://dev-meta-horse-general.oss-ap-northeast-1.aliyuncs.com/_item/_unity_config/_assetbundles/_windows/skin_zebra";

    public void DownloadAssets()
    {
        StartCoroutine(InstantiateObject());
    }

    IEnumerator InstantiateObject()
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogErrorFormat("Failed to download AssetBundle. Error: {0}", www.error);
            yield break;
        }

        AssetBundle remoteAssetBundle = DownloadHandlerAssetBundle.GetContent(www);

        if (remoteAssetBundle == null)
        {
            Debug.LogErrorFormat("Failed to load AssetBundle: {0}", assetBundleName);
            yield break;
        }

        // เช็คประเภทข้อมูลที่ดึงมา
        if (www.downloadHandler is DownloadHandlerAssetBundle)
        {
            GameObject prefab = remoteAssetBundle.LoadAsset<GameObject>(assetBundleName);

            if (prefab != null)
            {
                Instantiate(prefab);
            }
            else
            {
                Debug.LogErrorFormat("Failed to load GameObject from AssetBundle: {0}", assetBundleName);
            }
        }
        else
        {
            Debug.LogError("Downloaded data is not an Asset Bundle.");
        }

        remoteAssetBundle.Unload(false);
    }
}
