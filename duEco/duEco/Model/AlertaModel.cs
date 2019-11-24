using Plugin.LocalNotifications;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace duEco.Model
{
    public class AlertaModel
    {

        private int id;        
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private String usuarioId;
        [MaxLength(36)]
        public String UsuarioID
        {
            get { return usuarioId; }
            set { usuarioId = value; }
        }

        private String huertaId;
        [MaxLength(36)]
        public String HuertaID
        {
            get { return huertaId; }
            set { huertaId = value; }
        }

        private string descripcion;
        [MaxLength(300)]
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }

        private DateTime fechaHora;
        public DateTime FechaHora
        {
            get { return fechaHora; }
            set { fechaHora = value; }
        }

        public String avisa;
        [MaxLength(1)]
        public String Avisa
        {
            get { return avisa; }
            set { avisa = value; }
        }

        public String tipoAlertaId;
        [MaxLength(36)]
        public String TipoAlertaID
        {
            get { return tipoAlertaId; }
            set { tipoAlertaId = value; }
        }

        public String baja;
        [MaxLength(1)]
        public String Baja
        {
            get { return baja; }
            set { baja = value; }
        }

        //-------------------------------------------------------------        

        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        public AlertaModel()
        {
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);
        }

        public void Dispose()
        {
            _db.Dispose();
        }

        public bool CrearAlerta(int idIncremental, String idUsuario, String idHuerta, String descripcion, DateTime fechaHora, String avisa, String idTipoAlerta)
        {
            try
            {
                var nuevaAlerta = new Entidades.tbl_Alerta
                {
                    Ale_Id = idIncremental,
                    Ale_Usu_Id = idUsuario,
                    Ale_Hue_Id = idHuerta,
                    Ale_Descripcion = descripcion,
                    Ale_Fecha_Hora = fechaHora,
                    Ale_Avisa = avisa,
                    Ale_TAl_Id = idTipoAlerta,
                    Ale_Baja = "N"                    
                };
                _db.Insert(nuevaAlerta);                

                if(avisa == "S")
                    CrossLocalNotifications.Current.Show("duEco", descripcion, idIncremental, fechaHora);

                return true;
            }
            catch (Exception e)
            {
                return false;
                
            }
        }

        public bool EliminarAlerta(int AlertaID, String Avisa)
        {
            try
            {
                _db.Query<Entidades.tbl_Alerta>("UPDATE tbl_Alerta SET Ale_Baja = ? WHERE Ale_Id = ?", "S", AlertaID);

                if (Avisa == "S")
                    CrossLocalNotifications.Current.Cancel(AlertaID);

                return true;
            }
            catch (Exception e)
            {
                return false;
                throw new Exception(e.Message);
            }
        }

        public List<AlertaModel> ConsultarTodas(string usuarioID)
        {
            try
            {
                var lst = _db.Table<Entidades.tbl_Alerta>()
                         .Where(a => a.Ale_Baja == "N" && a.Ale_Usu_Id == usuarioID)
                        .ToList();

                List<AlertaModel> todasLasAlertas = new List<AlertaModel>();
                if (lst.Count > 0)
                {
                    foreach (Entidades.tbl_Alerta item in lst)
                    {
                        AlertaModel nAlerta = new AlertaModel();

                        nAlerta.id = item.Ale_Id;
                        nAlerta.usuarioId = item.Ale_Usu_Id;
                        nAlerta.huertaId = item.Ale_Hue_Id;
                        nAlerta.descripcion = item.Ale_Descripcion;
                        nAlerta.fechaHora = item.Ale_Fecha_Hora;
                        nAlerta.avisa = item.Ale_Avisa;
                        nAlerta.tipoAlertaId = item.Ale_TAl_Id;
                        nAlerta.baja = item.Ale_Baja;
                        
                        todasLasAlertas.Add(nAlerta);
                    }
                }

                return todasLasAlertas;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlertaModel> ConsultarPorHuerta(string huertaID)
        {
            try
            {
                var lst = _db.Table<Entidades.tbl_Alerta>()
                         .Where(a => a.Ale_Baja == "N" && a.Ale_Hue_Id == huertaID)
                        .ToList();

                List<AlertaModel> AlertasPorHuerta = new List<AlertaModel>();
                if (lst.Count > 0)
                {
                    foreach (Entidades.tbl_Alerta item in lst)
                    {
                        AlertaModel nAlerta = new AlertaModel();

                        nAlerta.id = item.Ale_Id;
                        nAlerta.usuarioId = item.Ale_Usu_Id;
                        nAlerta.huertaId = item.Ale_Hue_Id;
                        nAlerta.descripcion = item.Ale_Descripcion;
                        nAlerta.fechaHora = item.Ale_Fecha_Hora;
                        nAlerta.avisa = item.Ale_Avisa;
                        nAlerta.tipoAlertaId = item.Ale_TAl_Id;
                        nAlerta.baja = item.Ale_Baja;

                        AlertasPorHuerta.Add(nAlerta);
                    }
                }

                return AlertasPorHuerta;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<AlertaModel> ConsultarPorDia(string usuarioID, DateTime FechaDesde, DateTime FechaHasta)
        {
            try
            {
                var lst = _db.Table<Entidades.tbl_Alerta>()
                         .Where(a => a.Ale_Baja == "N" 
                                && a.Ale_Usu_Id == usuarioID 
                                && a.Ale_Fecha_Hora >= FechaDesde
                                && a.Ale_Fecha_Hora <= FechaHasta)
                        .ToList();

                List<AlertaModel> alertasPorDia = new List<AlertaModel>();
                if (lst.Count > 0)
                {
                    foreach (Entidades.tbl_Alerta item in lst)
                    {
                        AlertaModel nAlerta = new AlertaModel();

                        nAlerta.id = item.Ale_Id;
                        nAlerta.usuarioId = item.Ale_Usu_Id;
                        nAlerta.huertaId = item.Ale_Hue_Id;
                        nAlerta.descripcion = item.Ale_Descripcion;
                        nAlerta.fechaHora = item.Ale_Fecha_Hora;
                        nAlerta.avisa = item.Ale_Avisa;
                        nAlerta.tipoAlertaId = item.Ale_TAl_Id;
                        nAlerta.baja = item.Ale_Baja;

                        alertasPorDia.Add(nAlerta);
                    }
                }

                return alertasPorDia;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AlertaModel ConsultarPorId(int alertaID)
        {
            try
            {
                var qAlerta = _db.Table<Entidades.tbl_Alerta>()
                            .Where(a => a.Ale_Id == alertaID)
                            .FirstOrDefault();

                //IEnumerable<Entidades.tbl_Usuario> resultado = SELECT_WHERE(_db, usuarioLogin);
                if (qAlerta != null)
                {
                    return new AlertaModel
                    {
                        id = qAlerta.Ale_Id,
                        usuarioId = qAlerta.Ale_Usu_Id,
                        huertaId = qAlerta.Ale_Hue_Id,
                        descripcion = qAlerta.Ale_Descripcion,
                        fechaHora = qAlerta.Ale_Fecha_Hora,
                        avisa = qAlerta.Ale_Avisa,
                        tipoAlertaId = qAlerta.Ale_TAl_Id,
                        baja = qAlerta.Ale_Baja
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

        public int getUltimoIdAlerta()
        {
            try
            {
                var ultimaAlerta = _db.Table<Entidades.tbl_Alerta>().OrderByDescending(item => item.Ale_Id).FirstOrDefault();                
                int ultID = 0;

                if (ultimaAlerta != null)
                    ultID = ultimaAlerta.Ale_Id+=1;
                else
                    ultID = 0;

                return ultID;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }    
            #endregion
    }
}
