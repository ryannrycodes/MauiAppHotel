using System;
using Microsoft.Maui.Controls;

namespace MauiAppHotel.Views
{
    public partial class HospedagemContratada : ContentPage
    {
        public HospedagemContratada()
        {
            InitializeComponent();
        }

        public HospedagemContratada(string suite, int adultos, int criancas, DateTime checkin, DateTime checkout, int dias, double valorTotal)
        {
            InitializeComponent();

            try
            {
                lbl_adultos.Text = adultos.ToString();
                lbl_criancas.Text = criancas.ToString();
                lbl_checkin.Text = checkin.ToString("dd/MM/yyyy");
                lbl_checkout.Text = checkout.ToString("dd/MM/yyyy");
                lbl_estadia.Text = $"{dias} {(dias == 1 ? "dia" : "dias")}";
                lbl_valor.Text = $"R$ {valorTotal:F2}";
                this.Title = suite;

                var mainLayout = (ScrollView)Content;
                if (mainLayout.Content is VerticalStackLayout stack)
                {
                    foreach (var child in stack.Children)
                    {
                        if (child is Frame frame)
                        {
                            foreach (var item in ((Layout)frame.Content).Children)
                            {
                                if (item is Label lbl && lbl.Text.Contains("Suíte"))
                                {
                                    lbl.Text = suite;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DisplayAlert("Erro", $"Falha ao carregar dados: {ex.Message}", "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}