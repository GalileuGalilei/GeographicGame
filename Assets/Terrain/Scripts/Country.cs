using System.Collections.Generic;

namespace Assets.Terrain
{
    public struct Geometry
    {
        public string type;
        public List<List<List<double[]>>> coordinates;
    }

    public struct Properties
    {
        public string NAME_LONG;
    }

    public struct Country
    {
        public Properties properties;
        public Geometry geometry;
    }

    public struct JsonCountries
    {
        public List<Country> features;
    }
}
