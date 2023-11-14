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
            public Vector2[] polygon;
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

        // Start is called before the first frame update
        void Start()
        {
            string json = System.IO.File.ReadAllText(shapePath);
            JsonCountries jsonContries = JsonConvert.DeserializeObject<JsonCountries>(json);
            countries = new Country[jsonContries.features.Count];
            JsonToVector(jsonContries);
            CreateAllCountries();
            Debug.Log("Countries initialized");
        }

        private void JsonToVector(JsonCountries jsonCountries)
        {
            for (int i = 0; i < jsonCountries.features.Count; i++)
            {
                countries[i].name = jsonCountries.features[i].properties.NAME_LONG;
                countries[i].polygon = new Vector2[jsonCountries.features[i].geometry.coordinates[0][0].Count];
                for (int j = 0; j < jsonCountries.features[i].geometry.coordinates[0][0].Count; j++)
                {
                    countries[i].polygon[j] = new Vector2((float)jsonCountries.features[i].geometry.coordinates[0][0][j][0], (float)jsonCountries.features[i].geometry.coordinates[0][0][j][1]);
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
            GameObject country = Instantiate(countryPrefab);
            country.transform.SetParent(this.transform);
            lines = country.GetComponent<LineRenderer>();
            int size = countries[index].polygon.Count(); ;
            lines.positionCount = size;

            for (int i = 0; i < size; i++)
            {
                Vector2 lonLat = countries[index].polygon[i];
                Vector3 worldSpace = LonLatToPoint(earthCenter, lonLat, earthRadius);
                lines.SetPosition(i, worldSpace);
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