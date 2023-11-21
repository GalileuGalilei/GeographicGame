using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecievePackage : MonoBehaviour
{
    public GameObject player;
    private string objectiveCountry;

    // Start is called before the first frame update
    void Start()
    {
        objectiveCountry = player.GetComponent<DropPackage>().objectiveCountry;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Country")
        {
            if(other.transform.name == objectiveCountry)
            {
                Debug.Log("Package delivered");
                player.GetComponent<DropPackage>().GenNewObjective();
            }
            else
            {
                Debug.Log("Wrong country");
            }

            Destroy(gameObject, 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Planet")
        {
            Debug.Log("Package destroyed");
            Destroy(gameObject);
        }
    }
}
