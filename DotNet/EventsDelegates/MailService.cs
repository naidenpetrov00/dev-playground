class MailService
{
    public void OnVideoEncoded(object source, EncodedEventArgs args)
    {
        var delegateFunction = args.DelegateFunction!;
        delegateFunction();

        System.Console.WriteLine($"Email for {args.VideoTitle} Sended");
    }
}
