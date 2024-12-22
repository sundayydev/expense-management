- Download Library

- Thư viện này sử dụng để lấy các icon (https://github.com/MartinTopfstedt/FontAwesome6)

```
   PM> Install-Package FontAwesome6.Svg
```

```
   PM> Install-Package FontAwesome6.Svg.WinUI
```

```
   PM> Install-Package MaterialDesignThemes
```
    
- Cấu trúc các thư mục của dự án.

```    
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
├── BLL
│   ├── Services
│   │   ├── UserService.cs
│   │   ├── CategoryService.cs
│   │   └── ...
│   ├── DTO
│   │   ├── User 
│   │        ├── UserDTO.cs
│   │        ├── RegisterDTO.cs
│   │        └── ...
│   └── Appcontext.cs
│
│
└── DAL
    ├── Repositories
    │   ├── UserRepository.cs
    │   ├── CategoryRepository.cs
    │   └── ...
    └── Models
        ├── User.cs
        ├── Category.cs
        └── ...
```
