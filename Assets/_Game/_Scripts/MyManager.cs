using BenStudios.ScreenManagement;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class MyManager : MonoBehaviour
{
    
    //[ContextMenu("FetchData")]
    //public void FetchData()
    //{
    //    StartCoroutine(ContactServer());
    //}
    //private IEnumerator ContactServer()
    //{

    //    //using (UnityWebRequest request = UnityWebRequest.Get(_jsonDataApiURL))
    //    //{
    //    //    yield return request.SendWebRequest();
    //    //    if (request.result == UnityWebRequest.Result.Success)
    //    //    {
    //    //        string jsonData = request.downloadHandler.text;
    //    //        data = MyUtils.GetObjectFromJsonString<RewardsData>(jsonData);
    //    //    }
    //    //}
    //    //using (UnityWebRequest request1 = UnityWebRequest.Get(data.rewards[0].currency_image))
    //    //{
    //    //    yield return request1.SendWebRequest();
    //    //    //if (request1.result == UnityWebRequest.Result.Success)
    //    //    //{
    //    //    //    Texture2D tex = ((DownloadHandlerTexture)request1.downloadHandler).texture;
    //    //    //    _image.sprite = Sprite.Create(tex, new Rect(), Vector2.one * .5f);

    //    //    //}

    //    //    if (request1.result == UnityWebRequest.Result.Success)
    //    //    {

    //    //        Debug.Log("Server returned an error response code: " + request1.responseCode);
    //    //        Texture2D texture = new Texture2D(1, 1);
    //    //        if (texture.LoadImage(request1.downloadHandler.data))
    //    //        {
    //    //            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
    //    //            _image.sprite = sprite;
    //    //        }
    //    //    }
    //    //    else
    //    //    {
    //    //        Debug.LogError("Server returned an error response code: " + request1.responseCode);
    //    //    }




    //    //}

    //}
    private void Start()
    {
        ScreenManager.Instance.ChangeScreen(Window.OpenRewardsScreen);
    }
}
