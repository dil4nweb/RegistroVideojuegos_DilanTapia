using Microsoft.Data.Sqlite;

namespace RegistroVideojuegosDilanTapia.Datos;

public class ConexionBD
{
    private readonly string cadenaConexion = "Data Source=videojuegos.db";

    public SqliteConnection ObtenerConexion()
    {
        return new SqliteConnection(cadenaConexion);
    }
}