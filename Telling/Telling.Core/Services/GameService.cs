using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;

namespace Telling.Core.Services
{
    public interface IGameService
    {
        Task<List<Game>> GetGamesAsync();
    }

    public class GameService : BaseService, IGameService
    {
        public const string GAMES_URL = API_URL + "/games";

        public GameService(IRestService restService) : base(restService)
        {
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            return await RestService.GetAsync<List<Game>>(GAMES_URL).ConfigureAwait(false);
        }
    }
}
