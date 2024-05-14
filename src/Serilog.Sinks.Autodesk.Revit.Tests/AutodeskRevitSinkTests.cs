using NUnit.Framework.Internal;
using NUnit.Framework.Legacy;

using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;

namespace Serilog.Sinks.Autodesk.Revit.Tests;

public class AutodeskRevitSinkTests {
    [Test]
    public void WriteLogTest() {
        var revitOutput = Helpers.CreateRevitOutput();
        var useTimeStamps = false;
        var outputTemplate = Helpers.GetDefaultOutputTemplate();
        var restrictedToMinimumLevel = LevelAlias.Minimum;
        LoggingLevelSwitch? levelSwitch = null;
        IFormatProvider? formatProvider = null;

        var log = new LoggerConfiguration()
            .WriteTo.RevitJournal(
                revitOutput,
                useTimeStamps,
                outputTemplate,
                restrictedToMinimumLevel,
                levelSwitch,
                formatProvider)
            .CreateLogger();
        
        log.Information("Hello, world!");
        Assert.That(revitOutput.Comments[0], Does.Contain("Hello, world!"));
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void UseTimeStampTest(bool useTimeStamps) {
        var revitOutput = Helpers.CreateRevitOutput();
        var outputTemplate = Helpers.GetDefaultOutputTemplate();
        var restrictedToMinimumLevel = LevelAlias.Minimum;
        LoggingLevelSwitch? levelSwitch = null;
        IFormatProvider? formatProvider = null;

        var log = new LoggerConfiguration()
            .WriteTo.RevitJournal(
                revitOutput,
                useTimeStamps,
                outputTemplate,
                restrictedToMinimumLevel,
                levelSwitch,
                formatProvider)
            .CreateLogger();
        
        log.Information("Hello, world!");
        Assert.That(revitOutput.UseTimeStamp, Is.EqualTo(useTimeStamps));
    }
}