using BCS240031_LeHoangNam_KiemTraGiuaki.Data;
using BCS240031_LeHoangNam_KiemTraGiuaki.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BCS240031_LeHoangNam_KiemTraGiuaki.Controllers;

public class RoomTypesController(ApplicationDbContext context) : Controller
{
    public async Task<IActionResult> Index() => View(await context.RoomTypes_BCS240031
        .AsNoTracking().Include(t => t.Rooms).OrderBy(t => t.Name).ToListAsync());

    public IActionResult Create() => View();

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(RoomType_BCS240031 roomType)
    {
        if (!ModelState.IsValid) return View(roomType);
        context.Add(roomType);
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã thêm loại phòng.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var item = await context.RoomTypes_BCS240031.FindAsync(id);
        if (item is null) return Message("RoomTypeId không tồn tại.", 404);
        return View(item);
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, RoomType_BCS240031 roomType)
    {
        if (id != roomType.Id) return Message("RoomTypeId không hợp lệ.", 400);
        if (!ModelState.IsValid) return View(roomType);
        if (!await context.RoomTypes_BCS240031.AnyAsync(t => t.Id == id))
            return Message("RoomTypeId không tồn tại.", 404);
        context.Update(roomType);
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã cập nhật loại phòng.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await context.RoomTypes_BCS240031.Include(t => t.Rooms)
            .FirstOrDefaultAsync(t => t.Id == id);
        if (item is null) return Message("RoomTypeId không tồn tại.", 404);
        if (item.Rooms.Count != 0)
        {
            TempData["Error"] = $"Không thể xóa loại phòng '{item.Name}' vì đang có {item.Rooms.Count} phòng sử dụng.";
            return RedirectToAction(nameof(Index));
        }
        context.Remove(item);
        await context.SaveChangesAsync();
        TempData["Success"] = "Đã xóa loại phòng.";
        return RedirectToAction(nameof(Index));
    }

    private ViewResult Message(string message, int statusCode)
    {
        Response.StatusCode = statusCode;
        return View("Message", model: message);
    }
}
