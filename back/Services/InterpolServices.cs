using Back.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Back.Services
{
    public class InterpolServices : IInterpolService
    {
        private readonly HttpClient httpClient;
        public InterpolServices(HttpClient httpClient) { 
               this.httpClient = httpClient;
        }

        public async Task<string> CheckRedNoticedApplicant(InterpolDTO value)
        {

            string externalApiUrl = $"https://ws-public.interpol.int/notices/v1/red?" +
            $"forename={value.forename}&name={value.name}&nationality={value.nationality}&" +
                        $"ageMax={120}&ageMin={18}&sexId={value.gender}&arrestWarrantCountryId={value.nationality}&" +
                        $"page=1&resultPerPage=200";

            try
            {
                // Set up the request headers like in the provided request, without pseudo-headers
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

                // Send the GET request to the external API
                var externalApiResponse = await httpClient.SendAsync(requestMessage);

                // Ensure the request was successful
                externalApiResponse.EnsureSuccessStatusCode();

                // Get the response content
                var responseContent = await externalApiResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a JsonDocument for structured handling
                var jsonDocument = JsonDocument.Parse(responseContent);

                // Extract necessary information or return the full JSON document
                // Extract necessary information or return the full JSON document
                var jsonResponse = jsonDocument.RootElement;

                // Check if the '_embedded' property exists
                if (jsonResponse.TryGetProperty("_embedded", out var embeddedElement))
                {
                    // Check if the 'notices' array exists within '_embedded'
                    if (embeddedElement.TryGetProperty("notices", out var noticesElement))
                    {
                        // Check if the notices array is not empty
                        if (noticesElement.GetArrayLength() > 0)
                        {
                            // Notices array is not empty, proceed with further processing
                            return "found";
                        }
                        else
                        {
                            // Notices array is empty
                            return "not found";
                        }
                    }
                    else
                    {
                        // 'notices' property does not exist
                        return "'notices' not found in response";
                    }
                }
                else
                {
                    // '_embedded' property does not exist
                    return "'_embedded' not found in response";
                }
            }

            catch (HttpRequestException ex)
            {
                return "Error";
            }
        }

        public async Task<string> CheckYellowNoticedApplicant(InterpolDTO value)
        {
            string externalApiUrl = $"https://ws-public.interpol.int/notices/v1/yelolw?" +
           $"forename={value.forename}&name={value.name}&nationality={value.nationality}&" +
                       $"ageMax={120}&ageMin={18}&sexId={value.gender}&arrestWarrantCountryId={value.nationality}&" +
                       $"page=1&resultPerPage=200";

            try
            {
                // Set up the request headers like in the provided request, without pseudo-headers
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

                // Send the GET request to the external API
                var externalApiResponse = await httpClient.SendAsync(requestMessage);

                // Ensure the request was successful
                externalApiResponse.EnsureSuccessStatusCode();

                // Get the response content
                var responseContent = await externalApiResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a JsonDocument for structured handling
                var jsonDocument = JsonDocument.Parse(responseContent);

                // Extract necessary information or return the full JSON document
                // Extract necessary information or return the full JSON document
                var jsonResponse = jsonDocument.RootElement;

                // Check if the '_embedded' property exists
                if (jsonResponse.TryGetProperty("_embedded", out var embeddedElement))
                {
                    // Check if the 'notices' array exists within '_embedded'
                    if (embeddedElement.TryGetProperty("notices", out var noticesElement))
                    {
                        // Check if the notices array is not empty
                        if (noticesElement.GetArrayLength() > 0)
                        {
                            // Notices array is not empty, proceed with further processing
                            return "found";
                        }
                        else
                        {
                            // Notices array is empty
                            return "not found";
                        }
                    }
                    else
                    {
                        // 'notices' property does not exist
                        return "'notices' not found in response";
                    }
                }
                else
                {
                    // '_embedded' property does not exist
                    return "'_embedded' not found in response";
                }
            }

            catch (HttpRequestException ex)
            {
                return "Error";
            }
        }

        public async Task<string> CheckUNNoticedApplicant(InterpolDTO value)
        {
            string externalApiUrl = $"https://ws-public.interpol.int/notices/v1/yelolw?" +
 $"forename={value.forename}&name={value.name}&nationality={value.nationality}&" +
             $"ageMax={120}&ageMin={18}&sexId={value.gender}&arrestWarrantCountryId={value.nationality}&" +
             $"page=1&resultPerPage=200";

            try
            {
                // Set up the request headers like in the provided request, without pseudo-headers
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

                // Send the GET request to the external API
                var externalApiResponse = await httpClient.SendAsync(requestMessage);

                // Ensure the request was successful
                externalApiResponse.EnsureSuccessStatusCode();

                // Get the response content
                var responseContent = await externalApiResponse.Content.ReadAsStringAsync();

                // Deserialize the JSON response into a JsonDocument for structured handling
                var jsonDocument = JsonDocument.Parse(responseContent);

                // Extract necessary information or return the full JSON document
                // Extract necessary information or return the full JSON document
                var jsonResponse = jsonDocument.RootElement;

                // Check if the '_embedded' property exists
                if (jsonResponse.TryGetProperty("_embedded", out var embeddedElement))
                {
                    // Check if the 'notices' array exists within '_embedded'
                    if (embeddedElement.TryGetProperty("notices", out var noticesElement))
                    {
                        // Check if the notices array is not empty
                        if (noticesElement.GetArrayLength() > 0)
                        {
                            // Notices array is not empty, proceed with further processing
                            return "found";
                        }
                        else
                        {
                            // Notices array is empty
                            return "not found";
                        }
                    }
                    else
                    {
                        // 'notices' property does not exist
                        return "'notices' not found in response";
                    }
                }
                else
                {
                    // '_embedded' property does not exist
                    return "'_embedded' not found in response";
                }
            }

            catch (HttpRequestException ex)
            {
                return "Error";
            }
        }
    }
}
