# Serilog.Sinks.Autodesk.Revit

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