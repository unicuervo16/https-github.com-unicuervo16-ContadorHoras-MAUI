using Contador_de_Horas.Models;
using System;
using System.Globalization;

namespace Contador_de_Horas.Pages;

public partial class MainPage : ContentPage
{
    RegistroDiario registroHoy;

    public MainPage()
    {
        InitializeComponent();
        CargarRegistroDelDia();
    }

    async void CargarRegistroDelDia()
    {
        var hoy = DateTime.Today;
        labelFecha.Text = hoy.ToString("dddd dd/MM/yyyy", new CultureInfo("es-AR")).ToUpper();

        var registros = await App.Database.GetRegistrosAsync();
        registroHoy = registros.FirstOrDefault(r => r.Fecha.Date == hoy);

        if (registroHoy != null)
        {
            MostrarDatos();
        }
        else
        {
            registroHoy = new RegistroDiario { Fecha = hoy };
        }
    }

    async void OnRegistrarIngresoClicked(object sender, EventArgs e)
    {
        if (registroHoy.HoraIngreso != TimeSpan.Zero)
        {
            await DisplayAlert("Atención", "Ya registraste el ingreso de hoy.", "OK");
            return;
        }

        registroHoy.HoraIngreso = DateTime.Now.TimeOfDay;
        await App.Database.SaveRegistroAsync(registroHoy);
        MostrarDatos();
    }

    async void OnRegistrarEgresoClicked(object sender, EventArgs e)
    {
        if (registroHoy.HoraEgreso != TimeSpan.Zero)
        {
            await DisplayAlert("Atención", "Ya registraste el egreso de hoy.", "OK");
            return;
        }

        registroHoy.HoraEgreso = DateTime.Now.TimeOfDay;
        await App.Database.SaveRegistroAsync(registroHoy);
        MostrarDatos();
    }

    void MostrarDatos()
    {
        labelIngreso.Text = $"Ingreso: {registroHoy.HoraIngreso:hh\\:mm}";
        labelEgreso.Text = $"Egreso: {registroHoy.HoraEgreso:hh\\:mm}";

        if (registroHoy.HoraIngreso != TimeSpan.Zero && registroHoy.HoraEgreso != TimeSpan.Zero)
        {
            var total = registroHoy.HorasTrabajadas;
            labelTotal.Text = $"Total trabajado: {total.Hours:D2}:{total.Minutes:D2}";
        }
    }

    private async void OnVerHistorialClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new HistorialPage());
    }
}
