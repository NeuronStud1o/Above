using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider slider;

    [SerializeField] private int loadSceneIndex = 0;

    System.Random random = new System.Random();

    private WaitForSeconds shortDelay = new WaitForSeconds(0.03f);

    public void StartGame()
    {
        StartCoroutine(LoadAsynchronously(loadSceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        loadingScreen.SetActive(true);

        yield return new WaitForSeconds(1);

        float n = random.Next(1, 4);
        float m = 0;

        while(m < n / 10)
        {
            slider.value += 0.02f;
            m += 0.02f;
            yield return shortDelay;
            print(slider.value);
        }

        yield return new WaitForSeconds(1);

        n += random.Next(5, 9);

        while (m < n / 10)
        {
            slider.value += 0.05f;
            m += 0.05f;
            yield return shortDelay;
            print(slider.value);
        }

        yield return new WaitForSeconds(1);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01((operation.progress / 0.9f) + n / 10);
            print(slider.value);
            slider.value = progress;
            yield return null;
        }
    }
}