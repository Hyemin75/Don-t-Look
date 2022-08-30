using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{ 
    private static GameManager instance;
    private static bool IsGameStart;
    private bool IsGameOver;
    private bool IsGameEnd;

    [SerializeField]
    GameObject player;

    public bool isGameStart
    {
        get => IsGameStart; set => IsGameStart = value;
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
        IsGameStart = false;
       
        //HUD.Instance.OnGamePlayUI();
    }

    private void Update()
    {

        if(IsGameStart )
        {
            HUD.Instance.OnGamePlayUI();
        }

        if(IsGameOver)
        {
            HUD.Instance.OnGameOverUI();
        }
        
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
        {
            IsGameOver = false;
            SceneManager.LoadScene(0); // 로드 씬으로 객체들 파괴로 조치 필요
            HUD.Instance.OnGamePlayUI();
        }

        if(IsGameEnd)
        {
            player.SetActive(false);
            HUD.Instance.OnGameEndUI();
        }
    }

}

