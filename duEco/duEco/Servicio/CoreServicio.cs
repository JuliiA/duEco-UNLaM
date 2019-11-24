using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQLite;
using System.Security.Cryptography;

namespace duEco.Servicio
{
    public class CoreServicio
    {
        internal static void ValidarTablasCreadas()
        {
            string dbName = "duEcoSQLite.db3";
            string path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), dbName);
            var db = new SQLiteConnection(path);

            try
            {
                //Descomentar para crear tablas nuevamente
                //db.DropTable<Entidades.tbl_Planta>();
                //db.DropTable<Entidades.tbl_Cultivo>();
                //db.DropTable<Entidades.tbl_Alerta>();
                //               

                var existTable = db.GetTableInfo("tbl_Usuario"); //db.Query<Entidades.tbl_Usuario>("SELECT count(*) FROM sqlite_master WHERE type = 'Table' AND name = 'tbl_Usuario' ");

                if (existTable.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Usuario>();
                }

                var existPlanta = db.GetTableInfo("tbl_Planta"); //db.Query<Entidades.tbl_Usuario>("SELECT count(*) FROM sqlite_master WHERE type = 'Table' AND name = 'tbl_Planta' ");
                if (existPlanta.Count == 0)
                {                    
                    db.CreateTable<Entidades.tbl_Planta>();
                    if (db.Table<Entidades.tbl_Planta>().Count() == 0)
                    {
                        // only insert the data if it doesn't already exist
                        var newPlanta = new Entidades.tbl_Planta();
                        newPlanta.Pla_Id = "855C32D15C64E751238C1C80A4C782D9";
                        newPlanta.Pla_Nombre = "Boldo";
                        newPlanta.Pla_TPl_Id = "6f70a13783bf4860bf790627ce995767"; //Hoja 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita de Pleno Sol";
                        newPlanta.Pla_EpocaSiembra = "Todo el año";
                        newPlanta.Pla_TipoSiembra = "Por semilla, esquejes o transplante"; 
                        newPlanta.Pla_DistanciaEntrePlantas = "0 - Plantar solo";
                        newPlanta.Pla_Dificultad = "Intermedia";
                        newPlanta.Pla_ValorNutricional = "Es rico en aceites esenciales eucaliptol y el ascaridioil. Posee flavonoides y alcaloides. Posee accrion digestiva, antioxidante, depurativa, diuretica y antiinflamatoria";
                        db.Insert(newPlanta);

                        newPlanta.Pla_Id = "354CA05DDADF41E3FD143AC891E5D712";
                        newPlanta.Pla_Nombre = "Cebolla";
                        newPlanta.Pla_TPl_Id = "6c43e855c46aef9f07e0ced4ade1e8b6"; //Bulbo 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita de Pleno Sol";
                        newPlanta.Pla_EpocaSiembra = "Otoño/Invierno";
                        newPlanta.Pla_TipoSiembra = "Semilla: en almacigo (feb-marzo), Siembra directa: 2da quincena abril";
                        newPlanta.Pla_DistanciaEntrePlantas = "15 cm";
                        newPlanta.Pla_Dificultad = "Intermedia";
                        newPlanta.Pla_ValorNutricional = "Contiene minerales como calcio, fosforo, hierro, magnesio, potasio, nitrogeno y zinc, tambien contiene vitaminas A y C entre otros.";
                        db.Insert(newPlanta);

                        newPlanta.Pla_Id = "4FA83FDC0117F4C2E5C321ED8810AB70";
                        newPlanta.Pla_Nombre = "Espinaca";
                        newPlanta.Pla_TPl_Id = "6f70a13783bf4860bf790627ce995767"; //Hoja 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita de Pleno Sol, tolera sombra. Conviene realizar siembras escalonadas cada 20 días.";
                        newPlanta.Pla_EpocaSiembra = "Otoño/Invierno";
                        newPlanta.Pla_TipoSiembra = "Semilla: siembra directa.En almacigo y transplante";
                        newPlanta.Pla_DistanciaEntrePlantas = "10 cm";
                        newPlanta.Pla_Dificultad = "Facil";
                        newPlanta.Pla_ValorNutricional = "Rica en hierro, fosforo, calcio, potasio y vitaminas a, b y c. Contiene acido, citrico y malico";
                        db.Insert(newPlanta);

                        newPlanta.Pla_Id = "D3C1A05C5108A6B07F1864CC92003A4C";
                        newPlanta.Pla_Nombre = "Menta";
                        newPlanta.Pla_TPl_Id = "6f70a13783bf4860bf790627ce995767"; //Hoja 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita de Pleno Sol. Tolera sombra";
                        newPlanta.Pla_EpocaSiembra = "Otoño/Invierno";
                        newPlanta.Pla_TipoSiembra = "Transplante por estolones de raiz o gajos";
                        newPlanta.Pla_DistanciaEntrePlantas = "20 cm";
                        newPlanta.Pla_Dificultad = "Facil";
                        newPlanta.Pla_ValorNutricional = "El aceite de sus hojas sirve para combatir hongos y como recipiente de plagas. Tiene poder antiseptico. Hay distintas variedades que se utilizan en tragos, infusiones,etc";
                        db.Insert(newPlanta);

                        newPlanta.Pla_Id = "09440664B265A68564200AC25EB8E069";
                        newPlanta.Pla_Nombre = "Jengibre";
                        newPlanta.Pla_TPl_Id = "533b54b7ff9fe7b19bfd965ad37f12e4"; //Rizoma 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita sombra. El rizoma del jengibre crece de forma horizontal al suelo por lo que necesitas de un espacio amplio";
                        newPlanta.Pla_EpocaSiembra = "Primavera/Verano";
                        newPlanta.Pla_TipoSiembra = "Por division de matas o de rizomas";
                        newPlanta.Pla_DistanciaEntrePlantas = "30 cm";
                        newPlanta.Pla_Dificultad = "Intermedia";
                        newPlanta.Pla_ValorNutricional = "Se utiliza fresco rayado para agregar a jugos, te, pescados, wok, sopas, etc. En polvo como especia para cocinar";
                        db.Insert(newPlanta);

                        newPlanta.Pla_Id = "29B0718BB38C0D427134B9B28EEE56FA";
                        newPlanta.Pla_Nombre = "Zapallo";
                        newPlanta.Pla_TPl_Id = "f9e012ff0bbbf0c385910af0795786c9"; //Fruto 
                        newPlanta.Pla_Imagen = null;
                        newPlanta.Pla_Descripcion = "Necesita de Pleno Sol";
                        newPlanta.Pla_EpocaSiembra = "Primavera/Verano";
                        newPlanta.Pla_TipoSiembra = "Semilla siembra directa a 2cm de profundidad. Transplante";
                        newPlanta.Pla_DistanciaEntrePlantas = "100 cm";
                        newPlanta.Pla_Dificultad = "Alta";
                        newPlanta.Pla_ValorNutricional = "Se consume el fruto en comidas. Las flores fritas. Las semillas tostadas";
                        db.Insert(newPlanta);
                        var table = db.Table<Entidades.tbl_Planta>();

                        // correcciones 2da insercion
                        //db.Query<Entidades.tbl_Planta>("UPDATE tbl_Planta SET Pla_TPl_Id = ? WHERE Pla_Id = ?", "a25f21d368d34ff8bc64d0ec8396f3cd", "855C32D15C64E751238C1C80A4C782D9");
                        //db.Query<Entidades.tbl_Planta>("UPDATE tbl_Planta SET Pla_TPl_Id = ? WHERE Pla_Id = ?", "533b54b7ff9fe7b19bfd965ad37f12e4", "09440664B265A68564200AC25EB8E069");
                        //db.Query<Entidades.tbl_Planta>("UPDATE tbl_Planta SET Pla_TPl_Id = ? WHERE Pla_Id = ?", "f9e012ff0bbbf0c385910af0795786c9", "29B0718BB38C0D427134B9B28EEE56FA");
                        //db.Query<Entidades.tbl_Planta>("UPDATE tbl_Planta SET Pla_Nombre = ? WHERE Pla_Id = ?", "Zapallo", "29B0718BB38C0D427134B9B28EEE56FA");
                        //var tableC = db.Table<Entidades.tbl_Planta>();
                    }
                }

                var existTipoPlanta = db.GetTableInfo("tbl_TipoPlanta"); //db.Query<Entidades.tbl_Usuario>("SELECT count(*) FROM sqlite_master WHERE type = 'Table' AND name = 'tbl_Planta' ");
                
                if (existTipoPlanta.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_TipoPlanta>();
                    var newTipoPlanta = new Entidades.tbl_TipoPlanta();
                    newTipoPlanta.TPl_Id = "6f70a13783bf4860bf790627ce995767";
                    newTipoPlanta.TPl_Nombre = "Hoja";  
                    newTipoPlanta.TPl_Cat_Id = "48846075a34c2cf241c9c9300ae921f3";      //Hortaliza
                    db.Insert(newTipoPlanta);

                    newTipoPlanta.TPl_Id = "6c43e855c46aef9f07e0ced4ade1e8b6";
                    newTipoPlanta.TPl_Nombre = "Bulbo";
                    newTipoPlanta.TPl_Cat_Id = "48846075a34c2cf241c9c9300ae921f3";      //Hortaliza
                    db.Insert(newTipoPlanta);

                    newTipoPlanta.TPl_Id = "533b54b7ff9fe7b19bfd965ad37f12e4";
                    newTipoPlanta.TPl_Nombre = "Rizoma";
                    newTipoPlanta.TPl_Cat_Id = "a4fdh2d512gs3gee31h25tfea2s3ee3";      //Aromatica
                    db.Insert(newTipoPlanta);

                    newTipoPlanta.TPl_Id = "f9e012ff0bbbf0c385910af0795786c9";
                    newTipoPlanta.TPl_Nombre = "Fruto";
                    newTipoPlanta.TPl_Cat_Id = "48846075a34c2cf241c9c9300ae921f3";      //Hortaliza
                    db.Insert(newTipoPlanta);


                }
                var tableTP = db.Table<Entidades.tbl_TipoPlanta>().ToList();

                var existCategoria = db.GetTableInfo("tbl_Categoria"); //db.Query<Entidades.tbl_Usuario>("SELECT count(*) FROM sqlite_master WHERE type = 'Table' AND name = 'tbl_Planta' ");
                if (existCategoria.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Categoria>();
                    var newCategoria = new Entidades.tbl_Categoria();
                    newCategoria.Cat_Id = "48846075a34c2cf241c9c9300ae921f3";
                    newCategoria.Cat_Nombre = "Hortaliza";// item 0
                    db.Insert(newCategoria);

                    newCategoria.Cat_Id = "a4fdh2d512gs3gee31h25tfea2s3ee3";
                    newCategoria.Cat_Nombre = "Aromatica";//item 1
                    db.Insert(newCategoria);

                    newCategoria.Cat_Id = "6e51043bf9a7fb8f70092ce86474bcf7";
                    newCategoria.Cat_Nombre = "Arbusto"; // item 2
                    db.Insert(newCategoria);

                    var tableC = db.Table<Entidades.tbl_Categoria>();
                }

                ///Funcionalidad del UI
                var existHuerta = db.GetTableInfo("tbl_Huerta");
                if (existHuerta.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Huerta>();
                }

                var existCultivo = db.GetTableInfo("tbl_Cultivo");
                if (existCultivo.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Cultivo>();
                }

                var existTarea = db.GetTableInfo("tbl_Tarea");
                if (existTarea.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Tarea>();
                    var newTarea = new Entidades.tbl_Tarea();
                    newTarea.Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newTarea.Tar_Nombre = "Siembra";
                    newTarea.Tar_Baja = "N";
                    db.Insert(newTarea);

                    newTarea.Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newTarea.Tar_Nombre = "Riego";
                    newTarea.Tar_Baja = "N";
                    db.Insert(newTarea);

                    newTarea.Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newTarea.Tar_Nombre = "Cosecha";
                    newTarea.Tar_Baja = "N";
                    db.Insert(newTarea);
                }

                var existEtapa = db.GetTableInfo("tbl_Etapa");
                if (existEtapa.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Etapa>();
                    /*boldo*/
                    //Siembra
                    var newEtapa = new Entidades.tbl_Etapa();
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "855C32D15C64E751238C1C80A4C782D9";
                    newEtapa.Eta_Descripcion = "";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "855C32D15C64E751238C1C80A4C782D9";
                    newEtapa.Eta_Descripcion = "Riego escaso 3 veces a la semana durante el verano y 1 - 2 veces por semana el resto del año";
                    newEtapa.Eta_DiasDuracion = 90;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "855C32D15C64E751238C1C80A4C782D9";
                    newEtapa.Eta_Descripcion = "La cosecha del boldo se realiza durante el verano y sólo se trata de recolectar las hojas con la mano para luego secarlas";
                    newEtapa.Eta_DiasDuracion = 91;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);

                    /*cebolla*/
                    //Siembra
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "354CA05DDADF41E3FD143AC891E5D712";
                    newEtapa.Eta_Descripcion = "Respecto al tipo de suelo, prefiere suelos sueltos, permeables y ricos en materia orgánica, sin problemas de drenaje";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "354CA05DDADF41E3FD143AC891E5D712";
                    newEtapa.Eta_Descripcion = "regar por surco, suspendiendo los riegos 30 días antes de la cosecha";
                    newEtapa.Eta_DiasDuracion = 180;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "354CA05DDADF41E3FD143AC891E5D712";
                    newEtapa.Eta_Descripcion = "Podemos realizarla a los 180 a 210 días desde el transplante. Al iniciarse el proceso de maduración, las hojas a la altura del cuello del bulbo, se ablandan y se doblan sobre el suelo.";
                    newEtapa.Eta_DiasDuracion = 181;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);

                    /*espinaca*/
                    //Siembra
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "4FA83FDC0117F4C2E5C321ED8810AB70";
                    newEtapa.Eta_Descripcion = "Conviene realizar siembras escalonadas cada 20 días";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "4FA83FDC0117F4C2E5C321ED8810AB70";
                    newEtapa.Eta_Descripcion = "Riego moderado y frecuente";
                    newEtapa.Eta_DiasDuracion = 50;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "4FA83FDC0117F4C2E5C321ED8810AB70";
                    newEtapa.Eta_Descripcion = "Madura aproximadamente a los 75 días de la siembra, para ciclos invernales, y 40-45 días para los ciclos primaverales.";
                    newEtapa.Eta_DiasDuracion = 51;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);

                    /*menta*/
                    //Siembra
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "D3C1A05C5108A6B07F1864CC92003A4C";
                    newEtapa.Eta_Descripcion = "Si vas a plantar varios plántalos a 15 cm de distancia";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "D3C1A05C5108A6B07F1864CC92003A4C";
                    newEtapa.Eta_Descripcion = "Riega la menta con frecuencia durante el primer año";
                    newEtapa.Eta_DiasDuracion = 30;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "D3C1A05C5108A6B07F1864CC92003A4C";
                    newEtapa.Eta_Descripcion = "Revísala regularmente para asegurarte de que reciba la cantidad adecuada de agua, pero no demasiado";
                    newEtapa.Eta_DiasDuracion = 31;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);

                    /*jengibre*/
                    //Siembra
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "09440664B265A68564200AC25EB8E069";
                    newEtapa.Eta_Descripcion = " Planta la raíz de jengibre sobre la superficie de la tierra, a no más de 8 o 10 cm. de profundidad.";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "09440664B265A68564200AC25EB8E069";
                    newEtapa.Eta_Descripcion = "necesita de la humedad para crecer pues no soporta la sequía.";
                    newEtapa.Eta_DiasDuracion = 180;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "09440664B265A68564200AC25EB8E069";
                    newEtapa.Eta_Descripcion = "Hacia los 10 a 12 meses de la siembra, la planta ya está lo suficientemente madura para la recolección.";
                    newEtapa.Eta_DiasDuracion = 181;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);

                    /*zapallo*/
                    //Siembra
                    newEtapa.Eta_Id = "c97aeb6c7a3c993299bad1b85e79a050";
                    newEtapa.Eta_Tar_Id = "b560b578b94f54e5d9f15bd7a7bd84f0";
                    newEtapa.Eta_Pla_Id = "29B0718BB38C0D427134B9B28EEE56FA";
                    newEtapa.Eta_Descripcion = "Se realiza a golpe, de 3 a 5 semillas por golpe, a una profundidad de 2–3 cm";
                    newEtapa.Eta_DiasDuracion = 1;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Riego
                    newEtapa.Eta_Id = "999d3d55c6c0c5d403787c02342c6444";
                    newEtapa.Eta_Tar_Id = "5fa954634d4f9fd843f666110e91b8dc";
                    newEtapa.Eta_Pla_Id = "29B0718BB38C0D427134B9B28EEE56FA";
                    newEtapa.Eta_Descripcion = "";
                    newEtapa.Eta_DiasDuracion = 90;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                    //Cosecha
                    newEtapa.Eta_Id = "4cc709c1c7307d4b325a6b79dd4ddc3f";
                    newEtapa.Eta_Tar_Id = "78481b785c84ab474dd041b800f280b4";
                    newEtapa.Eta_Pla_Id = "29B0718BB38C0D427134B9B28EEE56FA";
                    newEtapa.Eta_Descripcion = "La cosecha se lleva a cabo a los 3-5 meses de la siembra";
                    newEtapa.Eta_DiasDuracion = 91;
                    newEtapa.Eta_Baja = "N";
                    db.Insert(newEtapa);
                }

                ///selects comprobatorios
                //    var allUsuarios = db.Query<Entidades.tbl_Usuario>("SELECT * FROM tbl_Usuario");
                //    var allEtapas = db.Query<Entidades.tbl_Etapa>("SELECT * FROM tbl_Etapa");
                //    var allTareas = db.Query<Entidades.tbl_Tarea>("SELECT * FROM tbl_Tarea");
                //    var allTareas = db.Query<Entidades.tbl_Planta>("SELECT * FROM tbl_Planta");
                //    var allHuertas = db.Table<Entidades.tbl_Huerta>().ToList();
                //    var allCultivos = db.Table<Entidades.tbl_Cultivo>().ToList();
                //TABLA TIPO ALERTA: CREACIÓN Y CARGA INICIAL
                var existTipoAlerta = db.GetTableInfo("tbl_TipoAlerta");
                if (existTipoAlerta.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_TipoAlerta>();
                    var newTipoAlerta = new Entidades.tbl_TipoAlerta();

                    newTipoAlerta.TAl_Id = "4cc709c1c7307d4brrra6b79dd4ddc3f";
                    newTipoAlerta.TAl_Descripcion = "Considerable";
                    newTipoAlerta.TAl_Baja = "N";
                    db.Insert(newTipoAlerta);

                    newTipoAlerta.TAl_Id = "4cc709c1c7307d4b325a6b79dd4sss3f";
                    newTipoAlerta.TAl_Descripcion = "Necesario";
                    newTipoAlerta.TAl_Baja = "N";
                    db.Insert(newTipoAlerta);

                    newTipoAlerta.TAl_Id = "4cc709c1c7307d4b325a6b79dd4ddm5i";
                    newTipoAlerta.TAl_Descripcion = "Fundamental";
                    newTipoAlerta.TAl_Baja = "N";
                    db.Insert(newTipoAlerta);
                }

                var existAlerta = db.GetTableInfo("tbl_Alerta");
                if (existAlerta.Count == 0)
                {
                    db.CreateTable<Entidades.tbl_Alerta>();
                }
                var compruebaAltaTabla = db.Table<Entidades.tbl_TipoAlerta>().ToList();
            }
            catch (Exception c)
            {
                throw c;
            }
        }

        public static class Encrypt
        {
            public static string GetMD5(string str)
            {
                MD5 md5 = MD5CryptoServiceProvider.Create();
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] stream = null;
                StringBuilder sb = new StringBuilder();
                stream = md5.ComputeHash(encoding.GetBytes(str));
                for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
                return sb.ToString();
            }
        }
    }
}
