using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public struct Data {
    public string Version;
    public string Url;
}

public class NewUpdatePopupUI : MonoBehaviour
{
    [SerializeField] GameObject uiCanvas;
    [SerializeField] Button uiNotNowButton;
    [SerializeField] Button uiUpdateButton;

    string jsonURL = "https://drive.google.com/uc?export=download&id=1s-cMUwVIocmz1g7-3f0ahaB6PRc8mFIe";
    bool isAlreadyCheckedForUpdates = false;

    Data latestGameData;

    // Start is called before the first frame update
    void Start()
    {
        if(!isAlreadyCheckedForUpdates)
            StartCoroutine(CheckForUpdates(jsonURL));
    }

    IEnumerator CheckForUpdates(string url)
    {
        UnityWebRequest request = UnityWebRequest.Get(url);

        yield return request.SendWebRequest();


        isAlreadyCheckedForUpdates = true;

        if (!(request.isNetworkError)){
            latestGameData = JsonUtility.FromJson<Data>(request.downloadHandler.text);
            print(latestGameData.Version + " " + Application.version);
            if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
            {
                // new update is available
                ShowPopup();
            }
        }

        request.Dispose();
    }

    void ShowPopup()
    {
        // Add buttons click listeners :
        uiNotNowButton.onClick.AddListener(() => {
            HidePopup();
        });

        uiUpdateButton.onClick.AddListener(() => {
            Application.OpenURL(latestGameData.Url);
            HidePopup();
        });

        uiCanvas.SetActive(true);
    }

    void HidePopup()
    {
        uiCanvas.SetActive(false);

        // Remove buttons click listeners :
        uiNotNowButton.onClick.RemoveAllListeners();
        uiUpdateButton.onClick.RemoveAllListeners();
    }


    void OnDestroy()
    {
        StopAllCoroutines();
    }
}
