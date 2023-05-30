using System.Text;

using Autodesk.Revit.UI;

using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

// ReSharper disable once CheckNamespace
namespace Serilog.Sinks.Autodesk.Revit;

/// <summary>
/// Autodesk Revit Sink
/// </summary>
internal sealed class AutodeskRevitSink : ILogEventSink {
    private readonly IRevitOutput _revitOutput;
    private readonly ITextFormatter _textFormatter;

    /// <summary>
    /// Creates Autodesk Revit Sink.
    /// </summary>
    /// <param name="revitOutput">Revit UIApplication.</param>
    /// <param name="textFormatter">Message format provider.</param>
    public AutodeskRevitSink(IRevitOutput revitOutput, ITextFormatter textFormatter) {
        _revitOutput = revitOutput ?? throw new ArgumentNullException(nameof(revitOutput));
        _textFormatter = textFormatter ?? throw new ArgumentNullException(nameof(textFormatter));
    }

    /// <summary>
    /// Use time stamps in revit journal.
    /// </summary>
    public bool UseTimeStamps { get; set; }

    /// <inheritdoc />
    public void Emit(LogEvent logEvent) {
        StringBuilder outputString = new();
        using StringWriter outputStream = new(outputString);
        _textFormatter.Format(logEvent, outputStream);
        _revitOutput.WriteJournalComment(outputString.ToString(), UseTimeStamps);
    }
}