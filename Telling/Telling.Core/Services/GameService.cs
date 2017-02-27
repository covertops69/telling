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
            //try
            //{
                return await CallToApi<List<Game>>(null, Endpoint.GET_GAMES);
            //}
            //catch (Exception ex)
            //{
            //    // TODO [JF]: timeout on HTTP client, sometimes causes JAVA IO exception on Android
            //    return ex.Message.ToLower().Equals("exception of type 'java.io.ioexception' was thrown.") ?
            //        HandleTimeout<AdviceMailResponse>(Endpoint.CONTACT_US.Verb.ToString(), Endpoint.CONTACT_US, ex, HttpStatusCode.GatewayTimeout, " [Through error]") :
            //        HandleCrash<AdviceMailResponse>(Endpoint.CONTACT_US.Verb.ToString(), Endpoint.CONTACT_US, ex, HttpStatusCode.InternalServerError);
            //}
        }
    }
}
