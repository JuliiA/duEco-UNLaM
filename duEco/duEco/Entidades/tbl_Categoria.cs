using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Categoria
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public string Cat_Id { get; set; }

        public string Cat_Nombre { get; set; }
    }
}
