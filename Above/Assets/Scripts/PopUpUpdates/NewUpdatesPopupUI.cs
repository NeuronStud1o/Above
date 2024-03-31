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

        [System.Obsolete]
        void Start()
        {
            if (!isAlreadyCheckedForUpdates)
            {
                StopAllCoroutines();
                StartCoroutine(CheckForUpdates());
            }
        }

        [System.Obsolete]
        private IEnumerator CheckForUpdates()
        {
            UnityWebRequest request = UnityWebRequest.Get(jsonDataURL);
            request.chunkedTransfer = false;
            request.disposeDownloadHandlerOnDispose = true;
            request.timeout = 60;

            yield return request.SendWebRequest();

            if (request.isDone)
            {
                isAlreadyCheckedForUpdates = true;

                if (!request.isNetworkError && !request.isHttpError)
                {
                    latestGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);

                    if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
                    {
                        Debug.Log("Latest game version: " + latestGameData.Version + " and latest game version on device: " + Application.version);
                        uiDescriptionText.text = latestGameData.Description;
                        ShowPopup();
                    }

                    if (!string.IsNullOrEmpty(latestGameData.TechnicalBreak) && latestGameData.TechnicalBreak == "true")
                    {
                        ShowTechPopup();
                    }
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
            //uiNotNowButton.onClick.AddListener (() => { HidePopup(); });

            uiUpdateButton.onClick.AddListener (() => { Application.OpenURL(latestGameData.Url); HidePopup(); });

            updateCanvas.SetActive (true);
        }

        void HidePopup()
        {
            updateCanvas.SetActive (false);

            //uiNotNowButton.onClick.RemoveAllListeners();
            uiUpdateButton.onClick.RemoveAllListeners();
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        } 
    }
}
