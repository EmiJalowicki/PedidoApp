using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using PedidoApp.DataBase;
using PedidoApp.Modelos;

namespace PedidoApp.Repositories
{
    internal class PizzaRepository
    {
        private readonly SQLiteAsyncConnection _conn;
        public PizzaRepository(AbrirConexionDB abrirConexionDB)
        {
            _conn = abrirConexionDB.Conexion;
        }

        public async Task<int> AgregarAsync(Pizza pizza)
        {
            return await _conn.ExecuteAsync(
                @"INSERT INTO PIZZA
                    (Nombre, Precio, EstaActivo)
                  VALUES (@Nombre, @Precio, 1)",
                new
                {
                    pizza.Nombre,
                    pizza.Precio
                });
        }
        public async Task<int> ModificarAsync(Pizza pizza)
        {
            return await _conn.ExecuteAsync(
                @"UPDATE PIZZA
                  SET Nombre = @Nombre
                      Precio = @Precio
                      EstaActivo = @EstaActivo
                  WHERE ID = @ID",
                new
                {
                    pizza.ID,
                    pizza.Nombre,
                    pizza.Precio,
                    pizza.EstaActivo
                });
        }
        public async Task<int> BorrarAsync(int pizzaID)
        {
            return await _conn.ExecuteAsync(
                @"DELETE FROM PIZZA
                    WHERE ID = @ID",
                new
                {
                    ID = pizzaID
                });
        }
        public async Task<Pizza?> ObtenerAsync(int pizzaID)
        {
            List<Pizza> resultados = await _conn.QueryAsync<Pizza>(
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM PIZZA
                  WHERE ID = @ID",
                new
                {
                    ID = pizzaID
                });
            return resultados.FirstOrDefault();
        }
        public Task<List<Pizza>> ListarAsync(bool soloActivos)
        {
            string consulta = soloActivos ?
                @"SELECT ID, Nombre, Precio, EstaActivo
                  FROM PIZZA 
                  WHERE EstaActivo = 1
                  ORDER BY NOMBRE"
               :@"SELECT ID, Nombre, Precio, EstaActivo
                  FROM PIZZA
                  ORDER BY NOMBRE";
            return _conn.QueryAsync<Pizza>(consulta);
        }

    }
}
