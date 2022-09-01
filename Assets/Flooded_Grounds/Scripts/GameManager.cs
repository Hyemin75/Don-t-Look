using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{ 
    private static GameManager instance;
    private bool IsGameRestart;
    private bool IsGameOver;
    private bool IsGameEnd;
    private static HUD HUD;

    GameObject player;

    public bool isGameRestart
    {
        get => IsGameRestart; set => IsGameRestart = value;
    }

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
        IsGameEnd = false;
        IsGameRestart = false;

        player = GameObject.Find("Player");    

        HUD = FindObjectOfType<HUD>(); 

        //HUD.Instance.OnGamePlayUI();
    }

    private void Update()
    {
        Cursor.visible = false;

        if (IsGameRestart)
        {
            HUD.OnGamePlayUI();
        }

        if(IsGameOver)
        {
            HUD.OnGameOverUI();
            if (Input.GetKeyDown(KeyCode.R))
            {
                IsGameOver = false;
                IsGameRestart = true;
                SceneManager.LoadScene(0); // 로드 씬으로 객체들 파괴로 조치 필
            }
        }
        
        if(IsGameEnd)
        {
            HUD.OnGameEndUI();
        }
    }

}

