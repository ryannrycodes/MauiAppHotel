using System;
using Microsoft.Maui.Controls;

namespace MauiAppHotel.Views
{
    public partial class ContratacaoHospedagem : ContentPage
    {
        public ContratacaoHospedagem()
        {
            InitializeComponent();
        }

        private async void BtnSobre_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert(
                "Sobre o Aplicativo",
                "Aplicativo desenvolvido para gestão de hospedagens.\n" +
                "Desenvolvido por: José Ryan Silva Nery\n" +
                "Ano de desenvolvimento: 2025",
                "Fechar");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                int adultos = (int)stp_adultos.Value;
                int criancas = (int)stp_criancas.Value;
                string suite = pck_quarto.SelectedItem?.ToString() ?? "Não selecionada";

                DateTime checkin = dtpck_checkin.Date;
                DateTime checkout = dtpck_checkout.Date;

                int dias = (checkout - checkin).Days;
                if (dias <= 0)
                {
                    await DisplayAlert("Erro", "A data de check-out deve ser posterior ao check-in.", "OK");
                    return;
                }

                double valorDiaria = suite switch
                {
                    "Suíte Standard" => 200.0,
                    "Suíte Master" => 350.0,
                    _ => 250.0
                };

                double valorTotal = (adultos * valorDiaria + criancas * (valorDiaria * 0.5)) * dias;

                await Navigation.PushAsync(new HospedagemContratada(
                    suite, adultos, criancas, checkin, checkout, dias, valorTotal
                ));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }

        private void dtpck_checkin_DateSelected(object sender, DateChangedEventArgs e)
        {
            DatePicker elemento = sender as DatePicker;

            DateTime dataSelecionadaCheckin = elemento.Date;

            dtpck_checkout.MinimumDate = dataSelecionadaCheckin.AddDays(1);
            dtpck_checkout.MaximumDate = dataSelecionadaCheckin.AddMonths(6);
        }
    }
}