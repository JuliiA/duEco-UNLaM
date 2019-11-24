using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace duEco.Model
{
    public class CultivoModel : PlantaModel
    {
        #region Propiedades
        private string idCultivo;

        public string IdCultivo
        {
            get { return idCultivo; }
            set { idCultivo = value; }
        }

        private string nombreCultivo;

        public string NombreCultivo
        {
            get { return nombreCultivo; }
            set { nombreCultivo = value; }
        }

        private DateTime iniciaCultivo;

        public DateTime IniciaCultivo
        {
            get { return iniciaCultivo; }
            set { iniciaCultivo = value; }
        }

        private DateTime finSiembra;

        public DateTime FinSiembra
        {
            get { return finSiembra; }
            set { finSiembra = value; }
        }

        private DateTime finCosecha;

        public DateTime FinCosecha
        {
            get { return finCosecha; }
            set { finCosecha = value; }
        }

        public float porcentaje { get; set; }

        #endregion
        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        public CultivoModel()
        {
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);
        }
             

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool CrearCultivo(CultivoModel Cultivo, string Id, string huertaId)
        {
            try
            {
                var nuevaCultivo = new Entidades.tbl_Cultivo
                {
                    Cul_Id = Id,
                    Cul_Nombre = Cultivo.NombreCultivo,
                    Cul_Pla_Id = idCultivo,
                    Cul_Hue_Id = huertaId,
                    Cul_FinSiembra = Cultivo.FinSiembra,
                    Cul_IniciaCultivo = Cultivo.IniciaCultivo,
                    Cul_FinCosecha = Cultivo.FinCosecha,
                    Cul_Baja = "N"
                };
                _db.Insert(nuevaCultivo);

                var tableTP = _db.Table<Entidades.tbl_Cultivo>().ToList();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// TESTEAR QUE ACTUALICE EL OBJETO CORRESPONDIENTE
        /// </summary>
        /// <param name="Cultivo"></param>
        /// <returns></returns>
        public bool ActualizarCultivo(CultivoModel Cultivo)
        {
            try
            {
                var updCultivo = new Entidades.tbl_Cultivo
                {
                    Cul_Id = Cultivo.idCultivo,
                    Cul_Nombre = Cultivo.NombreCultivo,
                    Cul_IniciaCultivo = Cultivo.iniciaCultivo,
                    Cul_FinSiembra = Cultivo.finSiembra,
                    Cul_FinCosecha = Cultivo.finCosecha
                };
                _db.Update(updCultivo);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Eliminar(string CultivoID)
        {
            try
            {
                _db.Query<Entidades.tbl_Cultivo>("UPDATE tbl_Cultivo SET Hue_Baja = ? WHERE Hue_Id = ?", "S", CultivoID);
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CultivoModel ConsultarPorId(String CultivoID)
        {
            try
            {
                var qCultivo = _db.Table<Entidades.tbl_Cultivo>()
                            .Where(t => t.Cul_Id == CultivoID)
                            .FirstOrDefault();

                var qPlanta = _db.Table<Entidades.tbl_Planta>()
                            .Where(p => p.Pla_Nombre == qCultivo.Cul_Nombre)
                            .FirstOrDefault();

                if (qCultivo != null && qPlanta != null)
                {
                    return new CultivoModel
                    {
                        idCultivo = qCultivo.Cul_Id,
                        id = qPlanta.Pla_Id,
                        nombreCultivo = qCultivo.Cul_Nombre,
                        nombre = qPlanta.Pla_Nombre,
                        descripcion = qPlanta.Pla_Descripcion,
                        tipoSiembra = qPlanta.Pla_TipoSiembra,                        
                        dificultad = qPlanta.Pla_Dificultad,
                        finSiembra = qCultivo.Cul_FinSiembra,
                        iniciaCultivo = qCultivo.Cul_IniciaCultivo,
                        finCosecha = qCultivo.Cul_FinCosecha
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

        public List<CultivoModel> ConsultarTodos(string huertaID)
        {
            try
            {
                var lst = _db.Table<Entidades.tbl_Cultivo>()
                    .Where(h => h.Cul_Baja == "N" && h.Cul_Hue_Id == huertaID)
                    .ToList();

                List<CultivoModel> todasLasCultivos = new List<CultivoModel>();
                if (lst.Count > 0)
                {
                    foreach (Entidades.tbl_Cultivo item in lst)
                    {
                        CultivoModel nCultivo = new CultivoModel
                        {
                            idCultivo = item.Cul_Id,
                            NombreCultivo = item.Cul_Nombre,
                            id = item.Cul_Pla_Id,
                            FinCosecha = item.Cul_FinCosecha,
                            IniciaCultivo = item.Cul_IniciaCultivo,
                            FinSiembra = item.Cul_FinSiembra
                        };
                        todasLasCultivos.Add(nCultivo);
                    }
                }

                return todasLasCultivos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CultivoModel obtenerFechasDeEtapas(string idPlanta)
        {
            CultivoModel infoSobreCultivo = new CultivoModel();

            var qInfo = (from p in _db.Table<Entidades.tbl_Etapa>()
                        where p.Eta_Pla_Id == idPlanta
                        select p).ToList();


            return infoSobreCultivo;
        }

        public List<TareasEtapas> obtenerTareasEtapas(string idPlanta)
        {
            List<TareasEtapas> resulta = new List<TareasEtapas>();
            var qResult = (from t in _db.Table<Entidades.tbl_Etapa>()
                           where t.Eta_Pla_Id == idPlanta
                           select t).ToList();

            if(qResult.Count > 0)
            {
                foreach (var item in qResult)
                {
                    var cTarea = (from et in _db.Table<Entidades.tbl_Tarea>()
                                  where et.Tar_Id == item.Eta_Tar_Id
                                  select et).FirstOrDefault();

                    TareasEtapas tEtapa = new TareasEtapas();
                    tEtapa.tareasEtapa =  cTarea.Tar_Nombre;
                    tEtapa.nombreEtapa = item.Eta_Descripcion;

                    resulta.Add(tEtapa);
                }
            }
            
            return resulta;
        }

        public class TareasEtapas {
            public string nombreEtapa { get; set; }
            public string tareasEtapa { get; set; }
        }

        #endregion
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
