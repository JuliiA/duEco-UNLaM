using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace duEco.Model
{
    public class PlantaModel
    {
        #region Propiedades
        private string Id;

        public string id
        {
            get { return Id; }
            set { Id = value; }
        }

        private string Nombre;

        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; }
        }

        private string ImagenPortada;

        public string imagenPortada
        {
            get { return ImagenPortada; }
            set { ImagenPortada = value; }
        }

        private string Descripcion;

        public string  descripcion
        {
            get { return Descripcion; }
            set { Descripcion = value; }
        }

        private string EpocaSiembra;

        public string epocaSiembra
        {
            get { return EpocaSiembra; }
            set { EpocaSiembra = value; }
        }

        private string TipoSiembra;

        public string tipoSiembra
        {
            get { return TipoSiembra; }
            set { TipoSiembra = value; }
        }

        private string Distancia;

        public string distancia
        {
            get { return Distancia; }
            set { Distancia = value; }
        }

        private string Dificultad;

        public string dificultad
        {
            get { return Dificultad; }
            set { Dificultad = value; }
        }

        private string ValorNutricional;

        public string valorNutricional
        {
            get { return ValorNutricional; }
            set { ValorNutricional = value; }
        }

        #endregion

        
        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        public PlantaModel()
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);
        }

        public List<PlantaModel> obtenerTodas()
        {
            var query = _db.Table<Entidades.tbl_Planta>()
                            .ToList();

            List<PlantaModel> todasLasPlantas = new List<PlantaModel>();
            if (query.Count > 0)
            {
                string urlImg = "iso_v7.PNG";
                foreach (Entidades.tbl_Planta item in query)
                {
                    PlantaModel nPlanta = new PlantaModel();
                    nPlanta.id = item.Pla_Id;
                    nPlanta.nombre = item.Pla_Nombre;
                    nPlanta.imagenPortada = urlImg;
                    nPlanta.dificultad = item.Pla_Dificultad;
                    todasLasPlantas.Add(nPlanta);
                }
            }

            return todasLasPlantas;
        }

        public PlantaModel buscarPorId(string v)
        {
            var query = _db.Table<Entidades.tbl_Planta>()
                            .Where(t => t.Pla_Id == v)
                            .FirstOrDefault();

            //IEnumerable<Entidades.tbl_Usuario> resultado = SELECT_WHERE(_db, usuarioLogin);
            if (query != null)
            {
                return new PlantaModel {
                                            id = query.Pla_Id,
                                            nombre = query.Pla_Nombre,
                                            imagenPortada = "",
                                            descripcion = query.Pla_Descripcion,
                                            dificultad = query.Pla_Dificultad,
                                            distancia = query.Pla_DistanciaEntrePlantas,
                                            epocaSiembra = query.Pla_EpocaSiembra,
                                            tipoSiembra = query.Pla_TipoSiembra,
                                            valorNutricional = query.Pla_ValorNutricional
                };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Las categorias corresponden a las Plantas
        /// </summary>
        /// <returns></returns>
        public List<CategoriaModel> todasLasCategorias()
        {
            var query = _db.Table<Entidades.tbl_Categoria>()
                            .ToList();

            List<CategoriaModel> todasLasCategorias = new List<CategoriaModel>();
            if (query.Count > 0)
            {
                foreach (Entidades.tbl_Categoria item in query)
                {
                    CategoriaModel nCategoria = new CategoriaModel();
                    nCategoria.id = item.Cat_Id;
                    nCategoria.nombre = item.Cat_Nombre;

                    todasLasCategorias.Add(nCategoria);
                }
            }

            return todasLasCategorias;
        }

        public List<PlantaModel> obtenerById(string itemSelec)
        {
            var query = (from pl in _db.Table<Entidades.tbl_Planta>()
                          from tp in _db.Table<Entidades.tbl_TipoPlanta>()
                          where pl.Pla_TPl_Id == tp.TPl_Id
                                && tp.TPl_Cat_Id == itemSelec
                          select pl).ToList();

            List <PlantaModel> todasLasPlantas = new List<PlantaModel>();
            if (query.Count > 0)
            {
                string urlImg = "iso_v7.PNG";
                foreach (Entidades.tbl_Planta item in query)
                {
                    PlantaModel nPlanta = new PlantaModel();
                    nPlanta.id = item.Pla_Id;
                    nPlanta.nombre = item.Pla_Nombre;
                    nPlanta.imagenPortada = urlImg;
                    nPlanta.dificultad = item.Pla_Dificultad;
                    todasLasPlantas.Add(nPlanta);
                }
            }
           
            return todasLasPlantas;
        }

        public string obtenerIdCategoria(int itemSelec)
        {
            var setDatos = todasLasCategorias();
            string strID = "";

            switch (itemSelec)
            {
                case 0:
                    strID = setDatos.Where(c => c.nombre == "Hortaliza").Select(c => c.id).FirstOrDefault(); break;
                case 1:
                    strID = setDatos.Where(c => c.nombre == "Aromatica").Select(c => c.id).FirstOrDefault(); break;
                case 2:
                    strID = setDatos.Where(c => c.nombre == "Arbusto").Select(c => c.id).FirstOrDefault(); break;
                default:
                    break;
            }
            return strID;
        }
        #endregion
    }
}
