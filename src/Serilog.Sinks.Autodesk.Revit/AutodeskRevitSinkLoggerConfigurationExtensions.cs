﻿using Autodesk.Revit.UI;

using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;
using Serilog.Sinks.Autodesk.Revit;

namespace Serilog;

/// <summary>
/// Extends <see cref="LoggerSinkConfiguration"/>.
/// </summary>
public static class AutodeskRevitSinkLoggerConfigurationExtensions {
    /// <summary>
    /// Default output template. From Serilog.Sinks.RollingFile.
    /// </summary>
    public const string DefaultOutputTemplate =
        "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}";

    /// <summary>
    /// Write log events to Autodesk Revit
    /// <a href="https://www.revitapidocs.com/2017.1/97ec1eca-ab92-1cee-fdda-7bf3ce91c504.htm">Journal</a>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="uiApplication">Revit UIApplication.</param>
    /// <param name="useTimeStamps">Use time stamps in revit journal.</param>
    /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
    /// <param name="restrictedToMinimumLevel"></param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level to be changed at runtime.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or <see langword="null" />.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">When <paramref name="sinkConfiguration"/> or <paramref name="uiApplication"/> is <see langword="null" />.</exception>
    public static LoggerConfiguration RevitJournal(this LoggerSinkConfiguration sinkConfiguration,
        UIApplication uiApplication,
        bool useTimeStamps = false,
        string outputTemplate = DefaultOutputTemplate,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null,
        IFormatProvider? formatProvider = null) {
        if(sinkConfiguration == null) {
            throw new ArgumentNullException(nameof(sinkConfiguration));
        }

        if(uiApplication == null) {
            throw new ArgumentNullException(nameof(uiApplication));
        }

        var revitOutput = new RevitApplicationOutput(uiApplication.Application);
        return sinkConfiguration.RevitJournal(
            revitOutput,
            useTimeStamps,
            outputTemplate,
            restrictedToMinimumLevel,
            levelSwitch,
            formatProvider);
    }

    /// <summary>
    /// Write log events to Autodesk Revit
    /// <a href="https://www.revitapidocs.com/2017.1/97ec1eca-ab92-1cee-fdda-7bf3ce91c504.htm">Journal</a>.
    /// </summary>
    /// <param name="sinkConfiguration">Logger sink configuration.</param>
    /// <param name="uiControlledApplication">Revit UIControlledApplication.</param>
    /// <param name="useTimeStamps">Use time stamps in revit journal.</param>
    /// <param name="outputTemplate">A message template describing the format used to write to the sink.</param>
    /// <param name="restrictedToMinimumLevel"></param>
    /// <param name="levelSwitch">A switch allowing the pass-through minimum level to be changed at runtime.</param>
    /// <param name="formatProvider">Supplies culture-specific formatting information, or <see langword="null" />.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException">When <paramref name="sinkConfiguration"/> or <paramref name="uiControlledApplication"/> is <see langword="null" />.</exception>
    public static LoggerConfiguration RevitJournal(this LoggerSinkConfiguration sinkConfiguration,
        UIControlledApplication uiControlledApplication,
        bool useTimeStamps = false,
        string outputTemplate = DefaultOutputTemplate,
        LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
        LoggingLevelSwitch? levelSwitch = null,
        IFormatProvider? formatProvider = null) {
        if(sinkConfiguration == null) {
            throw new ArgumentNullException(nameof(sinkConfiguration));
        }

        if(uiControlledApplication == null) {
            throw new ArgumentNullException(nameof(uiControlledApplication));
        }

        var revitOutput = new RevitControlledApplicationOutput(uiControlledApplication.ControlledApplication);
        return sinkConfiguration.RevitJournal(
            revitOutput,
            useTimeStamps,
            outputTemplate,
            restrictedToMinimumLevel,
            levelSwitch,
            formatProvider);
    }

    internal static LoggerConfiguration RevitJournal(this LoggerSinkConfiguration sinkConfiguration,
        IRevitOutput revitOutput,
        bool useTimeStamps,
        string outputTemplate,
        LogEventLevel restrictedToMinimumLevel,
        LoggingLevelSwitch? levelSwitch,
        IFormatProvider? formatProvider) {
        var textFormatter = CreateTextFormatter(outputTemplate, formatProvider);
        var autodeskRevitSink = new AutodeskRevitSink(revitOutput, textFormatter) {UseTimeStamps = useTimeStamps};
        return sinkConfiguration.Sink(autodeskRevitSink, restrictedToMinimumLevel, levelSwitch);
    }

    internal static ITextFormatter CreateTextFormatter(string outputTemplate, IFormatProvider? formatProvider) {
        return new MessageTemplateTextFormatter(outputTemplate, formatProvider);
    }
}