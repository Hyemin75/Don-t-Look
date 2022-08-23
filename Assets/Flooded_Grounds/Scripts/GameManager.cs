using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{ 
    private static GameManager instance;
    private bool IsGameOver;
    private bool IsGameEnd;


    public bool isGameOver
    {
        get => IsGameOver; set => IsGameOver = value; 
    }

    public bool isGameEnd
    {
        get => IsGameEnd; set => IsGameEnd = value;
    }


    private void Awake()
    { 
        IsGameOver = false;
    }

    private void Update()
    {
        if(IsGameOver == true)
        {
            HUD.Instance.OnGameOverUI();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            SceneManager.LoadScene(0); // 로드 씬으로 객체들 파괴로 조치 필요
            HUD.Instance.OnGamePlayUI();
        }

        if(IsGameEnd)
        {
            HUD.Instance.OnGameEndUI();
        }
    }
}

