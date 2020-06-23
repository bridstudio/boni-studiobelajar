using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using Firebase.Storage;

public class LoadImagePost : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference imageStorage_ref;
    public GameObject postImage;
    public GameObject dialogLoading;
    public GameObject postRowConfig;
    private string _imageUrl;    

    void Start()
    {
        _imageUrl = postRowConfig.GetComponent<PostRowConfig>().imageUrl.text.ToString();
        OpenDialogLoading(true);
        // string pushKey = Router.Post().Push().Key;
        storage = FirebaseStorage.DefaultInstance;
        imageStorage_ref = storage.GetReferenceFromUrl("gs://boni-studiobelajaranak.appspot.com/public/app-home/image/" + _imageUrl + ".jpg");
        Debug.Log(imageStorage_ref);

        DownloadingImage();
    }

    private void OpenDialogLoading(bool dialog)
    {
        dialogLoading.SetActive(dialog);
    }

    public async void DownloadingImage()
    {
        try
        {
            Uri uri = await imageStorage_ref.GetDownloadUrlAsync();
            StartCoroutine(GetImageFromFirebase(uri));
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
        }
    }

    private IEnumerator GetImageFromFirebase(Uri uri)
    {
        using(UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri))
        {
            yield return www.SendWebRequest();

            if(www.isNetworkError || www.isHttpError)
            {
                Debug.LogError(www.error);
            }
            else
            {
                OpenDialogLoading(false);
                Texture2D temp = ((DownloadHandlerTexture)www.downloadHandler).texture;

                Sprite sprite = Sprite.Create(temp, new Rect(0, 0, temp.width, temp.height), new Vector2(0.5f, 0.5f));

                Transform thumbnail = postImage.transform;
                thumbnail.GetComponent<Image>().sprite = sprite;
                thumbnail.GetComponent<Image>().SetNativeSize();
            }
        }
    }
}
