using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Models;

public class Room_BCS240031
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên phòng không được để trống.")]
    [Display(Name = "Tên phòng")]
    public string Name { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue, ErrorMessage = "Giá phải lớn hơn 0.")]
    [Column(TypeName = "decimal(18,2)")]
    [Display(Name = "Giá thuê")]
    public decimal Price { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Diện tích phải lớn hơn 0.")]
    [Column(TypeName = "decimal(10,2)")]
    [Display(Name = "Diện tích (m²)")]
    public decimal Area { get; set; }

    [Display(Name = "Còn phòng")]
    public bool IsAvailable { get; set; }

    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Vui lòng chọn loại phòng.")]
    [Display(Name = "Loại phòng")]
    public int RoomTypeId { get; set; }

    public RoomType_BCS240031? RoomType { get; set; }
    public ICollection<RoomImage_BCS240031> RoomImages { get; set; } = new List<RoomImage_BCS240031>();
}
