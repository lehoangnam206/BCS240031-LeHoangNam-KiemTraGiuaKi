using System.ComponentModel.DataAnnotations;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Models;

public class RoomImage_BCS240031
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Đường dẫn ảnh không được để trống.")]
    [Url(ErrorMessage = "Đường dẫn ảnh không hợp lệ.")]
    [Display(Name = "Đường dẫn ảnh")]
    public string ImageUrl { get; set; } = string.Empty;

    [Display(Name = "Ảnh đại diện")]
    public bool IsThumbnail { get; set; }

    public int RoomId { get; set; }
    public Room_BCS240031? Room { get; set; }
}
