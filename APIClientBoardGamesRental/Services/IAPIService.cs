using System.Net.Http;

namespace APIClientBoardGamesRental.Services
{
    public interface IAPIService
    {
        public HttpClient Client { get;  }
    }
}