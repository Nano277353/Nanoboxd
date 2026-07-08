using System.Text.Json;
using Classes;

namespace Services;

public class UserStore
{
    private static readonly string FilePath = "users.json";

    public static List<User> LoadUsers()
    {
        if (!File.Exists(FilePath))
            return [];

        string json = File.ReadAllText(FilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? [];
    }

    public static void SaveUsers(List<User> users)
    {
        string json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(FilePath, json);
    }
}
