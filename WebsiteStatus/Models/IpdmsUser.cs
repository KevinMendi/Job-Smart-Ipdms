using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class IpdmsUser
    {
        [Key]
        [JsonPropertyName("ipdmsUserId")]
        public int ipdms_user_id { get; set; }

        [JsonPropertyName("firstname")]
        public string first_name { get; set; }

        [JsonPropertyName("lastname")]
        public string last_name { get; set; }

        [JsonPropertyName("middlename")]
        public string middle_name { get; set; }

        [JsonPropertyName("userRoleId")]
        public int user_role_id { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

        [JsonPropertyName("password")]
        public string password { get; set; }

        [JsonPropertyName("createUserId")]
        public string CREATE_USER_ID { get; set; }

        [JsonPropertyName("createUserDate")]
        public DateTime? CREATE_USER_DATE { get; set; }

        [JsonPropertyName("lastUpdateUserId")]
        public string LAST_UPDATE_USER_ID { get; set; }

        [JsonPropertyName("lastUpdateUserDate")]
        public DateTime? LAST_UPDATE_USER_DATE { get; set; }



    }
}
