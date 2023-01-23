using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NewsPortal.Models
{
    public class News
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

        public string Content { get; set; }  
    }
}
