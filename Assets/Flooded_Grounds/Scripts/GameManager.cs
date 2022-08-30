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

    [SerializeField]
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

        HUD = FindObjectOfType<HUD>(); 

        //HUD.Instance.OnGamePlayUI();
    }

    private void Update()
    {

        if(IsGameRestart)
        {
            HUD.OnGamePlayUI();
        }

        if(IsGameOver)
        {
            isGameRestart = false;
            HUD.OnGameOverUI();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            IsGameOver = false;
            IsGameRestart = true;
            SceneManager.LoadScene(0); // 로드 씬으로 객체들 파괴로 조치 필
        }

        if(IsGameEnd)
        {
            player.SetActive(false);
            HUD.OnGameEndUI();
        }
    }

}

