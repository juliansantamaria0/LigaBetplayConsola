using System.Text;
using LigaBetPlayConsola.Models;

namespace LigaBetPlayConsola.Services
{
    
    /// Contiene reportes y consultas del torneo utilizando LINQ.
    
    public class ConsultasLinq
    {
        private readonly IEnumerable<Equipo> _equipos;

        public ConsultasLinq(IEnumerable<Equipo> equipos)
        {
            _equipos = equipos;
        }

        
        /// Retorna la tabla de posiciones ordenada por:
        /// 1. Puntos
        /// 2. Diferencia de gol
        /// 3. Goles a favor
        /// 4. Nombre
        
        public List<Equipo> ObtenerTablaPosiciones()
        {
            return _equipos
                .OrderByDescending(e => e.TP)
                .ThenByDescending(e => e.DiferenciaGol)
                .ThenByDescending(e => e.GF)
                .ThenBy(e => e.Nombre)
                .ToList();
        }

        public Equipo? ObtenerLider()
        {
            return ObtenerTablaPosiciones().FirstOrDefault();
        }

        public List<Equipo> ObtenerTop3()
        {
            return ObtenerTablaPosiciones().Take(3).ToList();
        }

        public List<Equipo> ObtenerInvictos()
        {
            return _equipos
                .Where(e => e.PP == 0)
                .OrderByDescending(e => e.TP)
                .ThenByDescending(e => e.DiferenciaGol)
                .ThenByDescending(e => e.GF)
                .ThenBy(e => e.Nombre)
                .ToList();
        }

        public List<Equipo> ObtenerEquiposSinVictorias()
        {
            return _equipos
                .Where(e => e.PG == 0)
                .OrderBy(e => e.Nombre)
                .ToList();
        }

        
        /// Promedio de goles a favor por equipo en el torneo.
        
        public double ObtenerPromedioGolesFavor()
        {
            return _equipos.Any() ? _equipos.Average(e => e.GF) : 0;
        }

        
        /// Total de puntos sumados por todos los equipos.
        
        public int ObtenerTotalPuntos()
        {
            return _equipos.Sum(e => e.TP);
        }

        public int ObtenerTotalGolesMarcados()
        {
            return _equipos.Sum(e => e.GF);
        }

        public string ObtenerResumenEstadistico()
        {
            int totalEquipos = _equipos.Count();
            double promedioGoles = ObtenerPromedioGolesFavor();
            int totalPuntos = ObtenerTotalPuntos();
            int totalGoles = ObtenerTotalGolesMarcados();

            StringBuilder sb = new();
            sb.AppendLine("===== RESUMEN GENERAL DEL TORNEO =====");
            sb.AppendLine($"Total de equipos: {totalEquipos}");
            sb.AppendLine($"Promedio de goles a favor por equipo: {promedioGoles:F2}");
            sb.AppendLine($"Total de goles marcados: {totalGoles}");
            sb.AppendLine($"Total de puntos sumados por todos los equipos: {totalPuntos}");

            return sb.ToString();
        }

        
        /// Genera una tabla de posiciones.
        
        public string GenerarTablaFormateada()
        {
            var tabla = ObtenerTablaPosiciones();
            StringBuilder sb = new();

            sb.AppendLine("============================================== TABLA DE POSICIONES ==============================================");
            sb.AppendLine(string.Format("{0,-4} {1,-22} {2,3} {3,3} {4,3} {5,3} {6,4} {7,4} {8,4} {9,4}",
                "Pos", "Equipo", "PJ", "PG", "PE", "PP", "GF", "GC", "DG", "TP"));
            sb.AppendLine(new string('-', 78));

            int posicion = 1;
            foreach (var equipo in tabla)
            {
                sb.AppendLine(string.Format("{0,-4} {1,-22} {2,3} {3,3} {4,3} {5,3} {6,4} {7,4} {8,4} {9,4}",
                    posicion,
                    equipo.Nombre,
                    equipo.PJ,
                    equipo.PG,
                    equipo.PE,
                    equipo.PP,
                    equipo.GF,
                    equipo.GC,
                    equipo.DiferenciaGol,
                    equipo.TP));
                posicion++;
            }

            return sb.ToString();
        }

        public string GenerarReporteDestacado()
        {
            var lider = ObtenerLider();
            var top3 = ObtenerTop3();
            var invictos = ObtenerInvictos();
            var sinVictorias = ObtenerEquiposSinVictorias();

            StringBuilder sb = new();
            sb.AppendLine("===== ESTADÍSTICAS DESTACADAS =====");
            sb.AppendLine($"Líder actual: {(lider is not null ? lider.Nombre : "No disponible")}");

            sb.AppendLine();
            sb.AppendLine("Top 3:");
            if (top3.Any())
            {
                int i = 1;
                foreach (var equipo in top3)
                {
                    sb.AppendLine($"{i}. {equipo.Nombre} - {equipo.TP} pts");
                    i++;
                }
            }
            else
            {
                sb.AppendLine("Sin datos.");
            }

            sb.AppendLine();
            sb.AppendLine("Invictos:");
            if (invictos.Any())
                sb.AppendLine(string.Join(", ", invictos.Select(e => e.Nombre)));
            else
                sb.AppendLine("No hay equipos invictos.");

            sb.AppendLine();
            sb.AppendLine("Equipos sin victorias:");
            if (sinVictorias.Any())
                sb.AppendLine(string.Join(", ", sinVictorias.Select(e => e.Nombre)));
            else
                sb.AppendLine("Todos los equipos han ganado al menos un partido.");

            return sb.ToString();
        }
    }
}