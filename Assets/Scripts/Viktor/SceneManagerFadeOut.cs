using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagerFadeOut : MonoBehaviour
{
    public static SceneManagerFadeOut instance;
    public Image fadeImage;
    public TextMeshProUGUI loadingText;
    public float fadeDuration = 1.0f;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void TransitionToScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoadScene(sceneName));
    }

    private IEnumerator FadeIn()
    {
        fadeImage.gameObject.SetActive(true); // Enable the image

        float elapsedTime = 0f;
        Color imageColor = fadeImage.color;
        Color textColor = loadingText.color;
        imageColor.a = 1f;
        textColor.a = 1f;
        fadeImage.color = imageColor;
        loadingText.color = textColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            textColor.a = Mathf.Clamp01(1.0f - (elapsedTime / fadeDuration));
            fadeImage.color = imageColor;
            loadingText.color = textColor;
            yield return null;
        }

        fadeImage.gameObject.SetActive(false); // Disable the image
    }

    private IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        fadeImage.gameObject.SetActive(true); // Enable the image

        float elapsedTime = 0f;
        Color imageColor = fadeImage.color;
        Color textColor = loadingText.color;
        imageColor.a = 0f;
        textColor.a = 0f;
        fadeImage.color = imageColor;
        loadingText.color = textColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            textColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = imageColor;
            loadingText.color = textColor;
            yield return null;
        }

        SceneManager.LoadScene(sceneName);

        // Wait for the scene to load, then fade in
        yield return new WaitUntil(() => SceneManager.GetActiveScene().name == sceneName);
        StartCoroutine(FadeIn());
    }
}
