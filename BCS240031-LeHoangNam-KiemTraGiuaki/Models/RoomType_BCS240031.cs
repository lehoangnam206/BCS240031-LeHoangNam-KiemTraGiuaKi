using System.ComponentModel.DataAnnotations;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Models;

public class RoomType_BCS240031
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên loại phòng không được để trống.")]
    [Display(Name = "Tên loại phòng")]
    public string Name { get; set; } = string.Empty;

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    public ICollection<Room_BCS240031> Rooms { get; set; } = new List<Room_BCS240031>();
}
