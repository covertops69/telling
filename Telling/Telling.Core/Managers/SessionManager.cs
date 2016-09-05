using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telling.Core.Models;
using Telling.Core.Services;

namespace Telling.Core.Managers
{
    public interface ISessionManager
    {
        Task<List<Session>> GetSessionsAsync();
        Task CreateSessionAsync(Session session);
    }

    public class SessionManager : ISessionManager
    {
        public ISessionService SessionService { get; }

        public SessionManager(ISessionService sessionService)
        {
            SessionService = sessionService;
        }

        public async Task<List<Session>> GetSessionsAsync()
        {
            return await SessionService.GetSessionsAsync();
        }

        public async Task CreateSessionAsync(Session session)
        {
            await SessionService.CreateSessionAsync(session);
        }
    }
}
