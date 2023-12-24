using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using System;

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
        [SerializeField] Button uiNotNowButton;
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

            yield return request.Send();

            if (request.isDone)
            {
                isAlreadyCheckedForUpdates = true;

                if (!request.isError)
                {
                    latestGameData = JsonUtility.FromJson<GameData>(request.downloadHandler.text);

                    if (!string.IsNullOrEmpty(latestGameData.Version) && !Application.version.Equals(latestGameData.Version))
                    {
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
            uiNotNowButton.onClick.AddListener (() => { HidePopup(); });

            uiUpdateButton.onClick.AddListener (() => { Application.OpenURL(latestGameData.Url); HidePopup(); });

            updateCanvas.SetActive (true);
        }

        void HidePopup()
        {
            updateCanvas.SetActive (false);

            uiNotNowButton.onClick.RemoveAllListeners();
            uiUpdateButton.onClick.RemoveAllListeners();
        }

        void OnDestroy()
        {
            StopAllCoroutines();
        } 
    }
}
