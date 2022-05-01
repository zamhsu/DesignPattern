namespace CreationalPatterns.FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            // 工廠方法
            Console.WriteLine("Factory Method (工廠方法)");

            // 目的：定義一個介面用於建立物件，但是讓子類決定初始化哪個類。
            // 工廠方法把一個類的初始化下放到子類。

            // 符合OCP
            // 如果要修改，只要修改User端即可。

            // 運用情境：建立單一產品

            IFactoryMethod factoryMethod = new TcpCommunicationFactory();
            ICommucation commucation = factoryMethod.Create();

            commucation.Connect("127.0.01:8888");

            Console.ReadLine();
        }
    }

    public interface IFactoryMethod
    {
        ICommucation Create();
    }

    public class TcpCommunicationFactory : IFactoryMethod
    {
        public ICommucation Create()
        {
            return new TcpCommunication();
        }
    }

    public class UdpCommunicationFactory : IFactoryMethod
    {
        public ICommucation Create()
        {
            return new UdpCommunication();
        }
    }

    public interface ICommucation
    {
        void Connect(string ip);
    }

    public class TcpCommunication : ICommucation
    {
        public void Connect(string ip)
        {
            Console.WriteLine($"使用TCP連接：{ip}");
        }
    }

    public class UdpCommunication : ICommucation
    {
        public void Connect(string ip)
        {
            Console.WriteLine($"使用UDP連接：{ip}");
        }
    }
}