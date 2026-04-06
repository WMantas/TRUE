
using System.Text.Json;

class User
{
    public int id { get; set; }
    public string Vardas { get; set; }
    public string Gmail { get; set; }
}

class Program
{
    static void Main()
    {
        // 1. Sukuriam kelis vartotojus
        List<User> users = new List<User>
        {
            new User { id = 1, Vardas = "Tomas", Gmail = "tomas@gmail.com" },
            new User { id = 2, Vardas = "Ieva", Gmail = "ieva@gmail.com" }
        };

        // 2. Išsaugom į JSON failą
        string jsonString = JsonSerializer.Serialize(users, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("users.json", jsonString);
        Console.WriteLine("users.json failas sukurtas!");

        // 3. Nuskaitom failą
        string jsonFromFile = File.ReadAllText("users.json");

        // 4. Deserialize į List<User>
        List<User> loadedUsers = JsonSerializer.Deserialize<List<User>>(jsonFromFile);

        // 5. LOOP per visus įrašus
        Console.WriteLine("\nVisi vartotojai iš users.json:");

        foreach (User user in loadedUsers)
        {
            Console.WriteLine($"ID: {user.id}");
            Console.WriteLine($"Vardas: {user.Vardas}");
            Console.WriteLine($"Gmail: {user.Gmail}");
            Console.WriteLine("---------------------");
        }
    }
}