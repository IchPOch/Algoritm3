using System.Collections.Generic;
using System.Collections;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Reflection;
using SimpleAlgorithmsApp;
using System;


public class Person
{
    public string firstName;
    public string secondName;
    public string lastName;
    public DateTime birthday;


};
public class DoublyNode<T>
{
    public DoublyNode(T data)
    {
        Data = data;
    }
    public T Data { get; set; }
    public DoublyNode<T> Previous { get; set; }
    public DoublyNode<T> Next { get; set; }

}

namespace SimpleAlgorithmsApp
{
    public class DoublyLinkedList<T> where T: IComparable<T>   // двусвязный список
    {
        DoublyNode<T> head; // головной/первый элемент
        DoublyNode<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        // добавление элемента
        public void Add(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);

            if (head == null)
                head = node;
            else
            {
                tail.Next = node;
                node.Previous = tail;
            }
            tail = node;
            count++;
        }
        public void AddFirst(T data)
        {
            DoublyNode<T> node = new DoublyNode<T>(data);
            DoublyNode<T> temp = head;
            node.Next = temp;
            head = node;
            if (count == 0)
                tail = head;
            else
                temp.Previous = node;
            count++;
        }
       
        public bool RemoveAtt(int index)
        {
            var current = this.head;

            for (int i = 0; i < index; i++)
            {
                current = current.Next;
            }
            if (current != null)
            {
                // если узел не последний
                if (current.Next != null)
                {
                    current.Next.Previous = current.Previous;
                }
                else
                {
                    // если последний, переустанавливаем tail
                    tail = current.Previous;
                }

                // если узел не первый
                if (current.Previous != null)
                {
                    current.Previous.Next = current.Next;
                }
                else
                {
                    // если первый, переустанавливаем head
                    head = current.Next;
                }
                count--;
                return true;
            }
            return false;
        }

        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        public void Sort()
        {
            quickSort(head, tail);
        }
        private void quickSort(DoublyNode<T> lo, DoublyNode<T> hi)
        {
            if (lo != null && hi != null && lo != hi && lo != hi.Next)
            {
                DoublyNode<T> p = partition(lo, hi);
                quickSort(lo, p.Previous);
                quickSort(p.Next, hi);
            }
        }
        private DoublyNode<T> partition(DoublyNode<T> lo, DoublyNode<T> hi)
        {
            DoublyNode<T> pivot = hi;
            DoublyNode<T> i = lo, j = lo;
            while (j != hi)
            {
                if (j.Data.CompareTo(pivot.Data) < 0)
                {
                    swap(j, i);
                    i = i.Next;
                }
                j = j.Next;
            }
            swap(i, pivot);
            return i;
        }

        private void swap(DoublyNode<T> a, DoublyNode<T> b)
        {
            T temp = a.Data;
            a.Data = b.Data;
            b.Data = temp;
        }
        public void Shuff()
        {
            Random rnd = new Random();
            var iNode = this.tail;
            for(int i = this.count - 1; i>=0;i--)
            {
                int k = rnd.Next(0,i);
                var jNode = this.head;
                for(int j = 0; j < k; j++)
                {
                    jNode = jNode.Next;
                }
                swap(iNode, jNode);
                iNode = iNode.Previous;
            }
        }


        public IEnumerable<T> BackEnumerator()
        {
            DoublyNode<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public T Peek()
        {
            return head.Data;
        }
        public int Size()
        {
            return this.count;
        }
    }
}

class Program
{

    static void Main(string[] args)
    {
        Program a = new Program();
        a.FirstTest();
        Console.WriteLine();
        a.SecondTest();
        Console.WriteLine();
        //a.ThirdTest();
        //Console.WriteLine();
        a.FourthTest();
        Console.WriteLine();
        a.FifthTest();
        Console.WriteLine();
    }

    int GetRnd()
    {
        Random rand = new Random();
        return rand.Next(-1000,1000);
    }

    void FillListWithIntegers(DoublyLinkedList<int> list, int count = 1000)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(GetRnd());
        }
    }
    void FillListWithIntegers(List<int> list, int count = 1000)
    {
        for (int i = 0; i < count; i++)
        {
            list.Add(GetRnd());
        }
    }
    int GetAge(Person person)
    {
        System.DateTime dt = new System.DateTime(1970, 1, 1);
        int age = dt.Year + 1900 - person.birthday.Year;
        int month = (dt.Month + 1) - person.birthday.Month;

        if (month < 0 || (month == 0) && dt.Day < person.birthday.Day)
            age--;

        return age;
    }

    string GetRndString(int len) {
        Random rand = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string tmp_s;
        return new string(Enumerable.Repeat(chars, len)
                .Select(s => s[rand.Next(s.Length)]).ToArray());
    }

    //Person GeneratePerson()
    //{
    //    Random rand = new Random();
    //    string[] names = {"Misha", "Denis", "Dima", "Ganzales"};

    //    string[] secondNames = {"Mikhailovich", "Denisovich", "Dmitrivich"};

    //    string[] lastNames = {"Korkishko", "Eprintsev", "Razumovski"};

    //    DateTime[] dates = {new DateTime(1, 1, 1990), new DateTime(26, 07, 2002),
    //                 new DateTime(1, 3, 1975), new DateTime(6, 10, 1984),
    //                 new DateTime(23, 11, 2003), new DateTime(12, 10, 2001),
    //                 new DateTime(13, 03, 1978), new DateTime(23, 05, 1980),
    //                 new DateTime(1, 1, 2021), new DateTime(3, 2, 2005)};

    //    var person = new Person();
    //    person.firstName = names[rand.Next(0, 7)];
    //    person.secondName = secondNames[rand.Next(0, 6)];
    //    person.lastName = lastNames[rand.Next(0, 8)];
    //    person.birthday = dates[rand.Next(0, 9)];

    //    return person;
    //}

    public void FirstTest()
    {
        Console.WriteLine("TEST№1");


        var list = new DoublyLinkedList<int>();
        FillListWithIntegers(list);

        int max = list.Peek(),
                min = list.Peek(),
                sum = 0,
                size = list.Size();

        foreach (var t in list.BackEnumerator())
        {
            if (max < t)
                max = t;

            if (min > t)
                min = t;

            sum += t;
        }

        Console.WriteLine("Max = "+ max + " Min = " + min + " Sum = " + sum + " Avg = " + (double)sum/size);

        list.Clear();
    }
    void SecondTest()
    {
        Console.WriteLine("TEST№2");

        var list = new DoublyLinkedList<string>();

        for (int i = 0; i < 10; ++i)
        {
            list.Add(GetRndString(10));
        }

        Console.WriteLine("Size" + list.Size());
        foreach (var t in list.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();

        Console.WriteLine("Deleted №4 ");
        list.RemoveAtt(4);
        Console.WriteLine("Size " + list.Size());
        foreach (var t in list.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();
        while(!list.IsEmpty)
        {
            list.RemoveAtt(0);
        }

        Console.WriteLine("Size: " + list.Size());

    }
    //void ThirdTest()
    //{
    //    Console.WriteLine("TEST№3");

    //    var list = new DoublyLinkedList<Person>();

    //    for (int i = 0; i < 100; ++i)
    //    {
    //        list.Add(GeneratePerson());
    //    }

    //    var less20 = new DoublyLinkedList<Person>();
    //    var more30 = new DoublyLinkedList<Person>();

    //    foreach (var t in list.BackEnumerator())
    //    {
    //        int age = GetAge(t);
    //        if (age < 20)
    //            less20.Add(t);
    //        else if (age > 30)
    //            more30.Add(t);
    //    }

    //    Console.WriteLine("People under 20: " + less20.Size());
    //    Console.WriteLine("People over 30: " + more30.Size());

    //    int wrongPeople = 0;

    //    foreach (var t in list.BackEnumerator())
    //    {
    //        int age = GetAge(t);
    //        if (age >= 20)
    //            wrongPeople++;
    //    }

    //    foreach (var t in list.BackEnumerator())
    //    {
    //        int age = GetAge(t);
    //        if (age <= 30)
    //            wrongPeople++;
    //    }

    //    Console.WriteLine("Filter errors: " + wrongPeople);
    //}

    void FourthTest()
    {
        Console.WriteLine("TEST№4");

        var list1 = new List<int>();
        var list2 = new DoublyLinkedList<int>();
        FillListWithIntegers(list1, 20);

        list1.ForEach(t => list2.Add(t));
        list1.ForEach(t => Console.Write(t + " "));
        Console.WriteLine();
        foreach (var t in list2.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();

        list2.Sort();
        list1.Sort();

        list1.ForEach(t => Console.Write(t + " "));
        Console.WriteLine();

        foreach (var t in list2.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();
    }
    void FifthTest()
    {
        Console.WriteLine("TEST№5");

        var list = new DoublyLinkedList<int>();

        for (int i = 0; i < 10; ++i)
        {
            list.Add(i);
        }

        foreach (var t in list.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();

        list.Shuff();
        foreach (var t in list.BackEnumerator())
        {
            Console.Write(t + " ");
        }
        Console.WriteLine();
    }
}