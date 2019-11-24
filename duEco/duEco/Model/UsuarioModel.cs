using duEco.Entidades;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace duEco.Model
{
   
    public class UsuarioModel : INotifyPropertyChanged
    {       
        private string Nombre;

        public string nombre
        {
            get { return Nombre; }
            set { Nombre = value; OnPropertyChanged(); }
        }

        private string Email;

        public string email
        {
            get { return Email; }
            set { Email = value; OnPropertyChanged(); }
        }       

        private string Password;

        public string password
        {
            get { return Password; }
            set { Password = value; OnPropertyChanged(); }
        }

        #region SQLite
        private static string _dbName;
        private static string _path;
        private static SQLiteConnection _db;

        internal static bool CrearUsuario(UsuarioModel nuevoUsuario, string Id)
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);

            try
            {
                var datos = new Entidades.tbl_Usuario
                {
                    Usu_Id = Id, Usu_Nombre = nuevoUsuario.nombre, Usu_Email = nuevoUsuario.email, Usu_Password = nuevoUsuario.password, Usu_Baja = "N"
                };

                _db.Insert(datos);
                return true;
            }
            catch (Exception es)
            {
                throw es;
            }
            
        }
        
        internal string ConsultarPorLog(string userId)
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);

            var qId = from u in _db.Table<Entidades.tbl_Usuario>()
                      where u.Usu_Email == userId && u.Usu_Baja == "N"
                      select u.Usu_Id;

            return qId.FirstOrDefault();
        }

        internal static bool BuscarByLogin(UsuarioModel usuarioLogin)
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);

            var query = _db.Table<tbl_Usuario>()
                            .Where(t => t.Usu_Email == usuarioLogin.email && t.Usu_Password == usuarioLogin.password)
                            .Select(t => t)
                            .FirstOrDefault();

          //IEnumerable<Entidades.tbl_Usuario> resultado = SELECT_WHERE(_db, usuarioLogin);
            if (query != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal static UsuarioModel BuscarEdicion(string userLog)
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);

            var qId = from u in _db.Table<Entidades.tbl_Usuario>()
                      where u.Usu_Email == userLog && u.Usu_Baja == "N"
                      select u;

            if (qId.FirstOrDefault() != null)
            {
                var edUsuario = qId.FirstOrDefault();
                return new UsuarioModel
                {
                    nombre = edUsuario.Usu_Nombre,
                    email = edUsuario.Usu_Email,
                    password = edUsuario.Usu_Password
                };
            }
            else
            {
                return null;
            }
        }

        internal static bool Actualizar(string id, string nombre, string email, string password)
        {
            // Acceso y conexion a la BD
            _dbName = "duEcoSQLite.db3";
            _path = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.Personal), _dbName);
            _db = new SQLiteConnection(_path);

            try
            {
                _db.Query<Entidades.tbl_Huerta>("UPDATE tbl_Usuario SET Usu_Nombre = ?, Usu_Email = ?, Usu_Password = ? WHERE Usu_Email == ? ", nombre, email, password, id);
                var allUsuarios = _db.Query<Entidades.tbl_Usuario>("SELECT * FROM tbl_Usuario");
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static IEnumerable<tbl_Usuario> SELECT_WHERE(SQLiteConnection db, UsuarioModel usuarioLogin)
        {
            return db.Query<Entidades.tbl_Usuario>("SELECT * FROM tbl_Usuario WHERE Usu_Email = ? and Usu_Password = ? AND Usu_Baja == 'N' = ?", usuarioLogin.email, usuarioLogin.password);                
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
