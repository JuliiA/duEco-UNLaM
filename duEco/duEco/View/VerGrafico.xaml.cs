using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microcharts;
using duEco.Servicio;
using duEco.Model;

namespace duEco.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VerGrafico : ContentPage
	{
        int cantConsid = 0;
        int cantNeces = 0;
        int cantFund = 0;

        public VerGrafico (String IdHuertaSelec)
		{
			InitializeComponent ();

            var lstMisAlertas = AlertaServicio.AlertasPorHuerta(IdHuertaSelec);
            if (lstMisAlertas.Count > 0)
            {
                CalcularCantidadPorPrioridad(lstMisAlertas);
            }

            //Defino las entradas con las que se va a llenar el grafico
            var entries = new[]
            {
                new Microcharts.Entry(cantConsid)
                {
                    Label = "Consid.",
                    ValueLabel = cantConsid.ToString(),
                    Color = SKColor.Parse("#5ac628")
                },
                new Microcharts.Entry(cantNeces)
                {
                    Label = "Neces.",
                    ValueLabel = cantNeces.ToString(),
                    Color = SKColor.Parse("#c6c628")
                },
                new Microcharts.Entry(cantFund)
                {
                    Label = "Fund.",
                    ValueLabel = cantFund.ToString(),
                    Color = SKColor.Parse("#c62828")
                }               
            };

            //Defino un nuevo gráfico de Barras y le asigno las entradas definidas antes
            var chart = new BarChart() { Entries = entries };
            chart.LabelTextSize = 60;            

            //Asigno el nuevo grafico al control ChartView
            this.chartView.Chart = chart;                            
        }

        private void CalcularCantidadPorPrioridad(List<AlertaModel> lstMisAlertas)
        {            
            foreach (AlertaModel item in lstMisAlertas)
            {
                if (item.tipoAlertaId == "4cc709c1c7307d4brrra6b79dd4ddc3f") //Considerable
                    cantConsid += 1;
                else if (item.tipoAlertaId == "4cc709c1c7307d4b325a6b79dd4sss3f") //Necesario
                    cantNeces += 1;
                else if (item.tipoAlertaId == "4cc709c1c7307d4b325a6b79dd4ddm5i") //Fundamental
                    cantFund += 1;
            }
        }
    }
}