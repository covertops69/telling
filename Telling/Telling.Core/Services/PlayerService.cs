using Cheesebaron.MvxPlugins.Settings.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;
using Telling.Core.ViewModels.Players;

namespace Telling.Core.Services
{
    public interface IPlayerService
    {
        Task<BaseResponse<List<PlayerViewModel>>> GetPlayersAsync();
    }

    public class PlayerService : BaseService, IPlayerService
    {
        public PlayerService(ISettings settings)
            : base(settings)
        {
        }

        public async Task<BaseResponse<List<PlayerViewModel>>> GetPlayersAsync()
        {
            return await CallToApi<List<PlayerViewModel>>(null, Endpoint.GET_PLAYERS);
        }
    }
}
