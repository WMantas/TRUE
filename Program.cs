using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

// Pagrindinė klasė
class Vartotojas
{
    public int Id { get; set; }
    public string Vardas { get; set; }
    public string Gmail { get; set; }
}

// Paveldimos klasės
class AdminVartotojas : Vartotojas
{
    public string Lygis { get; set; }
}

class PaprastasVartotojas : Vartotojas
{
    public int Taskai { get; set; }
}

class VartotojoWrapper
{
    public string Tipas { get; set; }
    public JsonElement Duomenys { get; set; }
}

class Program
{
    static void Main()
    {
        // 1 DALIS - paprasti vartotojai
        var vartotojai = new List<Vartotojas>
        {
            new Vartotojas { Id = 1, Vardas = "Tomas", Gmail = "tomas@gmail.com" },
            new Vartotojas { Id = 2, Vardas = "Ieva", Gmail = "ieva@gmail.com" },
            new Vartotojas { Id = 3, Vardas = "Mantas", Gmail = "mantas@gmail.com" }
        };

        File.WriteAllText("vartotojai.json", JsonSerializer.Serialize(vartotojai, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("vartotojai.json sukurtas!\n");

        // 2 DALIS - paveldėjimas
        var typedVartotojai = new List<object>
        {
            new { Tipas = "admin", Duomenys = new AdminVartotojas { Id = 101, Vardas = "Admin Tomas", Gmail = "admin@gmail.com", Lygis = "SuperAdmin" } },
            new { Tipas = "paprastas", Duomenys = new PaprastasVartotojas { Id = 102, Vardas = "User Ieva", Gmail = "user@gmail.com", Taskai = 250 } }
        };

        File.WriteAllText("vartotoju_tipas.json", JsonSerializer.Serialize(typedVartotojai, new JsonSerializerOptions { WriteIndented = true }));
        Console.WriteLine("vartotoju_tipas.json sukurtas!\n");

        // Skaitymas iš failo ir išvedimas
        var wrappers = JsonSerializer.Deserialize<List<VartotojoWrapper>>(File.ReadAllText("vartotoju_tipas.json"));

        foreach (var w in wrappers)
        {
            if (w.Tipas.ToLower() == "admin")
            {
                var a = w.Duomenys.Deserialize<AdminVartotojas>();
                Console.WriteLine($"[ADMIN] {a.Vardas} ({a.Gmail}) - Lygis: {a.Lygis}");
            }
            else
            {
                var p = w.Duomenys.Deserialize<PaprastasVartotojas>();
                Console.WriteLine($"[PAPRASTAS] {p.Vardas} ({p.Gmail}) - Taskai: {p.Taskai}");
            }
        }
    }
}