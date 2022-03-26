using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("fields")]
    public class Field
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("field_id")]
        public int Id { get; set; }
        [Column("field_name")]
        public string? Name { get; set; }
        [Column("field_description")]
        public string? Description { get; set; }
        [Column("field_type")]
        public string? Type { get; set; }
        [Column("db_id")]
        public int DatabaseId { get; set; }
        [Column("table_id")]
        public int TableId { get; set; }
        [ForeignKey("db_id")]
        public Database? Database { get; set; }
        [ForeignKey("table_id")]
        public Table? Table { get; set; }
    }
}
