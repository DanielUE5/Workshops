using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomLinkedList
{
    public class DoublyLinkedList
    {
        private ListNode head;
        private ListNode tail;
        private int count;

        public int Count => count;

        public void AddFirst(int element)
        {
            var newNode = new ListNode(element);
            if (count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Next = head;
                head.Previous = newNode;
                head = newNode;
            }
            count++;
        }

        public void AddLast(int element)
        {
            var newNode = new ListNode(element);
            if (count == 0)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                newNode.Previous = tail;
                tail.Next = newNode;
                tail = newNode;
            }
            count++;
        }

        public int RemoveFirst()
        {
            if (count == 0) throw new InvalidOperationException("The list is empty.");
            int value = head.Value;
            head = head.Next;
            if (head != null) head.Previous = null;
            else tail = null; // List became empty
            count--;
            return value;
        }

        public int RemoveLast()
        {
            if (count == 0) throw new InvalidOperationException("The list is empty.");
            int value = tail.Value;
            tail = tail.Previous;
            if (tail != null) tail.Next = null;
            else head = null; // List became empty
            count--;
            return value;
        }

        public void ForEach(Action<int> action)
        {
            var currentNode = head;
            while (currentNode != null)
            {
                action(currentNode.Value);
                currentNode = currentNode.Next;
            }
        }

        public int[] ToArray()
        {
            int[] array = new int[count];
            var currentNode = head;
            for (int i = 0; i < count; i++)
            {
                array[i] = currentNode.Value;
                currentNode = currentNode.Next;
            }
            return array;
        }
    }
}
