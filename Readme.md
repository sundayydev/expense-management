# Ứng Dụng Quản Lý Chi Tiêu

## Giới Thiệu
Kho lưu trữ này chứa mã nguồn cho ứng dụng Quản Lý Chi Tiêu, một dự án WPF được xây dựng theo mô hình 3 lớp. Ứng dụng giúp người dùng quản lý chi tiêu với các tính năng như xác thực người dùng, quản lý danh mục, và giao diện hiện đại.

---

## Tác Giả

- Ngô Mạnh Hùng

- Lê Hoài Huân

- Lê Hữu Duy Hoàng

---

## Tính Năng
- Đăng nhập và đăng ký người dùng.
- Quản lý chi tiêu và danh mục.
- Giao diện hiện đại sử dụng Material Design và biểu tượng FontAwesome6.

---

## Hướng Dẫn Cài Đặt
### Yêu Cầu
- .NET Framework / .NET Core
- Visual Studio (hoặc IDE tương thích với phát triển WPF)

### Thư Viện Sử Dụng
Cài đặt các gói NuGet sau:
```sh
PM> Install-Package FontAwesome6.Svg
PM> Install-Package FontAwesome6.Svg.WinUI
PM> Install-Package MaterialDesignThemes
```

---

## Cấu Trúc Dự Án
```plaintext
ExpenseManagement
│
├── GUI
│   ├── Views
│   │   ├── MainWindow.xaml
│   │   ├── Login.xaml
│   │   └── ...
│   ├── UserControls
│   │   ├── MainViewModel.cs
│   │   ├── LoginViewModel.cs
│   │   └── ...
│   └── Library
│       ├── Colors.xaml
│       └── ...
│
├── BLL (Lớp Logic Nghiệp Vụ)
│   ├── Services
│   │   ├── UserService.cs
│   │   ├── CategoryService.cs
│   │   └── ...
│   ├── DTO
│   │   ├── User
│   │   │   ├── UserDTO.cs
│   │   │   ├── RegisterDTO.cs
│   │   │   └── ...
│   └── AppContext.cs
│
└── DAL (Lớp Truy Cập Dữ Liệu)
    ├── Repositories
    │   ├── UserRepository.cs
    │   ├── CategoryRepository.cs
    │   └── ...
    └── Models
        ├── User.cs
        ├── Category.cs
        └── ...
```
### Mô Tả Thư Mục
- **GUI**: Chứa các thành phần giao diện người dùng bao gồm các view và user control.
- **BLL (Lớp Logic Nghiệp Vụ)**: Triển khai logic nghiệp vụ cốt lõi và các dịch vụ.
- **DAL (Lớp Truy Cập Dữ Liệu)**: Quản lý tương tác dữ liệu, mô hình và kho dữ liệu.

---

## Cách Chạy Ứng Dụng
1. Clone kho lưu trữ:
   ```sh
   git clone https://github.com/sundayydev/expense-management.git
   ```
2. Mở tệp solution (`.sln`) trong Visual Studio.
3. Tải các gói NuGet.
4. Xây dựng và chạy dự án.

---

## Giấy Phép
...

---

## Thư vi
- [FontAwesome6](https://github.com/MartinTopfstedt/FontAwesome6)
- [Material Design Themes](https://github.com/MaterialDesignInXAML/MaterialDesignInXamlToolkit)

