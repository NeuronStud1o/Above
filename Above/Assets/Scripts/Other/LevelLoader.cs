using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;

    public int loadSceneIndex = 0;

    System.Random random = new System.Random();

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
            slider.value += 0.05f;
            m += 0.05f;
            yield return new WaitForSeconds(0.1f);
            print(slider.value);
        }

        yield return new WaitForSeconds(1);

        n += random.Next(5, 9);

        while (m < n / 10)
        {
            slider.value += 0.05f;
            m += 0.05f;
            yield return new WaitForSeconds(0.1f);
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