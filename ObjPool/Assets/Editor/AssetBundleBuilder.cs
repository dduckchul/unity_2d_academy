using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // 유니티 에디터용 스크립트


public class AssetBundleBuilder
{
    [MenuItem("Assets/BuildAssetBundles")] // 메뉴 아이템을 생성할 때 달아줌
    static void BuildAllAssetBundles()
    {
        string assetBundleDir = "Assets/AssetBundles";  // 주소는 오타 없을것

        if (!System.IO.Directory.Exists(assetBundleDir))
        {
            System.IO.Directory.CreateDirectory(assetBundleDir);
        }

        BuildPipeline.BuildAssetBundles(
            assetBundleDir,  // 경로
            BuildAssetBundleOptions.None, // 압축 방식
            BuildTarget.StandaloneOSX  // 타깃 플랫폼
        );
    }
}
