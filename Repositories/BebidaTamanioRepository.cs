using PedidoApp.DataBase;
using PedidoApp.Modelos;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Repositories
{
    internal class BebidaTamanioRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public BebidaTamanioRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }
        //BebidaTamanio
        public async Task<int> AgregarAsync(BebidaTamanio bebidaTamanio)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO BEBIDA_TAMANIO
                    (BebidaID, TamanioBebidaID, Precio, EstaActivo)
                  VALUES
                    (@BebidaID, @TamanioBebidaID, @Precio, 1)",
                new
                {
                    bebidaTamanio.BebidaID,
                    bebidaTamanio.TamanioBebidaID,
                    bebidaTamanio.Precio
                });
        }
        public async Task<int> ModificarAsync(BebidaTamanio bebidaTamanio)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE BEBIDA_TAMANIO
                  SET Precio = @Precio,
                      EstaActivo = @EstaActivo
                  WHERE BebidaID = @BebidaID AND TamanioBebidaID = @TamanioBebidaID",
                new
                {
                    bebidaTamanio.BebidaID,
                    bebidaTamanio.TamanioBebidaID,
                    bebidaTamanio.Precio,
                    bebidaTamanio.EstaActivo
                });
        }
        public async Task<int> BorrarAsync(int bebidaID, int tamanioBebidaID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM BEBIDA_TAMANIO
                  WHERE BebidaID = @BebidaID AND TamanioBebidaID = @TamanioBebidaID",
                new
                {
                    BebidaID = bebidaID,
                    TamanioBebidaID = tamanioBebidaID
                });
        }
        public async Task<BebidaTamanio?> ObtenerAsync(int bebidaID, int tamanioBebidaID)
        {
            List<BebidaTamanio> resultado = await _conn.QueryAsync<BebidaTamanio>(
                @"SELECT BebidaID, TamanioBebidaID, Precio, EstaActivo
                  FROM BEBIDA_TAMANIO
                  WHERE BebidaID = @BebidaID AND TamanioBebidaID = @TamanioBebidaID",
                new
                {
                    BebidaID = bebidaID,
                    TamanioBebidaID = tamanioBebidaID
                });
            return resultado.FirstOrDefault();
        }
        public Task<List<BebidaTamanio>> ListarAsync(bool soloActivos)
        {
            string consulta = soloActivos ?
                @"SELECT BebidaID, TamanioBebidaID, Precio, EstaActivo
                  FROM BEBIDA_TAMANIO
                  WHERE EstaActivo = 1"
               : @"SELECT BebidaID, TamanioBebidaID, Precio, EstaActivo
                  FROM BEBIDA_TAMANIO";
            return _conn.QueryAsync<BebidaTamanio>(consulta);
        }

        //BebidaConTamanio (JOIN)
        public Task<List<BebidaConTamanio>> ListarTamaniosDeBebidaAsync(int bebidaID) //Tamaños de una bebida
        {
            return _conn.QueryAsync<BebidaConTamanio>(
                @"SELECT
                    B.ID AS BebidaID,
                    B.Nombre AS BebidaNombre,
                    TB.ID AS TamanioBebidaID,
                    TB.Medida,
                    BT.Precio,
                    BT.EstaActivo
                  FROM BEBIDA_TAMANIO BT
                  INNER JOIN BEBIDA B
                    ON B.ID = BT.BebidaID
                  INNER JOIN TAMANIO_BEBIDA TB
                    ON TB.ID = BT.TamanioBebidaID
                  WHERE BT.BebidaID = @BebidaID
                    AND BT.EstaActivo = 1
                  ORDER BY TB.Orden",
                new
                {
                    BebidaID = bebidaID
                });
        }
        public Task<List<BebidaConTamanio>> ListarBebidasDisponiblesAsync() //Mostrar todas las botellas disponibles
        {
            return _conn.QueryAsync<BebidaConTamanio>(
                @"SELECT
                    B.ID AS BebidaID,
                    B.Nombre AS BebidaNombre,
                    TB.ID AS TamanioBebidaID,
                    TB.Medida,
                    BT.Precio,
                    BT.EstaActivo
                  FROM BEBIDA_TAMANIO BT
                  INNER JOIN BEBIDA B
                      ON B.ID = BT.BebidaID
                  INNER JOIN TAMANIO_BEBIDA TB
                      ON TB.ID = BT.TamanioBebidaID
                  WHERE BT.EstaActivo = 1
                  ORDER BY B.Nombre, TB.Orden");
        }

    }
}
