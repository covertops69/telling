using Cheesebaron.MvxPlugins.Settings.Interfaces;
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
        Task<BaseResponse<List<Game>>> GetGamesAsync();
    }

    public class GameManager : BaseService, IGameManager
    {
        public IGameService GameService { get; }

        public GameManager(ISettings settings)
            : base(settings)
        {
        }

        public async Task<BaseResponse<List<Game>>> GetGamesAsync()
        {
            return await CallToApi<List<Game>>(null, Endpoint.GET_GAMES);
        }
    }
}
