namespace LigaBetPlayConsola.Models
{

    /// Representa un equipo dentro del torneo.
    /// Las estadísticas se actualizan únicamente a través del método ActualizarResultado
    /// para proteger el encapsulamiento del modelo.
    public class Equipo
    {
        public string Nombre { get; }

        public int PJ { get; private set; }
        public int PG { get; private set; }
        public int PP { get; private set; }
        public int PE { get; private set; }
        public int GF { get; private set; }
        public int GC { get; private set; }

    
        public int TP => (PG * 3) + PE;

    
        /// Diferencia de gol calculada automáticamente.
    
        public int DiferenciaGol => GF - GC;

        public Equipo(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del equipo no puede estar vacío.");

            Nombre = nombre.Trim();
        }

    
        /// Actualiza las estadísticas del equipo después de un partido.
        /// Suma partidos jugados, goles a favor, goles en contra
        /// y determina si fue victoria, empate o derrota.
    
        /// <param name="golesFavor">Goles anotados por el equipo.</param>
        /// <param name="golesContra">Goles recibidos por el equipo.</param>
        public void ActualizarResultado(int golesFavor, int golesContra)
        {
            if (golesFavor < 0 || golesContra < 0)
                throw new ArgumentException("Los goles no pueden ser negativos.");

            PJ++;
            GF += golesFavor;
            GC += golesContra;

            if (golesFavor > golesContra)
            {
                PG++;
            }
            else if (golesFavor == golesContra)
            {
                PE++;
            }
            else
            {
                PP++;
            }
        }

        public override string ToString()
        {
            return Nombre;
        }
    }
}