using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PedidoApp.DataBase
{
    public class InicializarDB
    {
        readonly string _rutaDB;
        private const int VERSION_ACTUAL = 1;
        public InicializarDB()
        {
            _rutaDB = Path.Combine(
                FileSystem.AppDataDirectory, "PedidoAppDB.db3");
        }
        public void Inicializar()
        {
            if (!File.Exists(_rutaDB))
            {
                CrearBaseDatos();
                return;
            }
            //Si existe, verifica version de DB.
            using SQLiteConnection conn = new SQLiteConnection(_rutaDB);
            int versionActual = conn.ExecuteScalar<int>(
                "PRAGMA user_version;");
            if (versionActual < VERSION_ACTUAL)
            {
                AplicarMigraciones(conn, versionActual, VERSION_ACTUAL);
            }
            
        }
        private void CrearBaseDatos()
        {
            using Stream stream = FileSystem.OpenAppPackageFileAsync("Create_DB.sql").GetAwaiter().GetResult();
            using StreamReader reader = new StreamReader(stream);

            string scriptSQL = reader.ReadToEnd();

            using SQLiteConnection conn = new SQLiteConnection(_rutaDB);
            conn.Execute("PRAGMA foreign_keys = ON;");

            string[] comandos = scriptSQL.Split(
                new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string comando in comandos)
            {
                string sql = comando.Trim();
                if (!string.IsNullOrWhiteSpace(sql))
                {
                    conn.Execute(sql);
                }
            }
        }
        private void AplicarMigraciones(
            SQLiteConnection conn, int versionActual, int versionObjetivo)
        {
            //Falta implementar actualización por versión.
        }

    }

}
