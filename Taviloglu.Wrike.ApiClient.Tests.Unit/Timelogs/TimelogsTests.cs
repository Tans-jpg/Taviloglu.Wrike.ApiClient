﻿using NUnit.Framework;

namespace Taviloglu.Wrike.ApiClient.Tests.Unit.Timelogs
{
    [TestFixture]
    public class TimelogsTests
    {
        WrikeClient _wrikeClient;

        [OneTimeSetUp]
        public void Setup()
        {
            _wrikeClient = new WrikeClient("eyJ0dCI6InAiLCJhbGciOiJIUzI1NiIsInR2IjoiMSJ9.eyJkIjoie1wiYVwiOjIzMTc2ODQsXCJpXCI6NTM3NDAyNCxcImNcIjo0NTk1MDE0LFwidlwiOm51bGwsXCJ1XCI6NDc2NzU4MSxcInJcIjpcIlVTXCIsXCJzXCI6W1wiV1wiLFwiRlwiLFwiSVwiLFwiVVwiLFwiS1wiLFwiQ1wiXSxcInpcIjpbXSxcInRcIjowfSIsImlhdCI6MTUzNzMyMTkyOH0.r8MaouEsyTiWJ0qPqUt2McslSPP2NTinL9YrnQ9Lcow");
        }

        [Test]
        public void TimelogsProperty_ShouldReturnTimelogsClient()
        {
            Assert.IsInstanceOf(typeof(IWrikeTimelogsClient), _wrikeClient.Timelogs);
        }
    }
}
