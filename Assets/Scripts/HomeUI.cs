using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUI : MonoBehaviour
{
    [SerializeField] private GameObject startText;
    [SerializeField] private GameObject difficulty;

    void Start()
    {
        startText.SetActive(true);
        difficulty.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            startText.SetActive(true);
            difficulty.SetActive(false);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            startText.SetActive(false);
            difficulty.SetActive(true);
        }
    }
    public void Play(int difficulty)
    {
        DifficultyManager.instance.SetDifficulty(difficulty);
        SceneManager.LoadScene("Intro", LoadSceneMode.Single);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
