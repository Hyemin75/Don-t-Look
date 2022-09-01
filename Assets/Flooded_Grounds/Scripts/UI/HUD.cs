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


    public void OnMainUI()
    {
        mainSceneCamera.SetActive(true);
        MainUI.SetActive(true);
    }

    public void OnGamePlayUI()
    {
        GameOverUI.SetActive(false);
        fadeEffect.gameObject.SetActive(false);

        mainSceneCamera.SetActive(false);
        MainUI.SetActive(false);

        GamePlayUI.SetActive(true);
        playerCamera.SetActive(true);

    }

    public void OnGameOverUI()
    {
        GamePlayUI.SetActive(false);
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.PlayerFadeOut(darkImage);
        GameOverUI.SetActive(true);
    }

    public void OnGameEndUI()
    {
        playerCamera.SetActive(false);
        GamePlayUI.SetActive(false);
        EndCamera.SetActive(true);
        GameEndUI.SetActive(true);
        fadeEffect.gameObject.SetActive(true);
        fadeEffect.PlayerFadeOut(darkImage);
    }


    public void PushStartButton()
    {
        Cursor.visible = false;
        OnGamePlayUI();
    }

    public void AppExit()
    {
        Application.Quit();
    }

}
