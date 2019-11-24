using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Usuario
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public string Usu_Id { get; set; }

        [MaxLength(200)]
        public string Usu_Nombre { get; set; }

        [MaxLength(80)]
        public string Usu_Email { get; set; }

        [MaxLength(80)]
        public string Usu_Password { get; set; }

        public string Usu_Baja { get; set; }
    }
}
