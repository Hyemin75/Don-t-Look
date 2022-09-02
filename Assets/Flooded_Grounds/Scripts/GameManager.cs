using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class GameManager : SingletonBehaviour<GameManager>
{ 
    private static GameManager instance;
    private bool IsGameRestart;
    private bool IsGameOver;
    private bool IsGameEnd;
    
    [HideInInspector]
    public bool IsGameStart;
    [HideInInspector]
    public bool IsMain;

    GameObject player;

    private static HUD HUD;

    public Action<int> EventGameSequence { get; private set; }
    public Action<int> EventGameStart { get; private set; }

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
        EventGameSequence += OnGameUI;
        
        IsGameStart = false;
    }

    private void Start()
    {
        HUD = FindObjectOfType<HUD>();
        
    }

    private void Update()
    {
        if(IsGameStart)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }

        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            IsGameOver = false;
            IsGameRestart = true;
            SceneManager.LoadScene(0);
        }
    }

    public void OnGameUI(int _type)
    {
        switch(_type)
        {
            case 0: HUD.OnGamePlayUI(); HUD.OffGamePuseUI(); break;
            case 1: HUD.OnGameOverUI(); IsGameOver = true; break;
            case 2: HUD.OnGameEndUI(); IsGameEnd = true; break;
            case 3: HUD.OnGamePausUI(); break;  
            case 4: SceneManager.LoadScene(0); break;
        }
    }

}

