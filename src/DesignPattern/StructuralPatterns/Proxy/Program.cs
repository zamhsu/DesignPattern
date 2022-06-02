using System.Text;

namespace StructuralPatterns.Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            // 代理
            Console.WriteLine("Proxy (代理)");

            // 目的：為其他物件提供一種代理，藉由這種方式控制對該物件的存取。

            string path = ".";

            // 使用者
            User user = new User();
            user.Name = "王曉明";
            // 設定檔案存取權限(多個enum成員)
            user.FileAuthority = (Authority.Read | Authority.Write);

            // 找代理做事
            FileProcessProxy writeProxy = new FileProcessProxy(user);
            writeProxy.Write(path, Encoding.UTF8.GetBytes("1234"));

            Console.ReadLine();
        }
    }

    public class User
    {
        public string? Name { get; set; }
        public Authority FileAuthority { get; set; }
    }

    public interface IFileProcess
    {
        byte[] Read(string path);

        void Write(string path, byte[] data);
    }

    /// <summary>
    /// FileProcess實作
    /// </summary>
    public class FileProcessAdapter : IFileProcess
    {
        public byte[] Read(string path)
        {
            byte[] result = new byte[0];
            return result;
        }

        public void Write(string path, byte[] data)
        {
            //throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 代理物件，協助判斷該使用者是否有權限後再做指定的事情
    /// </summary>
    public class FileProcessProxy : IFileProcess
    {
        private IFileProcess _subject;
        private User _user;

        public FileProcessProxy(User user)
        {
            _user = user;
            _subject = new FileProcessAdapter();
        }

        public byte[] Read(string path)
        {
            // 判斷是否符合條件
            if (CanRead())
            {
                // 通過
                return _subject.Read(path);
            }
            else
            {
                // 不通過
                throw new UnauthorizedAccessException("使用者沒有讀取權限");
            }
        }

        public void Write(string path, byte[] data)
        {
            // 判斷是否符合條件
            if (CanWrite())
            {
                // 通過
                _subject.Write(path, data);
            }
            else
            {
                // 不通過
                throw new UnauthorizedAccessException("使用者沒有寫入權限");
            }
        }

        private bool CanRead()
        {
            // 多個enum成員判斷是否有該值的寫法
            return (_user.FileAuthority & Authority.Read) == Authority.Read;
        }

        private bool CanWrite()
        {
            // 多個enum成員判斷是否有該值的寫法
            return (_user.FileAuthority & Authority.Write) == Authority.Write;
        }
    }

    public enum Authority
    {
        Read,
        Write
    }
}