class AlarmSystem
{
    public delegate void AlarmHandler(string name);

    public event AlarmHandler FireAlarm;

    public void DetectFire()
    {
        Console.WriteLine("Fire detected!");
        FireAlarm?.Invoke("OPOP");
    }
}
