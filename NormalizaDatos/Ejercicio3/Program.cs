using System;
using System.Collections.Generic;
using System.Linq;

// Clase que representa un equipo
class Equipo
{
    public int Id { get; set; }              // ID único del equipo
    public string? Nombre { get; set; }      // Nombre del equipo
}

// Clase que representa una competición
class Competicion
{
    public int Id { get; set; }              // ID único de la competición
    public string? Nombre { get; set; }      // Nombre de la competición
}

// Clase que representa un partido
class Partido
{
    public int Id { get; set; }              // ID único del partido
    public int Equipo1Id { get; set; }       // ID del primer equipo
    public int Equipo2Id { get; set; }       // ID del segundo equipo
    public int CompeticionId { get; set; }   // ID de la competición
    public string? Resultado { get; set; }   // Resultado del partido
    public DateOnly Fecha { get; set; }      // Fecha del partido
}


namespace Ejercicio3
{
    class Program
    {
        static void Main()
        {
            // Lista de partidos en bruto
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

            // --- PASO 1: Convertimos cada línea en un array de strings ---
            var registros = (from linea in datos
                             let partes = linea.Split(';')  // Separa cada parte
                             select partes).ToList();

            // --- PASO 2: Obtenemos los equipos únicos ---
            var equipos = (from p in registros
                           from nombre in new[] { p[0], p[1] }  // Tomamos equipo1 y equipo2
                           select nombre)
                          .Distinct()                               // Eliminamos duplicados
                          .Select((nombre, i) => new Equipo { Id = i + 1, Nombre = nombre }) // Creamos objetos Equipo con ID
                          .ToList();

            // --- PASO 3: Obtenemos las competiciones únicas ---
            var competiciones = (from p in registros
                                 select p[3])                             // Tomamos la columna de la competición
                                .Distinct()
                                .Select((nombre, i) => new Competicion { Id = i + 1, Nombre = nombre })
                                .ToList();

            // --- PASO 4: Creamos la lista de partidos con IDs ---
            var partidos = (from p in registros
                            select new Partido
                            {
                                Id = registros.IndexOf(p) + 1,             // ID consecutivo del partido
                                Equipo1Id = equipos.First(e => e.Nombre == p[0]).Id,  // ID del primer equipo
                                Equipo2Id = equipos.First(e => e.Nombre == p[1]).Id,  // ID del segundo equipo
                                CompeticionId = competiciones.First(c => c.Nombre == p[3]).Id, // ID de la competición
                                Resultado = p[2],                          // Resultado
                                Fecha = DateOnly.Parse(p[4])               // Fecha convertida
                            }).ToList();

            // --- SALIDA POR CONSOLA ---
            Console.WriteLine("EQUIPOS:");
            foreach (var e in equipos)
                Console.WriteLine($"{e.Id}: {e.Nombre}");          // Mostramos los equipos

            Console.WriteLine("\nCOMPETICIONES:");
            foreach (var c in competiciones)
                Console.WriteLine($"{c.Id}: {c.Nombre}");         // Mostramos las competiciones

            Console.WriteLine("\nPARTIDOS:");
            foreach (var p in partidos)
                // Mostramos los partidos con IDs y demás datos
                Console.WriteLine($"{p.Id}: Equipo1={p.Equipo1Id}, Equipo2={p.Equipo2Id}, Competicion={p.CompeticionId}, Resultado={p.Resultado}, Fecha={p.Fecha}");
        }
    }
}

