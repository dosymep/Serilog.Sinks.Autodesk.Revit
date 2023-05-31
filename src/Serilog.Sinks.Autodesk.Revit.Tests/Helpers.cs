using NUnit.Framework.Internal;

using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Autodesk.Revit.Tests; 

internal class Helpers {
    public static RevitOutputTest CreateRevitOutput() {
        return new RevitOutputTest();
    }
    
    public static ILogEventSink CreateJournalSink(IRevitOutput revitOutput) {
        var outputTemplate = GetDefaultOutputTemplate();
        var textFormatter = CreateTextFormatter(outputTemplate);
        return new AutodeskRevitSink(revitOutput, textFormatter);
    }

    public static LogEvent CreateLogEvent(string messageTemplate, params object[] propertyValues) {
        var log = new LoggerConfiguration().CreateLogger();
        // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
        if(!log.BindMessageTemplate(messageTemplate, propertyValues, out var template, out IEnumerable<LogEventProperty>? properties)) {
            throw new NUnitException("Template could not be bound.");
        }

        return new LogEvent(DateTimeOffset.Now, LogEventLevel.Information, null, template, properties);
    }

    public static string GetDefaultOutputTemplate() {
        return AutodeskRevitSinkLoggerConfigurationExtensions.DefaultOutputTemplate;
    }
    
    public static ITextFormatter CreateTextFormatter(string outputTemplate, IFormatProvider? formatProvider = null) {
        return AutodeskRevitSinkLoggerConfigurationExtensions.CreateTextFormatter(outputTemplate, formatProvider);
    }
}