namespace BehavioralPatterns.TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            // 範本方法
            Console.WriteLine("Template Method (範本方法)");

            // 目的：先安排好程式固定的框架流程步驟，步驟細節交由子類別實作。

            // 開放Predicate由繼承者自行實作判斷標準，其餘流程都固定

            List<int> list = new List<int>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(i);
            }

            PredicateInt p = new PredicateInt(list);

            var result = p.DoWhere();

            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }
    }

    public abstract class CustomClass<T>
    {
        private IEnumerable<T> _source;

        public CustomClass(IEnumerable<T> source)
        {
            _source = source;
        }

        public IEnumerable<T> DoWhere()
        {
            // 固定流程
            foreach (var item in _source)
            {
                // 自行實作處
                if (Predicate(item))
                {
                    yield return item;
                }
            }
        }

        // 待繼承者實作
        protected abstract bool Predicate(T item);
    }

    public class PredicateInt : CustomClass<int>
    {
        public PredicateInt(IEnumerable<int> source) : base(source)
        { }

        // 自行實作的內容
        protected override bool Predicate(int item)
        {
            return item > 5;
        }
    }
}