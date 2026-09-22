using MauiApTempo.models;
using Newtonsoft.Json.Linq;

namespace MauiApTempo.services
{
    public class DataService
    {
        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            Tempo t = null;

            string chave = "9acc4796270d58093ea86a210e6240f2";

            string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                         $"q={cidade}&units=metric&appid={chave}";

            using (HttpClient Client = new HttpClient())
            {
                HttpResponseMessage resp = await Client.GetAsync(url);
            
                if(resp.IsSuccessStatusCode)
                {
                    string json = await resp.Content.ReadAsStringAsync();


                    var rascunho = JObject.Parse(json);

                    DateTime time = new ();
                    DateTime sunrise = time.AddSeconds((double)rascunho["sys"]["sunrise"]).ToLocalTime();
                    DateTime sunset  = time.AddSeconds((double)rascunho["sys"]["sunset"]).ToLocalTime();
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

                }//if


            }//using

                return t;
        }
    }
}
