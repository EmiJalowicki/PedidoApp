using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class EmpanadaRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public EmpanadaRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }

        public async Task<int> AgregarAsync(Empanada empanada)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO EMPANADA
                    (Nombre, Precio, EstaActivo)
                  VALUES (@Nombre, @Precio, 1)",
                new
                {
                    empanada.Nombre,
                    empanada.Precio
                });
        }
        public async Task<int> ModificarAsync(Empanada empanada)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE EMPANADA
                  SET Nombre = @Nombre
                      Precio = @Precio
                      EstaActivo = @EstaActivo
                  WHERE ID = @ID",
                new
                {
                    empanada.ID,
                    empanada.Nombre,
                    empanada.Precio,
                    empanada.EstaActivo
                });
        }
        public async Task<int> BorrarAsync(int empanadaID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM EMPANADA
                    WHERE ID = @ID",
                new
                {
                    ID = empanadaID
                });
        }
        public async Task<Empanada?> ObtenerAsync(int empanadaID)
        {
            List<Empanada> resultados = await _conn.QueryAsync<Empanada>(
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM EMPANADA
                  WHERE ID = @ID",
                new
                {
                    ID = empanadaID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<Empanada>> ListarAsync(bool soloActivos)
        {
            string consulta = soloActivos ?
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM EMPANADA 
                  WHERE EstaActivo = 1
                  ORDER BY NOMBRE"
               : @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM EMPANADA
                  ORDER BY NOMBRE";
            return _conn.QueryAsync<Empanada>(consulta);
        }

    }
}
