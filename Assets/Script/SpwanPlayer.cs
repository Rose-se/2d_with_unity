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
        assetReferenceGameObject.InstantiateAsync().Completed +=
        (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                
            }
            else
            {
                Debug.LogError($"Failed to instantiate player: {handle.DebugName}");
            }
        };
    }
}
