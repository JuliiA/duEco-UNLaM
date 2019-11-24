using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
namespace duEco.Model
{
    public class TipoAlertaModel
    {
        #region Propiedades
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string descripcion;
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private string baja;
        public string Baja
        {
            get { return baja; }
            set { baja = value; }
        }

        #endregion


        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        public TipoAlertaModel()
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);
        }

        public List<TipoAlertaModel> obtenerTodosTiposAlerta()
        {
            var query = _db.Table<Entidades.tbl_TipoAlerta>()
                            .ToList();

            List<TipoAlertaModel> lstTiposAlerta = new List<TipoAlertaModel>();
            if (query.Count > 0)
            {
                foreach (Entidades.tbl_TipoAlerta item in query)
                {
                    TipoAlertaModel objTipoAlerta = new TipoAlertaModel();
                    objTipoAlerta.id = item.TAl_Id;
                    objTipoAlerta.Descripcion = item.TAl_Descripcion;
                    objTipoAlerta.Baja = item.TAl_Baja;
                    lstTiposAlerta.Add(objTipoAlerta);
                }
            }

            return lstTiposAlerta;
        }

        public TipoAlertaModel ConsultarPorId(String tipoAlertaID)
        {
            try
            {
                var qTipoAlerta = _db.Table<Entidades.tbl_TipoAlerta>()
                            .Where(t => t.TAl_Id == tipoAlertaID)
                            .FirstOrDefault();

                //IEnumerable<Entidades.tbl_Usuario> resultado = SELECT_WHERE(_db, usuarioLogin);
                if (qTipoAlerta != null)
                {
                    return new TipoAlertaModel
                    {
                        Id = qTipoAlerta.TAl_Id,
                        Descripcion = qTipoAlerta.TAl_Descripcion,
                        Baja = qTipoAlerta.TAl_Baja
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        #endregion
    }
}
