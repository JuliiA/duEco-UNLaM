using duEco.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace duEco.Servicio
{
    public class HuertaServicio
    {
        internal static List<HuertaModel> TodasLasHuertas(string userId)
        {
            var objHuerta = new HuertaModel();
            var objUsuario = new UsuarioModel();

            var idUser = objUsuario.ConsultarPorLog(userId);
            return objHuerta.ConsultarTodos(idUser);
        }

        internal static HuertaModel BuscarPorId(string huertaID)
        {
            return new HuertaModel().ConsultarPorId(huertaID);
        }

        internal static bool CrearHuerta(string text1, string text2, string userLog)
        {
            var objUsuario = new UsuarioModel();

            var idUser = objUsuario.ConsultarPorLog(userLog);
            return new HuertaModel().CrearHuerta(text1, text2, idUser, CoreServicio.Encrypt.GetMD5(text1.Substring(2)));

        }

        internal static bool ModificarHuerta(string text1, string text2, string huertaId)
        {
            var modHuerta = new HuertaModel();

            return modHuerta.ActualizarHuerta(text1, text2, huertaId);
        }

        internal static bool Eliminar(string huertaID)
        {
            return new HuertaModel().Eliminar(huertaID);
        }
    }
}
