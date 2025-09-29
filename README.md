# HW4 - ASP.NET Web Forms University Database Management System

A comprehensive university database management system built with ASP.NET Web Forms, featuring user authentication and CRUD operations for managing students, courses, faculty, and other academic data.

## 🏗️ Project Structure

```
HW4/
├── Default.aspx              # Main application page with database operations
├── Default.aspx.cs           # Code-behind for main page
├── LogIn.aspx               # User authentication page
├── LogIn.aspx.cs            # Authentication logic
├── Web.config               # ASP.NET configuration
├── HW4.slnx                 # Solution file
├── start-server.ps1         # PowerShell script to start development server
├── App_Code/
│   └── myDatabaseConnection.cs  # Database connection and operations
├── App_Data/                # Data files for the application
│   ├── course.dat
│   ├── student.dat
│   ├── faculty.dat
│   ├── major.dat
│   ├── section.dat
│   ├── enrollment.dat
│   └── grade.dat
└── .vscode/                 # VS Code configuration
    ├── tasks.json           # Build and run tasks
    └── launch.json          # Debug configuration
```

## 🚀 Quick Start

### Prerequisites

- **Visual Studio Code** with C# extension installed
- **IIS Express** (usually installed with Visual Studio)
- **.NET Framework** (target framework defined in Web.config)
- **Windows** operating system

### Setup Instructions

1. **Clone or download** this repository
2. **Open the project** in VS Code:
   ```powershell
   cd "C:\Users\deast\Desktop\ITWP2300\HW4"
   code .
   ```

3. **Start the development server** using any of these methods:

#### Method 1: Using VS Code Tasks (Recommended)
- Press `Ctrl+Shift+P` → Type "Tasks: Run Task" → Select "Start IIS Express Server"
- Or press `Ctrl+Shift+P` → Type "Tasks: Run Build Task" → Select "iisexpress"

#### Method 2: Using PowerShell Script
```powershell
.\start-server.ps1
```

#### Method 3: Manual IIS Express Command
```powershell
& "C:\Program Files\IIS Express\iisexpress.exe" /path:"C:\Users\deast\Desktop\ITWP2300\HW4" /port:52061
```

4. **Open your browser** and navigate to:
   ```
   http://localhost:52061/LogIn.aspx
   ```

## 🛠️ Development Setup for VS Code

### Installing Required Extensions

1. **C# Extension** (Microsoft):
   ```
   ext install ms-dotnettools.csharp
   ```

2. **Optional but helpful extensions**:
   - ASP.NET Core Snippets
   - C# XML Documentation Comments
   - Auto Rename Tag
   - Bracket Pair Colorizer

### VS Code Configuration Files

The project includes pre-configured VS Code settings:

#### `.vscode/tasks.json`
Contains tasks for:
- **IIS Express Server**: Background task to run the development server
- **Build Task**: MSBuild compilation task
- **Stop Server**: Task to terminate the development server

#### `.vscode/launch.json`
Provides debugging configuration:
- **Launch with IIS Express**: Automatically starts server and opens browser
- **Attach to Process**: For debugging running applications

### Key VS Code Commands

| Action | Command | Shortcut |
|--------|---------|----------|
| Start Server | Tasks: Run Task → "Start IIS Express Server" | `Ctrl+Shift+P` |
| Build Project | Tasks: Run Build Task | `Ctrl+Shift+B` |
| Start Debugging | Debug: Start Debugging | `F5` |
| Open Command Palette | View: Show Command Palette | `Ctrl+Shift+P` |

## 🔧 Build Process

### Automatic Build Configuration

The project uses MSBuild for compilation with the following configuration:

1. **Solution File**: `HW4.slnx` defines the project structure
2. **Web.config**: Contains compilation and runtime settings
3. **Build Task**: Configured in `tasks.json` to use MSBuild with proper parameters

### Manual Build Steps

If you need to build manually:

```powershell
# Build the solution
msbuild HW4.slnx /property:GenerateFullPaths=true /consoleloggerparameters:NoSummary

# Or use the VS Code task
# Ctrl+Shift+P → Tasks: Run Build Task
```

### Development Workflow

1. **Code Changes**: Edit .aspx, .aspx.cs, or .cs files
2. **Auto-Build**: IIS Express automatically recompiles on file changes
3. **Browser Refresh**: Refresh browser to see changes
4. **Debugging**: Use F5 to start with debugging, or attach to running process

## 📖 Application Features

### Authentication System
- **Login Page**: Email and Date of Birth validation
- **Cookie Management**: 30-day authentication cookies
- **Security**: Matching email/DOB confirmation required

### Database Operations
- **CRUD Operations**: Create, Read, Update, Delete for all entities
- **Entity Management**: Students, Courses, Faculty, Majors, Sections, Enrollments, Grades
- **Data Import**: Load data from .dat files in App_Data folder
- **SQL Server Integration**: Remote database connection support

### User Interface
- **Responsive Design**: Clean, functional web forms
- **Error Handling**: User-friendly error messages
- **Navigation**: Intuitive workflow between operations

## 🗃️ Database Configuration

### Connection String
Located in `App_Code/myDatabaseConnection.cs`:
```csharp
Server: SQL5025.myWindowsHosting.com
Database: chabotr564_HW4
User Prefix: chabotr564_
```

### Data Files Format
All .dat files in App_Data use pipe-delimited format (|) for easy parsing.

## 🐛 Troubleshooting

### Common Issues and Solutions

**Server Won't Start**
- Verify IIS Express is installed: `Test-Path "C:\Program Files\IIS Express\iisexpress.exe"`
- Check if port 52061 is available: `netstat -an | findstr :52061`
- Run PowerShell as Administrator if needed

**C# Extension Not Working**
- Reload VS Code: `Ctrl+Shift+P` → "Developer: Reload Window"
- Check OmniSharp output: `View` → `Output` → Select "OmniSharp Log"
- Restart OmniSharp: `Ctrl+Shift+P` → "OmniSharp: Restart OmniSharp"

**Build Errors**
- Ensure .NET Framework is installed
- Check Web.config for correct compilation settings
- Verify all references are available

**Authentication Issues**
- Clear browser cookies for localhost
- Check date format (MM/DD/YYYY required)
- Verify App_Data folder permissions

### Debug Mode

To run in debug mode:
1. Press `F5` or use "Debug: Start Debugging"
2. Set breakpoints in .cs files
3. Step through code execution
4. Inspect variables and call stack

## 📚 Additional Resources

### ASP.NET Web Forms Documentation
- [Microsoft ASP.NET Web Forms Guide](https://docs.microsoft.com/en-us/aspnet/web-forms/)
- [IIS Express Documentation](https://docs.microsoft.com/en-us/iis/extensions/introduction-to-iis-express/)

### VS Code for .NET Development
- [VS Code C# Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
- [Debugging .NET with VS Code](https://code.visualstudio.com/docs/languages/csharp)

---

## 📄 License

This is a university assignment project. Please follow your institution's academic integrity guidelines.

## 🤝 Contributing

This is an educational project. For improvements or questions, please consult with your instructor or teaching assistant.