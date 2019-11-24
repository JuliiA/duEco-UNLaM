using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_AlbumTratamiento
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public char AlT_Id { get; set; }

        [MaxLength(300)]
        public String AlT_UrlImagen { get; set; }

        [MaxLength(36)]
        public char AlT_Tra_Id { get; set; }

        public char AlT_Baja { get; set; }
    }
}
