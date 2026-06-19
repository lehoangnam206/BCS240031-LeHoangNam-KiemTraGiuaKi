# Quản lý phòng trọ

**Tác Giả** Lê Hoàng Nam
**Mã Sinh Viên** BCS240031

Bài kiểm tra giữa kỳ xây dựng bằng ASP.NET Core MVC và Entity Framework Core.

## Yêu cầu môi trường

- .NET SDK 10.0
- Visual Studio 2022 hoặc mới hơn
- SQL Server LocalDB
- Kết nối Internet để hiển thị ảnh từ URL

## Hướng dẫn chạy

### 1. Clone dự án

```bash
git clone https://github.com/lehoangnam206/BCS240031-LeHoangNam-KiemTraGiuaKi.git
cd BCS240031-LeHoangNam-KiemTraGiuaKi
2. Khởi tạo SQL Server LocalDB
sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
3. Khôi phục package
dotnet restore
4. Chạy dự án
dotnet run --project BCS240031-LeHoangNam-KiemTraGiuaki
Hoặc mở file BCS240031-LeHoangNam-KiemTraGiuaki.slnx bằng Visual Studio và nhấn F5.
Ứng dụng tự động tạo:
Database: MID_BCS240031
Bảng Rooms_BCS240031
Bảng RoomTypes_BCS240031
Bảng RoomImages_BCS240031
3 loại phòng và 5 phòng mẫu
Mô tả cấu trúc code
Models
Room_BCS240031: thông tin phòng, giá thuê, diện tích, trạng thái và loại phòng.
RoomType_BCS240031: thông tin loại phòng.
RoomImage_BCS240031: đường dẫn ảnh và trạng thái ảnh đại diện.
Data
ApplicationDbContext cấu hình:
Tên ba bảng có MSSV.
Quan hệ một loại phòng có nhiều phòng.
Quan hệ một phòng có nhiều ảnh.
Khóa ngoại và ràng buộc xóa.
Không cho phép trùng tên phòng trong cùng loại.
Dữ liệu mẫu ban đầu.
Controllers
RoomsController
Cung cấp các chức năng:
Hiển thị danh sách phòng và loại phòng.
Tìm kiếm theo một phần tên phòng.
Lọc theo loại phòng và trạng thái.
Lọc theo giá tối đa.
Sắp xếp theo giá hoặc diện tích.
Thêm và chỉnh sửa phòng.
Hiển thị chi tiết cùng nhiều ảnh.
Thêm ảnh bằng URL.
Chọn và thay đổi ảnh đại diện.
Xử lý RoomId, RoomTypeId và RoomImageId không tồn tại.
RoomTypesController
Cung cấp các chức năng:
Hiển thị danh sách loại phòng.
Thêm và chỉnh sửa loại phòng.
Không cho phép xóa loại phòng đang có phòng sử dụng.
Hiển thị thông báo thay vì để xảy ra lỗi database.
Validation
Tên phòng không được để trống.
Giá thuê phải lớn hơn 0.
Diện tích phải lớn hơn 0.
Loại phòng phải tồn tại.
Tên phòng không được trùng trong cùng loại phòng.
URL ảnh phải hợp lệ.
Giá thuê trên mét vuông
Giá thuê trên một mét vuông được tính khi hiển thị:
Giá trên 1 m² = Price / Area
Giá trị này không được lưu thành cột trong database.
