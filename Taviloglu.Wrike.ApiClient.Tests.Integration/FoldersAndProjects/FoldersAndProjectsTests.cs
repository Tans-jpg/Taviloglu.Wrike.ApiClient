﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Taviloglu.Wrike.Core.FoldersAndProjects;

namespace Taviloglu.Wrike.ApiClient.Tests.Integration.FoldersAndProjects
{
    [TestFixture]
    public class FoldersAndProjectsTests
    {

        const string RootFolderId = "IEACGXLUI7777777";
        const string RecycleBinFolderId = "IEACGXLUI7777776";

        readonly List<string> DefaultFolderIds = new List<string> { RootFolderId, RecycleBinFolderId, "IEACGXLUI4IEQ6NG", "IEACGXLUI4IEQ6NH", "IEACGXLUI4IEQ6NB", "IEACGXLUI4IHJMYP" };

        [OneTimeTearDown]
        public void ReturnToDefaults()
        {
            var folderTree = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.GetFolderTreeAsync().Result;

            var folderTreeOfRecycleBin = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.GetFolderTreeAsync(RecycleBinFolderId).Result;

            foreach (var folder in folderTree)
            {
                if (!DefaultFolderIds.Contains(folder.Id) && !folderTreeOfRecycleBin.Any(f=> f.Id == folder.Id))
                {
                    WrikeClientFactory.GetWrikeClient().FoldersAndProjects.DeleteAsync(folder.Id).Wait();
                }
            }
        }

        [Test]
        public void GetFolderTreeAsync_ShouldReturnFolderTrees()
        {
            var folderTrees = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.GetFolderTreeAsync().Result;
            Assert.IsNotNull(folderTrees);
            Assert.GreaterOrEqual(folderTrees.Count, 5);
        }

        [Test]
        public void GetFoldersAsync_ShouldReturnDefaultFolders()
        {
            var folders = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.GetFoldersAsync(DefaultFolderIds).Result;
            Assert.IsNotNull(folders);
            Assert.AreEqual(4, folders.Count);
        }

        [Test]
        public void CreateAsync_ShouldAddNewFolderWithTitle()
        {
            var newFolder = new WrikeFolder("TestFolder #1");

            var createdFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.CreateAsync(RootFolderId, newFolder).Result;

            Assert.IsNotNull(createdFolder);
            Assert.AreEqual(newFolder.Title, createdFolder.Title);

            //TODO: test other parameters
        }


        [Test]
        public void CopyAsync_ShouldCopyFolder()
        {
            var parentFolder = new WrikeFolder("My Parent Folder");
            parentFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .CreateAsync(RootFolderId, parentFolder).Result;

            var folderToBeCopied = new WrikeFolder("My Folder To Be Copied");
            folderToBeCopied = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .CreateAsync(RootFolderId, folderToBeCopied).Result;

            var expectedTitle = "Copied";
            var copiedFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .CopyAsync(folderToBeCopied.Id, parentFolder.Id, "Copied").Result;

            Assert.IsNotNull(copiedFolder);
            Assert.AreEqual(expectedTitle, copiedFolder.Title);
            Assert.IsNotNull(copiedFolder.ParentIds);
            Assert.AreEqual(parentFolder.Id, copiedFolder.ParentIds.First());
        }

        [Test]
        public void UpdateAsync_ShouldUpdateFolderTitle()
        {
            var newFolder = new WrikeFolder("My Folder #1");
            newFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .CreateAsync(RootFolderId, newFolder).Result;

            var expectedTitle = "My Folder #1 [Updated]";
            var updatedFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .UpdateAsync(newFolder.Id, expectedTitle).Result;

            Assert.IsNotNull(updatedFolder);
            Assert.AreEqual(expectedTitle, updatedFolder.Title);

            //TODO: test other parameters
        }

        [Test]
        public void DeleteAsync_ShouldDeleteNewFolder()
        {
            var newFolder = new WrikeFolder("My Folder #2");
            newFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects
                .CreateAsync(RootFolderId, newFolder).Result;

            var deletedFolder = WrikeClientFactory.GetWrikeClient().FoldersAndProjects.DeleteAsync(newFolder.Id).Result;

            Assert.IsNotNull(deletedFolder);
        }
    }
}
