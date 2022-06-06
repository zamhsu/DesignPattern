namespace BehavioralPatterns.Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            // 觀察者
            Console.WriteLine("Observer (觀察者)");

            // 目的：定義一個一對多的物件依存關係，讓物件狀態一有變動，就自動通知其他的相依物件執行更新的動作。

            // Observer本身其實就是Event trigger，在C#當中有Event的方法可以讓多個Function去註冊。
            // 那假設不用C# Event如何靠類別與抽象實做出Event的功能。

            // ConcreteSubject是一個發布者(廣播)，ConcreateObserver(受眾)收聽了廣播
            // 當廣播有更新時會觸發Notify()，將受眾清單拿出來逐一通知

            ConcreateSubject s = new ConcreateSubject();
            ConcreateObserver o1 = new ConcreateObserver(s) { Name = "OB 1" };
            ConcreateObserver o2 = new ConcreateObserver(s) { Name = "OB 2" };

            s.SubjectState = "This is a dog";

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 觀察者的抽象
    /// </summary>
    public interface IObserver
    {
        void Update();
    }

    /// <summary>
    /// 實作觀察者
    /// </summary>
    public class ConcreateObserver : IObserver
    {
        public string? Name { get; set; }
        private ConcreateSubject _subject;
        public ConcreateObserver(ConcreateSubject subject)
        {
            _subject = subject;
            _subject.AddObserver(this);
        }

        public void Update()
        {
            Console.WriteLine($"{Name} Update Subject State : {_subject.SubjectState}");
        }
    }

    /// <summary>
    /// 被觀察者(通知者)的抽象
    /// </summary>
    public abstract class Subject
    {
        private List<IObserver> _observers;
        public Subject()
        {
            _observers = new List<IObserver>();
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        protected void Notify()
        {
            foreach (IObserver observer in _observers)
            {
                observer.Update();
            }
        }
    }

    /// <summary>
    /// 實作通知者
    /// </summary>
    public class ConcreateSubject : Subject
    {
        private string? _subjectState;
        public string? SubjectState
        {
            get { return _subjectState; }
            set
            {
                if (value != _subjectState)
                {
                    _subjectState = value;
                    Notify();
                }
            }
        }
    }
}