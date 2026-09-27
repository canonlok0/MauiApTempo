using MauiApTempo.models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiApTempo.services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            try
            {
                Tempo t = null;

                string chave = "9acc4796270d58093ea86a210e6240f2";

                string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                             $"q={cidade}&units=metric&appid={chave}";

                using (HttpClient Client = new HttpClient())
                {
                    HttpResponseMessage resp = await Client.GetAsync(url);
                    if ((int)resp.StatusCode == 404)
                    {
                        throw new Exception("A cidade não foi encontrada, verifique o nome e tente novamente.");

                    }

                    if (!resp.IsSuccessStatusCode)
                    {
                        if (resp.StatusCode == HttpStatusCode.NotFound)
                        {
                            throw new Exception("A cidade não foi encontrada, verifique o nome e tente novamente.");

                        }
                        else
                        {
                            throw new Exception($"Erro na busca de dados. codigo {resp.StatusCode}");

                        }

                    }//if

                    string json = await resp.Content.ReadAsStringAsync();


                        var rascunho = JObject.Parse(json);

                        DateTime time = new();
                        DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                        DateTime sunset = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
                        t = new()
                        {
                            lat = (double)rascunho["coord"]["lat"],
                            lon = (double)rascunho["coord"]["lon"],
                            description = (string)rascunho["weather"][0]["description"],
                            main = (string)rascunho["weather"][0]["main"],
                            humidity = (int)rascunho["main"]["humidity"],
                            temp_min = (double)rascunho["main"]["temp_min"],
                            temp_max = (double)rascunho["main"]["temp_max"],
                            speed = (double)rascunho["wind"]["speed"],
                            visibility = (int)rascunho["visibility"],
                            sunrise = sunrise.ToString(),
                            sunset = sunset.ToString(),

                        };//objtempo

                    


                }//using

                return t;
            }catch (HttpRequestException)
            {
                throw new Exception("sem conecxão, Verifique a internet");
            }
            catch (Exception ex)
            {
               throw new Exception(ex.Message);

            }
        }
    }
}
