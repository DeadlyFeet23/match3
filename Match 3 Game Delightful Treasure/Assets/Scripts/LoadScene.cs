using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    [SerializeField]
    private GameObject helpPanel;
    [SerializeField]
    private GameObject settingsPanel;
    [SerializeField]
    private GameObject allowPanel;
    [SerializeField]
    private GameObject pausePanel;
    [SerializeField]
    List<GameObject> buttonList;
    [SerializeField]

    public Image chest;

    public AudioMixerGroup grp;

    public Sprite openChest;
    public Sprite closeChest;

    public GameObject diamods;

    int hasPlayed;
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenChest()
    {
        if(chest.sprite == closeChest)
        {
            chest.sprite = openChest;
            diamods.SetActive(true);
        }
        else
        {
            chest.sprite = closeChest;
            diamods.SetActive(false);
        }
        
    }

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }
    public void SceneLoad(string name)
    {
        LoadLevel.targetScene = name;
        SceneManager.LoadScene("LoadScreen");
    }
    public void LevelLoad(string name)
    {
        if(PlayerPrefs.GetInt(name + "_allow") == 1)
        {
            LoadLevel.targetScene = name;
            SceneManager.LoadScene("LoadScreen");
        }
        else
        {
            SFXManager.instance.PlaySFX(Clip.Error);
        }
    }
    public void OpenHelp()
    {
        helpPanel.SetActive(true);
    }
    public void CloseHelp()
    {
        helpPanel.SetActive(false);
    }
    public void QuitGame()
    {
        // If we are running in a standalone build of the game
        #if UNITY_ANDROID
			// Quit the application
			Application.Quit();
        #endif

        // If we are running in the editor
        #if UNITY_EDITOR
        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
    public void ContinueGame()
    {
        pausePanel.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        //PlayerPrefs.DeleteAll();
        hasPlayed = PlayerPrefs.GetInt("hasPlayed");
        PlayerPrefs.SetInt("level1_allow", 1);
        for (int i = 0; i < buttonList.Count; i++)
        {
            if (PlayerPrefs.GetInt("level" + (i + 1) + "_result") == 1)
            {
                buttonList[i].GetComponent<Image>().color = Color.green;
            }
        }
        if(hasPlayed == 0)
        {
            PlayerPrefs.SetInt("sfx_on", 1);
            PlayerPrefs.SetInt("music_on", 1);
            PlayerPrefs.SetInt("hasPlayed", 1);
        }
        OnSoundsInAndroid();
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android ||
            Application.platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pausePanel.SetActive(true);
            }
        }
    }
    void OnSoundsInAndroid()
    {
        if(PlayerPrefs.GetInt("sfx_on") == 0)
            grp.audioMixer.SetFloat("sfx_volume", -80);
        else
            grp.audioMixer.SetFloat("sfx_volume", -13);
        if (PlayerPrefs.GetInt("music_on") == 0)
            grp.audioMixer.SetFloat("music_volume", -80);
        else
            grp.audioMixer.SetFloat("music_volume", 0);
    }
}
