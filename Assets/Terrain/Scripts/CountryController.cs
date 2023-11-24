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
        countryLine.enabled = false;
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
        for (float f = countryLine.material.GetFloat("_Alpha"); f >= 0; f -= fadeSpeed)
        { 
            countryLine.material.SetFloat("_Alpha", f);
            yield return new WaitForSeconds(fadeSpeed);
        }

        countryLine.enabled = false;
    }

    IEnumerator FadeIn()
    {
        countryLine.enabled = true;

        for (float f = countryLine.material.GetFloat("_Alpha"); f <= 1; f += fadeSpeed)
        {
            countryLine.material.SetFloat("_Alpha", f);
            yield return new WaitForSeconds(fadeSpeed);
        }
    }
}
