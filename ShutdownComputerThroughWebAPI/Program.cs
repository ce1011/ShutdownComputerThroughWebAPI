using System.Diagnostics;

var builder = WebApplication.CreateSlimBuilder(args);

// Configure Kestrel to listen on all network interfaces
builder.WebHost.ConfigureKestrel(options =>
{
    options.Listen(System.Net.IPAddress.Any, 12000); // Port 5282 as per your example
});

var app = builder.Build();

app.MapGet("/shutdown", () =>
{
    // Start shutdown with 10-second delay
    ProcessStartInfo shutdownInfo = new ProcessStartInfo
    {
        FileName = "shutdown",
        Arguments = "/s /t 10", // /s = shutdown, /t 10 = 10-second delay
        CreateNoWindow = true,
        UseShellExecute = false
    };
    Process.Start(shutdownInfo);

    // Open a command prompt window with instructions
    ProcessStartInfo cmdInfo = new ProcessStartInfo
    {
        FileName = "cmd.exe",
        Arguments = "/k echo Shutdown initiated. Type 'shutdown /a' to cancel within 10 seconds.",
        CreateNoWindow = false,
        UseShellExecute = true
    };
    Process.Start(cmdInfo);
    
    return "Success";
});

app.Run();
