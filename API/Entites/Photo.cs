using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entites
{
    [Table("Photos")] /* pemamo potrebe za dataContext-om za Photo pa smo dodali samo  Table, kako bi ef napravio sliku*/
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string publicId { get; set; }
        public AppUser AppUser { get; set; }
        public int AppUserId { get; set; }
    }
}