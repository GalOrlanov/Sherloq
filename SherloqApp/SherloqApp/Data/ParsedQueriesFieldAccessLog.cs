using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("parsed_queries_field_access_log")]
    public class ParsedQueriesFieldAccessLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("field_access_log_id")]
        public int Id { get; set; }
        [Column("field_id")]
        public int FieldId { get; set; }
        [Column("table_id")]
        public int TableId { get; set; }
        [Column("db_id")]
        public int DatabaseId { get; set; }
        [Column("sherloq_query_execution_id")]
        public int SherloqQueryExecutionId { get; set; }
        [Column("query_submission_datetime")]
        public DateTime QuerySubmissionDateTime { get; set; }

        [ForeignKey("field_id")]
        public Field Field { get; set; }
        [ForeignKey("table_id")]
        public Table Table { get; set; }
        [ForeignKey("db_id")]
        public Database Database { get; set; }
    }
}
