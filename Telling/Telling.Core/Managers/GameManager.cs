using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.Managers
{
    public interface IGameManager
    {
        Task<List<Game>> GetGamesAsync();
    }

    public class GameManager : IGameManager
    {
        public IGameService GameService { get; }

        public GameManager(IGameService gameService)
        {
            GameService = gameService;
        }

        public async Task<List<Game>> GetGamesAsync()
        {
            return await GameService.GetGamesAsync();
        }
    }
}
