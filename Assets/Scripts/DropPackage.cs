using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class DropPackage : MonoBehaviour
{
    [SerializeField] private GameObject airDropPrefab;
    [SerializeField] private GameObject countriesController;
    [SerializeField] private Transform playerModel;
    [SerializeField] private float dropCooldown = 3f;
    private float lastDropTime = 0f;
    private string[] countries;
    public string objectiveCountry;

    private void Start()
    {
        Assets.Terrain.ContriesInitializer countries_initializer = countriesController.GetComponent<Assets.Terrain.ContriesInitializer>();
        countries = countries_initializer.GetCountryNames();
      
        GenNewObjective();
    }

    void Update()
    {
        if (Time.time - lastDropTime > dropCooldown)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                InstantiatePrefab();
                lastDropTime = Time.time;
            }
        }
    }

    public void GenNewObjective()
    {
        var random = new System.Random();
        int index = random.Next(countries.Length);
        objectiveCountry = countries[index];
        FindAnyObjectByType<GameStats>().NewObjective(objectiveCountry);
    }

    void InstantiatePrefab()
    {
        // Instantiate the prefab at the player's position
        GameObject airDrop =  Instantiate(airDropPrefab, playerModel.position, Quaternion.identity);

        // Set the gravity body to the planet
        GravityBody gravityBody = airDrop.GetComponent<GravityBody>();
        gravityBody.attractor = GameObject.FindWithTag("Planet").GetComponent<GravityAttractor>();

        // Set the player as the package's owner
        RecievePackage recievePackage = airDrop.GetComponent<RecievePackage>();
        recievePackage.player = gameObject;
    }
}


