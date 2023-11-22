using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthMenuController : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    float rotationSpeed = 2;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}
