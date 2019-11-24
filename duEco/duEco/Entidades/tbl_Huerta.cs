using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Huerta
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public String Hue_Id { get; set; }

        [MaxLength(200)]
        public String Hue_Nombre { get; set; }

        [MaxLength(500)]
        public String Hue_Descripcion { get; set; }

        [MaxLength(36)]
        public String Hue_Usu_Id { get; set; }

        [MaxLength(1)]
        public String Hue_Baja { get; set; }
         
    }
}
