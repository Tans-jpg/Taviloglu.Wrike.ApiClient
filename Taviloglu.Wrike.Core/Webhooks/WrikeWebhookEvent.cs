﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Taviloglu.Wrike.Core.WebHooks
{
    public class WrikeWebHookEvent : IWrikeObject
    {   
        [JsonProperty("taskId")]
        public string TaskId { get; set; }
        [JsonProperty("webhookId")]
        public string WebHookId { get; set; }
        [JsonProperty("eventAuthorId")]
        public string EventAuthorId { get; set; }
        [JsonProperty("eventType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WrikeWebHookEventType Type { get; set; }
        [JsonProperty("lastUpdatedDate")]
        public DateTime LastUpdatedDate { get; set; }
    }

}
