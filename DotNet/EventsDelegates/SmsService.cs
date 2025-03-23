class SmsService
{
    public void OnVideoEncoded(object source, EncodedEventArgs args)
    {
        var delegateFunction = args.DelegateFunction!;
        delegateFunction();

        System.Console.WriteLine($"SMS for {args.VideoTitle} Sended");
    }
}
