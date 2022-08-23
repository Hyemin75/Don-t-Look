using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : SingletonBehaviour<HUD>
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


    private void Start()
    {
        fadeEffect = GetComponent<FadeEffect>();
        
        playerCamera.SetActive(false);
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
        // FadeOn - update or corutien 사용 필요
        mainSceneCamera.SetActive(false);
        MainUI.SetActive(false);

        GamePlayUI.SetActive(true);
        playerCamera.SetActive(true);
    }

    public void OnGameOverUI()
    {
        GamePlayUI.SetActive(false);

        fadeEffect.PlayerFadeOut(darkImage);
        GameOverUI.SetActive(true);
    }

    public void OnGameEndUI()
    {
        GamePlayUI.SetActive(false);

        fadeEffect.PlayerFadeOut(darkImage);
        GameEndUI.SetActive(true);
    }


}
