using Autodesk.Revit.ApplicationServices;

namespace Serilog.Sinks.Autodesk.Revit; 

internal class RevitControlledApplicationOutput : IRevitOutput {
    private readonly ControlledApplication _controlledApplication;

    public RevitControlledApplicationOutput(ControlledApplication controlledApplication) {
        _controlledApplication = controlledApplication;
    }

    public void WriteJournalComment(string comment, bool timeStamp) {
        _controlledApplication.WriteJournalComment(comment, timeStamp);
    }
}