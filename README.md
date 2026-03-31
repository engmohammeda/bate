# SitMaster Pro for Civil 3D (2020-2025)

SitMaster Pro is a high-end, professional C# plugin for Autodesk Civil 3D, designed to provide a seamless and modern experience for importing survey points.

## 🚀 Professional Features
- **Architectural Excellence:** Built using the **MVVM (Model-View-ViewModel)** design pattern for ultimate scalability and maintainability.
- **Modern Autodesk UI:** Features a sleek dark-themed interface using WPF, meticulously styled to match the look and feel of modern Autodesk software.
- **Multi-Version Support:** Single codebase targeting both .NET Framework 4.8 and .NET 8, covering Civil 3D versions 2020 through 2025.
- **Robust Parsing Engine:** Dynamic support for multiple point formats (PNEZD, PENZD, PNEZ, ENZ) with intelligent error handling and validation.
- **Ribbon Integration:** Adds a professional "SITMASTER PRO" tab with high-quality branding and tooltips.

## 📁 Project Structure
- `SitMasterPro.bundle/`: The Autodesk App Bundle structure for distribution.
- `SitMasterPro/Logic/`: Core business logic and Civil 3D command implementations.
- `SitMasterPro/Models/`: Data models for point formats.
- `SitMasterPro/UI/`: Modern WPF Views and ViewModels (MVVM).
- `SitMasterPro/Resources/`: High-resolution icons and assets.

## 🛠️ Development & Build
1. **Requirements:** Visual Studio 2022+, Civil 3D 2020-2025 SDKs.
2. **Build Targets:** The project uses multi-targeting (`net48` and `net8.0-windows`).
3. **Execution:** Build the solution and deploy the generated DLLs to the `SitMasterPro.bundle` contents.

## 📦 Deployment
Copy the `SitMasterPro.bundle` folder to:
`%AppData%\Autodesk\ApplicationPlugins\`

## 📝 License
Professional license for SitMaster Pro.
