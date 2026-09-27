using MauiApTempo.models;
using MauiApTempo.services;
using System.Threading.Tasks;

namespace MauiApTempo
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private async  void Button_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao = $"descrição: {t.description} \n " +
                                         $"Latitude: {t.lat} \n " +
                                         $"Longitude: {t.lon} \n" +
                                         $"Ventos: {t.speed} \n" +
                                         $"Visibilidadde {t.visibility} \n " +
                                         $"Nascer do Sol:  {t.sunrise} \n " +
                                         $"Por do Sol: {t.sunset} \n" +
                                         $"Temp max: {t.temp_max} \n" +
                                         $"Temp min: {t.temp_min} \n" +
                                         $"umidade: {t.humidity} \n";

                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                            
                            lbl_res.Text = "Sem dados de Previsão";

                    }

                }
                else
                {
                    lbl_res.Text = "Preencha o campo";
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Atenção", ex.Message, "OK");
                lbl_res.Text = "Erro ao buscar previsão.";
            }
        }
    }

}
