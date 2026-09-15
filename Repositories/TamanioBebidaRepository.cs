using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class TamanioBebidaRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public TamanioBebidaRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }
        public async Task<int> AgregarAsync(TamanioBebida tamanio)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO TAMANIO_BEBIDA (Medida, Orden)
                  VALUES (@Medida, @Orden)",
                new
                {
                    tamanio.Medida,
                    tamanio.Orden
                });
        }
        public async Task<int> ModificarAsync(TamanioBebida tamanio)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE TAMANIO_BEBIDA
                  SET Medida = @Medida,
                      Orden = @Orden
                  WHERE ID = @ID",
                new
                {
                    tamanio.ID,
                    tamanio.Medida,
                    tamanio.Orden
                });
        }
        public async Task<int> BorrarAsync(int tamanioID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM TAMANIO_BEBIDA
                  WHERE ID = @ID",
                new
                {
                    ID = tamanioID
                });
        }
        public async Task<TamanioBebida?> ObtenerTamanioBebidaAsync(int tamanioID)
        {
            List<TamanioBebida> resultados = await _conn.QueryAsync<TamanioBebida>(
                @"SELECT ID, Medida, Orden
                  FROM TAMANIO_BEBIDA
                  WHERE ID = @ID",
                new
                {
                    ID = tamanioID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<TamanioBebida>> ObtenerTamanioBebidasAsync()
        {
            return _conn.QueryAsync<TamanioBebida>(
                @"SELECT ID, Medida, Orden
                  FROM TAMANIO_BEBIDA
                  ORDER BY Orden");
        }

    }
}
