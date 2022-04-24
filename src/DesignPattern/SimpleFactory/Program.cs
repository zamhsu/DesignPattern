namespace CreationalPatterns.SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 簡單工廠
            Console.WriteLine("Simple Factory (簡單工廠)");

            // 目的：統一集中管理產生Instance的地方。
            // 後續如果需要再加入其他Commucation，只要加入新的方式後
            // 修改CommucationType, Factory的switch case即可

            ICommucation commucation = CommucationFactory.GetInstance(CommucationType.Tcp);

            commucation.Connect("127.0.01:8888");

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 定義工廠模式
    /// </summary>
    public enum CommucationType
    {
        Tcp,
        Udp
    }

    /// <summary>
    /// 使用簡單分支運算
    /// </summary>
    public class CommucationFactory
    {
        public static ICommucation GetInstance(CommucationType type)
        {
            switch (type)
            {
                case CommucationType.Tcp:
                    return new TcpCommunication();
                case CommucationType.Udp:
                    return new UdpCommunication();
                default:
                    throw new ArgumentOutOfRangeException();
            }
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