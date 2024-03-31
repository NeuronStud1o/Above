using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.IO;

[System.Serializable]
public struct Events
{
    [System.Serializable]
    public struct WWWEvents
    {
        public string name;
        public string description;
        public string url;
        public string imageUrl;
    }

    public List<WWWEvents> currentEvents;
}

public class EventsReaderWWW : MonoBehaviour
{
    [SerializeField] private string jsonURL = "https://drive.google.com/uc?export=download&id=1NbNsgypJ3wY59WzB7ymv1hOfsS38YW5O";
    [SerializeField] private GameObject eventPrefab;
    [SerializeField] private GameObject layout;

    private Events events;

    IEnumerator Start()
    {
        yield return StartCoroutine(GetJsonData());
        InstanceEvents();
    }

    private IEnumerator GetJsonData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(jsonURL))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                events = JsonUtility.FromJson<Events>(request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Error loading JSON: " + request.error);
            }
        }
    }

    private void InstanceEvents()
    {
        foreach (Events.WWWEvents www in events.currentEvents)
        {
            GameObject i = Instantiate(eventPrefab);
            i.transform.SetParent(layout.transform);

            i.transform.localScale = new Vector3(1, 1, 1);
            

            EventPrefab go = i.GetComponent<EventPrefab>();
            go.Title.text = www.name;
            go.Description.text = www.description;

            StartCoroutine(GetImage(www.imageUrl, go.Icon));

            if (www.url == "")
            {
                go.Url.gameObject.SetActive(false);
            }
            go.Url.onClick.AddListener(() => OpenUrl(www.url));
        }
    }

    private IEnumerator GetImage(string url, Image icon)
    {
        using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Texture2D texture = DownloadHandlerTexture.GetContent(request);
                icon.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
            else
            {
                Debug.LogError("Error loading image: " + request.error);
            }
        }
    }

    private void OpenUrl(string url)
    {
        Application.OpenURL(url);
    }
}