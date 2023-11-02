using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OSGeo;
using System;
using OSGeo.OGR;

public struct Contry
{
    Vector2[] polygon;
    string contryName;
}


public class ContriesInitializer : MonoBehaviour
{
    private List<Contry> world;

    [SerializeField]
    private string shapePath;
    
    // Start is called before the first frame update
    void Start()
    {
        Ogr.Open(shapePath, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
