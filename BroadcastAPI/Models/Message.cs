using System.ComponentModel.DataAnnotations;

namespace BroadcastAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string? Author { get; set; }
        [StringLength(280)]
        public string? Text { get; set; }
    }
}
