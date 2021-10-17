using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MoneyRemittance.ServiceIntegration
{
    public static class HttpHelper
    {
        public static async Task<string> PostAsync(HttpClient httpClient, string apiUrl, string jsonString)
        {
            var Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(apiUrl, Content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
