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
    private float rotation;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        speed = Vector3.zero;
        rotation = 0;

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
            rotation = -Time.deltaTime * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            rotation = Time.deltaTime * rotationSpeed;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed *= turboSpeed;
            rotation *= turboRotationSpeed;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            FindAnyObjectByType<GameStats>().SkipCountry();
            FindAnyObjectByType<DropPackage>().GenNewObjective();
            FindAnyObjectByType<AudioManager>().PlaySoundEffectWrong();
        }
        
        transform.Translate(speed * Time.deltaTime, Space.World);
        transform.RotateAround(transform.position, transform.up, rotation);
    }

    


}
