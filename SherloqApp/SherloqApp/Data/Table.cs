using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("tables")]
    public class Table
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("table_id")]
        public int Id { get; set; }
        [Column("table_name")]
        public string? Name { get; set; }
        [Column("table_description")]
        public string? Description { get; set; }
        [Column("db_id")]
        public int DatabaseId { get; set; }
        [ForeignKey("schema_id")]
        public Schema? Schema { get; set; }
        [ForeignKey("db_id")]
        public Database? Database { get; set; }
    }
}
