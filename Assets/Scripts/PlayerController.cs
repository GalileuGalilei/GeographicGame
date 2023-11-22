using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turboSpeed = 20f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float turboRotationSpeed = 200f;

    private Vector3 speed;
    private Vector3 rotation;

    // Update is called once per frame
    void Update()
    {
        speed = Vector3.zero;
        rotation = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            speed += transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            speed -= transform.forward * moveSpeed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rotation -= transform.up * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotation += transform.up * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= turboSpeed;
            rotation *= turboRotationSpeed;
        }

        transform.Translate(speed * Time.deltaTime, Space.World);
        transform.Rotate(rotation * Time.deltaTime);
    }

    


}
