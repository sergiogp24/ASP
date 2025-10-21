using System;
using System.Collections.Generic;
using System.Linq;

// Clase que representa a un equipo de fútbol
class Equipo
{
    public int Id { get; set; }              // Identificador único del equipo
    public string? Nombre { get; set; }      // Nombre del equipo (por ejemplo, Real Madrid)
}

// Clase que representa una competición (Liga, Copa del Rey, etc.)
class Competicion
{
    public int Id { get; set; }              // Identificador único de la competición
    public string? Nombre { get; set; }      // Nombre de la competición
}

// Clase que representa un partido entre dos equipos dentro de una competición
class Partido
{
    public int Id { get; set; }              // Identificador único del partido
    public int Equipo1Id { get; set; }       // ID del primer equipo (local)
    public int Equipo2Id { get; set; }       // ID del segundo equipo (visitante)
    public int CompeticionId { get; set; }   // ID de la competición donde se juega
    public string? Resultado { get; set; }   // Resultado del partido (por ejemplo, "2-1")
    public DateOnly Fecha { get; set; }      // Fecha del partido
}


namespace Ejercicio2
{
    class Program
    {
        static void Main()
        {
            // Lista con todos los partidos en formato texto, separados por punto y coma
            List<string> datos = new List<string> {
                "Real Madrid;Barcelona;2-1;Liga;2025-10-12",
                "Atlético de Madrid;Sevilla;1-0;Liga;2025-10-13",
                "Barcelona;Valencia;3-2;Copa del Rey;2025-10-14",
                "Sevilla;Real Madrid;0-2;Liga;2025-10-15",
                "Valencia;Atlético de Madrid;1-1;Copa del Rey;2025-10-16",
                "Real Madrid;Atlético de Madrid;2-2;Liga;2025-10-17",
                "Barcelona;Sevilla;4-0;Liga;2025-10-18",
                "Valencia;Real Madrid;0-1;Copa del Rey;2025-10-19",
                "Atlético de Madrid;Barcelona;1-3;Liga;2025-10-20",
                "Sevilla;Valencia;2-2;Copa del Rey;2025-10-21"
            };

            // Separamos cada línea del texto en partes (equipo1, equipo2, resultado, competición, fecha)
            var registros = datos.Select(linea => linea.Split(';')).ToList();

            // --- EQUIPOS ---
            // Seleccionamos todos los equipos (tanto locales como visitantes)
            // Con SelectMany unimos ambas columnas de equipos en una sola lista
            // Distinct elimina duplicados
            // Luego asignamos IDs de forma automática
            var equipos = registros
                .SelectMany(p => new[] { p[0], p[1] })                  // Coge equipo1 y equipo2 de cada partido
                .Distinct()                                             // Elimina nombres repetidos
                .Select((nombre, i) => new Equipo { Id = i + 1, Nombre = nombre }) // Crea objetos Equipo con ID consecutivo
                .ToList();                                              // Lo convertimos en una lista

            // --- COMPETICIONES ---
            // Lo mismo pero con los nombres de las competiciones
            var competiciones = registros
                .Select(p => p[3])                                      // Coge el nombre de la competición (posición 3)
                .Distinct()                                             // Elimina duplicados (Liga, Copa del Rey)
                .Select((nombre, i) => new Competicion { Id = i + 1, Nombre = nombre })
                .ToList();

            // --- PARTIDOS ---
            // Ahora construimos la lista de partidos completa usando los IDs
            var partidos = registros
                .Select((p, i) => new Partido
                {
                    Id = i + 1,                                         // ID consecutivo para el partido
                    Equipo1Id = equipos.First(e => e.Nombre == p[0]).Id, // Busca el ID del primer equipo
                    Equipo2Id = equipos.First(e => e.Nombre == p[1]).Id, // Busca el ID del segundo equipo
                    CompeticionId = competiciones.First(c => c.Nombre == p[3]).Id, // Busca el ID de la competición
                    Resultado = p[2],                                   // Resultado (por ejemplo "2-1")
                    Fecha = DateOnly.Parse(p[4])                        // Convierte la fecha del texto a DateOnly
                })
                .ToList();

            // --- SALIDA POR CONSOLA ---

            Console.WriteLine("EQUIPOS:");
            foreach (var e in equipos)
                Console.WriteLine($"{e.Id}: {e.Nombre}");                // Muestra cada equipo con su ID

            Console.WriteLine("\nCOMPETICIONES:");
            foreach (var c in competiciones)
                Console.WriteLine($"{c.Id}: {c.Nombre}");                // Muestra las competiciones con su ID

            Console.WriteLine("\nPARTIDOS:");
            foreach (var p in partidos)
                // Muestra todos los datos del partido con los IDs en lugar de los nombres
                Console.WriteLine($"{p.Id}: Equipo1={p.Equipo1Id}, Equipo2={p.Equipo2Id}, Competicion={p.CompeticionId}, Resultado={p.Resultado}, Fecha={p.Fecha}");
        }
    }
}


