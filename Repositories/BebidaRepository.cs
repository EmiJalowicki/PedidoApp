using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class BebidaRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public BebidaRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }
        public async Task<int> AgregarAsync(Bebida bebida)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO BEBIDA
                    (Nombre, EsAlcoholica)
                  VALUES (@Nombre, @EsAlcoholica)",
                new
                {
                    bebida.Nombre,
                    bebida.EsAlcoholica
                });
        }
        public async Task<int> ModificarAsync(Bebida bebida)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE BEBIDA
                  SET Nombre = @Nombre
                      EsAlcoholica = @EsAlcoholica
                  WHERE ID = @ID",
                new
                {
                    bebida.ID,
                    bebida.Nombre,
                    bebida.EsAlcoholica
                });
        }
        public async Task<int> BorrarAsync(int bebidaID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM BEBIDA
                  WHERE ID = @ID",
                new
                {
                    ID = bebidaID
                });
        }
        public async Task<Bebida?> ObtenerAsync(int bebidaID)
        {
            List<Bebida> resultados = await _conn.QueryAsync<Bebida>(
                @"SELECT ID, Nombre, EsAlcoholica
                  FROM BEBIDA
                  WHERE ID = @ID",
                new
                {
                    ID = bebidaID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<Bebida>> ListarAsync()
        {
            return _conn.QueryAsync<Bebida>(
                @"SELECT ID, Nombre, EsAlcoholica
                  FROM BEBIDA
                  ORDER BY Nombre");
        }

    }
}
