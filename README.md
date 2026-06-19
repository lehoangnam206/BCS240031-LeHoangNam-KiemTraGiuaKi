# Quản lý phòng trọ

**Tác Giả** Lê Hoàng Nam
**Mã Sinh Viên** BCS240031

Bài kiểm tra giữa kỳ xây dựng bằng ASP.NET Core MVC và Entity Framework Core.

## Yêu cầu môi trường

- .NET SDK 10.0
- Visual Studio 2022 hoặc mới hơn
- SQL Server LocalDB
- Kết nối Internet để hiển thị ảnh từ URL

## link video: https://drive.google.com/drive/folders/1EU9NlQEh0j9waaPHF3e4Xsc87vjU4XqI?usp=drive_link

## Hướng dẫn chạy

### 1. Clone dự án

git clone https://github.com/lehoangnam206/BCS240031-LeHoangNam-KiemTraGiuaKi.git
cd BCS240031-LeHoangNam-KiemTraGiuaKi

## 2. Khởi tạo SQL Server LocalDB
sqllocaldb create MSSQLLocalDB
sqllocaldb start MSSQLLocalDB


## 3. Khôi phục package
dotnet restore


## 4. Chạy dự án
Tại thư mục chứa file solution, chạy lệnh:

```bash
dotnet run --project BCS240031-LeHoangNam-KiemTraGiuaki
```

Sau khi ứng dụng khởi động, mở địa chỉ được hiển thị trong Terminal, ví dụ:

```text
http://localhost:5201
```

Bạn cũng có thể mở file `BCS240031-LeHoangNam-KiemTraGiuaki.slnx` bằng Visual Studio, sau đó nhấn `F5` để chạy.

Khi khởi động lần đầu, ứng dụng sẽ tự động tạo database `MID_BCS240031`, ba bảng và dữ liệu mẫu.
