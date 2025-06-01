// Services/SessionService.cs
using PoputkeeLite.Core.Models;
using System.IO;
using System.Text.Json;

namespace PoputkeeLite.Core.Services
{
    public static class SessionService
    {
        private static readonly string SessionPath = Path.Combine(DataService.CurrentDataFolder(), "session.json");

        public static void SaveSession(User user)
        {
            var json = JsonSerializer.Serialize(user);
            File.WriteAllText(SessionPath, json);
        }

        public static User LoadSession()
        {
            if (!File.Exists(SessionPath)) return null;

            var json = File.ReadAllText(SessionPath);
            return JsonSerializer.Deserialize<User>(json);
        }

        public static void ClearSession()
        {
            if (File.Exists(SessionPath))
                File.Delete(SessionPath);
        }
    }
}