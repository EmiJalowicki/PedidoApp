using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class TamanioHeladoRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public TamanioHeladoRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }
        public async Task<int> AgregarAsync(TamanioHelado helado)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO TAMANIO_HELADO
                    (Descripcion, Orden, Precio, EstaActivo)
                  VALUES (@Descripcion, @Orden, @Precio, 1)",
                new
                {
                    helado.Descripcion,
                    helado.Orden,
                    helado.Precio,
                });
        }
        public async Task<int> ModificarAsync(TamanioHelado helado)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE TAMANIO_HELADO
                  SET Descripcion = @Descripcion,
                      Orden = @Orden,
                      Precio = @Precio,
                      EstaActivo = @EstaActivo
                  WHERE ID = @ID",
                new
                {
                    helado.ID,
                    helado.Descripcion,
                    helado.Orden,
                    helado.Precio,
                    helado.EstaActivo
                });
        }
        public async Task<int> BorrarAsync(int heladoID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM TAMANIO_HELADO
                  WHERE ID = @ID",
                new
                {
                    ID = heladoID
                });
        }
        public async Task<TamanioHelado?> ObtenerAsync(int heladoID)
        {
            List<TamanioHelado> resultados = await _conn.QueryAsync<TamanioHelado>(
                @"SELECT ID, Descripcion, Orden, Precio, EstaActivo
                  FROM TAMANIO_HELADO
                  WHERE ID = @ID",
                new
                {
                    ID = heladoID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<TamanioHelado>> ListarAsync(bool soloActivos)
        {
            string consulta = soloActivos ?
                @"SELECT ID, Descripcion, Orden, Precio, EstaActivo
                  FROM TAMANIO_HELADO
                  WHERE EstaActivo = 1
                  ORDER BY Orden"
              : @"SELECT ID, Descripcion, Orden, Precio, EstaActivo
                  FROM TAMANIO_HELADO
                  ORDER BY Orden";
            return _conn.QueryAsync<TamanioHelado>(consulta);
        }

    }
}
