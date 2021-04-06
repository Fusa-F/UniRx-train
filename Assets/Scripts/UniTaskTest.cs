using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System;

/// <summary>
/// 
/// </summary>
public class UniTaskTest : MonoBehaviour
{
    private async void Start()
    {
        var result = await Work();
        print(result);

        // // テクスチャDL->貼り付け
        // var uri =
        //     "https://2.bp.blogspot.com/-tcLjNKJqOIQ/WkXHUuSC4qI/AAAAAAABJX0/ArQTS8DS9SEOJI4Mb5tvZg4GXuoED8iIQCLcBGAs/s800/otaku_winter.png";

        // //テクスチャをダウンロード
        // var texture = await DownloadTexture(uri);

        // // テクスチャセット
        // this.gameObject.GetComponent<Renderer>().material.mainTexture = texture;

        var webResult = await DLWeb();
        print(webResult);
    }

    async UniTask<string> Work()
    {
        return await Task.Run(() => "Hello");
    }

    // テクスチャをダウンロードする
    async UniTask<Texture> DownloadTexture(string uri)
    {
        // 適当に画像のURL

        var r = UnityWebRequestTexture.GetTexture(uri);

        await r.SendWebRequest(); // UnityWebRequestをawaitできる

        return DownloadHandlerTexture.GetContent(r);
    }

    // URL先のHTMLをDL
    async UniTask<string> DLWeb()
    {
        var request = UnityWebRequest.Get("https://unity3d.com");

        // ToUniTaskで途中経過取得
        var result = await request.SendWebRequest().ToUniTask(Progress.Create<float>(x => Debug.Log(x)));

        return result.downloadHandler.text;
    }
}
