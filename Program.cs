using Microsoft.Data.Sqlite;
using RegistroVideojuegosDilanTapia.Datos;

Console.WriteLine("=================================");
Console.WriteLine("     REGISTRO DE VIDEOJUEGOS");
Console.WriteLine("=================================");

ConexionBD conexionBD = new ConexionBD();

using (SqliteConnection conexion = conexionBD.ObtenerConexion())
{
    conexion.Open();
    Console.WriteLine("\nConexion con SQLite exitosa.");

    string crearTabla = @"
        CREATE TABLE IF NOT EXISTS Videojuegos (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Nombre TEXT NOT NULL,
            Genero TEXT NOT NULL,
            Anio INTEGER NOT NULL
        );";

    using (SqliteCommand comando = new SqliteCommand(crearTabla, conexion))
    {
        comando.ExecuteNonQuery();
    }

    Console.WriteLine("Tabla creada correctamente.");

    string contarRegistros = "SELECT COUNT(*) FROM Videojuegos;";
    int cantidad;

    using (SqliteCommand comando = new SqliteCommand(contarRegistros, conexion))
    {
        cantidad = Convert.ToInt32(comando.ExecuteScalar());
    }

    if (cantidad == 0)
    {
        string insertar = @"
            INSERT INTO Videojuegos (Nombre, Genero, Anio)
            VALUES (@nombre, @genero, @anio);";

        using (SqliteCommand comando = new SqliteCommand(insertar, conexion))
        {
            comando.Parameters.AddWithValue("@nombre", "Minecraft");
            comando.Parameters.AddWithValue("@genero", "Sandbox");
            comando.Parameters.AddWithValue("@anio", 2011);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@nombre", "GTA V");
            comando.Parameters.AddWithValue("@genero", "Accion");
            comando.Parameters.AddWithValue("@anio", 2013);
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();

            comando.Parameters.AddWithValue("@nombre", "FIFA 23");
            comando.Parameters.AddWithValue("@genero", "Deportes");
            comando.Parameters.AddWithValue("@anio", 2022);
            comando.ExecuteNonQuery();
        }

        Console.WriteLine("Tres videojuegos registrados correctamente.");
    }
    else
    {
        Console.WriteLine("Los videojuegos ya se encuentran registrados.");
    }

    Console.WriteLine("\nLISTADO DE VIDEOJUEGOS");
    Console.WriteLine("---------------------------------");

    string consulta = "SELECT * FROM Videojuegos;";

    using (SqliteCommand comando = new SqliteCommand(consulta, conexion))
    {
        using (SqliteDataReader lector = comando.ExecuteReader())
        {
            while (lector.Read())
            {
                Console.WriteLine("ID: " + lector["Id"]);
                Console.WriteLine("Nombre: " + lector["Nombre"]);
                Console.WriteLine("Genero: " + lector["Genero"]);
                Console.WriteLine("Anio: " + lector["Anio"]);
                Console.WriteLine("---------------------------------");
            }
        }
    }
}

Console.WriteLine("\nPrograma finalizado.");