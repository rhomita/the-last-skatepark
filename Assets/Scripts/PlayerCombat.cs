using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Transform cam;
    [SerializeField] private Transform skateSlot;
    [SerializeField] private Transform skateRideSlot;

    private Skate skate;
    private PlayerController playerController;
    private bool hasSkate = false;

    void Start()
    {
        playerController = transform.GetComponent<PlayerController>();
        skate = GameManager.instance.GetSkate().GetComponent<Skate>();
    }

    void Update()
    {
        if (!hasSkate) return;
        if (GameManager.instance.IsPaused()) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (playerController.IsRiding())
            {
                // TODO: add message or throw skate anyways.
            } else
            {
                Attack();
            }
            return;            
        }

        if (Input.GetMouseButtonDown(1))
        {
            ToggleRide();
        }
    }

    void Attack()
    {
        hasSkate = false;
        skate.transform.parent = null;
        skate.Throw(cam.forward);
    }

    void ToggleRide()
    {
        playerController.ToggleRide();
        if (playerController.IsRiding())
        {
            skate.transform.parent = skateRideSlot;
        } else
        {
            skate.transform.parent = skateSlot;
        }
        skate.transform.localPosition = Vector3.zero;
        skate.transform.localRotation = Quaternion.identity;
    }

    public void Take()
    {
        skate.Take();
        skate.transform.parent = skateSlot;
        skate.transform.localPosition = Vector3.zero;
        skate.transform.localRotation = Quaternion.identity;
        hasSkate = true;
    }
    
    public bool HasSkate()
    {
        return hasSkate;
    }
}
