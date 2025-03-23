using System.Runtime.CompilerServices;

class VideoEncoder
{
    public delegate void DelegateFunction();

    DelegateFunction delegateFunction = () =>
    {
        System.Console.WriteLine("delegate runing");
    };

    public delegate void EncodedHandler(object source, EncodedEventArgs args);

    public event EncodedHandler? VideoEncoded;

    public async Task EncodeAsync(Video video)
    {
        System.Console.WriteLine("Encoding");
        await Task.Delay(3000);

        OnVideoEncoder(video.Title);
    }

    protected virtual void OnVideoEncoder(string videoTitle)
    {
        var args = new EncodedEventArgs();
        args.DelegateFunction = delegateFunction;
        args.VideoTitle = videoTitle;
        VideoEncoded!(this, args);
    }
}

class EncodedEventArgs : EventArgs
{
    public VideoEncoder.DelegateFunction? DelegateFunction { get; set; }

    public string VideoTitle { get; set; }
}
