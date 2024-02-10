using System.Text.Json.Serialization;

namespace dotnet_rpg.Models.Enums {

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ERpgClass {
        Knight = 1,
        Mage = 2,
        Cleric = 3
    }
}