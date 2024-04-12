using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void Start()
    {
        Resources.LoadAll("Animator");
    }
    public void LoadScene(string aSceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(aSceneName);
    }


}
