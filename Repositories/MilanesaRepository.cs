using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class MilanesaRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public MilanesaRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }

        public async Task<int> AgregarAsync(Milanesa milanesa)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO MILANESA
                    (Nombre, Precio, EstaActivo)
                  VALUES (@Nombre, @Precio, 1)",
                new
                {
                    milanesa.Nombre,
                    milanesa.Precio
                });
        }
        public async Task<int> ModificarAsync(Milanesa milanesa)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE MILANESA
                  SET Nombre = @Nombre
                      Precio = @Precio
                      EstaActivo = @EstaActivo
                  WHERE ID = @ID",
                new
                {
                    milanesa.ID,
                    milanesa.Nombre,
                    milanesa.Precio,
                    milanesa.EstaActivo
                });
        }
        public async Task<int> BorrarAsync(int milanesaID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM MILANESA
                    WHERE ID = @ID",
                new
                {
                    ID = milanesaID
                });
        }
        public async Task<Milanesa?> ObtenerAsync(int milanesaID)
        {
            List<Milanesa> resultados = await _conn.QueryAsync<Milanesa>(
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM MILANESA
                  WHERE ID = @ID",
                new
                {
                    ID = milanesaID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<Milanesa>> ListarAsync(bool soloActivos)
        {
            string consulta = soloActivos ?
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM MILANESA 
                  WHERE EstaActivo = 1
                  ORDER BY NOMBRE"
               : @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM MILANESA
                  ORDER BY NOMBRE";
            return _conn.QueryAsync<Milanesa>(consulta);
        }

    }
}
