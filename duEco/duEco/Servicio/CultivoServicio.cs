using duEco.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace duEco.Servicio
{
    public class CultivoServicio
    {
        public class InfoPlanta
        {
            public List<CultivoModel.TareasEtapas> tareas { get; set; }
        }

        internal static CultivoModel BuscarPorId(string cultivoID)
        {
            return new CultivoModel().ConsultarPorId(cultivoID);
        }

        internal static bool CrearCultivo(string laPlanta, string laHuerta, DateTime fechaSiembraSelec, string idCultivo)
        {            
            //falta cul_FinSiembra == comienzo de riego
            var finSiembra = fechaSiembraSelec.AddDays(2);
            //falta cul_finCosecha
            var finCosecha = finSiembra.AddDays(50);
            var plantaPadre = new PlantaModel().buscarPorId(laPlanta);

            var nuevoCultivo = new CultivoModel
            {
                IdCultivo = idCultivo,
                FinSiembra = finSiembra,
                FinCosecha = finCosecha,
                IniciaCultivo = fechaSiembraSelec,
                NombreCultivo = plantaPadre.nombre
            };

            var ok = nuevoCultivo.CrearCultivo(nuevoCultivo, laPlanta, laHuerta);
            if (ok)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static List<CultivoModel.TareasEtapas> TraerTareas(string idPlanta)
        {
            var info = new CultivoModel().obtenerTareasEtapas(idPlanta);
            //.obtenerFechasDeEtapas(idPlanta);

            return info;
        }
    }
}
