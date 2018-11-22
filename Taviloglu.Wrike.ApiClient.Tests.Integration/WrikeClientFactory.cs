﻿using System.Threading;

namespace Taviloglu.Wrike.ApiClient.Tests.Integration
{
    public static class WrikeClientFactory
    {
        static readonly WrikeClient _wrikeClient;

        static WrikeClientFactory() {
            _wrikeClient = new WrikeClient("eyJ0dCI6InAiLCJhbGciOiJIUzI1NiIsInR2IjoiMSJ9.eyJkIjoie1wiYVwiOjIzMTc2ODQsXCJpXCI6NTM3NDAyNCxcImNcIjo0NTk1MDE0LFwidlwiOm51bGwsXCJ1XCI6NDc2NzU4MSxcInJcIjpcIlVTXCIsXCJzXCI6W1wiV1wiLFwiRlwiLFwiSVwiLFwiVVwiLFwiS1wiLFwiQ1wiXSxcInpcIjpbXSxcInRcIjowfSIsImlhdCI6MTUzNzMyMTkyOH0.r8MaouEsyTiWJ0qPqUt2McslSPP2NTinL9YrnQ9Lcow");
        }

        public static WrikeClient GetWrikeClient()
        {
            //Make each test wait for a while
            //Wrike API has request count limit in a given period of time
            Thread.Sleep(200);

            return _wrikeClient;
        }
    }
}
