﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.ApiClient
{
    public partial class WrikeClient : IWrikeGroupsClient
    {
        public IWrikeGroupsClient Groups
        {
            get
            {
                return (IWrikeGroupsClient)this;
            }

        }
        async Task IWrikeGroupsClient.DeleteAsync(string groupId, bool isTest)
        {
            if (string.IsNullOrWhiteSpace(groupId))
            {
                throw new ArgumentNullException(nameof(groupId),"groupId can not be null or empty");
            }

            var requestUri = $"groups/{groupId}";
            if (isTest)
            {
                requestUri += "?test=true";

            }
            var response = await SendRequest<WrikeGroup>(requestUri, HttpMethods.Delete).ConfigureAwait(false);
            //TODO: anything to do with the response?
            return;
        }

        async Task<List<WrikeGroup>> IWrikeGroupsClient.GetAsync(string groupId, List<string> optionalFields, WrikeMetadata metaDataFilter) {
            
            string requestUri = string.Empty;
            WrikeGetUriBuilder uriBuilder;

            if (!string.IsNullOrWhiteSpace(groupId))
            {
                //user has provided groupId
                requestUri = $"groups/{groupId}";
                uriBuilder = new WrikeGetUriBuilder(requestUri)
               .AddParameter("fields", optionalFields);
            }
            else
            {
                requestUri = $"groups";
                uriBuilder = new WrikeGetUriBuilder(requestUri)
                .AddParameter("metadata", metaDataFilter)
                .AddParameter("fields", optionalFields);
            }

            var response = await SendRequest<WrikeGroup>(uriBuilder.GetUri(), HttpMethods.Get).ConfigureAwait(false);
            return GetReponseDataList(response);
        }

        async Task<WrikeGroup> IWrikeGroupsClient.CreateAsync(WrikeGroup newGroup, string parentId, WrikeGroupAvatar avatar)
        {
            if (newGroup == null)
            {
                throw new ArgumentNullException(nameof(newGroup), "newGroup can not be null");
            }

            if (string.IsNullOrWhiteSpace(newGroup.Title))
            {
                throw new ArgumentNullException(nameof(newGroup.Title),"newGroup.Title can not be null or empty.");
            }

            var requestUri = $"groups";

            var postDataBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("title",newGroup.Title)
                .AddParameter("members", newGroup.MemberIds)
                .AddParameter("parent", parentId)
                .AddParameter("avatar", avatar)
                .AddParameter("metadata", newGroup.Metadata);

            var postContent = postDataBuilder.GetContent();
            var response = await SendRequest<WrikeGroup>(requestUri, HttpMethods.Post, postContent).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }

        async Task<WrikeGroup> IWrikeGroupsClient.UpdateAsync(string groupId, string title, List<string> membersToAdd, List<string> membersToRemove, string parentId, WrikeGroupAvatar avatar, List<WrikeMetadata> metaData)
        {
            var contentBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("title", title)
                .AddParameter("addMembers", membersToAdd)
                .AddParameter("removeMembers", membersToRemove)
                .AddParameter("parent", parentId)
                .AddParameter("avatar", avatar)
                .AddParameter("metadata", metaData);

            var response = await SendRequest<WrikeGroup>($"groups/{groupId}", HttpMethods.Put, contentBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }


    }
}
