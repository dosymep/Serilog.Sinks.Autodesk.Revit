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
    private readonly UIApplication _uiApplication;
    private readonly ITextFormatter _textFormatter;

    /// <summary>
    /// Creates Autodesk Revit Sink.
    /// </summary>
    /// <param name="uiApplication">Revit UIApplication.</param>
    /// <param name="textFormatter">Message format provider.</param>
    public AutodeskRevitSink(UIApplication uiApplication, ITextFormatter textFormatter) {
        _uiApplication = uiApplication ?? throw new ArgumentNullException(nameof(uiApplication));
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
        _uiApplication.Application.WriteJournalComment(outputString.ToString(), UseTimeStamps);
    }
}