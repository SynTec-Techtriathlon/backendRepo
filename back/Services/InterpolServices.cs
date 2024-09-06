using Back.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Back.Services
{
    public class InterpolServices : IInterpolService
    {
        private readonly HttpClient httpClient;

        public InterpolServices(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        private async Task<string> CheckNoticedApplicant(InterpolDTO value, string noticeType)
        {
            string externalApiUrl = $"https://ws-public.interpol.int/notices/v1/{noticeType}?" +
                                    $"forename={value.forename}&name={value.name}&nationality={value.nationality}&" +
                                    $"ageMax={120}&ageMin={18}&sexId={value.gender}&arrestWarrantCountryId={value.nationality}&" +
                                    $"page=1&resultPerPage=200";

            try
            {
                var requestMessage = new HttpRequestMessage(HttpMethod.Get, externalApiUrl);
                requestMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/avif,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3;q=0.7");
                requestMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br, zstd");
                requestMessage.Headers.Add("Accept-Language", "en-US,en;q=0.9");
                requestMessage.Headers.Add("Priority", "u=0, i");
                requestMessage.Headers.Add("sec-ch-ua", "\"Chromium\";v=\"128\", \"Not;A=Brand\";v=\"24\", \"Google Chrome\";v=\"128\"");
                requestMessage.Headers.Add("sec-ch-ua-mobile", "?0");
                requestMessage.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
                requestMessage.Headers.Add("sec-fetch-dest", "document");
                requestMessage.Headers.Add("sec-fetch-mode", "navigate");
                requestMessage.Headers.Add("sec-fetch-site", "none");
                requestMessage.Headers.Add("sec-fetch-user", "?1");
                requestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
                requestMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36");

                var externalApiResponse = await httpClient.SendAsync(requestMessage);
                externalApiResponse.EnsureSuccessStatusCode();

                var responseContent = await externalApiResponse.Content.ReadAsStringAsync();
                var jsonDocument = JsonDocument.Parse(responseContent);
                var jsonResponse = jsonDocument.RootElement;

                if (jsonResponse.TryGetProperty("_embedded", out var embeddedElement) &&
                    embeddedElement.TryGetProperty("notices", out var noticesElement) &&
                    noticesElement.GetArrayLength() > 0)
                {
                    return "found";
                }

                return "not found";
            }
            catch (HttpRequestException)
            {
                return "Error";
            }
        }

        public Task<string> CheckRedNoticedApplicant(InterpolDTO value) => CheckNoticedApplicant(value, "red");

        public Task<string> CheckYellowNoticedApplicant(InterpolDTO value) => CheckNoticedApplicant(value, "yellow");

        public Task<string> CheckUNNoticedApplicant(InterpolDTO value) => CheckNoticedApplicant(value, "un");
    }
}
