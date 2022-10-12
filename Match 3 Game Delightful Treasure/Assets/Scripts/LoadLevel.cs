using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField]
    Image _loadingBar;
    public static string targetScene { get; set; }

    public void Awake()
    {
    }
    IEnumerator LoadNextLevel()
    {
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(targetScene);
        loadLevel.allowSceneActivation = false;
        while (!loadLevel.isDone)
        {
            _loadingBar.fillAmount = loadLevel.progress;

            if(loadLevel.progress >= 0.9f)
            {
                loadLevel.allowSceneActivation = true;
            }
            yield return null;
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(LoadNextLevel());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}