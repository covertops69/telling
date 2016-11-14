using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;

namespace Telling.Core.Services
{
    public interface IPlayerService
    {
        Task<List<Player>> GetPlayersAsync();
    }

    public class PlayerService : BaseService, IPlayerService
    {
        public const string PLAYERS_URL = API_URL + "/players";

        public PlayerService(IRestService restService) : base(restService)
        {
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await RestService.GetAsync<List<Player>>(PLAYERS_URL).ConfigureAwait(false);
        }
    }
}
