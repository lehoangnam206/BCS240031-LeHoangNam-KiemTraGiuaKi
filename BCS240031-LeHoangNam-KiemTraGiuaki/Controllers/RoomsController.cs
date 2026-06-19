using BCS240031_LeHoangNam_KiemTraGiuaki.Data;
using BCS240031_LeHoangNam_KiemTraGiuaki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Controllers;

public class RoomsController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, int? roomTypeId, bool? isAvailable,
        decimal? maxPrice, string sort = "price_asc")
    {
        IQueryable<Room_BCS240031> query = context.Rooms_BCS240031
            .AsNoTracking().Include(r => r.RoomType).Include(r => r.RoomImages);

        if (!string.IsNullOrWhiteSpace(search))
            query = query.Where(r => EF.Functions.Like(r.Name, $"%{search.Trim()}%"));
        if (roomTypeId.HasValue)
            query = query.Where(r => r.RoomTypeId == roomTypeId.Value);
        if (isAvailable.HasValue)
            query = query.Where(r => r.IsAvailable == isAvailable.Value);
        if (maxPrice.HasValue)
            query = query.Where(r => r.Price <= maxPrice.Value);

        query = sort switch
        {
            "price_desc" => query.OrderByDescending(r => r.Price),
            "area_desc" => query.OrderByDescending(r => r.Area),
            _ => query.OrderBy(r => r.Price)
        };

        ViewBag.RoomTypes = new SelectList(await context.RoomTypes_BCS240031.AsNoTracking()
            .OrderBy(t => t.Name).ToListAsync(), "Id", "Name", roomTypeId);
        ViewBag.Search = search;
        ViewBag.RoomTypeId = roomTypeId;
        ViewBag.IsAvailable = isAvailable;
        ViewBag.MaxPrice = maxPrice;
        ViewBag.Sort = sort;
        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var room = await context.Rooms_BCS240031.AsNoTracking()
            .Include(r => r.RoomType).Include(r => r.RoomImages)
            .FirstOrDefaultAsync(r => r.Id == id);
        return room is null ? ErrorMessage("Không tìm thấy phòng với RoomId đã yêu cầu.", 404) : View(room);
    }

    public async Task<IActionResult> Create()
    {
        await LoadRoomTypes();
        return View(new Room_BCS240031 { IsAvailable = true });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Room_BCS240031 room)
    {
        await ValidateRoom(room);
        if (!ModelState.IsValid)
        {
            await LoadRoomTypes(room.RoomTypeId);
            return View(room);
        }

        context.Add(room);
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã thêm phòng thành công.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var room = await context.Rooms_BCS240031.FindAsync(id);
        if (room is null) return ErrorMessage("Không tìm thấy phòng cần chỉnh sửa.", 404);
        await LoadRoomTypes(room.RoomTypeId);
        return View(room);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Room_BCS240031 room)
    {
        if (id != room.Id) return ErrorMessage("RoomId không hợp lệ.", 400);
        if (!await context.Rooms_BCS240031.AnyAsync(r => r.Id == id))
            return ErrorMessage("Không tìm thấy phòng cần chỉnh sửa.", 404);

        await ValidateRoom(room, id);
        if (!ModelState.IsValid)
        {
            await LoadRoomTypes(room.RoomTypeId);
            return View(room);
        }

        context.Update(room);
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật phòng thành công.";
        return RedirectToAction(nameof(Details), new { id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddImage(int roomId, string imageUrl, bool isThumbnail)
    {
        if (!await context.Rooms_BCS240031.AnyAsync(r => r.Id == roomId))
            return ErrorMessage("RoomId không tồn tại.", 404);
        if (string.IsNullOrWhiteSpace(imageUrl) ||
            !Uri.TryCreate(imageUrl, UriKind.Absolute, out var uri) ||
            (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
        {
            TempData["Error"] = "Đường dẫn ảnh không hợp lệ.";
            return RedirectToAction(nameof(Details), new { id = roomId });
        }

        if (isThumbnail || !await context.RoomImages_BCS240031.AnyAsync(i => i.RoomId == roomId))
        {
            await context.RoomImages_BCS240031.Where(i => i.RoomId == roomId)
                .ExecuteUpdateAsync(s => s.SetProperty(i => i.IsThumbnail, false));
            isThumbnail = true;
        }

        context.Add(new RoomImage_BCS240031
            { RoomId = roomId, ImageUrl = imageUrl.Trim(), IsThumbnail = isThumbnail });
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã thêm ảnh.";
        return RedirectToAction(nameof(Details), new { id = roomId });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> SetThumbnail(int id)
    {
        var image = await context.RoomImages_BCS240031.FindAsync(id);
        if (image is null) return ErrorMessage("RoomImageId không tồn tại.", 404);

        await context.RoomImages_BCS240031.Where(i => i.RoomId == image.RoomId)
            .ExecuteUpdateAsync(s => s.SetProperty(i => i.IsThumbnail, false));
        image.IsThumbnail = true;
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã đổi ảnh đại diện.";
        return RedirectToAction(nameof(Details), new { id = image.RoomId });
    }

    private async Task ValidateRoom(Room_BCS240031 room, int? currentId = null)
    {
        if (!await context.RoomTypes_BCS240031.AnyAsync(t => t.Id == room.RoomTypeId))
            ModelState.AddModelError(nameof(room.RoomTypeId), "RoomTypeId không tồn tại.");
        if (await context.Rooms_BCS240031.AnyAsync(r => r.RoomTypeId == room.RoomTypeId &&
            r.Name == room.Name && (!currentId.HasValue || r.Id != currentId.Value)))
            ModelState.AddModelError(nameof(room.Name), "Tên phòng đã tồn tại trong loại phòng này.");
    }

    private async Task LoadRoomTypes(int? selected = null) =>
        ViewBag.RoomTypeId = new SelectList(await context.RoomTypes_BCS240031.AsNoTracking()
            .OrderBy(t => t.Name).ToListAsync(), "Id", "Name", selected);

    private ViewResult ErrorMessage(string message, int statusCode)
    {
        Response.StatusCode = statusCode;
        return View("Message", model: message);
    }
}
