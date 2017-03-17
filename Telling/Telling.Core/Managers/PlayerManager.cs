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
    public interface IPlayerManager
    {
        Task<BaseResponse<List<Player>>> GetPlayersAsync();
    }

    public class PlayerManager : BaseService, IPlayerManager
    {
        public IPlayerService PlayerService { get; }

        public PlayerManager(ISettings settings)
            : base(settings)
        {
        }

        public async Task<BaseResponse<List<Player>>> GetPlayersAsync()
        {
            return await CallToApi<List<Player>>(null, Endpoint.GET_PLAYERS);
        }
    }
}
