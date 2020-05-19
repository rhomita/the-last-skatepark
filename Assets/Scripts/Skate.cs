using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skate : MonoBehaviour
{
    private Vector3 target;
    private bool isFlying = false;
    private bool isGoingBack = false;
    private bool isTaken = false;

    private Transform player;

    private static float FLYING_TIME = 1.5f;

    private float flyingTime = 0;
    private float throwSpeed = 600;

    private Rigidbody rb;
    private Collider collider;

    private Animator animator;

    private AudioSource audio;

    void Start()
    {
        player = GameManager.instance.GetPlayer();
        rb = transform.GetComponent<Rigidbody>();
        collider = transform.GetComponent<Collider>();
        animator = transform.GetComponent<Animator>();
        audio = transform.GetComponent<AudioSource>();
    }

    void Update()
    {
        rb.isKinematic = isTaken;
        collider.enabled = !isTaken;

        animator.SetBool("isFlying", isFlying);
        if (!isFlying)
        {
            return;
        }
        
        flyingTime -= Time.deltaTime;

        if (flyingTime <= 0)
        {
            if (!isGoingBack)
            {
                flyingTime = FLYING_TIME;
                isGoingBack = true;
            } else
            {
                isFlying = false;
            }
        }

        if (isGoingBack)
        {
            target = player.position;
            transform.position = Vector3.Slerp(transform.position, target, Time.deltaTime * 5f);
        }
    }

    public void Throw(Vector3 direction)
    {
        isTaken = false;
        isFlying = true;
        isGoingBack = false;
        audio.Play();
        transform.position += direction;
        target = direction * throwSpeed;
        flyingTime = FLYING_TIME;

        rb.isKinematic = false;
        rb.AddForce((direction + Vector3.up / 6) * 80, ForceMode.Impulse);
    }
    
    public void Take()
    {
        audio.Stop();
        isTaken = true;
        isGoingBack = false;
        isFlying = false;
        flyingTime = 0;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.CompareTag("Environment")) return;

        audio.Stop();
        isFlying = false;
        isGoingBack = false;
        isTaken = false;
    }

    public bool IsFlying()
    {
        return isFlying;
    }
}
