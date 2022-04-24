using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CreationalPatterns.Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            // 單例(Lazy Initialization)
            Console.WriteLine("Singleton (單例)");

            // 目的:在程式運作中，永遠只維持一份Instance在系統中，讓所有User取得相同一個Instance。

            // 運用情境1：在開發Socket TCP的系統中
            // Socket Server的連線數量對公司而言是最直接的成本因素
            // 所以會盡量保持Client端系統永遠只保持一條連線。

            // 運用情境2：對於DB讀取一份資料屬於長期不變動的資料
            // 一般系統都會做Cache，減少對DB的負擔。

            // 範例為Lazy Initialization(第一次使用時才產生實例)

            SocketClass.SocketObject.Connect("127.0.0.1", 3254);
            byte[] sendBuffer = GetSendBuffer();
            SocketClass.SocketObject.Send(sendBuffer);

            byte[] receiveBuffer = SocketClass.SocketObject.Receive();
            SocketClass.SocketObject.Disconnect();

            Console.WriteLine(GetReceiveString(receiveBuffer));

            Console.ReadLine();
        }

        private static byte[] GetSendBuffer()
        {
            string data = "Hi Singleton";
            return Encoding.UTF8.GetBytes(data);
        }

        private static string GetReceiveString(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }
    }

    public class SocketClass
    {
        private Socket socket;

        private SocketClass()
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Send(byte[] bytes)
        {
            socket.Send(bytes);
        }

        public bool Connect(string target, int port)
        {
            IPAddress ip;

            if (IPAddress.TryParse(target, out ip))
            {
                socket.Connect(ip, port);
            }

            return socket.Connected;
        }

        public byte[] Receive()
        {
            byte[] buffer = new byte[1024];
            int receiveSize = socket.Receive(buffer);
            Array.Resize(ref buffer, receiveSize);

            return buffer;
        }

        public void Disconnect()
        {
            socket.Close();
        }

        private static SocketClass _SocketObject;
        private static object _syncRoot = new object();

        public static SocketClass SocketObject
        {
            // Lazy Initialization
            get
            {
                if (_SocketObject == null)
                {
                    // 避免多執行緒可能會產生兩個以上的實例,所以lock
                    lock (_syncRoot)
                    {
                        // double locking
                        if (_SocketObject == null)
                        {
                            GetSingleton();
                        }
                    }
                }

                return _SocketObject;
            }
        }

        private static void GetSingleton()
        {
            _SocketObject = new SocketClass();
        }
    }
}