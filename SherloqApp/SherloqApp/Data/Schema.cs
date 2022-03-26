using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("schemas")]
    public class Schema
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("schema_id")]
        public int Id { get; set; }
        [Column("schema_name")]
        public string? Name { get; set; }
        [Column("schema_description")]
        public string? Description { get; set; }
        [Column("db_id")]
        public int DatabaseId { get; set; }
        [ForeignKey("db_id")]
        public Database? Database { get; set; }
    }
}
