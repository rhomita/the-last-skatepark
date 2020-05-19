using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVoice : MonoBehaviour
{
    [SerializeField] private List<AudioClip> clips;
    [SerializeField] private AudioClip deadClip;

    private EnemyCombat combat;
    private AudioSource audio;

    private float cooldown = 0f;
    private float cooldownVoice;

    void Start()
    {
        combat = transform.GetComponent<EnemyCombat>();
        audio = transform.GetComponent<AudioSource>();

        cooldownVoice = Random.Range(5, 20);
        cooldown = cooldownVoice;
    }

    
    void Update()
    {
        if (combat.IsDead())
        {
            PlayClip(deadClip);
            Destroy(this);
            return;
        }
        if (cooldown <= 0)
        {
            int clipIndex = Random.Range(0, clips.Count - 1);
            AudioClip clip = clips[clipIndex];
            PlayClip(clip);
            cooldown = cooldownVoice;
        }
        cooldown -= Time.deltaTime;
    }

    void PlayClip(AudioClip clip)
    {
        if (audio.isPlaying)
        {
            audio.Stop();
        }
        audio.clip = clip;
        audio.Play();
    }
}
