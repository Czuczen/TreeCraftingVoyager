using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using TreeCraftingVoyager.Server.Logging;
using TreeCraftingVoyager.Server.Models.ViewModels;

namespace TreeCraftingVoyager.Server.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/[controller]")]
[ApiController]
public class AppManagementController : ControllerBase
{
    private readonly IConfiguration _configuration;


    public AppManagementController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpGet("GetLogs")]
    public IActionResult Logs()
    {
        var ret = new LogsViewModel();

        var logFilePath = _configuration.GetSection("Logging:FileLogging").Get<FileLoggerConfiguration>().LogFilePath;
        var logDirectory = Path.GetDirectoryName(logFilePath);
        var fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFilePath);
        var fileExtension = Path.GetExtension(logFilePath);
        var logFiles = Directory.GetFiles(logDirectory, $"{fileNameWithoutExtension}*{fileExtension}")
            .OrderByDescending(System.IO.File.GetLastWriteTime).Reverse();

        foreach (var logFile in logFiles)
        {
            using var fs = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var sr = new StreamReader(fs, Encoding.Default);
            var buffer = new char[(int)sr.BaseStream.Length];
            sr.Read(buffer, 0, (int)sr.BaseStream.Length);

            var rawLogs = new string(buffer);
            var stringSeparators = new[] { "\r\n" };
            var lines = rawLogs.Split(stringSeparators, StringSplitOptions.None);

            var log = new List<string>();
            foreach (var line in lines)
            {
                var isNewLog = false;

                if (line.StartsWith("TRACE"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Trace.Add(log);
                }

                if (line.StartsWith("DEBUG"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Debug.Add(log);
                }

                if (line.StartsWith("INFORMATION"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Info.Add(log);
                }

                if (line.StartsWith("WARN"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Warn.Add(log);
                }

                if (line.StartsWith("ERROR"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Error.Add(log);
                }

                if (line.StartsWith("CRITICAL"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.Critical.Add(log);
                }

                if (line.StartsWith("NONE"))
                {
                    isNewLog = true;

                    log = new List<string> { line };
                    ret.None.Add(log);
                }

                if (!isNewLog)
                    log.Add(line);
            }
        }

        ret.Trace.Reverse();
        ret.Debug.Reverse();
        ret.Info.Reverse();
        ret.Warn.Reverse();
        ret.Error.Reverse();
        ret.Critical.Reverse();
        ret.None.Reverse();

        return Ok(ret);
    }
}