using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;

namespace Telling.Core.Services
{
    public interface ISessionService
    {
        Task<List<Session>> GetSessionsAsync();
    }

    public class SessionService : BaseService, ISessionService
    {
        public const string SESSIONS_URL = API_URL + "/sessions";

        public SessionService(IRestService restService) : base(restService)
        {
        }

        public async Task<List<Session>> GetSessionsAsync()
        {
            return await RestService.GetAsync<List<Session>>(SESSIONS_URL).ConfigureAwait(false);
        }
    }
}
