using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Tratamiento
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public char Tra_Id { get; set; }

        [MaxLength(500)]
        public String Tra_Descripcion { get; set; }

        [MaxLength(36)]
        public char Tra_Cul_Id { get; set; }

        public char Tra_Baja { get; set; }
        
    }
}
