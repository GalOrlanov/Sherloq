using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("databases")]
    public class Database
    {
        [Column("db_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Column("db_name")]
        public string? Name { get; set; }
        [Column("db_description")]
        public string? Description { get; set; }
    }
}
