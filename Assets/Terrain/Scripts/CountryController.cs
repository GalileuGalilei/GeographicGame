using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryController : MonoBehaviour
{
    private Transform parent;
    private LineRenderer countryLine;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        countryLine = GetComponent<LineRenderer>();
        //RotateCountryLine();
    }

    private void RotateCountryLine()
    {
        Vector3[] positions = new Vector3[countryLine.positionCount];
        countryLine.GetPositions(positions);
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = parent.InverseTransformPoint(positions[i]);
        }
        countryLine.SetPositions(positions);
    }
}
