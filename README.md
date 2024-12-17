# Tahiry - Worship Presentation Software

**Tahiry** is a modern desktop application designed to streamline and enhance worship presentations for churches. Initially developed for a local church in Madagascar, it is now available for anyone who wants to use or improve it. Inspired by [MyVideoPsalm](https://myvideopsalm.weebly.com/), Tahiry provides a seamless way to display hymnals, Bible passages, and other worship content.

---

## Features

- **Hymnals and Bible Integration**: Access a wide variety of hymnals and Bible versions for worship services.
- **Dual-Screen Support**: Control the content displayed on the main screen and a second display for the congregation.
- **Offline Capability**: Fully functional without an internet connection.
- **DevExpress-Powered UI**: Provides a modern and professional user interface.
- **Registry-Based Configuration**: Saves settings in the Windows Registry for secure and persistent configuration.
- **Cross-Reference Songs**: A feature that allows users to relate songs that share the same melody but have different content. This feature is especially useful for identifying songs with a common tune across multiple hymnals or languages

---

## Songs Included

Tahiry comes preloaded with the following hymnals and song collections:

- **Fihirana Advantista Malagasy**  
- **Hymne et Louange**  
- **Seventh-day Adventist Hymnal**  
- **Chant Jeunesse**  
- **J’aime l’Eternel (JEM)**  
- **Fihirana Fanampiny (FFPM)**  
- **Donnez-lui Gloire**

---

## Bibles Included

These Bible versions are integrated into Tahiry:

- **Baiboly Malagasy**  
- **Baiboly DIEM**  
- **Bible Louis Second**  
- **Bible du Semeur**  
- **New International Version**  
- **King James Version**

---

## Technology Stack

- **Programming Language**: C#, Javascript (for animations)
- **Framework**: .NET 8  
- **UI Framework**: DevExpress WinForms 23.2.10
- **Database**: SQLite (automatically configured)
- WebView 2

---

## Download

You can download Tahiry from [fihirana.rindra.org](https://fihirana.rindra.org).

---

## Installation

1. Visit [fihirana.rindra.org](https://fihirana.rindra.org) to download the latest version of Tahiry.
2. **Important Note**: Your browser may block the download because the certificate is not recognized, and the publisher is marked as unknown. This is purely a certificate issue and does not pose any harm to your computer.  
   - To proceed, manually accept the download by clicking **"Keep"** or **"Allow"** in your browser's warning message.
3. Once the download is complete, run the installer and follow the on-screen instructions.
4. Launch the app and set up your preferences, including themes and data sources.

---

## Usage

1. Open the app and navigate to the settings menu to configure your data source and display preferences.
2. Select songs or Bible passages for your worship presentation.
3. Use the dual-screen feature to project content for the congregation while managing it from the main display.
4. Customize themes for a visually appealing experience.

---

## Key Features in Code

### Registry-Based Configuration
Tahiry uses the Windows Registry to securely save application settings:
```csharp
using RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@"Software\\RindraSoftware\\Tahiry");
registryKey?.SetValue("Provider", "SQLite");
registryKey?.SetValue("DataSource", $"{SpecialFolderPath}\\Rindrasoftware\\Tahiry\\tahiry.db");
```
---

## Installing DevExpress

To use **Tahiry**, you need to install **DevExpress** (WinForms components). Follow these steps:

### Step 1: Install DevExpress
1. **Download DevExpress**:
   - Visit the [DevExpress website](https://www.devexpress.com/) and log in to your account. If you don't have an account, create one.
   - Navigate to the **Download Manager** section and download the latest version of DevExpress WinForms.

2. **Install DevExpress**:
   - Run the installer and follow the instructions to complete the installation.

3. **Activate DevExpress**:
   - During the installation, you will be prompted to activate your license using your DevExpress credentials.

---

### Step 2: Add DevExpress to Your Project
1. **Configure Your Project**:
   - Open your **Tahiry** project in Visual Studio.
   - Go to `Tools > NuGet Package Manager > Manage NuGet Packages for Solution`.

2. **Install DevExpress NuGet Packages**:
   - Search for the following packages and install them:
     - `DevExpress.WinForms`
     - `DevExpress.Data`
     - `DevExpress.Utils`

   These packages include the necessary libraries for the application.

3. **Update References**:
   - Ensure that the DevExpress libraries are correctly referenced in your project:
     - In the `Solution Explorer`, right-click your project and select **Add Reference**.
     - Browse to the DevExpress installation directory (default: `C:\Program Files (x86)\DevExpress 23.x\Components\Bin\Framework`) and add the required DLLs, such as:
       - `DevExpress.XtraEditors.dll`
       - `DevExpress.XtraBars.dll`
       - `DevExpress.XtraGrid.dll`

---

### Step 3: Verify Installation
1. **Build the Project**:
   - Rebuild your solution in Visual Studio by pressing `Ctrl+Shift+B`.
   - Ensure that there are no errors related to missing DevExpress libraries.

2. **Test the Application**:
   - Run the application to verify that all DevExpress components are working correctly.

---

### Additional Notes
- **DevExpress License**:
  - Ensure that you have an active DevExpress license. DevExpress components require proper licensing for production use.
- **Trial Version**:
  - If you're using a trial version, there may be limitations or watermarks on the components.


# License

## MIT License

Copyright (c) 2018-2024 
Licensed under CC BY-NC 4.0 
**Rindra Razafinjatovo**

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

**THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES, OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.**

