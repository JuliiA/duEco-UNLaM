using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace duEco.Entidades
{
    public class tbl_Etapa
    {
        /// <summary>
        ///  id interno para sqlite
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        // Atributos coincidentes con los Modelos de Carpeta -> Model
        [MaxLength(36)]
        public String Eta_Id { get; set; }

        [MaxLength(36)]
        public String Eta_Tar_Id { get; set; }

        [MaxLength(36)]
        public String Eta_Pla_Id { get; set; }

        public int Eta_DiasDuracion { get; set; }

        [MaxLength(300)]
        public String Eta_Descripcion { get; set; }

        public String Eta_Baja { get; set; }
    }
}
