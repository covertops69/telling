using Cheesebaron.MvxPlugins.Settings.Interfaces;
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
        Task<BaseResponse<List<Game>>> GetGamesAsync();
    }

    public class GameService : BaseService, IGameService
    {
        public GameService(ISettings settings)
            : base(settings)
        {
        }

        public async virtual Task<BaseResponse<List<Game>>> GetGamesAsync()
        {
            return await CallToApi<List<Game>>(null, Endpoint.GET_GAMES);
        }
    }
}
