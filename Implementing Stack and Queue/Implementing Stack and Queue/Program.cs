using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace Implementing_Stack_and_Queue
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("This is a list and its functionalities:");
            CustomList list = new CustomList();
            for (int i = 0; i < 10; i++)
            {
                list.Add(Random.Shared.Next(100));
            }

            for (int i = 0; i < 10; i++)
            {
                int randomIndex = Random.Shared.Next(list.Count + 1);
                int randomElement = Random.Shared.Next(100);

                Console.WriteLine($"Insert element {randomElement} at index #{randomIndex}");
                list.InsertAt(randomIndex, randomElement);
                PrintList(list);
            }

            for (int i = 0; i < 20; i++)
            {
                int randomIndex = Random.Shared.Next(list.Count);
                int removedElement = list.RemoveAt(randomIndex);

                Console.WriteLine($"Removing element {removedElement} at index #{randomIndex}");
                PrintList(list);
            }
            Console.WriteLine();

            Console.WriteLine("This is a stack and its functionalities:");
            CustomStack stack = new CustomStack();

            for (int i = 0; i < 10; i++)
            {
                stack.Push(Random.Shared.Next(100));
            }

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Peek: {stack.Peek()}; Pop: {stack.Pop()}");
            }

            Console.WriteLine();
            Console.WriteLine("This is a queue and its functionalities:");
            CustomQueue queue = new CustomQueue();

            for (int i = 0; i < 10; i++)
            {
                int value = Random.Shared.Next(100);
                queue.Enqueue(value);

                Console.Write($"After Enqueue {value}: ");
                PrintQueue(queue);
            }

            while (queue.Count > 0)
            {
                int removed = queue.Dequeue();

                Console.Write($"After Dequeue {removed}: ");
                PrintQueue(queue);
            }
        }

        private static void PrintList(CustomList list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("(empty list)");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                if (i > 0) Console.Write(", ");
                Console.Write(list[i]);
            }
            Console.WriteLine();
        }

        private static void PrintQueue(CustomQueue queue)
        {
            if (queue.Count == 0)
            {
                Console.WriteLine("(empty queue)");
                return;
            }

            int printed = 0;

            queue.ForEach(x =>
            {
                if (printed > 0)
                    Console.Write(", ");

                Console.Write(x);
                printed++;
            });

            Console.WriteLine();
        }
    }
}
