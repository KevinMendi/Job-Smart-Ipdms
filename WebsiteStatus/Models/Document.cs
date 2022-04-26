using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IpdmsJob.Models
{
    public class Document
    {
        [Key]
        [JsonPropertyName("documentId")]
        public int document_id { get; set; }

        [JsonPropertyName("officeActionId")]
        public int office_action_id { get; set; }

        [JsonPropertyName("projectId")]
        public int project_id { get; set; }

        [JsonPropertyName("pdfName")]
        public string pdf_name { get; set; }

        [JsonPropertyName("pdfContent")]
        public string pdf_content { get; set; }

        [JsonPropertyName("pdfFileSize")]
        public int pdf_file_size { get; set; }

        [JsonPropertyName("mailDate")]
        public DateTime? mail_date { get; set; }

        [JsonPropertyName("fillingDate")]
        public DateTime? filling_date { get; set; }

        [JsonPropertyName("responseDate")]
        public DateTime? response_date { get; set; }

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
    }
}
