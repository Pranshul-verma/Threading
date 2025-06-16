// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
AutoResetEvent evenAuto = new AutoResetEvent(false);
AutoResetEvent oddAuto = new AutoResetEvent(false);

Thread todd = new Thread(PrintOdd);
Thread teven = new Thread(PrintEven);
teven.Start();
todd.Start();
oddAuto.Set();
Console.Read();





void PrintEven()
{
	for (int i = 0; i < 10; i=i+2)
	{
        oddAuto.WaitOne();
		Console.WriteLine(i);
        evenAuto.Set();
	}
}

void PrintOdd()
{
    for (int i = 1; i < 10; i = i + 2)
    {
        evenAuto.WaitOne();
        Console.WriteLine(i);
        oddAuto.Set();
    }
}
