using NUnit.Framework.Internal;

using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Autodesk.Revit.Tests;

public class AutodeskRevitSinkTests {
    [Test]
    public void WriteLogTest() {
        var revitOutput = Helpers.CreateRevitOutput();
        var journalSink = Helpers.CreateJournalSink(revitOutput);
        var logEvent = Helpers.CreateLogEvent("Hello, world!");
        
        journalSink.Emit(logEvent);
        
        StringAssert.Contains("Hello, world!", revitOutput.Comments[0]);
    }
}