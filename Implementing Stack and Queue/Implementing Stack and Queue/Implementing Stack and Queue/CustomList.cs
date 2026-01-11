using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementing_Stack_and_Queue
{
    public class CustomList
    {
        private const int DefaualtCapacity = 4;
        public int[] _buffer;

        public CustomList()
            : this(DefaualtCapacity)
        {
        }

        public CustomList(int capacity)
        {
            if (capacity <= 0)
            {
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));
            }

            this._buffer = new int[capacity];
        }

        public int Count { get; private set; }

        // Indexer
        public int this[int index] {
            get
            {
                this.ValidateIndex(index);
                return this._buffer[index];
            }
            set
            {
                this.ValidateIndex(index);
                this._buffer[index] = value;
            } 
        }

        // Adds an element to the end of the list
        public void Add(int element)
        {
            if (this.Count == this._buffer.Length)
            {
                this.Resize();
            }

            this._buffer[this.Count] = element;
            this.Count++;
        }

        public void InsertAt(int index, int element)
        {
            if (index == this.Count)
            {
                this.Add(element);
                return;
            }

            this.ValidateIndex(index);

            if (this.Count == this._buffer.Length)
            {
                this.Resize();
            }

            for (int i = this.Count - 1; i >= index; i--)
            {
                this._buffer[i + 1] = this._buffer[i];
            }

            this._buffer[index] = element;
            this.Count++;
        }


        // Removes the element at the given index and returns it
        public int RemoveAt(int index)
        {
            this.ValidateIndex(index);

            int removedElement = this._buffer[index];

            for (int i = index; i < this.Count - 1; i++)
            {
                this._buffer[i] = this._buffer[i + 1];
            }

            this._buffer[--this.Count] = default;

            return removedElement;
        }


        // Returns true if the list contains the given element
        public bool Contains(int element)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this._buffer[i] == element)
                {
                    return true;
                }
            }
            return false;
        }

        // Swaps the elements at the given indices
        public void Swap(int firstIndex, int secondIndex)
        {
            this.ValidateIndex(firstIndex);
            this.ValidateIndex(secondIndex);

            if (firstIndex != secondIndex)
            {
                int swap = this._buffer[firstIndex];
                this._buffer[firstIndex] = this._buffer[secondIndex];
                this._buffer[secondIndex] = swap;
            }
        }

        // Doubles the size of the internal buffer
        private void Resize()
        {
            int[] newBuffer = new int[this._buffer.Length * 2];
            Array.Copy(this._buffer, newBuffer, this.Count);

            //for (int i = 0; i < this._buffer.Length; i++)
            //{
            //    newBuffer[i] = this._buffer[i];
            //}

            this._buffer = newBuffer;
        }

        // Validates that the given index is within the bounds of the list
        private void ValidateIndex(int index)
        {
            if (index < 0 || index >= this.Count)
            {
                throw new IndexOutOfRangeException($"Index must be in the range [0,{this.Count})");
            }
        }
    }
}
