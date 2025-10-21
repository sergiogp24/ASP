class Equipo
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}


class Competicion
{
    public int Id { get; set; }
    public string? Nombre { get; set; }
}


class Partido
{
    public int Id { get; set; }
    public int Equipo1Id { get; set; }
    public int Equipo2Id { get; set; }
    public int CompeticionId { get; set; }
    public string? Resultado { get; set; }
    public DateOnly Fecha { get; set; }
}


namespace Ejercicio1
{
    class Program
    {
        static void Main()
        {
            // Lista con los datos de los partidos en bruto (texto separado por ;)
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

            // Listas vacías donde iremos guardando los objetos normalizados
            List<Equipo> equipos = new();
            List<Competicion> competiciones = new();
            List<Partido> partidos = new();

            // Contadores para asignar IDs únicos
            int equipoId = 1;
            int competicionId = 1;
            int partidoId = 1;

            // Recorremos cada línea de datos para procesarla
            foreach (var linea in datos)
            {
                // Separamos los datos por ';'
                var partes = linea.Split(';');
                string nombre1 = partes[0];         // Nombre del primer equipo
                string nombre2 = partes[1];         // Nombre del segundo equipo
                string resultado = partes[2];       // Resultado del partido
                string nombreCompeticion = partes[3]; // Nombre de la competición
                DateOnly fecha = DateOnly.Parse(partes[4]); // Fecha del partido

                // --- EQUIPO 1 ---
                // Buscamos si el equipo ya existe
                Equipo? e1 = equipos.Find(e => e.Nombre == nombre1);
                if (e1 == null) // Si no existe, lo creamos y lo añadimos
                {
                    e1 = new Equipo { Id = equipoId++, Nombre = nombre1 };
                    equipos.Add(e1);
                }

                // --- EQUIPO 2 ---
                // Hacemos lo mismo para el segundo equipo
                Equipo? e2 = equipos.Find(e => e.Nombre == nombre2);
                if (e2 == null)
                {
                    e2 = new Equipo { Id = equipoId++, Nombre = nombre2 };
                    equipos.Add(e2);
                }

                // --- COMPETICIÓN ---
                // Comprobamos si la competición ya existe
                Competicion? c = competiciones.Find(x => x.Nombre == nombreCompeticion);
                if (c == null) // Si no existe, la creamos y la añadimos
                {
                    c = new Competicion { Id = competicionId++, Nombre = nombreCompeticion };
                    competiciones.Add(c);
                }

                // --- PARTIDO ---
                // Creamos el objeto Partido con todos los datos procesados
                partidos.Add(new Partido
                {
                    Id = partidoId++,
                    Equipo1Id = e1.Id,
                    Equipo2Id = e2.Id,
                    CompeticionId = c.Id,
                    Resultado = resultado,
                    Fecha = fecha
                });
            }

            // --- MOSTRAMOS LOS RESULTADOS ---

            Console.WriteLine("EQUIPOS:");
            foreach (var e in equipos)
                Console.WriteLine($"{e.Id}: {e.Nombre}");

            Console.WriteLine("\nCOMPETICIONES: ");
            foreach (var c in competiciones)
                Console.WriteLine($"{c.Id}: {c.Nombre}");

            Console.WriteLine("\nPARTIDOS: ");
            foreach (var p in partidos)
                Console.WriteLine($"{p.Id}: Equipo1={p.Equipo1Id}, Equipo2={p.Equipo2Id}, Competicion={p.CompeticionId}, Resultado={p.Resultado},Fecha= {p.Fecha}");
        }
    }
}