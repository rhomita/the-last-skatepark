using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt : MonoBehaviour
{
    private float maxScale = 5f;
    private float minScale = 1.5f;
    private float value;
    void Start()
    {
        value = Random.Range(minScale, maxScale);
        transform.localScale = Vector3.one * value;
        GameManager.instance.AddDirt(value / 5);
        StartCoroutine(Remove());
    }

    IEnumerator Remove()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
