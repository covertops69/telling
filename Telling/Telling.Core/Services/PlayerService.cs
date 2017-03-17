using Cheesebaron.MvxPlugins.Settings.Interfaces;
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
        Task<BaseResponse<List<Player>>> GetPlayersAsync();
    }

    public class PlayerService : BaseService, IPlayerService
    {
        public PlayerService(ISettings settings)
            : base(settings)
        {
        }

        public async Task<BaseResponse<List<Player>>> GetPlayersAsync()
        {
            return await CallToApi<List<Player>>(null, Endpoint.GET_PLAYERS);
        }
    }
}
