using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace duEco.Entidades
{
    public class tbl_Tarea
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public string Tar_Id { get; set; }

        [MaxLength(80)]
        public String Tar_Nombre { get; set; }

        [MaxLength(1)]
        public String Tar_Baja { get; set; }
    }
}
