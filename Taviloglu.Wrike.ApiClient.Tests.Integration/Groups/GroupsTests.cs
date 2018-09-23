﻿using NUnit.Framework;
using System.Linq;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.ApiClient.Tests.Integration.Groups
{
    [TestFixture]
    public class GroupsTests
    {
        const string DefaultGroupId = "KX74WSKU";        

        [OneTimeTearDown]
        public void SetToDefaults()
        {
            var groups = WrikeClientFactory.GetWrikeClient().Groups.GetAsync().Result;

            foreach (var group in groups)
            {
                if (group.Id != DefaultGroupId)
                {
                    WrikeClientFactory.GetWrikeClient().Groups.DeleteAsync(group.Id).Wait();
                }
            }
        }

        [Test]
        public void GetAsync_ShouldReturnDefaultGroup()
        {
            SetToDefaults();

            var groups = WrikeClientFactory.GetWrikeClient().Groups.GetAsync().Result;
            Assert.IsNotNull(groups);
            Assert.AreEqual(1, groups.Count);
            Assert.AreEqual(DefaultGroupId, groups.First().Id);
        }


        [Test]
        public void CreateAsync_ShouldAddNewGroupWithTitle()
        {
            var newGroup = new WrikeGroup("Sinan's Test Group");

            var createdGroup = WrikeClientFactory.GetWrikeClient().Groups.CreateAsync(newGroup).Result;

            Assert.IsNotNull(createdGroup);
            Assert.AreEqual(newGroup.Title, createdGroup.Title);
        }
        
        [Test]
        public void UpdateAsync_ShouldUpdateGroupTitle()
        {
            var newGroup = new WrikeGroup("Sinan's Test Group");
            newGroup = WrikeClientFactory.GetWrikeClient().Groups.CreateAsync(newGroup).Result;

            var expectedGroupTitle = "Sinan's Test Group [Updated]";
            var updatedGroup = WrikeClientFactory.GetWrikeClient().Groups.UpdateAsync(newGroup.Id, expectedGroupTitle).Result;

            Assert.IsNotNull(updatedGroup);
            Assert.AreEqual(expectedGroupTitle, updatedGroup.Title);
        }

        [Test]
        public void DeleteAsync_ShouldDeleteNewGroup()
        {
            var newGroup = new WrikeGroup("Sinan's Test Group");
            newGroup = WrikeClientFactory.GetWrikeClient().Groups.CreateAsync(newGroup).Result;

            WrikeClientFactory.GetWrikeClient().Groups.DeleteAsync(newGroup.Id).Wait();

            var groups = WrikeClientFactory.GetWrikeClient().Groups.GetAsync().Result;
            var isGroupDeleted = !groups.Any(g => g.Id == newGroup.Id);

            Assert.IsTrue(isGroupDeleted);
        }
    }
}
