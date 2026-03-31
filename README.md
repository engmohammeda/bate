# SitMaster Pro for Civil 3D (2020-2025)

SitMaster Pro is a powerful and modern C# plugin for Autodesk Civil 3D, designed to simplify the process of importing COGO points from text and CSV files.

## Features
- **Multi-Version Support:** Compatible with Civil 3D versions 2020 through 2025.
- **Modern WPF Interface:** A sleek and intuitive user interface built with WPF.
- **Ribbon Integration:** Adds a dedicated "SITMASTER PRO" tab to the Civil 3D ribbon.
- **Flexible Import:** Supports various point formats (PNEZD, PENZD, etc.).

## Project Structure
- `SitMasterPro.bundle/`: The Autodesk App Bundle structure for distribution.
- `SitMasterPro/`: The C# source code, WPF UI, and business logic.
- `SitMasterPro.csproj`: Multi-targeting project file (net48 for 2020-2024 and net8.0-windows for 2025).

## How to Build
1. Open the project in Visual Studio 2022 or newer.
2. Ensure you have the necessary Civil 3D SDKs or NuGet references (see .csproj).
3. Build the solution for both `net48` and `net8.0-windows` targets.
4. Copy the compiled DLLs to their respective folders in `SitMasterPro.bundle/Contents/`:
   - `net48` DLL -> `SitMasterPro.bundle/Contents/2020/`
   - `net8.0` DLL -> `SitMasterPro.bundle/Contents/2025/`

## How to Install
1. Copy the `SitMasterPro.bundle` folder to your Autodesk ApplicationPlugins folder:
   - `%AppData%\Autodesk\ApplicationPlugins\`
2. Restart Civil 3D. The "SITMASTER PRO" tab will appear automatically.

## Requirements
- Windows 10/11 x64
- Autodesk Civil 3D (2020, 2021, 2022, 2023, 2024, or 2025)
- .NET Framework 4.8 (for 2020-2024)
- .NET 8.0 (for 2025)
