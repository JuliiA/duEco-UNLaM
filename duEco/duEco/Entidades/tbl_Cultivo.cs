using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Cultivo
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public String Cul_Id { get; set; }

        [MaxLength(200)]
        public String Cul_Nombre { get; set; }

        [MaxLength(36)]
        public String Cul_Pla_Id { get; set; }

        [MaxLength(36)]
        public String Cul_Hue_Id { get; set; }
        /// <summary>
        /// Campo fecha calculable 
        /// </summary>
        public DateTime Cul_IniciaCultivo { get; set; }

        /// <summary>
        /// Campo fecha calculable 
        /// </summary>
        public DateTime Cul_FinSiembra { get; set; }

        /// <summary>
        /// Campo fecha calculable 
        /// </summary>
        public DateTime Cul_FinCosecha { get; set; }

        [MaxLength(1)]
        public String Cul_Baja { get; set; }           

    }
}
