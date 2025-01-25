using System.Diagnostics;
using System.Threading.Tasks;

namespace EventsDelegates;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // var alarmSystem = new AlarmSystem();
        // alarmSystem.FireAlarm += CallFireDepartment;
        // alarmSystem.FireAlarm += OpenEmergencyExits;

        // alarmSystem.DetectFire();


        var video = new Video { Title = "Video_1" };
        var video2 = new Video { Title = "Video_2" };
        var videoEncoder = new VideoEncoder();
        var mailService = new MailService();
        var smsService = new SmsService();

        videoEncoder.VideoEncoded += mailService.OnVideoEncoded;
        videoEncoder.VideoEncoded += smsService.OnVideoEncoded;

        var stopwatch = Stopwatch.StartNew();

        var task1 = videoEncoder.EncodeAsync(video);
        var task2 = videoEncoder.EncodeAsync(video2);

        await Task.WhenAll(task1, task2);

        // await videoEncoder.EncodeAsync(video);
        // await videoEncoder.EncodeAsync(video2);

        stopwatch.Stop();
        Console.WriteLine($"Total time taken: {stopwatch.Elapsed.TotalSeconds}");
    }

    public static void CallFireDepartment(string name)
    {
        Console.WriteLine(name);
        Console.WriteLine("Calling the fire department...");
    }

    public static void OpenEmergencyExits(string name)
    {
        Console.WriteLine(name);
        Console.WriteLine("Opening emergency exits...");
    }
}
