using System.ComponentModel.DataAnnotations;

namespace CineMatrixAPI.Entities.Common
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
