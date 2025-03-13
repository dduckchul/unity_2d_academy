using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AssetBundleLoader : MonoBehaviour
{
    private string _bundlePath = "Assets/AssetBundles/stage1barrel"; // 에셋 번들 위치
    private string url = "https://docs.google.com/uc?export=download&id=1lc8bDupmNzdHsDfAPukpzLgOKnxD7Ca2";
    
    private void Start()
    {
        // AssetBundle bundle = AssetBundle.LoadFromFile(_bundlePath);
        StartCoroutine(AssetFromWeb(url));
    }

    IEnumerator AssetFromWeb(string url)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("에셋 다운로드 error");
            yield break;
        }

        AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
        
        if (bundle == null)
        {
            Debug.Log("에셋 번들 못 찾음");
            yield break;;
        }
        
        // LoadAllTest(bundle);

        // 모든 번들의 이름 조회하기
        string[] assetNames = bundle.GetAllAssetNames();
        
        foreach (var asset in assetNames)
        {
            Debug.Log("로드된 에셋 :" + asset);
        }

        // 배럴이란 이름의, GameObject 형식의 에셋을 불러와서 반환해줌
        var barrel = bundle.LoadAsset<GameObject>("Barrel");

        if (barrel != null)
        {
            Instantiate(barrel);
        }
        else
        {
            Debug.Log("배럴 없는데용?");
        }

        var mat = bundle.LoadAsset<Material>("Barrel-Barrel");

        if (mat != null)
        {
            Debug.Log(mat);
        }
        else
        {
            Debug.Log("mat 없는데용?");
        }        
        
        
        /* 근데 에셋 번들은 옛날 레거시에서 쓰는 회사에서 보통 많이씀, 이후에 Addressable 배울것이다. */
        // bundle.Unload(true);    // 번들은 해제가 되고, 모든 에셋도 메모리서 해제, 이 리소스 가지고 만든 정보들도 같이 날아간다
        // bundle.Unload(false);   // 번들은 해제가 되고, 번들 내에서 로드된 에셋은 메모리서 유지된다. (번들만 언로드)          
    }

    public void LoadAllTest(AssetBundle bundle)
    {
        // 모든 번들 가져오기
        UnityEngine.Object[] allAssets = bundle.LoadAllAssets();

        foreach (var asset in allAssets)
        {
            Debug.Log("로드된 에셋 :" + asset.name + "(" + asset.GetType() + ")");
        }
    }

}
