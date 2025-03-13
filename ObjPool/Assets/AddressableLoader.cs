using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableLoader : MonoBehaviour
{
    // public string prefabName = "AuroraModel";
    public AssetReference aurora;
    
    void Start()
    {
        // 지정한 이름으로 로딩
        // Addressables.LoadAssetAsync<GameObject>(prefabName).Completed += OnPrefabLoaded;
        aurora.LoadAssetAsync<GameObject>().Completed += OnPrefabLoaded;
    }

    void OnPrefabLoaded(AsyncOperationHandle<GameObject> op)
    {
        if (op.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(op.Result);
        }
    }
}
