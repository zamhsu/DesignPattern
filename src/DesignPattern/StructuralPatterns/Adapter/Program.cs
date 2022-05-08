using System.Net.Sockets;
using System.Text;

namespace CreationalPatterns.SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 轉接器
            Console.WriteLine("Adapter (轉接器)");

            // 目的：將兩個以上不同的介面統一成一個介面，讓User更輕鬆維護。
            // 實際情境中，可能出現架構上已經設計兩套Library在專案中，
            // 突然需求需要第三個Library，這時候Adapter模式下只需要將共用介面引用至第三個Library中開發完交付給User，
            // User只有修改new出第三套Libraray所產生的Instance就大功告成了。

            // 這樣可以達到User在操作Send和Receive的邏輯部分完全切割，
            // 不會因為修改不同通訊方法導致需要修改Send和Reveive的程式碼段落

            // 與Fecade都是利用夾層的概念來切割User和子系統之間的連動性
            // 需要先共同制定interface的規格出來後才開始開發

            // Lib_1
            ICommucation tunnel = new UdpCommunication();
            // Lib_2
            //ICommucation tunnel = new TcpCommunication();
            // Lib_3
            //ICommucation tunnel = new MqttCommunication();

            tunnel.Connect("127.0.01:8888", 3254);
            byte[] sendBuffer = GetSendBuffer();
            tunnel.Send(sendBuffer);

            byte[] receiveBuffer = tunnel.Receive();
            tunnel.Disconnect();
            Console.WriteLine(GetReceiveString(receiveBuffer));

            Console.ReadLine();
        }

        private static byte[] GetSendBuffer()
        {
            string data = "Hi tunnel!";
            return Encoding.UTF8.GetBytes(data);
        }

        private static string GetReceiveString(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }
    }

    public interface ICommucation
    {
        bool Connect(string target, int port);

        void Disconnect();
        void Send(byte[] buffer);

        byte[] Receive();
    }

    public class UdpCommunication : ICommucation, IDisposable
    {
        private Socket client;

        public bool Connect(string target, int port)
        {
            throw new NotImplementedException();
        }

        public void Disconnect()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public byte[] Receive()
        {
            throw new NotImplementedException();
        }

        public void Send(byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}