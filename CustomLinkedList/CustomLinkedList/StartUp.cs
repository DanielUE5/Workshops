namespace CustomLinkedList
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            DoublyLinkedList list = new DoublyLinkedList();

            list.AddFirst(10);
            list.AddLast(20);
            list.AddFirst(5);
            list.AddLast(25);

            Console.WriteLine("Current List:");
            list.ForEach(Console.WriteLine);

            Console.WriteLine($"Removed First: {list.RemoveFirst()}");
            Console.WriteLine($"Removed Last: {list.RemoveLast()}");

            Console.WriteLine("List After Removals:");
            list.ForEach(Console.WriteLine);

            int[] array = list.ToArray();
            Console.WriteLine("Array Representation: " + string.Join(", ", array));
        }
    }
}
