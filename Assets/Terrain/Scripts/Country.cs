using System.Collections.Generic;

namespace Assets.Terrain
{
    internal struct Geometry
    {
        public string type;
        public List<List<List<double[]>>> coordinates;
    }

    internal struct Properties
    {
        public string NAME_LONG;
        public string ISO_A2;
    }

    internal struct Country
    {
        public Properties properties;
        public Geometry geometry;
    }

    internal struct JsonCountries
    {
        public List<Country> features;
    }
}
