﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core.Workflows;

namespace Taviloglu.Wrike.ApiClient
{
    public partial class WrikeClient : IWrikeWorkflowsClient
    {
        public IWrikeWorkflowsClient Workflows
        {
            get
            {
                return (IWrikeWorkflowsClient)this;
            }
        }

        async Task<WrikeWorkflow> IWrikeWorkflowsClient.CreateAsync(WrikeWorkflow newWorkflow)
        {
            if (newWorkflow == null)
            {
                throw new ArgumentNullException(nameof(newWorkflow));
            }

            var postDataBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("name", newWorkflow.Name);

            var response = await SendRequest<WrikeWorkflow>("workflows", HttpMethods.Post, postDataBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }

        async Task<List<WrikeWorkflow>> IWrikeWorkflowsClient.GetAsync()
        {
            var response = await SendRequest<WrikeWorkflow>("workflows", HttpMethods.Get).ConfigureAwait(false);
            return GetReponseDataList(response);
        }

        async Task<WrikeWorkflow> IWrikeWorkflowsClient.UpdateAsync(WrikeClientIdParameter id, string name, bool? isHidden, WrikeCustomStatus customStatus)
        {
            var contentBuilder = new WrikeFormUrlEncodedContentBuilder()
               .AddParameter("name", name)
               .AddParameter("hidden", isHidden)
               .AddParameter("customStatus", customStatus);

            var response = await SendRequest<WrikeWorkflow>($"workflows/{id}", HttpMethods.Put, contentBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }
    }
}
