using duEco.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace duEco.Servicio
{
    public class AlertaServicio
    {        
        internal static bool CrearAlerta(String userLog, String idHuerta, String descripcion, DateTime fechaAlerta, bool avisa, String idTipoAlerta)                        
        {
            AlertaModel objAlerta = new AlertaModel();
            int ultIdAlerta = objAlerta.getUltimoIdAlerta();

            String strAvisa = avisa ? "S" : "N";

            var objUsuario = new UsuarioModel();
            var idUser = objUsuario.ConsultarPorLog(userLog);

            return objAlerta.CrearAlerta(ultIdAlerta, idUser, idHuerta, descripcion, fechaAlerta, strAvisa, idTipoAlerta);            
        }

        internal static bool EliminarAlerta(int idAlerta, String Avisa)
        {
            AlertaModel objAlerta = new AlertaModel();                       

            return objAlerta.EliminarAlerta(idAlerta, Avisa);
        }

        internal static List<AlertaModel> TodasLasAlertas(string userLog)
        {
            var objAlerta = new AlertaModel();
            var objUsuario = new UsuarioModel();

            var idUser = objUsuario.ConsultarPorLog(userLog);
            return objAlerta.ConsultarTodas(idUser);
        }

        internal static List<AlertaModel> AlertasPorHuerta(string idHuerta)
        {
            var objAlerta = new AlertaModel();           
            return objAlerta.ConsultarPorHuerta(idHuerta);
        }

        internal static List<AlertaModel> AlertasPorDia(string userLog, DateTime fecha)
        {
            var objAlerta = new AlertaModel();
            var objUsuario = new UsuarioModel();
            DateTime fechaDesde = fecha;
            DateTime fechaHasta = fecha.AddDays(1).AddSeconds(-1); //para que sea el mismo dia 23:59:59

            var idUser = objUsuario.ConsultarPorLog(userLog);
            return objAlerta.ConsultarPorDia(idUser, fechaDesde, fechaHasta);
        }

        internal static AlertaModel BuscarPorId(int alertaID)
        {
            return new AlertaModel().ConsultarPorId(alertaID);
        }
    }    
}
