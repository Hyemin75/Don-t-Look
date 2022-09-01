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
        player = GameObject.Find("Player");    
    }


    private void Start()
    {
        HUD = FindObjectOfType<HUD>();
    }
    private void Update()
    {

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
                HUD.OnGamePlayUI();
                SceneManager.LoadScene(0);
            }
        }
        
        if(IsGameEnd)
        {
            HUD.OnGameEndUI();
        }
    }

}

