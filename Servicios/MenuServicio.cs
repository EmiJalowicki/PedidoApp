using PedidoApp.Repositories;
using PedidoApp.Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace PedidoApp.Servicios
{
    internal class MenuServicio
    {
        private readonly PizzaRepository _pizzaRepository;
        private readonly EmpanadaRepository _empanadaRepository;
        private readonly MilanesaRepository _milanesaRepository;

        public MenuServicio(
            PizzaRepository pizzaRepository,
            EmpanadaRepository empanadaRepository,
            MilanesaRepository milanesaRepository)
        {
            _pizzaRepository = pizzaRepository;
            _empanadaRepository = empanadaRepository;
            _milanesaRepository = milanesaRepository;
        }
        public async Task AgregarAsync(NuevoItemMenu item)
        {
            switch (item.Categoria)
            {
                case "Pizza":
                    await _pizzaRepository.AgregarAsync(new Pizza
                    {
                        Nombre = item.Nombre,
                        Precio = item.Precio
                    });
                    break;

                case "Empanada":
                    await _empanadaRepository.AgregarAsync(new Empanada
                    {
                        Nombre = item.Nombre,
                        Precio = item.Precio
                    });
                    break;

                case "Milanesa":
                    await _milanesaRepository.AgregarAsync(new Milanesa
                    {
                        Nombre = item.Nombre,
                        Precio = item.Precio
                    });
                    break;

                default:
                    throw new ArgumentException(
                        $"Categoría no soportada: {item.Categoria}");
            }

        }
    }

}
