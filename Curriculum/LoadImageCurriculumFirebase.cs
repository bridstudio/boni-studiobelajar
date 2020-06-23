using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Firebase.Storage;
using Firebase.Database;

public class LoadImageCurriculumFirebase : MonoBehaviour
{
    FirebaseStorage storage;
    StorageReference imageStorage_ref;
    public GameObject logoImage;
    public GameObject dialogLoading;    
    public GameObject curriculumRowConfig;
    public Button playButton;
    private string curriculumID, sceneToLoad;    

    void Start()
    {        
        OpenDialogLoading(true);
        playButton.onClick.AddListener(() => LoadGameScene());

        storage = FirebaseStorage.DefaultInstance;
        
        curriculumID = curriculumRowConfig.GetComponent<CurriculumRowConfig>().id.text.ToString();
        imageStorage_ref = storage.GetReferenceFromUrl("gs://boni-studiobelajaranak.appspot.com/public/app-curriculum/image" + curriculumID + ".jpg");
        
        DownloadImage();
        SceneToLoadString();
    }    
    
    private void SceneToLoadString()
    {
        if(curriculumID == "1")
        {
            sceneToLoad = "AlbumColoring";
        }
        else if(curriculumID == "2")
        {
            sceneToLoad = "Drag-N-Drop";
        }
        else if(curriculumID == "3")
        {
            sceneToLoad = "Album";
        }
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void OpenDialogLoading(bool dialog)
    {
        dialogLoading.SetActive(dialog);
    }

    private async void DownloadImage()
    {
        try
        {
            Uri uri = await imageStorage_ref.GetDownloadUrlAsync();
            StartCoroutine(LoadCoroutine(uri));
        }
        catch(Exception ex)
        {
            Debug.LogError(ex.InnerException.Message);
        }
    }

    private IEnumerator LoadCoroutine(Uri uri)
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

                Transform thumbnail = logoImage.transform;
                thumbnail.GetComponent<Image>().sprite = sprite;
            }
        }
    }
}
