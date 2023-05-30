// ReSharper disable once CheckNamespace
namespace Serilog.Sinks.Autodesk.Revit; 

internal interface IRevitOutput {
    void WriteJournalComment(string comment, bool timeStamp);
}