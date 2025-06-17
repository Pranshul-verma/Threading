using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer_Consumer
{
    internal class BlockingConcurrentQueue<T> : IProducerConsumerCollection<T>

    {
        ConcurrentQueue<T> Queue = new();
        private static readonly object Locker = new object();
        CancellationToken cancellationToken;
        public BlockingConcurrentQueue(int count, CancellationToken cancellationToken)
        {
            this.Count = count;
            this.cancellationToken = cancellationToken;
        }
        public int Count { get; set; }

        public bool IsSynchronized => throw new NotImplementedException();

        public object SyncRoot => throw new NotImplementedException();

        public void CopyTo(T[] array, int index)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public T[] ToArray()
        {
            throw new NotImplementedException();
        }

        public bool TryAdd(T item)
        {
            lock (Locker)
            {
                while (Queue.Count() >= Count)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    Monitor.Wait(Locker);
                }
                Queue.Enqueue(item);
                Monitor.PulseAll(Locker);
                return true;


                //throw new NotImplementedException();
            }
        }

        public bool TryTake([MaybeNullWhen(false)] out T item)
        {
            lock (Locker)
            {
                while (Queue.Count == 0)
                {
                    // if (_isAddingCompleted)
                    //  throw new InvalidOperationException("No more items to take, and adding is completed.");

                    cancellationToken.ThrowIfCancellationRequested();
                    Monitor.Wait(Locker);
                }
                Queue.TryDequeue(out item);
                Monitor.PulseAll(Locker);
                return true;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
