using duEco.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace duEco.Servicio
{
    public class PlantaServicio
    {
        internal static List<PlantaModel> todasLasPlantas()
        {
            return new PlantaModel().obtenerTodas();
        }

        internal static List<CategoriaModel> obtenerCategorias()
        {
            return new PlantaModel().todasLasCategorias();
        }

        internal static PlantaModel buscarPorId(string v)
        {
           return new PlantaModel().buscarPorId(v);
        }

        internal static List<PlantaModel> obtenerByCategoria(int itemSelec)
        {
            var dbPlanta = new PlantaModel();
            string CategoriaID = dbPlanta.obtenerIdCategoria(itemSelec);

            return dbPlanta.obtenerById(CategoriaID);
        }
    }
}
