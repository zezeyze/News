namespace NewsPage.Models
{
    public class Haber
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string ImagePath { get; set; }
        public bool IsPublish { get; set; } = false;
        public DateTime PublishedDate { get; set; } = DateTime.Now;

    }
}
