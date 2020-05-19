using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject skatepark;
    [SerializeField] private GameObject skate;
    [SerializeField] private GameUI gameUI;

    private static float DIRT_MAX = 100;
    private float dirtAmount = 0;
    private float seconds = 183;
    private bool isFinished = false;
    private bool isPaused = false;
    
    #region Singleton
    public static GameManager instance { get; private set; }
    void Awake()
    {
        instance = this;
    }
    #endregion

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }

        if (isFinished) return;

        Time.timeScale = isPaused ? 0 : 1;

        gameUI.UpdatePercentage(dirtAmount);
        gameUI.UpdateTimer(seconds);
        seconds -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                EnableCursor();
                gameUI.ShowPauseUI();
            }
            else
            {
                HideCursor();
                gameUI.ShowPlayUI();
            }
        }

        if (seconds <= 0)
        {
            Finish();
            return;
        }
    }

    private void Finish()
    {
        isFinished = true;
        Destroy(player.GetComponent<PlayerCombat>());
        Destroy(player.GetComponent<PlayerController>());
        Time.timeScale = 0;
        string finishMessage = dirtAmount < DIRT_MAX ? "You win! The skatepark is safe." : "You lose! The skatepark is broken.";
        gameUI.ShowFinishUI(finishMessage);
        EnableCursor();
    }

    public bool IsPaused()
    {
        return isPaused;
    }

    public void Restart()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }

    public void Exit()
    {
        isPaused = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void AddDirt(float _amount)
    {
        dirtAmount += _amount;
        if (dirtAmount >= DIRT_MAX)
        {
            Finish();
        }
    }

    public Transform GetPlayer()
    {
        return player.transform;
    }

    public Transform GetSkatepark()
    {
        return skatepark.transform;
    }

    public Transform GetSkate()
    {
        return skate.transform;
    }
    
    public GameUI GetGameUI()
    {
        return gameUI;
    }

    void EnableCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void HideCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
