using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.Managers
{
    public interface IPlayerManager
    {
        Task<List<Player>> GetPlayersAsync();
    }

    public class PlayerManager : IPlayerManager
    {
        public IPlayerService PlayerService { get; }

        public PlayerManager(IPlayerService playerService)
        {
            PlayerService = playerService;
        }

        public async Task<List<Player>> GetPlayersAsync()
        {
            return await PlayerService.GetPlayersAsync();
        }
    }
}
