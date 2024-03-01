using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadScene(string aSceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(aSceneName);
    }


}
