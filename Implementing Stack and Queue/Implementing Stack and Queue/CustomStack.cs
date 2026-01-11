using System;

namespace Implementing_Stack_and_Queue
{
    public class CustomStack
    {
        private const int DefaultCapacity = 4;
        private int[] _buffer;

        public CustomStack() 
            : this(DefaultCapacity) 
        { 
        }

        public CustomStack(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            }

            this._buffer = new int[capacity];
        }

        public int Count { get; private set; }

        // Adds an element to the top of the stack
        public void Push(int element)
        {
            if (this.Count == this._buffer.Length) this.Resize();
            this._buffer[this.Count++] = element;
        }

        // Removes and returns the top element
        public int Pop()
        {
            this.ValidateNotEmpty();

            int removedElement = this._buffer[this.Count - 1];
            this._buffer[--this.Count] = default;

            return removedElement;
        }

        // Returns the top element without removing it
        public int Peek()
        {
            ValidateNotEmpty();
            return this._buffer[Count - 1];
        }

        // Executes the given action for each element in the stack
        public void ForEach(Action<int> action)
        {
            for (int i = 0; i < this.Count; i++)
            {
                action(this._buffer[i]);
            }
        }

        // Doubles the size of the internal buffer
        private void Resize()
        {
            int[] newBuffer = new int[_buffer.Length * 2];
            Array.Copy(this._buffer, newBuffer, this.Count);

            this._buffer = newBuffer;
        }

        // Validates that the stack is not empty
        private void ValidateNotEmpty()
        {
            if (this.Count == 0)
                throw new InvalidOperationException("The stack is empty.");
        }
    }
}
