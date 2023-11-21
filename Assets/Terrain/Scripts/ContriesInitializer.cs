using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.Linq;

namespace Assets.Terrain
{
    public class ContriesInitializer : MonoBehaviour
    {
        internal struct Country
        {
            public string name;
            public List<List<Vector2>> polygons;
        }

        Country[] countries;
        LineRenderer lines;

        [SerializeField]
        private string shapePath;
        [SerializeField]
        private Vector3 earthCenter;
        [SerializeField]
        private float earthRadius;
        [SerializeField]
        private GameObject countryPrefab;

        void Awake()
        {
            string json = System.IO.File.ReadAllText(shapePath);
            JsonCountries jsonContries = JsonConvert.DeserializeObject<JsonCountries>(json);
            countries = new Country[jsonContries.features.Count];
            JsonToVector(jsonContries);
            CreateAllCountries();
            Debug.Log("Countries initialized");
        }

        public string[] GetCountryNames()
        {
            string[] country_array = countries.Select(c => c.name).ToArray();
            return country_array.Distinct().ToArray();
        }



        private void JsonToVector(JsonCountries jsonCountries)
        {
            for (int i = 0; i < jsonCountries.features.Count; i++)
            {
                countries[i].name = jsonCountries.features[i].properties.NAME_LONG;
                countries[i].polygons = new List<List<Vector2>>();

                foreach (List<List<double[]>> polygons in jsonCountries.features[i].geometry.coordinates)
                {
                    List<Vector2> polygon = new List<Vector2>();

                    foreach (List<double[]> points in polygons)
                    {
                        foreach (double[] point in points)
                        {
                            polygon.Add(new Vector2((float)point[0], (float)point[1]));
                        }
                    }

                    countries[i].polygons.Add(polygon);
                }
            }
        }

        private void CreateAllCountries()
        {
            int l = countries.Length;

            for(int i = 0; i < l; i++)
            {
                CreateCountry(i);
            }
        }

        private void CreateCountry(int index)
        {
            foreach(List<Vector2> polygon in countries[index].polygons)
            {
                GameObject go = Instantiate(countryPrefab, transform);
                go.name = countries[index].name;
                go.tag = "Country";

                Vector3[] points = new Vector3[polygon.Count];

                for(int i = 0; i < polygon.Count; i++)
                {
                    points[i] = LonLatToPoint(earthCenter, polygon[i], earthRadius);
                }

                LineRenderer lr = go.GetComponent<LineRenderer>();
                lr.positionCount = points.Length;
                lr.SetPositions(points);
                go.AddComponent<BoxCollider>();
            }
        }

        // Convert geographic coordinates to Unity world space coordinates
        public Vector3 LonLatToPoint(Vector3 origin, Vector2 coordinate, float radius)
        {
            // Convert degrees to radians
            float lonRad = coordinate.x * Mathf.Deg2Rad;
            float latRad = coordinate.y * Mathf.Deg2Rad;

            // Calculate x, y, z coordinates in world space
            float x = origin.x + radius * Mathf.Cos(latRad) * Mathf.Cos(lonRad);
            float y = origin.y + radius * Mathf.Cos(latRad) * Mathf.Sin(lonRad);
            float z = origin.z + radius * Mathf.Sin(latRad);

            return new Vector3(x, y, z);
        }
    }
}