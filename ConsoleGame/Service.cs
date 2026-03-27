using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Shared;

namespace ConsoleGame
{
    internal class Service
    {
        private readonly HttpClient _httpClient;

        public Service()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7230/API/MineSweeper/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public BoardViewModel FieldClick(int row, int column)
        {
            HttpResponseMessage response = GetResponseMessage($"fieldClick/{row}/{column}");
            BoardViewModel board = BoardViewModel.GetFromJSON(response.Content.ReadAsStringAsync().Result);
            return board;
        }

        public BoardViewModel AddFlag(int row, int column)
        {
            HttpResponseMessage response = GetResponseMessage($"addflag/{row}/{column}");
            response = GetResponseMessage("getboard");
            BoardViewModel board = BoardViewModel.GetFromJSON(response.Content.ReadAsStringAsync().Result);
            return board;
        }

        public BoardViewModel RemoveFlag(int row, int column)
        {
            HttpResponseMessage response = GetResponseMessage($"removeflag/{row}/{column}");
            response = GetResponseMessage("getboard");
            BoardViewModel board = BoardViewModel.GetFromJSON(response.Content.ReadAsStringAsync().Result);
            return board;
        }

        public BoardViewModel CreateBoard(int width, int height, int numberOfMines)
        {
            HttpResponseMessage response = GetResponseMessage($"createboard/{width}/{height}/{numberOfMines}");
            BoardViewModel board = BoardViewModel.GetFromJSON(response.Content.ReadAsStringAsync().Result);
            return board;
        }

        private HttpResponseMessage GetResponseMessage(string url)
        {
            HttpResponseMessage response = _httpClient.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
