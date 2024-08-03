using System.Collections;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OtherSettings : MonoBehaviour
{
    [SerializeField] private ControllerOtherSettings controller;

    [Header ("## Rename nickname : ")]
    [SerializeField] private TMP_InputField renameNickField;
    [SerializeField] private Button renameNickButton;
    [SerializeField] private GameObject renameNickPanel;
    [SerializeField] private TextMeshProUGUI renameNickcoinsCountText;

    public void ShowLevelRanks(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.data.otherSettings.showLevelRanks = true;
        }
        else
        {
            JsonStorage.instance.data.otherSettings.showLevelRanks = false;
        }

        controller.ShowLevelRanks(tog);

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }

    public void OpenDocumentation()
    {
        Application.OpenURL("https://docs.google.com/document/d/1ANJd7XmfpLubOtsssmON4Fuc_4nhoc4LQUoeockF8RY/edit?usp=sharing");
    }

    public void PlayTutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    public void Logout()
    {
        SceneManager.LoadSceneAsync("Authentication");
    }

    public async void DeleteAccount()
    {
        await AccountDeleting();
    }

    async Task AccountDeleting()
    {
        await StorageData.instance.DeleteUser();

        Debug.Log("Data is deleted");

        GameManager.instance.SetActiveLoadingScreen(false);
        SceneManager.LoadSceneAsync("Authentication");
    }

    public void OpenRanameNicknamePanel()
    {
        renameNickPanel.SetActive(true);

        if (JsonStorage.instance.data.userData.coinsS >= 5)
        {
            renameNickButton.interactable = true;
            renameNickcoinsCountText.color = Color.white;
        }
        else
        {
            renameNickButton.interactable = false;
            renameNickcoinsCountText.color = Color.red;
        }
    }

    public void RenameNick()
    {
        StartCoroutine(TryRenameNickname());
    }

    private IEnumerator TryRenameNickname()
    {
        if (JsonStorage.instance.data.userData.coinsS >= 5)
        {
            UserProfile profile = new UserProfile { DisplayName = renameNickField.text };

            var updateProfileTask = UserData.instance.User.UpdateUserProfileAsync(profile);

            yield return new WaitUntil(() => updateProfileTask.IsCompleted);

            if (updateProfileTask.Exception != null)
            {
                Debug.LogError(updateProfileTask.Exception);

                FirebaseException firebaseException = updateProfileTask.Exception.GetBaseException() as FirebaseException;
                AuthError authError = (AuthError)firebaseException.ErrorCode;
            }
            else
            {
                JsonStorage.instance.data.userData.coinsS -= 5;

                StorageData.instance.SaveJsonData();

                SceneManager.LoadSceneAsync(1);
            }
        }
    }

    public void ActivateParticles(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.data.otherSettings.particles = true;
        }
        else
        {
            JsonStorage.instance.data.otherSettings.particles = false;
        }

        controller.ActivateParticles(tog);

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }

    public void ShowTheSelectedBoost(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.data.otherSettings.showSelectedBoostInGame = true;
        }
        else
        {
            JsonStorage.instance.data.otherSettings.showSelectedBoostInGame = false;
        }

        controller.ShowTheSelectedBoost(tog);

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }

    public void CameraShake(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.data.otherSettings.cameraShake = true;
        }
        else
        {
            JsonStorage.instance.data.otherSettings.cameraShake = false;
        }

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }

    public void Vibration(bool tog)
    {
        if (tog == true)
        {
            JsonStorage.instance.data.otherSettings.vibration = true;
        }
        else
        {
            JsonStorage.instance.data.otherSettings.vibration = false;
        }

        CryptoHelper.Encrypt(JsonStorage.instance.data, JsonStorage.instance.password);
    }
}
