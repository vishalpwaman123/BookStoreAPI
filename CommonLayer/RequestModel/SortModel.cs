using System.ComponentModel.DataAnnotations;

namespace CommonLayer.RequestModel
{
    public class SortModel
    {
        [Required(ErrorMessage = "Book Name Required")]
        public string attribute { get; set; }

        [Required(ErrorMessage = "Author Name Required")]
        public string operation { get; set; }
    }
}
