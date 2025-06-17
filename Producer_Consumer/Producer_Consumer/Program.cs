// See https://aka.ms/new-console-template for more information
using Producer_Consumer;

Console.WriteLine("Hello, World!");
int item = 0;
CancellationTokenSource cts = new CancellationTokenSource();
BlockingConcurrentQueue<int> queue = new(5,cts.Token);
Thread t1 = new Thread(()=>TakeItem(out item));
Thread t2 = new Thread(() => insertItem(5));
t1.Start();
Console.WriteLine("Taken item is " + item);
t2.Start();

Thread.Sleep(1000);

Console.ReadLine();
void TakeItem(out int item)
{
    queue.TryTake(out item);
    Console.WriteLine("Taken item is " + item);
}
void insertItem(int item)
{
    queue.TryAdd(item);
}

    




