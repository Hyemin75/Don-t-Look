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
    GameObject playerCamera;
    [SerializeField]
    GameObject mainSceneCamera;


    private void Start()
    {
        fadeEffect = GetComponent<FadeEffect>();
        OnMainUI();
    }

    private void Update()
    {

    }


    public void OnMainUI()
    {
        fadeEffect.PlayerFadeIn(darkImage); // update or corutien 사용 필요
        mainSceneCamera.SetActive(true);
        playerCamera.SetActive(false);
        MainUI.SetActive(true);
        GamePlayUI.SetActive(false);
        GameOverUI.SetActive(false);
    }

    public void OnGamePlayUI()
    {
        mainSceneCamera.SetActive(false);
        playerCamera.SetActive(true);
        MainUI.SetActive(false);
        GamePlayUI.SetActive(true);
        GameOverUI.SetActive(false);
    }

    public void OnGameOverUI()
    {
        mainSceneCamera.SetActive(false);
        playerCamera.SetActive(true);
        MainUI.SetActive(false);
        GamePlayUI.SetActive(false);
        GameOverUI.SetActive(true);
        fadeEffect.PlayerFadeOut(darkImage);
       
    }

    public void OnGameEndUI()
    {

    }


}
