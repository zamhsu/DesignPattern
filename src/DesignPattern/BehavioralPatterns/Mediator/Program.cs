namespace BehavioralPatterns.Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            // 中介者
            Console.WriteLine("Mediator (中介者)");

            // 目的：定義一個中介物件，將另外一群物件的互動方式封裝起來。
            // 中介者使得這一群物件之間可以不需要直接認識對方，也不需要直接互動，降低他們的耦合關係，並且能夠獨立地改變他們之間的互動操作。

            // 類別間的依賴從網狀結構(一對多)改為星狀結構(一對一)
            // 中介者會膨脹得很大，而且邏輯複雜

            // Mediator為中介者，專門註冊為一對一
            // 看似和Observer一樣，但是Observer是全部人都收到，Mediator是指定的人才收到

            Mediator mediator = new Mediator();
            Seller seller = new Seller(mediator) { Name = "賣藥的" };
            Buyer buyer = new Buyer(mediator) { Name = "缺藥的" };

            mediator.Seller = seller;
            mediator.Buyer = buyer;

            seller.Send("要不要買");
            buyer.Send("好啊");

            Console.ReadLine();
        }
    }

    public abstract class AbstractMediator
    {
        public abstract void Send(string message, Role role);
    }

    public class Mediator : AbstractMediator
    {
        private Seller seller = null!;
        private Buyer buyer = null!;

        public Seller Seller
        {
            set { seller = value; }
        }

        public Buyer Buyer
        {
            set { buyer = value; }
        }

        public override void Send(string message, Role role)
        {
            if (role == seller)
            {
                buyer.Notify(message);
            }
            else
            {
                seller.Notify(message);
            }
        }
    }

    public abstract class Role
    {
        protected Mediator _mediator;
        public Role(Mediator mediator)
        {
            _mediator = mediator;
        }
    }

    public class Seller : Role
    {
        public Seller(Mediator mediator) : base(mediator)
        {
        }

        public string? Name { get; set; }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"Seller {Name} 接收到了訊息：{message}");
        }
    }

    public class Buyer : Role
    {
        public Buyer(Mediator mediator) : base(mediator)
        {
        }

        public string? Name { get; set; }

        public void Send(string message)
        {
            _mediator.Send(message, this);
        }

        public void Notify(string message)
        {
            Console.WriteLine($"Buyer {Name} 接收到了訊息：{message}");
        }
    }
}