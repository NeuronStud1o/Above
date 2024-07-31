using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;

namespace UpgradeSystem
{
    struct GameData
    {
        public string Description;
        public string Version;
        public string Url;
        public string TechnicalBreak;
    }

    public class NewUpdatesPopupUI : MonoBehaviour
    {
        public static NewUpdatesPopupUI instance;

        [Header ("## UI References :")]
        [SerializeField] GameObject updateCanvas;
        [SerializeField] GameObject techCanvas;
        [SerializeField] Button uiUpdateButton;
        [SerializeField] TextMeshProUGUI uiDescriptionText;

        [Space (20f)]
        [Header ("## Settings :")]
        [SerializeField] [TextArea (1, 5)] string jsonDataURL;

        static bool isAlreadyCheckedForUpdates = false;

        GameData latestGameData;

        void Awake()
        {
            instance = this;
        }

        public void StartAction()
        {
            if (!isAlreadyCheckedForUpdates)
            {
                Debug.Log("is not already checked");
                StopAllCoroutines();
                StartCoroutine(CheckForUpdates());
            }
        }

        
        private IEnumerator CheckForUpdates()
        {
            UnityWebRequest request = UnityWebRequest.Get(jsonDataURL);
            request.disposeDownloadHandlerOnDispose = true;
            request.timeout = 60;

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                GameManager.instance.SetActiveLoadingScreen(true);
                GameManager.instance.SetMessage("Error");
            }
            else
            {
                isAlreadyCheckedForUpdates = true;

                latestGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);

                if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
                {
                    Debug.Log("Latest game version: " + latestGameData.Version + " and latest game version on device: " + Application.version);
                    uiDescriptionText.text = latestGameData.Description;
                    ShowPopup();
                }
                else if (!string.IsNullOrEmpty(latestGameData.TechnicalBreak) && latestGameData.TechnicalBreak == "true")
                {
                    ShowTechPopup();
                }
                else
                {
                    Debug.Log("No updates");
                    FirebaseAuthManager.instance.StartAction();
                }
            }

            request.Dispose();
        }

        void ShowTechPopup()
        {
            techCanvas.SetActive(true);
        }

        void ShowPopup()
        {
            uiUpdateButton.onClick.AddListener (() => { Application.OpenURL(latestGameData.Url); });

            updateCanvas.SetActive (true);
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        } 
    }
}
