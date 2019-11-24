using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Alerta
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model

        public int Ale_Id { get; set; }

        [MaxLength(36)]
        public string Ale_Usu_Id { get; set; }

        [MaxLength(36)]
        public String Ale_Hue_Id { get; set; }

        [MaxLength(300)]
        public String Ale_Descripcion { get; set; }

        public DateTime Ale_Fecha_Hora { get; set; }

        [MaxLength(1)]
        public String Ale_Avisa { get; set; }

        [MaxLength(36)]
        public String Ale_TAl_Id { get; set; }

        [MaxLength(1)]
        public String Ale_Baja { get; set; }
    }
}
