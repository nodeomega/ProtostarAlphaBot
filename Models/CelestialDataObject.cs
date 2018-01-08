using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ProtostarAlphaBot.Models
{
    public class CelestialDataObject
    {
        public enum CelestialObjectType
        {
            Star,
            Planet,
            DwarfPlanet,
            Asteroid,
            Comet,
            Other
        }

        public string Name { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public CelestialObjectType Type { get; set; }

        public string Info { get; set; }

    }
}