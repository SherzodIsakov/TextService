using RepositoryBase.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace TextService.Repositories.Entities
{
    [Table("TextEntity")]
    public class TextEntity : BaseEntity
    {
        public string Text { get; set; }
    }
}
