using System;
using System.Collections.Generic;
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
            _httpClient=new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7230/API/MineSweeper/");
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public BoardViewModel FieldClick(int row, int column)
        {
            HttpResponseMessage response =_httpClient.GetAsync("fieldClick").Result;
            response.EnsureSuccessStatusCode(); // throw exception if not successful
            BoardViewModel board=BoardViewModel.GetFromJSON(response.Content.ReadAsStringAsync().Result);
            return board;
        }

        /*public bool AddFlag(int row, int column)
        {
            return _mineSweeperBL.AddFlag(row, column);
        }  

        public bool RemoveFlag(int row, int column)
        {
            return _mineSweeperBL.RemoveFlag(row, column);
        }

        public BoardViewModel CreateBoard(int width, int height, int numberOfMines)
        {
            return _mineSweeperBL.CreateGame(width, height, numberOfMines);
        } */
    }
}
