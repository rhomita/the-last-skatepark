using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{

    [SerializeField] private GameObject playUI;
    [SerializeField] private Text percentageText;
    [SerializeField] private Text timeText;
    [SerializeField] private GameObject finishUI;
    [SerializeField] private Text finishText;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject aimDot;
    [SerializeField] private GameObject infoText;

    void Start()
    {
        ShowPlayUI();
    }

    void HideAll()
    {
        aimDot.SetActive(false);
        pauseUI.SetActive(false);
        finishUI.SetActive(false);
        playUI.SetActive(false);
        HideInfo();
    }

    public void ShowFinishUI(string text)
    {
        HideAll();
        finishText.text = text;
        finishUI.SetActive(true);
    }

    public void ShowPlayUI()
    {
        HideAll();
        playUI.SetActive(true);
        aimDot.SetActive(true);
    }

    public void ShowPauseUI()
    {
        HideAll();
        pauseUI.SetActive(true);
    }

    public void ShowInfo()
    {
        infoText.SetActive(true);
    }

    public void HideInfo()
    {
        infoText.SetActive(false);
    }

    public void UpdatePercentage(float value)
    {
        percentageText.text = System.Math.Round(value, 0).ToString() + "%";
    }

    public void UpdateTimer(float secondsRemaining)
    {
        System.TimeSpan seconds = System.TimeSpan.FromSeconds(secondsRemaining);
        timeText.text = seconds.ToString(@"m\:ss");
    }
}
