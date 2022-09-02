using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Image darkImage;

    FadeEffect fadeEffect;

    [SerializeField]
    GameObject MainUI;
    [SerializeField]
    GameObject GamePlayUI;
    [SerializeField]
    GameObject GameOverUI;
    [SerializeField]
    GameObject GameEndUI;
    [SerializeField]
    GameObject GameExplainUI;
    [SerializeField]
    GameObject GamePauseUI;

    [SerializeField]
    GameObject playerCamera;
    [SerializeField]
    GameObject mainSceneCamera;
    [SerializeField]
    GameObject EndCamera;

    GameObject MainSceneAngel;

    private void Awake()
    {
        MainSceneAngel = GameObject.Find("MainSceneAngel");
        fadeEffect = GetComponent<FadeEffect>();
    }

    private void Start()
    {
        if(GameManager.Instance.isGameRestart == true)
        {
            OnGamePlayUI();
            return;
        }
        
        playerCamera.SetActive(false);
        EndCamera.SetActive(false);
        GamePlayUI.SetActive(false);
        GameOverUI.SetActive(false);
        GameEndUI.SetActive(false);

        OnMainUI();
    }


    private void Update()
    {

        if(GameManager.Instance.isGameOver || GameManager.Instance.isGameEnd)
        {
            fadeEffect.PlayerFadeOut(darkImage);
        }
        
        if(GameManager.Instance.isGameRestart)
        {
            fadeEffect.PlayerFadeIn(darkImage);
        }
    }


    public void OnMainUI()
    {
        GameExplainUI.SetActive(false);
        GamePlayUI.SetActive(false);
        playerCamera.SetActive(false);
        GamePauseUI.SetActive(false);

        mainSceneCamera.SetActive(true);
        MainUI.SetActive(true);
    }

    public void OnGamePlayUI()
    {
        GamePauseUI.SetActive(false);
        GameOverUI.SetActive(false);

        mainSceneCamera.SetActive(false);
        MainUI.SetActive(false);

        GamePlayUI.SetActive(true);
        playerCamera.SetActive(true);

    }

    public void OnGameOverUI()
    {
        GamePlayUI.SetActive(false);
        GameOverUI.SetActive(true);
    }

    public void OnGameEndUI()
    {
        playerCamera.SetActive(false);
        GamePlayUI.SetActive(false);
        EndCamera.SetActive(true);
        GameEndUI.SetActive(true);
    }

    public void OnGamePausUI()
    {
        Time.timeScale = 0;
        GamePauseUI.SetActive(true);
    }

    public void OffGamePuseUI()
    {
        Time.timeScale = 1;
        GamePauseUI.SetActive(false);
    }

    public void PushStartButton()
    {
        GameManager.Instance.IsGameStart = true;
        OnGamePlayUI();
    }

    public void PushExplainButton()
    {
        GameExplainUI.SetActive(true);
    }

    public void PushGoToMainButton()
    {
        GameManager.Instance.IsGameStart = false;
        OffGamePuseUI();
        OnMainUI();
    }

    public void AppExit()
    {
        Application.Quit();
    }

}
