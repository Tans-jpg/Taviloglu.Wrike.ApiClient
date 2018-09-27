﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core.Invitations;
using Taviloglu.Wrike.Core.Users;

namespace Taviloglu.Wrike.ApiClient
{
    public partial class WrikeClient : IWrikeInvitationsClient
    {
        public IWrikeInvitationsClient Invitations
        {
            get
            {
                return (IWrikeInvitationsClient)this;
            }
        }

        async Task<WrikeInvitation> IWrikeInvitationsClient.CreateAsync(WrikeInvitation newInvitation, string subject, string message)
        {
            if (newInvitation == null)
            {
                throw new ArgumentNullException(nameof(newInvitation), "newInvitation can not be null");
            }            

            var contenBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("email", newInvitation.Email)
                .AddParameter("firstName", newInvitation.FirstName)
                .AddParameter("lastName", newInvitation.LastName)
                .AddParameter("role", newInvitation.Role)
                .AddParameter("external", newInvitation.External)
                .AddParameter("subject", subject)
                .AddParameter("message", message);

            var response = await SendRequest<WrikeInvitation>("invitations", HttpMethods.Post, contenBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }

        async Task IWrikeInvitationsClient.DeleteAsync(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id.Trim() == string.Empty)
            {
                throw new ArgumentException("id can not be empty",nameof(id));
            }

            await SendRequest<WrikeInvitation>($"invitations/{id}", HttpMethods.Delete).ConfigureAwait(false);
        }

        async Task<List<WrikeInvitation>> IWrikeInvitationsClient.GetAsync()
        {
            var response = await SendRequest<WrikeInvitation>("invitations", HttpMethods.Get).ConfigureAwait(false);
            return GetReponseDataList(response);
        }

        async Task<WrikeInvitation> IWrikeInvitationsClient.UpdateAsync(string id, bool? resend, WrikeUserRole? role, bool? external)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (id.Trim() == string.Empty)
            {
                throw new ArgumentException("id can not be empty", nameof(id));
            }

            var contentBuilder = new WrikeFormUrlEncodedContentBuilder()
                .AddParameter("resend", resend)
                .AddParameter("role", role)
                .AddParameter("external", external);

            var response = await SendRequest<WrikeInvitation>($"invitations/{id}", HttpMethods.Put, contentBuilder.GetContent()).ConfigureAwait(false);
            return GetReponseDataFirstItem(response);
        }
    }
}
