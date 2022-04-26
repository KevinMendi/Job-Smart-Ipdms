using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class Project
    {
        [Key]
        [JsonPropertyName("projectId")]
        public int project_id { get; set; }

        [JsonPropertyName("applicationTypeId")]
        public int application_type_id { get; set; }

        [JsonPropertyName("projectStatusId")]
        public int project_status_id { get; set; }

        [JsonPropertyName("ipdmsUserId")]
        public int ipdms_user_id { get; set; }

        [JsonPropertyName("applicationNo")]
        public string application_no { get; set; }

        [JsonPropertyName("projectTitle")]
        public string project_title { get; set; }

        [JsonPropertyName("applicantName")]
        public string applicant_name { get; set; }

        [JsonPropertyName("projectPath")]
        public string project_path { get; set; }

        [JsonPropertyName("isDeleted")]
        public bool is_deleted { get; set; }

        [JsonPropertyName("createUserId")]
        public int CREATE_USER_ID { get; set; }

        [JsonPropertyName("createUserDate")]
        public DateTime? CREATE_USER_DATE { get; set; }

        [JsonPropertyName("lastUpdateUserId")]
        public int LAST_UPDATE_USER_ID { get; set; }

        [JsonPropertyName("lastUpdateUserDate")]
        public DateTime? LAST_UPDATE_USER_DATE { get; set; }

        [JsonPropertyName("isConverted")]
        public bool is_converted { get; set; }

        [JsonPropertyName("refProjectId")]
        public int? ref_project_id { get; set; }
    }
}
