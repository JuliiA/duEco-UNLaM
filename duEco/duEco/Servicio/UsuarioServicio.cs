using duEco.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace duEco.Servicio
{
    public class UsuarioServicio
    {
        public static bool Modificar(string id, string nombre, string email, string password)
        {
            return UsuarioModel.Actualizar(id, nombre, email, password);
        }

        public void Eliminar(UsuarioModel usuario)
        {

        }

        internal static bool UsuarioExiste(UsuarioModel usuarioLogin)
        {
            return UsuarioModel.BuscarByLogin(usuarioLogin);
        }

        internal static UsuarioModel Editar(string userLog)
        {
            return UsuarioModel.BuscarEdicion(userLog);
        }

        internal static bool Registrar(string text1, string text2)
        {
            if (email_bien_escrito(text1))
            {
                UsuarioModel nuevoUsuario = new UsuarioModel
                {
                    email = text1,
                    password = text2,
                    nombre = ""
                };
                return UsuarioModel.CrearUsuario(nuevoUsuario, CoreServicio.Encrypt.GetMD5(nuevoUsuario.email.Substring(6)));
            }
            else
            {
                return false;
            }
        }

        private static Boolean email_bien_escrito(String email)
        {
            String expresion;
            expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
