using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Planta
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public string Pla_Id { get; set; }

        [MaxLength(200)]
        public String Pla_Nombre { get; set; }

        [MaxLength(36)]
        public string Pla_TPl_Id { get; set; }

        public byte[] Pla_Imagen { get; set; }

        [MaxLength(300)]
        public string Pla_Descripcion { get; set; }

        [MaxLength(36)]
        public string Pla_EpocaSiembra { get; set; }

        [MaxLength(150)]
        public string Pla_TipoSiembra { get; set; }

        [MaxLength(25)]
        public string Pla_DistanciaEntrePlantas { get; set; }

        [MaxLength(25)]
        public string Pla_Dificultad { get; set; }

        [MaxLength(300)]
        public string Pla_ValorNutricional { get; set; }
    }
}
