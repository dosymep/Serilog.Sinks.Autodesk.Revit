using Autodesk.Revit.ApplicationServices;

// ReSharper disable once CheckNamespace
namespace Serilog.Sinks.Autodesk.Revit;

internal class RevitApplicationOutput : IRevitOutput {
    private readonly Application _application;

    public RevitApplicationOutput(Application application) {
        _application = application;
    }

    public void WriteJournalComment(string comment, bool timeStamp) {
        _application.WriteJournalComment(comment, timeStamp);
    }
}