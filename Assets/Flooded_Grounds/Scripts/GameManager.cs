using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : SingletonBehaviour<GameManager>
{ 
    private static GameManager instance;
    private bool IsGameOver;

    [SerializeField]
    HUD hud;

    public bool isGameOver
    {
        get => IsGameOver; set => IsGameOver = value; 
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
    }


    public void EndGame()
    {
        isGameOver = true;
        HUD.Instance.OnGameEndUI();
    }
}

