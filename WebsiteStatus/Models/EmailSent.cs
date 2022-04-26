using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class EmailSent
    {
        [Key]
        [JsonPropertyName("emailSentId")]
        public int email_sent_id { get; set; }

        [JsonPropertyName("documentId")]
        public int document_id { get; set; }

        [JsonPropertyName("projectId")]
        public int project_id { get; set; }

        [JsonPropertyName("dayFive")]
        public bool day_five { get; set; }

        [JsonPropertyName("dayThree")]
        public bool day_three { get; set; }

        [JsonPropertyName("dayOne")]
        public bool day_one { get; set; }

        [JsonPropertyName("createUserDate")]
        public DateTime? CREATE_USER_DATE { get; set; }
    }
}
