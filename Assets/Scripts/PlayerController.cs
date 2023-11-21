using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float turboSpeed = 20f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float turboRotationSpeed = 200f;

    private Vector3 moveDir;
    private Rigidbody rb;
    private float horizontal;
    private float vertical;
    private float speed;
    private float rotationSpd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = moveSpeed;
        rotationSpd = rotationSpeed;
    }

    void Update()
    {
        GetInput();

        moveDir = new Vector3(horizontal, 0f, vertical).normalized;

        float rotationAmount = horizontal * rotationSpd * Time.deltaTime;
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = turboSpeed;
            rotationSpd = turboRotationSpeed;
        }
        else
        {
            speed = moveSpeed;
            rotationSpd = rotationSpeed;
        }   
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.TransformDirection(moveDir) * speed * Time.fixedDeltaTime);
    }


}
