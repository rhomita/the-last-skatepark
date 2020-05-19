using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private GameObject subtext;

    private AudioSource audio;

    void Start()
    {
        subtext.SetActive(false);
        audio = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Play();
            return;
        }
    }

    public void PlayAudio()
    {
        audio.Play();
    }

    public void ChangeText(string _text)
    {
        text.text = _text;
        if (subtext.activeSelf) return;
        subtext.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene("Play", LoadSceneMode.Single);
    }
}
