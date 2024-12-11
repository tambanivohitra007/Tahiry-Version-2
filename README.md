# Tahiry - Worship Presentation Software

**Tahiry** is a modern desktop application designed to streamline and enhance worship presentations for churches. Initially developed for a local church in Madagascar, it is now available for anyone who wants to use or improve it. Inspired by [MyVideoPsalm](https://myvideopsalm.weebly.com/), Tahiry provides a seamless way to display hymnals, Bible passages, and other worship content.

---

## Features

- **Hymnals and Bible Integration**: Access a wide variety of hymnals and Bible versions for worship services.
- **Dual-Screen Support**: Control the content displayed on the main screen and a second display for the congregation.
- **Customizable Themes**: Switch between dynamic themes and palettes to match your church's style.
- **Offline Capability**: Fully functional without an internet connection.
- **DevExpress-Powered UI**: Provides a modern and professional user interface.
- **Registry-Based Configuration**: Saves settings in the Windows Registry for secure and persistent configuration.

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

- **Programming Language**: C#  
- **Framework**: .NET 8  
- **UI Framework**: DevExpress WinForms  
- **Database**: SQLite (automatically configured)

---

## Download

You can download Tahiry from [fihirana.rindra.org](https://fihirana.rindra.org).

---

## Installation

1. Visit [fihirana.rindra.org](https://fihirana.rindra.org) to download the latest version of Tahiry.
2. Run the installer and follow the on-screen instructions.
3. Launch the app and set up your preferences, including themes and data sources.

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
