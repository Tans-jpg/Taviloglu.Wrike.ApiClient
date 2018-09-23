﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Taviloglu.Wrike.Core;

namespace Taviloglu.Wrike.ApiClient
{
    /// <summary>
    /// Comment operations
    /// </summary>
    public interface IWrikeCommentsClient
    {
        /// <summary>
        /// Get all comments in current account. 
        /// Scopes: Default, wsReadOnly, wsReadWrite
        /// </summary>
        /// <param name="limit">Maximum number of returned comments, Default:1000</param>
        /// <param name="plainText">Get comment text as plain text, HTML otherwise, Default: false</param>
        /// <param name="updatedDate">Updated date filter, get all comments created or updated in the range specified by dates. Time range between dates must be less than 7 days</param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/get-comments"/>
        Task<List<WrikeComment>> GetAsync(bool? plainText = null, int? limit = null, WrikeDateFilterRange updatedDate = null);

        /// <summary>
        ///  Get task comments.
        ///  Scopes: Default, wsReadOnly, wsReadWrite
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="plainText">Get comment text as plain text, HTML otherwise</param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/get-comments"/>
        Task<List<WrikeTaskComment>> GetInTaskAsync(string taskId, bool? plainText=null);


        /// <summary>
        /// Get folder comments.
        /// Scopes: Default, wsReadOnly, wsReadWrite
        /// </summary>
        /// <param name="folderId"></param>
        /// <param name="plainText">Get comment text as plain text, HTML otherwise</param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/get-comments"/>
        Task<List<WrikeFolderComment>> GetInFolderAsync(string folderId, bool? plainText=null);

        /// <summary>
        /// Get single or multiple comments by their IDs.
        /// Scopes: Default, wsReadOnly, wsReadWrite
        /// </summary>
        /// <param name="commentIds"></param>
        /// <param name="plainText">Get comment text as plain text, HTML otherwise, Default: false</param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/get-comments"/>
        Task<List<WrikeComment>> GetAsync(List<string> commentIds, bool? plainText = null);

        /// <summary>
        /// Delete comment by ID.
        /// Scopes: Default, wsReadWrite
        /// </summary>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/delete-comment"/>        
        Task DeleteAsync(string commentId);

        /// <summary>
        ///  Update Comment by ID. A comment is available for updates only during the 5 minutes after creation.
        ///  Scopes: Default, wsReadWrite
        /// </summary>
        /// <param name="id">CommentId</param>
        /// <param name="plainText">Get comment text as plain text, HTML otherwise, Default: false</param>
        /// <param name="text">Comment text, can not be empty</param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/update-comment"/>
        Task<WrikeComment> UpdateAsync(string id, string text, bool? plainText = null);

        /// <summary>
        ///  Create a comment in the folder/task. The virtual Root and Recycle Bin folders cannot have comments.
        ///  Scopes: Default, wsReadWrite
        /// </summary>
        /// <param name="plainText">Treat comment text as plain text, HTML otherwise</param>
        /// <param name="newComment">Use ctor <see cref="WrikeComment.WrikeComment(string, string, string)"/></param>
        /// See <see href="https://developers.wrike.com/documentation/api/methods/create-comment"/>
        Task<T> CreateAsync<T>(T newComment, bool? plainText = null) where T : WrikeComment;

    }
}
