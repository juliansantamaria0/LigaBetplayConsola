using LigaBetPlayConsola.Services;

namespace LigaBetPlayConsola
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestionTorneo gestionTorneo = new();
            bool salir = false;

            CargarEquiposIniciales(gestionTorneo);

            while (!salir)
            {
                Console.Clear();
                MostrarEncabezado();
                MostrarMenu();

                Console.Write("\nSeleccione una opción: ");
                string? opcion = Console.ReadLine();

                Console.Clear();

                switch (opcion)
                {
                    case "1":
                        ListarEquipos(gestionTorneo);
                        break;

                    case "2":
                        RegistrarEquipo(gestionTorneo);
                        break;

                    case "3":
                        SimularPartido(gestionTorneo);
                        break;

                    case "4":
                        VerTablaPosiciones(gestionTorneo);
                        break;

                    case "5":
                        VerEstadisticasDestacadas(gestionTorneo);
                        break;

                    case "6":
                        VerResumenGeneral(gestionTorneo);
                        break;

                    case "0":
                        salir = true;
                        Console.WriteLine("Saliendo del sistema... ¡Hasta pronto!");
                        break;

                    default:
                        Console.WriteLine("Opción inválida. Intente nuevamente.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        static void MostrarEncabezado()
        {
            Console.WriteLine("======================================================");
            Console.WriteLine("      SIMULADOR DE LA LIGA BETPLAY - CONSOLA C#       ");
            Console.WriteLine("======================================================");
        }

        static void MostrarMenu()
        {
            Console.WriteLine("1. Listar equipos");
            Console.WriteLine("2. Registrar equipo");
            Console.WriteLine("3. Simular partido");
            Console.WriteLine("4. Ver tabla de posiciones");
            Console.WriteLine("5. Consultar estadísticas destacadas");
            Console.WriteLine("6. Ver resumen general del torneo");
            Console.WriteLine("0. Salir");
        }

        static void CargarEquiposIniciales(GestionTorneo gestionTorneo)
        {
            string[] equiposIniciales =
            {
                "Atlético Nacional",
                "Millonarios",
                "América de Cali",
                "Junior",
                "Independiente Medellín",
                "Santa Fe"
            };

            foreach (var equipo in equiposIniciales)
            {
                gestionTorneo.RegistrarEquipo(equipo);
            }
        }

        static void ListarEquipos(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== EQUIPOS REGISTRADOS =====");

            if (!gestionTorneo.Equipos.Any())
            {
                Console.WriteLine("No hay equipos registrados.");
                return;
            }

            int contador = 1;
            foreach (var equipo in gestionTorneo.Equipos.OrderBy(e => e.Nombre))
            {
                Console.WriteLine($"{contador}. {equipo.Nombre}");
                contador++;
            }
        }

        static void RegistrarEquipo(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== REGISTRAR EQUIPO =====");
            Console.Write("Ingrese el nombre del equipo: ");
            string? nombre = Console.ReadLine();

            bool registrado = gestionTorneo.RegistrarEquipo(nombre ?? string.Empty);

            if (registrado)
                Console.WriteLine("Equipo registrado correctamente.");
            else
                Console.WriteLine("No fue posible registrar el equipo. Verifique que el nombre no esté vacío o duplicado.");
        }

        static void SimularPartido(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== SIMULAR PARTIDO =====");

            if (gestionTorneo.Equipos.Count < 2)
            {
                Console.WriteLine("Se requieren al menos 2 equipos registrados para simular un partido.");
                return;
            }

            Console.Write("Ingrese el nombre del equipo local: ");
            string local = Console.ReadLine() ?? string.Empty;

            Console.Write("Ingrese el nombre del equipo visitante: ");
            string visitante = Console.ReadLine() ?? string.Empty;

            int golesLocal = LeerEnteroNoNegativo("Ingrese los goles del equipo local: ");
            int golesVisitante = LeerEnteroNoNegativo("Ingrese los goles del equipo visitante: ");

            string resultado = gestionTorneo.SimularPartido(local, visitante, golesLocal, golesVisitante);
            Console.WriteLine(resultado);
        }

        static void VerTablaPosiciones(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== TABLA DE POSICIONES =====\n");
            ConsultasLinq consultas = new(gestionTorneo.Equipos);
            Console.WriteLine(consultas.GenerarTablaFormateada());
        }

        static void VerEstadisticasDestacadas(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== CONSULTAS Y REPORTES =====\n");
            ConsultasLinq consultas = new(gestionTorneo.Equipos);
            Console.WriteLine(consultas.GenerarReporteDestacado());
        }

        static void VerResumenGeneral(GestionTorneo gestionTorneo)
        {
            Console.WriteLine("===== RESUMEN GENERAL =====\n");
            ConsultasLinq consultas = new(gestionTorneo.Equipos);
            Console.WriteLine(consultas.ObtenerResumenEstadistico());
        }

        static int LeerEnteroNoNegativo(string mensaje)
        {
            int valor;
            do
            {
                Console.Write(mensaje);
                string? entrada = Console.ReadLine();

                if (int.TryParse(entrada, out valor) && valor >= 0)
                    return valor;

                Console.WriteLine("Entrada inválida. Debe ingresar un número entero mayor o igual a cero.");
            }
            while (true);
        }
    }
}