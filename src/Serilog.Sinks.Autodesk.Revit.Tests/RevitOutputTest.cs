namespace Serilog.Sinks.Autodesk.Revit.Tests;

public class RevitOutputTest : IRevitOutput {
    public bool? UseTimeStamp = null;
    public List<string> Comments = new();
    
    public void WriteJournalComment(string comment, bool timeStamp) {
        Comments.Add(comment);
        UseTimeStamp = timeStamp;
    }
}