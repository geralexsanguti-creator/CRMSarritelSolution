using System.Diagnostics;

Console.WriteLine("Starting CRMSarritel Frontend...");

var startInfo = new ProcessStartInfo
{
    FileName = "npm",
    Arguments = "run dev",
    WorkingDirectory = Directory.GetCurrentDirectory(),
    UseShellExecute = true
};

Process.Start(startInfo);
Console.WriteLine("Frontend started at http://localhost:5173");
Console.ReadLine();