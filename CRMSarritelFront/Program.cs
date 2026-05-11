using System.Diagnostics;

var command = args.Length > 0 ? args[0] : "dev";

Console.WriteLine($"Starting React frontend: npm run {command}");

var processInfo = new ProcessStartInfo
{
    FileName = "npm",
    Arguments = $"run {command}",
    WorkingDirectory = Directory.GetCurrentDirectory(),
    UseShellExecute = false,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    CreateNoWindow = true
};

using var process = Process.Start(processInfo);
if (process == null)
{
    Console.WriteLine("Failed to start npm process");
    return 1;
}

process.OutputDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };
process.ErrorDataReceived += (s, e) => { if (e.Data != null) Console.WriteLine(e.Data); };

process.BeginOutputReadLine();
process.BeginErrorReadLine();

process.WaitForExit();
return process.ExitCode;