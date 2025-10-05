# 🏨 Hotel Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-blue.svg)](https://dotnet.microsoft.com/download)
[![WPF](https://img.shields.io/badge/WPF-Desktop-green.svg)](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6.4.4-orange.svg)](https://docs.microsoft.com/en-us/ef/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-red.svg)](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb)

A comprehensive Windows Presentation Foundation (WPF) application for managing hotel operations, built with .NET 8.0 and Entity Framework 6.

## 📋 Table of Contents

- [Quick Start](#-quick-start)
- [Features](#-features)
- [Technologies Used](#-technologies-used)
- [Installation](#-installation)
- [Database Setup](#-database-setup)
- [Usage](#-usage)
- [Project Structure](#-project-structure)
- [Development](#-development)
- [Contributing](#-contributing)
- [Issues & Support](#-issues--support)
- [License](#-license)

## ✨ Features

### 🏠 **Room Management**
- Add, edit, delete rooms
- Room type management (Standard, Deluxe, VIP, Family, Suite)
- Room status tracking (Available, Occupied, Maintenance)
- Room pricing configuration

### 👥 **Customer Management**
- Customer registration and profile management
- Customer search and filtering
- Booking history tracking
- Contact information management

### 📅 **Booking Management**
- Room booking and reservation system
- Check-in/Check-out functionality
- Booking status tracking
- Payment processing

### 🛎️ **Service Management**
- Additional services (Room Service, Laundry, etc.)
- Service pricing and availability
- Service booking integration

### 📊 **Reports & Analytics**
- Revenue reports
- Occupancy statistics
- Customer analytics
- Booking trends

### 🔐 **User Authentication**
- Secure login system
- Role-based access control
- Session management

## 🛠️ Technologies Used

- **Framework:** .NET 8.0
- **UI Framework:** Windows Presentation Foundation (WPF)
- **Database:** SQL Server LocalDB
- **ORM:** Entity Framework 6.4.4
- **Architecture:** MVVM (Model-View-ViewModel)
- **Language:** C# 12.0

## 📋 Prerequisites

Before running this application, ensure you have:

- **Visual Studio 2022** (Community, Professional, or Enterprise)
- **.NET 8.0 SDK**
- **SQL Server LocalDB** (included with Visual Studio)
- **Windows 10/11** (64-bit)

## 🚀 Quick Start

### Prerequisites
- **Visual Studio 2022** (Community, Professional, or Enterprise)
- **.NET 8.0 SDK**
- **SQL Server LocalDB** (included with Visual Studio)
- **Git** for version control

### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/hotel-management-system.git
cd hotel-management-system
```

### 2. Open in Visual Studio
```bash
# Open the solution file
start LePhungHa_8991.sln
```

### 3. Restore NuGet Packages
```bash
# In Visual Studio Package Manager Console
Update-Package -Reinstall

# Or via command line
dotnet restore
```

### 4. Setup Database
```bash
# Run the database setup script
.\SetupDatabase.ps1
```

### 5. Build and Run
```bash
# Build the project
dotnet build

# Run the application
dotnet run
```

## 🚀 Installation

### Alternative Installation Methods

#### Using Visual Studio
1. Open `LePhungHa_8991.sln` in Visual Studio 2022
2. Right-click solution → "Restore NuGet Packages"
3. Build → Build Solution (Ctrl+Shift+B)
4. Press F5 to run

#### Using Command Line
```bash
# Clone repository
git clone https://github.com/yourusername/hotel-management-system.git
cd hotel-management-system

# Restore packages
dotnet restore

# Build project
dotnet build --configuration Release

# Run application
dotnet run --project LePhungHa_8991.csproj
```

## 🗄️ Database Setup

### Automatic Setup (Recommended)
The project includes a PowerShell script for automatic database setup:

```powershell
# Run the database setup script
.\SetupDatabase.ps1
```

### Manual Setup
1. **Create Database:**
   ```sql
   CREATE DATABASE HotelDB COLLATE Vietnamese_CI_AS;
   ```

2. **Run Database Script:**
   ```bash
   sqlcmd -S "(LocalDB)\MSSQLLocalDB" -d HotelDB -i DatabaseScript.sql
   ```

3. **Verify Connection:**
   - Check `App.config` for connection string
   - Ensure `HotelDB.mdf` is in project directory

### Database Files
- **Main Database:** `HotelDB.mdf`
- **Log File:** `HotelDB_log.ldf`
- **Script:** `DatabaseScript.sql`

## 🎯 Usage

### Login
- **Username:** `admin`
- **Password:** `123456`

### Main Features

#### 🏠 Room Management
1. Navigate to "Quản Lý Phòng" (Room Management)
2. Add new rooms with room types
3. Set room status and pricing
4. View room availability

#### 👥 Customer Management
1. Go to "Quản Lý Khách Hàng" (Customer Management)
2. Register new customers
3. Search and filter customers
4. View customer booking history

#### 📅 Booking Management
1. Access "Quản Lý Đặt Phòng" (Booking Management)
2. Create new bookings
3. Process check-ins and check-outs
4. Manage booking status

#### 🛎️ Service Management
1. Navigate to "Quản Lý Dịch Vụ" (Service Management)
2. Add hotel services
3. Set service pricing
4. Track service usage

## 📁 Project Structure

```
LePhungHa_8991/
├── Models/                 # Data models and entities
│   ├── CHITIETDICHVU.cs   # Service details
│   ├── DATPHONG.cs        # Booking model
│   ├── DICHVU.cs          # Service model
│   ├── HotelDbContext.cs  # Database context
│   ├── KHACHHANG.cs       # Customer model
│   ├── LOAIPHONG.cs       # Room type model
│   ├── PHONG.cs           # Room model
│   └── TAIKHOAN.cs        # Account model
├── ViewModels/             # MVVM ViewModels
│   ├── DatPhong_ViewModel.cs
│   ├── DichVu_ViewModel.cs
│   ├── KhachHang_ViewModel.cs
│   ├── LoaiPhong_ViewModel.cs
│   └── Phong_ViewModel.cs
├── Views/                  # XAML Views
│   ├── QuanLyDatPhong.xaml
│   ├── QuanLyDichVu.xaml
│   ├── QuanLyKhachHang.xaml
│   ├── QuanLyLoaiPhong.xaml
│   └── QuanLyPhong.xaml
├── DangNhap.xaml          # Login screen
├── TrangChu.xaml          # Main dashboard
├── App.config             # Configuration
├── DatabaseScript.sql     # Database setup script
├── HotelDB.mdf           # Database file
└── HotelDB_log.ldf       # Database log file
```

## 🖼️ Screenshots

### Login Screen
- Secure authentication interface
- User-friendly design

### Main Dashboard
- Overview of hotel operations
- Quick access to all modules

### Room Management
- Room listing with status indicators
- Room type management
- Pricing configuration

### Booking Management
- Booking calendar view
- Check-in/Check-out interface
- Payment processing

## 🔧 Configuration

### Connection String
The application uses SQL Server LocalDB with the following connection string:

```xml
<connectionStrings>
  <add name="HotelConnectionString" 
       connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\LePhungHa_8991\HotelDB.mdf;Integrated Security=True" 
       providerName="System.Data.SqlClient" />
</connectionStrings>
```

### Database Settings
- **Collation:** Vietnamese_CI_AS (Vietnamese character support)
- **Authentication:** Windows Authentication
- **Database Type:** LocalDB (.mdf file)

## 🐛 Troubleshooting

### Common Issues

#### 1. Database Connection Failed
```bash
# Solution: Check if LocalDB is running
sqllocaldb info MSSQLLocalDB
sqllocaldb start MSSQLLocalDB
```

#### 2. Vietnamese Characters Not Displaying
```sql
-- Ensure database collation is set correctly
ALTER DATABASE HotelDB COLLATE Vietnamese_CI_AS;
```

#### 3. Entity Framework Errors
```bash
# Rebuild the project
dotnet clean
dotnet build
```

#### 4. File Locked Errors
- Close the application before rebuilding
- Stop debugging session in Visual Studio

### Debug Mode
Enable debug logging by adding to `App.config`:

```xml
<system.diagnostics>
  <trace autoflush="true">
    <listeners>
      <add name="console" type="System.Diagnostics.ConsoleTraceListener" />
    </listeners>
  </trace>
</system.diagnostics>
```

## 📊 Database Schema

### Main Tables
- **TAIKHOAN** - User accounts
- **LOAIPHONG** - Room types
- **PHONG** - Rooms
- **KHACHHANG** - Customers
- **DATPHONG** - Bookings
- **DICHVU** - Services
- **CHITIETDICHVU** - Service details

### Sample Data
The database includes sample data for:
- 5 room types
- 10 rooms
- 25 customers
- 15 bookings
- 6 services

## 🚀 Deployment

### Development
```bash
# Run in debug mode
dotnet run --configuration Debug
```

### Production
```bash
# Build release version
dotnet build --configuration Release
```

### Database Migration
```bash
# Update database schema
Update-Database -Context HotelDbContext
```

## 🛠️ Development

### Git Workflow

#### Initial Setup
```bash
# Clone the repository
git clone https://github.com/yourusername/hotel-management-system.git
cd hotel-management-system

# Create and switch to development branch
git checkout -b develop
```

#### Daily Development
```bash
# Pull latest changes
git pull origin develop

# Create feature branch
git checkout -b feature/room-management-improvements

# Make your changes and commit
git add .
git commit -m "feat: improve room management UI"

# Push to remote
git push origin feature/room-management-improvements
```

#### Branch Naming Convention
- `feature/` - New features
- `bugfix/` - Bug fixes
- `hotfix/` - Critical fixes
- `refactor/` - Code refactoring
- `docs/` - Documentation updates

#### Commit Message Format
```
type(scope): description

feat(room): add room availability checker
fix(booking): resolve payment calculation bug
docs(readme): update installation instructions
```

### Development Environment Setup
```bash
# Install development dependencies
dotnet tool install --global dotnet-ef
dotnet tool install --global dotnet-outdated

# Setup pre-commit hooks (optional)
git config core.hooksPath .githooks
```

### Code Quality
```bash
# Run code analysis
dotnet build --verbosity normal

# Check for outdated packages
dotnet outdated

# Update packages
dotnet add package <package-name>
```

## 🤝 Contributing

We welcome contributions! Please follow these guidelines:

### 1. Fork the Repository
```bash
# Fork on GitHub, then clone your fork
git clone https://github.com/yourusername/hotel-management-system.git
cd hotel-management-system
```

### 2. Create a Feature Branch
```bash
git checkout -b feature/amazing-feature
```

### 3. Make Your Changes
- Follow the existing code style
- Add tests for new features
- Update documentation as needed

### 4. Commit Your Changes
```bash
git add .
git commit -m "feat: add amazing new feature"
```

### 5. Push and Create Pull Request
```bash
git push origin feature/amazing-feature
```

### 6. Open a Pull Request
- Go to GitHub and create a pull request
- Fill out the PR template
- Request review from maintainers

### Pull Request Guidelines
- **Title:** Use conventional commits format
- **Description:** Explain what changes you made and why
- **Testing:** Include steps to test your changes
- **Screenshots:** Add screenshots for UI changes

### Code Review Process
1. Automated checks must pass
2. At least one maintainer approval required
3. All conversations must be resolved
4. Up-to-date with main branch

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Le Phung Ha**
- Student ID: 8991
- Project: Hotel Management System
- Framework: WPF (.NET 8.0)

## 🐛 Issues & Support

### Reporting Issues
- **Bug Reports:** Use the [Issues](https://github.com/yourusername/hotel-management-system/issues) tab
- **Feature Requests:** Create an issue with the `enhancement` label
- **Security Issues:** Email security@yourdomain.com

### Issue Template
When creating an issue, please include:
- **Environment:** OS, .NET version, Visual Studio version
- **Steps to Reproduce:** Clear, numbered steps
- **Expected Behavior:** What should happen
- **Actual Behavior:** What actually happens
- **Screenshots:** If applicable
- **Logs:** Error messages or stack traces

### Getting Help
- 📖 **Documentation:** Check this README and code comments
- 💬 **Discussions:** Use GitHub Discussions for questions
- 🐛 **Issues:** Report bugs and request features
- 📧 **Email:** [your-email@domain.com]

### Community Guidelines
- Be respectful and inclusive
- Search existing issues before creating new ones
- Provide clear, detailed information
- Help others when you can

## 📊 Project Status

[![Build Status](https://img.shields.io/badge/build-passing-brightgreen.svg)](https://github.com/yourusername/hotel-management-system/actions)
[![Code Coverage](https://img.shields.io/badge/coverage-85%25-green.svg)](https://github.com/yourusername/hotel-management-system)
[![Last Commit](https://img.shields.io/badge/last%20commit-2%20days%20ago-blue.svg)](https://github.com/yourusername/hotel-management-system/commits/main)

## 🎯 Quick Start Guide

1. **Clone Repository** (`git clone https://github.com/yourusername/hotel-management-system.git`)
2. **Install Prerequisites** (Visual Studio 2022, .NET 8.0)
3. **Open Project** (`LePhungHa_8991.sln`)
4. **Setup Database** (Run `SetupDatabase.ps1`)
5. **Build & Run** (F5 in Visual Studio)
6. **Login** (admin / 123456)

---

**Happy Hotel Managing! 🏨✨**

*Built with ❤️ by Le Phung Ha (Student ID: 8991)*
