using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models.Domain
{
    public class Room
    {
        public int Id { get; set; }

        [StringLength(14)]
        [Required]
        public string RoomIdentifier { get; set; }
    }
}