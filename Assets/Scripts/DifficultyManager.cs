using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    private int difficulty = 0; // 1, 2

    #region Singleton
    public static DifficultyManager instance { get; private set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion


    public void SetDifficulty(int _difficulty)
    {
        difficulty = _difficulty;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }
}
