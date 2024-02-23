using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class SpawnPlayer : MonoBehaviour
{
    [SerializeField] private AssetReferenceGameObject assetReferenceGameObject;

    private void Awake()
    {
        SpawnPlayerAddress();
    }

    private void SpawnPlayerAddress()
    {
        var gamemanager = GameManager.Instance;
        assetReferenceGameObject.InstantiateAsync().Completed +=
        (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                GameObject player = handle.Result;
                gamemanager.SpawnPlayer(player);
            }
            else
            {
                Debug.LogError($"Failed to instantiate player: {handle.DebugName}");
            }
        };
    }
}
