using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace duEco.Model
{
    public class HuertaModel : INotifyPropertyChanged
    {
        #region Propiedades
        private string id;
            
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string nombre;

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        private string descripcion;

        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private List<CultivoModel> listaCultivos;

        public List<CultivoModel> ListaCultivos
        {
            get { return listaCultivos; }
            set { listaCultivos = value; }
        }

        #endregion
        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        public HuertaModel()
        {
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool CrearHuerta(string nombreHuerta, string descriocion, string IdUser, string IdEncrip)
        {
            try
            {
                var nuevaHuerta = new Entidades.tbl_Huerta
                {
                    Hue_Id = IdEncrip,
                    Hue_Nombre = nombreHuerta,
                    Hue_Usu_Id = IdUser,
                    Hue_Descripcion = descriocion,
                    Hue_Baja = "N"
                };
                _db.Insert(nuevaHuerta);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }           
        }

        public bool ActualizarHuerta(string text1, string text2, string huertaId)
        {
            try
            {
                var updHuerta = new Entidades.tbl_Huerta
                {
                    Hue_Id = huertaId,
                    Hue_Nombre = text1,
                    Hue_Descripcion = text2
                };
                _db.Query<Entidades.tbl_Huerta>("UPDATE tbl_Huerta SET Hue_Nombre = ?, Hue_Descripcion = ? WHERE Hue_Id = ?", text1, text2, huertaId);

                var t = _db.Table<Entidades.tbl_Huerta>().ToList();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool Eliminar(string huerta)
        {
            try
            {
                _db.Query<Entidades.tbl_Huerta>("UPDATE tbl_Huerta SET  Hue_Baja = ? WHERE Hue_Id = ?","S", huerta);
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public HuertaModel ConsultarPorId(String huertaID)
        {
            try
            {
                var qHuerta = _db.Table<Entidades.tbl_Huerta>()
                            .Where(t => t.Hue_Id == huertaID)
                            .FirstOrDefault();

                //IEnumerable<Entidades.tbl_Usuario> resultado = SELECT_WHERE(_db, usuarioLogin);
                if (qHuerta != null)
                {
                    return new HuertaModel
                    {
                        id = qHuerta.Hue_Id,
                        nombre = qHuerta.Hue_Nombre,
                        descripcion = qHuerta.Hue_Descripcion,
                        listaCultivos = new CultivoModel().ConsultarTodos(huertaID)
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<HuertaModel> ConsultarTodos(string usuarioID)
        {
            try
            {
                var lst = _db.Table<Entidades.tbl_Huerta>()
                         .Where(h => h.Hue_Baja == "N" && h.Hue_Usu_Id == usuarioID)
                        .ToList();

                List<HuertaModel> todasLasHuertas = new List<HuertaModel>();
                if (lst.Count > 0)
                {
                    foreach (Entidades.tbl_Huerta item in lst)
                    {
                        HuertaModel nHuerta = new HuertaModel();
                        nHuerta.id = item.Hue_Id;
                        nHuerta.nombre = item.Hue_Nombre;
                        nHuerta.descripcion = item.Hue_Descripcion;

                        todasLasHuertas.Add(nHuerta);
                    }
                }

                return todasLasHuertas;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

        #region Implements
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string nombre = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nombre));
        }
        #endregion
    }
}
