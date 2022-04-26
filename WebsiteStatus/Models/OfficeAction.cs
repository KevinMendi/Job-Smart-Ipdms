using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class OfficeAction
    {
        [Key]
        [JsonPropertyName("officeActionId")]
        public int office_action_id { get; set; }

        [JsonPropertyName("officeActionName1")]
        public string office_action_name1 { get; set; }

        [JsonPropertyName("officeActionName2")]
        public string office_action_name2 { get; set; }

        [JsonPropertyName("officeActionName3")]
        public string office_action_name3 { get; set; }

        [JsonPropertyName("officeActionDesc")]
        public string office_action_desc { get; set; }

        [JsonPropertyName("officeActionDue")]
        public int? office_action_due { get; set; }
    }
}
