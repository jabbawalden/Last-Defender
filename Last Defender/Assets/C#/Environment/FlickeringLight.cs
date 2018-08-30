using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    public Light flickerLight;

    // Use this for initialization
    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            float r = Random.Range(0, 0.5f);
            yield return new WaitForSeconds(r);
            flickerLight.enabled =! flickerLight.enabled;
        }
    }
}
