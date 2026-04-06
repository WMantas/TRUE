
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
        User user = new User
        {
            id = 205604043,
            Vardas = "Tomas",
            Gmail = "Hotto@gmail.com"
        };

        string jsonString = JsonSerializer.Serialize(user, new JsonSerializerOptions
        {
            WriteIndented = true
        });

        File.WriteAllText("users.json", jsonString);

        Console.WriteLine("Failas sukurtas!");
        Console.WriteLine(jsonString);
    }
}