using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryController : MonoBehaviour
{
    private Transform parent;
    private GameObject player;
    private Material countryMaterial;
    private LineRenderer countryLine;
    private BoxCollider countryCollider;

    [SerializeField]
    private bool dynamicCollision = false;
    [SerializeField]
    private float fadeSpeed = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        countryCollider = GetComponent<BoxCollider>();
        countryCollider.isTrigger = true;
        countryLine = GetComponent<LineRenderer>();
        countryLine.material.color = new Color(1, 1, 1, 0);
    } 

    //line renderer only visible if its colliding with player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeIn());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(FadeOut());
        }
    }

    // fade function using coroutine
    IEnumerator FadeOut()
    {
        for (float f = 1f; f >= 0; f -= fadeSpeed)
        {
            Color c = countryLine.material.color;
            c.a = f;
            countryLine.material.color = c;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1; f += fadeSpeed)
        {
            Color c = countryLine.material.color;
            c.a = f;
            countryLine.material.color = c;
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
}
