using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public static GUIManager instance;

    [SerializeField]
    private int goal;
    [SerializeField]
    private int moveCounter;

    public Text yourScoreTxt;
    public Text highScoreTxt;
    public Text goalTxt;
    public Text finalTxt;

    [SerializeField]
    string levelScore;
    [SerializeField]
    string levelResult;

    public Text scoreTxt;
    public Text moveCounterTxt;

    private int score = 0;

    public GameObject panelGameOver;

    public GameObject nextButton;
    private void Awake()
    {
        instance = GetComponent<GUIManager>();
        moveCounterTxt.text = moveCounter.ToString();
        goalTxt.text = goal.ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }
    int GetLevelNumber(string levelResult)
    {
        int value;
        int.TryParse(string.Join("", levelResult.Where(c => char.IsDigit(c))), out value);
        return value;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTxt.text = score.ToString();
    }

    public void Score(int value)
    {
        score += value;
        scoreTxt.text = score.ToString();
        yourScoreTxt.text = "Счет: " + score.ToString();
        
    }

    public void Moves(int value)
    {
        moveCounter -= value;
        moveCounterTxt.text = moveCounter.ToString();
    }
    public void AddMoves(int value)
    {
        moveCounter += value;
        moveCounterTxt.text = moveCounter.ToString();
    }
    private void GameOver()
    {
        yourScoreTxt.text = "Счет: " + score.ToString();
        if (score < goal)
        {
            finalTxt.text = "Проигрыш!";
        }
        else if (score >= goal)
        {
            PlayerPrefs.SetInt(levelResult, 1);
            PlayerPrefs.SetInt("level" + (GetLevelNumber(levelResult) + 1) + "_allow", 1);
            finalTxt.text = "Победа!";
            if(GetLevelNumber(levelResult) == 24)
                nextButton.SetActive(false);
            else
                nextButton.SetActive(true);
        }
        if (score > PlayerPrefs.GetInt(levelScore))
        {
            PlayerPrefs.SetInt(levelScore, score);
            highScoreTxt.text = "Рекорд: " + score.ToString();
        }
        else
        {
            highScoreTxt.text = "Рекорд: " + PlayerPrefs.GetInt(levelScore);
        }
        panelGameOver.SetActive(true);
    }

    public void isGameOver()
    {
        if (moveCounter <= 0 || score >= goal)
        {
            moveCounter = 0;
            StartCoroutine(WaitForShifting());
        }
    }
    private IEnumerator WaitForShifting()
    {
        yield return new WaitUntil(() => MainGame.instance.killedPiece);
        yield return new WaitForSeconds(.25f);
        GameOver();
    }
    public void LoadNextLevel()
    {
        LoadLevel.targetScene = "level" + (GetLevelNumber(levelResult) + 1);
        SceneManager.LoadScene("LoadScreen");
    }
}
