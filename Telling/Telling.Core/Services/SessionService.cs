﻿using Cheesebaron.MvxPlugins.Settings.Interfaces;
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
        Task<BaseResponse<List<Session>>> GetSessionsAsync();
        Task<BaseResponse<EmptyDto>> CreateSessionAsync(Session session);
    }

    public class SessionService : BaseService, ISessionService
    {
        public SessionService(ISettings settings)
            : base(settings)
        {
        }

        public async Task<BaseResponse<List<Session>>> GetSessionsAsync()
        {
            return await CallToApi<List<Session>>(null, Endpoint.GET_SESSIONS);
        }

        public async Task<BaseResponse<EmptyDto>> CreateSessionAsync(Session session)
        {
            return await CallToApi<EmptyDto>(session, Endpoint.CREATE_SESSION);
        }
    }
}
