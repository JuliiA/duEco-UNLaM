using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_TipoPlanta
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public string TPl_Id { get; set; }

        [MaxLength(200)]
        public String TPl_Nombre { get; set; }

        [MaxLength(36)]
        public string TPl_Cat_Id { get; set; }        
    }
}
