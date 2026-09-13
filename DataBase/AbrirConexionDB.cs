using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace PedidoApp.DataBase
{
    public class AbrirConexionDB
    {
        private readonly SQLiteAsyncConnection _conexion;
        public AbrirConexionDB(){
            string rutaDB = Path.Combine(
                FileSystem.AppDataDirectory, "PedidoAppDB.db3");
            _conexion = new SQLiteAsyncConnection(rutaDB);
        }
        public SQLiteAsyncConnection Conexion
        {
            get { return _conexion; }
        }

    }
}
