using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecievePackage : MonoBehaviour
{
    public GameObject player;
    private string objectiveCountry;
    private List<string> countriesCollided;

    void Start()
    {
        objectiveCountry = player.GetComponent<DropPackage>().objectiveCountry;
        countriesCollided = new List<string>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Country")
        {
            countriesCollided.Add(other.transform.name);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Planet")
        {
            Destroy(gameObject, 1f);
            VerifyCountriesCollided();
        }
    }

    private void VerifyCountriesCollided()
    {
        if (countriesCollided.Contains(objectiveCountry))
        {
            FindAnyObjectByType<GameStats>().CorrectCountry();
            player.GetComponent<DropPackage>().GenNewObjective();
            FindAnyObjectByType<AudioManager>().PlaySoundEffectCorrect();
        }
        else
        {
            FindAnyObjectByType<GameStats>().WrongCountry();
            FindAnyObjectByType<AudioManager>().PlaySoundEffectWrong();
        }

        countriesCollided.Clear();
    }
}
