﻿using OpenAI_API.Completions;
using OpenAI_API;
using Chatbot.Models;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace Chatbot.Services
{
    public class ChatbotService
    {
        HttpClient httpClient = new HttpClient();
        public string CallOpenAPI_text(string prompt)
        {
            DotNetEnv.Env.Load();
            string apikey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            string answer = string.Empty;

            var openai = new OpenAIAPI(apikey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = prompt;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 100;

            var result = openai.Completions.CreateCompletionsAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
            }
            return answer;
        }

        public string CallOpenAPI_Title(string prompt)
        {
            
            string query = "I need a title for the " + prompt;
            
            DotNetEnv.Env.Load();
            string apikey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            string answer = string.Empty;

            var openai = new OpenAIAPI(apikey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = query;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 100;

            var result = openai.Completions.CreateCompletionsAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
            }
            return answer;
        }

        public string CallOpenAPI_Description(string prompt)
        {
            string query = "I need a Description for the " + prompt;

            DotNetEnv.Env.Load();
            string apikey = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            string answer = string.Empty;

            var openai = new OpenAIAPI(apikey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = query;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 100;

            var result = openai.Completions.CreateCompletionsAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
            }
            return answer;
        }

        public async Task<string> Call_StableDiffusion(string prompt)
        {
            DotNetEnv.Env.Load();

            string apiKey = Environment.GetEnvironmentVariable("STABLE_DIFFUSION_KEY");
            //string prompt = reqPrompt;
            string apiUrl = "https://stablediffusionapi.com/api/v3/text2img";

            using HttpClient client = new HttpClient();

            var requestData = new
            {
                key = apiKey,
                prompt = prompt
            };

            var httpResponse = await client.PostAsJsonAsync(apiUrl,requestData );

            if (httpResponse.IsSuccessStatusCode)
            {
                string responseContent = await httpResponse.Content.ReadAsStringAsync();
                return responseContent;
            }
            else
            {
                // Handle the case when the API request fails
                // You can throw an exception or return an appropriate error message
                return string.Empty;
            }
        }
    }
}
