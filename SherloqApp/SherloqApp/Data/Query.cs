using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SherloqApp.Data
{
    [Table("queries")]
    public class Query
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("query_id") ]
        public int Id { get; set; }
        [Column("query")]
        public string QueryString { get; set; }
        [Column("query_description")]
        public string Description { get; set; }
        [Column("query_name")]
        public string Name { get; set; }
    }
}
