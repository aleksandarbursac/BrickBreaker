using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameStatus : MonoBehaviour
{
    //config
    [Range(0.1f,10f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] TextMeshProUGUI scoreText;
    //state vars
    //TODO Redesign score to be variable on block level, not on gamestatus level 
    [SerializeField] int currentScore = 0;
    [SerializeField] int blockScoreWorth = 80;
    [SerializeField] bool isAutoPlayEnabled;

    //always loads before start
    void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        scoreText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText.text = currentScore.ToString();
    }

    public void IncreaseScore()
    {
        currentScore += blockScoreWorth;
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }

}
