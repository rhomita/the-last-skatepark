using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesAutoDestroy : MonoBehaviour
{
    private ParticleSystem ps;
    void Start()
    {
        ps = transform.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ps.IsAlive()) return;
        Destroy(gameObject);
    }
}
