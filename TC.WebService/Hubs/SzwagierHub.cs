using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TC.Common.Consts;
using TC.DataAccess;
using TC.DataAccess.Repositories.Interfaces;
using TC.WebService.Extensions;
using TC.WebService.Models;
using TC.WebService.Services.Interface;

namespace TC.WebService.Hubs
{
    //  [Authorize]
    public partial class SzwagierHub : Hub
    {
        // private static List<SzwagierModel> szwagierModels = new List<SzwagierModel>();
        private ICacheService _cacheService;
        private ITestRunHistoryRepository _testRunHistoryRepository;
        private ITestRunResultRepository _testRunResultRepository;
        private IUnitOfWork _unitOfWork;
      

        public SzwagierHub(
            ICacheService cacheService,
            ITestRunHistoryRepository testRunHistoryRepository,
            ITestRunResultRepository testRunResultRepository,
            IUnitOfWork unitOfWork)
        {
            _cacheService = cacheService;
            _testRunHistoryRepository = testRunHistoryRepository;
            _testRunResultRepository = testRunResultRepository;
            _unitOfWork = unitOfWork;
        }

        public override async Task OnConnectedAsync()
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

            List<SzwagierModel> szwagierModels = null;


            szwagierModels = await _cacheService.GetSzwagierModelAsync(cacheKey);

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
                    await Clients.Client(szwagierItem.ConnectionId).SendAsync("duplicateConnection");
                    szwagierModels = await removeSzwagierAsync(szwagierModels, szwagierItem.ConnectionId);
                }

            }
            szwagierModels.Add(szwagier);
            //  }
            await _cacheService.SetSzwagierModelAsync(cacheKey, szwagierModels);
           
            foreach (var szw in szwagierModels)
            {
                await Clients.User(szw.ConnectionId).SendAsync("UpdateSzwagierList", szwagierModels);
            }

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {            
            var szwagierModels = await _cacheService.GetSzwagierModelAsync(getCacheKey());
            szwagierModels = await removeSzwagierAsync(szwagierModels, Context.ConnectionId);
            await Clients.All.SendAsync("UpdateSzwagierList", szwagierModels);
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendTriggerTest(int testId)
        {
            await Clients.All.SendAsync("ReciveTriggerTest", testId);
        }
        public async Task GetSzwagierList()
        {
            var szwagierModels = await _cacheService.GetSzwagierModelAsync(getCacheKey());
            await Clients.All.SendAsync("UpdateSzwagierList", szwagierModels);
        }
        private async Task<List<SzwagierModel>> removeSzwagierAsync(List<SzwagierModel> szwagierModels, string connectionId)
        {
            if (szwagierModels != null)
            {
                var itemToRemove = szwagierModels.SingleOrDefault(r => r.ConnectionId == connectionId);
                if (itemToRemove != null)
                {
                    szwagierModels.Remove(itemToRemove);
                }
                await _cacheService.SetSzwagierModelAsync(getCacheKey(), szwagierModels);
            }
            return szwagierModels;
        }
        private string getCacheKey()
        {
            try
            {
                if (Context.User.Claims.FirstOrDefault(x => x.Type == "Guid") == null)
                {
                    return null;
                }
                return GenericConsts.SZWAGIER_LIST_KEY + Context.User.Claims.First(x => x.Type == "Guid").Value;
            }
            catch (Exception)
            {
                //TODO log this error
                return null;
            }
        }
    }
}
