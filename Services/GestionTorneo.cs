using LigaBetPlayConsola.Models;

namespace LigaBetPlayConsola.Services
{
    
    /// Gestiona el registro de equipos y la simulación de partidos del torneo
    public class GestionTorneo
    {
        private readonly List<Equipo> _equipos = new();

        
        /// Expone los equipos en modo solo lectura para evitar modificaciones externas.
        public IReadOnlyList<Equipo> Equipos => _equipos.AsReadOnly();

        
        /// Registra un nuevo equipo en el torneo.
        /// Evita nombres duplicados ignorando mayúsculas/minúsculas.
        /// <param name="nombre">Nombre del equipo.</param>
        /// <returns>True si se registró correctamente; false si ya existe o el nombre es inválido.</returns>
        public bool RegistrarEquipo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return false;

            string nombreNormalizado = nombre.Trim();

            bool existe = _equipos.Any(e =>
                e.Nombre.Equals(nombreNormalizado, StringComparison.OrdinalIgnoreCase));

            if (existe)
                return false;

            _equipos.Add(new Equipo(nombreNormalizado));
            return true;
        }

        
        /// Busca un equipo por nombre.
        public Equipo? ObtenerEquipoPorNombre(string nombre)
        {
            return _equipos.FirstOrDefault(e =>
                e.Nombre.Equals(nombre.Trim(), StringComparison.OrdinalIgnoreCase));
        }

        
        /// Simula un partido entre dos equipos existentes y actualiza automáticamente
        /// las estadísticas de ambos.
        /// <param name="nombreEquipoLocal">Nombre del primer equipo.</param>
        /// <param name="nombreEquipoVisitante">Nombre del segundo equipo.</param>
        /// <param name="golesLocal">Goles del equipo local.</param>
        /// <param name="golesVisitante">Goles del equipo visitante.</param>
        /// <returns>Mensaje con el resultado de la operación.</returns>
        public string SimularPartido(string nombreEquipoLocal, string nombreEquipoVisitante, int golesLocal, int golesVisitante)
        {
            if (string.IsNullOrWhiteSpace(nombreEquipoLocal) || string.IsNullOrWhiteSpace(nombreEquipoVisitante))
                return "Los nombres de los equipos son obligatorios.";

            if (golesLocal < 0 || golesVisitante < 0)
                return "Los goles no pueden ser negativos.";

            if (nombreEquipoLocal.Trim().Equals(nombreEquipoVisitante.Trim(), StringComparison.OrdinalIgnoreCase))
                return "Un equipo no puede jugar contra sí mismo.";

            Equipo? local = ObtenerEquipoPorNombre(nombreEquipoLocal);
            Equipo? visitante = ObtenerEquipoPorNombre(nombreEquipoVisitante);

            if (local is null || visitante is null)
                return "Uno o ambos equipos no existen. Verifica los nombres antes de simular el partido.";

            local.ActualizarResultado(golesLocal, golesVisitante);
            visitante.ActualizarResultado(golesVisitante, golesLocal);

            return $"Partido registrado: {local.Nombre} {golesLocal} - {golesVisitante} {visitante.Nombre}";
        }
    }
}