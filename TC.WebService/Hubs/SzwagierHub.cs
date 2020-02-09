using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using TC.WebService.Helpers;
using System.Threading.Tasks;
using TC.WebService.Models;
using Microsoft.AspNetCore.Identity;
using TC.Entity.Entities;
using TC.WebService.Services;
using TC.DataAccess.Repositories;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;

namespace TC.WebService.Hubs
{
    //  [Authorize]
    public partial class SzwagierHub : Hub
    {
        // private static List<SzwagierModel> szwagierModels = new List<SzwagierModel>();
        private IDistributedCache _distributedCache;
        private ITestRunHistoryRepository _testRunHistoryRepository;
        private ITestRunResultRepository _testRunResultRepository;
        private IUnitOfWork _unitOfWork;
        private const string szwagierListKey = "SzwagierList";

        public SzwagierHub(
            IDistributedCache distributedCache,
            ITestRunHistoryRepository testRunHistoryRepository,
            ITestRunResultRepository testRunResultRepository,
            IUnitOfWork unitOfWork)
        {
            _distributedCache = distributedCache;
            _testRunHistoryRepository = testRunHistoryRepository;
            _testRunResultRepository = testRunResultRepository;
            _unitOfWork = unitOfWork;
        }

        public override Task OnConnectedAsync()
        {

            string type = Context.GetHttpContext().Request.Query["t"];
            SzwagierType szwagierType;
            switch (type)
            {
                case "d":
                    szwagierType = SzwagierType.SzwagierDashboard;
                    break;
                case "e":
                    szwagierType = SzwagierType.SzwagierBrowserExtension;
                    break;
                case "c":
                    szwagierType = SzwagierType.SzwagierConsole;
                    break;
                default:
                    throw new Exception("Szwagier type unknown");
            }
            var cacheKey = getCacheKey();
            if (cacheKey == null)
            {
                throw new Exception("Forbidden");
            }
                

            var szwagierModels = _distributedCache.GetAsync<List<SzwagierModel>>(getCacheKey()).GetAwaiter().GetResult();
            if (szwagierModels == null)
            {
                szwagierModels = new List<SzwagierModel>();
            }

            var szwagier = new SzwagierModel()
            {
                Name = Context.User.Claims.First(x => x.Type == "Name").Value,
                Location = "Here",
                SzwagierType = szwagierType,
                ConnectionId = Context.ConnectionId,
                UserId = Context.User.Claims.First(x => x.Type == "Guid").Value
            };
            //if (szwagier.SzwagierType == SzwagierType.SzwagierConsole)
            //{
            //    //if szwagier is public 
            //    // by login in with user name and password is private by default 
            //    // TODO if you want to create public 
            //    // 1. make option in user settings to change pref to have public browser agent 
            //    // 2. public browser agent can be only created 
            //    szwagierModels.Add(szwagier);
            //}
            //else
            //{
            var searchedSzwagier = szwagierModels.Where(x => x.UserId == szwagier.UserId && x.SzwagierType == szwagier.SzwagierType);
            if (searchedSzwagier != null)
            {
                // TODO user is already connected -> should be disconnected or other connection shoud be disconnected ????
                foreach (var szwagierItem in searchedSzwagier.ToList())
                {
                    Clients.Client(szwagierItem.ConnectionId).SendAsync("duplicateConnection");
                    szwagierModels = removeSzwagier(szwagierModels, szwagierItem.ConnectionId);
                }

            }
            szwagierModels.Add(szwagier);
            //  }
            _distributedCache.SetAsync<List<SzwagierModel>>(getCacheKey(), szwagierModels, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) }).GetAwaiter();

            foreach (var szw in szwagierModels)
            {
                Clients.User(szw.ConnectionId).SendAsync("UpdateSzwagierList", szwagierModels);
            }

            return base.OnConnectedAsync();
        }
        public override Task OnDisconnectedAsync(Exception exception)
        {
            var szwagierModels = _distributedCache.GetAsync<List<SzwagierModel>>(getCacheKey()).GetAwaiter().GetResult();
            szwagierModels = removeSzwagier(szwagierModels, Context.ConnectionId);
            Clients.All.SendAsync("UpdateSzwagierList", szwagierModels);
            return base.OnDisconnectedAsync(exception);
        }
        public async Task SendTriggerTest(int testId)
        {
            await Clients.All.SendAsync("ReciveTriggerTest", testId);
        }
        public async Task GetSzwagierList()
        {
            var szwagierModels = _distributedCache.GetAsync<List<SzwagierModel>>(getCacheKey()).GetAwaiter().GetResult();
            await Clients.All.SendAsync("UpdateSzwagierList", szwagierModels);
        }
        private List<SzwagierModel> removeSzwagier(List<SzwagierModel> szwagierModels, string connectionId)
        {
            if (szwagierModels != null)
            {
                var itemToRemove = szwagierModels.SingleOrDefault(r => r.ConnectionId == connectionId);
                if (itemToRemove != null)
                {
                    szwagierModels.Remove(itemToRemove);
                }
                _distributedCache.SetAsync<List<SzwagierModel>>(getCacheKey(), szwagierModels, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(2) }).GetAwaiter();
            }
            return szwagierModels;
        }
        private string getCacheKey()
        {
            if (Context.User.Claims.First(x => x.Type == "Guid") == null)
            {
                return null;
            }
            return szwagierListKey + Context.User.Claims.First(x => x.Type == "Guid").Value;
        }
    }
}
