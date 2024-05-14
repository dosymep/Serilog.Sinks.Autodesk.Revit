# Serilog.Sinks.Autodesk.Revit

[![JetBrains Rider](https://img.shields.io/badge/JetBrains-Rider-blue.svg)](https://www.jetbrains.com/rider)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE.md)
[![Revit 2017-2025](https://img.shields.io/badge/Revit-2017--2025-blue.svg)](https://www.autodesk.com/products/revit/overview)
[![main](https://github.com/dosymep/Serilog.Sinks.Autodesk.Revit/actions/workflows/main.yml/badge.svg)](https://github.com/dosymep/Serilog.Sinks.Autodesk.Revit/actions/workflows/main.yml)

Writes [Serilog](https://serilog.net) events to Autodesk Revit [Journal](https://www.revitapidocs.com/2022/97ec1eca-ab92-1cee-fdda-7bf3ce91c504.htm).

### Getting started

Install the [Serilog.Sinks.Autodesk.Revit](https://www.nuget.org/packages/Serilog.Sinks.Autodesk.Revit/) package from NuGet:

```powershell
Install-Package Serilog.Sinks.Autodesk.Revit
```

To configure the sink in C# code, call `WriteTo.RevitJournal()` during logger configuration:

```csharp
// IExternalCommand.Execute
public Result Execute(ExternalCommandData commandData, 
                      out string message, 
                      ElementSet elements) {
    UIApplication uiApplication = commandData.Application;
    var log = new LoggerConfiguration()
        .WriteTo.RevitJournal(uiApplication)
        .CreateLogger();
}
```

```csharp
// IExternalApplication.Execute
public Result OnStartup(UIControlledApplication application) {
    var log = new LoggerConfiguration()
        .WriteTo.RevitJournal(application)
        .CreateLogger();
}
```