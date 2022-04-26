using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class ApplicationType
    {
        [Key]
        [JsonPropertyName("applicationTypeId")]
        public int application_type_id { get; set; }

        [JsonPropertyName("applicationTypeName")]
        public string application_type_name { get; set; }

        [JsonPropertyName("applicationTypeDesc")]
        public string application_type_desc { get; set; }
    }
}
