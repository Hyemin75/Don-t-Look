using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public void PushStartButton()
    {
        Debug.Log("push");
        HUD.Instance.OnGamePlayUI();
    }

    public void AppExit()
    {
        Application.Quit();
    }
}
