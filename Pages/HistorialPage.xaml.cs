using Contador_de_Horas.Models;
using System.Collections.ObjectModel;

namespace Contador_de_Horas.Pages;

public partial class HistorialPage : ContentPage
{
    public class RegistroVista
    {
        public string Fecha { get; set; }
        public string HoraIngreso { get; set; }
        public string HoraEgreso { get; set; }
        public string Total { get; set; }
        public Color Color { get; set; }
    }

    public HistorialPage()
    {
        InitializeComponent();
        CargarHistorial();
    }

    async void CargarHistorial()
    {
        var registros = await App.Database.GetRegistrosAsync();
        var historial = new ObservableCollection<RegistroVista>();

        var registrosOrdenados = registros
            .OrderBy(r => r.Fecha)
            .GroupBy(r => System.Globalization.CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                r.Fecha, System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek.Monday));

        foreach (var semana in registrosOrdenados)
        {
            TimeSpan totalSemana = TimeSpan.Zero;

            foreach (var r in semana)
            {
                var horas = r.HorasTrabajadas;
                totalSemana += horas;

                historial.Add(new RegistroVista
                {
                    Fecha = r.Fecha.ToString("dddd dd/MM/yyyy"),
                    HoraIngreso = r.HoraIngreso.ToString(@"hh\:mm"),
                    HoraEgreso = r.HoraEgreso.ToString(@"hh\:mm"),
                    Total = $"{horas.Hours:D2}:{horas.Minutes:D2}",
                    Color = Colors.Black
                });
            }

            historial.Add(new RegistroVista
            {
                Fecha = $"⏱ TOTAL SEMANA {semana.Key}",
                HoraIngreso = "",
                HoraEgreso = "",
                Total = $"{(int)totalSemana.TotalHours:D2}:{totalSemana.Minutes:D2}",
                Color = totalSemana.TotalHours >= 35 ? Colors.Green : Colors.Red
            });
        }

        historialView.ItemsSource = historial;
    }

    //private async void OnVolverClicked(object sender, EventArgs e)
    //{
    //    await Navigation.PopAsync();
    //}
}
