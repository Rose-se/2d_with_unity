using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadFromUrl : MonoBehaviour
{
    [SerializeField] private Image img;
    string assetUrl = "http://dev-meta-horse-general.oss-ap-northeast-1.aliyuncs.com/_item/_unity_config/_assetbundles/_windows/skin_zebra";
    string imageUrl = "Assets/AssetBundleData/SkinZebra/T_Zebra.png";

    public void DownloadAssets()
    {
        StartCoroutine(IEAssetBundle(assetUrl));
    }

    IEnumerator IEAssetBundle(string url)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogErrorFormat("Failed to download AssetBundle. Error: {0}", www.error);
            yield break;
        }

        AssetBundle assetBundle = DownloadHandlerAssetBundle.GetContent(www);

        // Load AssetAsync
        AssetBundleRequest request = assetBundle.LoadAssetAsync<Texture2D>(imageUrl);
        yield return request;

        Texture2D texture2D = request.asset as Texture2D;
        Sprite sprite = Sprite.Create(texture2D, new Rect(0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0.5f, 0.5f), 100.0f);

        img.sprite = sprite;

        // Unload AssetBundle
        assetBundle.Unload(false);
    }
}
