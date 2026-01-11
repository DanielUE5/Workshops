using System;

namespace Implementing_Stack_and_Queue
{
    public class CustomQueue
    {
        private const int DefaultCapacity = 4;

        private int[] _buffer;
        private int _head;
        private int _tail;

        public CustomQueue() : this(DefaultCapacity) { }

        public CustomQueue(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

            _buffer = new int[capacity];
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        public int Count { get; private set; }

        public void Enqueue(int element)
        {
            if (Count == _buffer.Length)
                Resize();

            _buffer[_tail] = element;
            _tail = (_tail + 1) % _buffer.Length;
            Count++;
        }

        public int Dequeue()
        {
            ValidateNotEmpty();

            int removed = _buffer[_head];
            _buffer[_head] = default;

            _head = (_head + 1) % _buffer.Length;
            Count--;

            return removed;
        }

        public int Peek()
        {
            ValidateNotEmpty();
            return _buffer[_head];
        }

        public void Clear()
        {
            _buffer = new int[DefaultCapacity];
            _head = 0;
            _tail = 0;
            Count = 0;
        }

        public void ForEach(Action<int> action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            for (int i = 0; i < Count; i++)
            {
                int index = (_head + i) % _buffer.Length;
                action(_buffer[index]);
            }
        }

        private void Resize()
        {
            int[] newBuffer = new int[_buffer.Length * 2];

            for (int i = 0; i < Count; i++)
                newBuffer[i] = _buffer[(_head + i) % _buffer.Length];

            _buffer = newBuffer;
            _head = 0;
            _tail = Count;
        }

        private void ValidateNotEmpty()
        {
            if (Count == 0)
                throw new InvalidOperationException("The queue is empty.");
        }
    }
}
