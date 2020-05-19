using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkateInteract : MonoBehaviour
{
    [SerializeField] private Transform interact;
    [SerializeField] private LayerMask skateMask;

    private GameUI gameUI;
    private PlayerCombat playerCombat;
    private bool skateFound = false;
    private float radiusInteract = 1.2f;

    void Start()
    {
        playerCombat = GameManager.instance.GetPlayer().GetComponent<PlayerCombat>();
        gameUI = GameManager.instance.GetGameUI();
    }

    void Update()
    {
        Check();
        if (Input.GetKey(KeyCode.E))
        {
            Interact();
        }
    }

    void Check()
    {
        RaycastHit hit;
        Collider[] hitColliders = Physics.OverlapSphere(interact.position, radiusInteract, skateMask);
        if (hitColliders.Length > 0)
        {
            foreach (Collider collider in hitColliders)
            {
                Skate _skate = collider.GetComponent<Skate>();
                if (_skate != null)
                {
                    gameUI.ShowInfo();
                    skateFound = true;
                    return;
                }
            }
        }
        gameUI.HideInfo();
        skateFound = false;
    }

    void Interact()
    {
        if (!skateFound) return;
        playerCombat.Take();
        skateFound = false;
    }
}
