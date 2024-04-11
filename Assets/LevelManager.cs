using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public GameObject transitionContainer;
    private SceneTransition[]  transitions;
    public Slider progressBar;
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
        transitions = transitionContainer.GetComponentsInChildren<SceneTransition>();
    }

    public void LoadScene(string sceneName, string transitionName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, transitionName));
    }

    private IEnumerator //LoadSceneAsync(string sceneName, string transitionName)
    {
        //Scenetransition transition = transitions.First(t =< transitionContainer.name )
    }
}
