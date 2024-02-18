using System.Text.Json.Serialization;
using dotnet_rpg.Models.Enums;

namespace dotnet_rpg.Models {

    public class User {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Character> Characters { get; set; } = new List<Character>();
    }
}