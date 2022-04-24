namespace BehavioralPatterns.Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 迭代器
            Console.WriteLine("Iterator (迭代器)");

            // 目的：提供一種方法訪問一個容器物件內的各個元素，而又不需要暴露該物件內部細節。

            IListCollection list = new ConcreteList();
            IIterator iterator = list.GetIterator();

            while (iterator.MoveNext())
            {
                string item = (string)iterator.GetCurrent();
                Console.WriteLine(item);
                iterator.Next();
            }

            Console.ReadLine();
        }
    }

    public interface IListCollection
    {
        IIterator GetIterator();
    }

    public interface IIterator
    {
        bool MoveNext();
        Object GetCurrent();
        void Next();
        void Reset();
    }

    public class ConcreteList : IListCollection
    {
        private readonly string[] _collection;

        public ConcreteList()
        {
            _collection = new string[] { "A", "B", "C", "D", "E" };
        }

        public IIterator GetIterator()
        {
            return new ConcreteIterator(this);
        }

        public int Length
        {
            get { return _collection.Length; }
        }

        public string GetElement(int index)
        {
            return _collection[index];
        }
    }

    public class ConcreteIterator :IIterator
    {
        private ConcreteList _list;
        private int _index;

        public ConcreteIterator(ConcreteList list)
        {
            _list = list;
            _index = 0;
        }

        public bool MoveNext()
        {
            if (_index < _list.Length)
            {
                return true;
            }

            return false;
        }

        public Object GetCurrent()
        {
            return _list.GetElement(_index);
        }

        public void Reset()
        {
            _index = 0;
        }

        public void Next()
        {
            if (_index < _list.Length)
            {
                _index++;
            }
        }
    }
}