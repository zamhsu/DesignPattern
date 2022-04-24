using System.IO.Compression;
using System.Text;

namespace StructuralPatterns.Decorator
{
    class Program
    {
        // 生產線輸送帶
        static void Main(string[] args)
        {
            // 裝飾器
            Console.WriteLine("Decorator (裝飾器)");

            // 目的：將原本舊有的類別新增新的職責。

            // 在某一物件動態加上功能。
            // 一層一層的將功能套上去，每一層執行的是不同的物件。
            // 「被裝飾者」和「裝飾功能」要繼承某一共同類別，並實做某一共同的方法。
            // 需要注意加入的順序就是執行順序

            // 如今天一條做壓模的生產線，初始設計會是輸送帶與壓模器這兩樣製作過程是一體成型的
            // 日後老闆說我們今天壓模的圖案要改，這時候工程師就要去製造新的一條輸送帶與壓模器
            // 此時就擁有兩條輸送帶。

            // 裝飾模式就如你所想，當初製作壓模器怎麼沒有把模具可以抽換的概念設計進去呢
            // 此時抽換模具的這一個功能就是裝飾模式，讓你可以抽換壓模的圖案
            // 不需要建造新的輸送帶，輸送帶就是你的程式主流程。

            string path = ".";

            // 壓模的原物料就是方塊的一塊鐵片
            byte[] writeBytes = Encoding.UTF8.GetBytes("123");

            // 實際產生賓士的Logo模具，FileProcess代表包裝的禮盒
            var writeDecorator = new Base64FileDecorator(new FileProcess());

            //實際產生BMW的Logo模具，FileProcess代表包裝的禮盒
            //var writeDecorator = new GZipFileDecorator(new FileProcess());

            // 對鐵片執行壓模動作
            writeDecorator.Write(path, writeBytes);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 檔案處理(包裝的禮盒)
    /// </summary>
    public interface IFilePrecess
    {
        byte[] Read(string path);

        void Write(string path, byte[] data);
    }

    public class FileProcess : IFilePrecess
    {
        public byte[] Read(string path)
        {
            throw new NotImplementedException();
        }

        public void Write(string path, byte[] data)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Decorator 的抽象(建立輸送帶)
    /// </summary>
    public abstract class FileDecorator : IFilePrecess
    {
        // 模具的抽屜軌道，讓你可以抽換的滑軌，等具體的模組進來
        protected readonly IFilePrecess _filePrecess;

        // 利用建構式，執行插入模具
        public FileDecorator(IFilePrecess filePrecess)
        {
            _filePrecess = filePrecess;
        }

        public abstract byte[] Read(string path);

        // 壓模器的動作
        public abstract void Write(string path, byte[] data);
    }

    /// <summary>
    /// Base64 裝飾器(實作賓圖案模具)
    /// </summary>
    public class Base64FileDecorator : FileDecorator
    {
        public Base64FileDecorator(IFilePrecess filePrecess) : base(filePrecess)
        { 
            
        }

        public override byte[] Read(string path)
        {
            byte[] base64Bytes = _filePrecess.Read(path);

            return Decode(base64Bytes);
        }
        
        private byte[] Decode(byte[] base64Bytes)
        {
            byte[] bytes = Convert.FromBase64String(Encoding.UTF8.GetString(base64Bytes));

            return bytes;
        }

        public override void Write(string path, byte[] data)
        {
            // 執行壓模Encode(代表賓士的圖案)
            byte[] base64Bytes = Encode(data);
            _filePrecess.Write(path, base64Bytes);
        }

        private byte[] Encode(byte[] data)
        {
            return Encoding.UTF8.GetBytes(Convert.ToBase64String(data));
        }
    }

    /// <summary>
    /// Gzip 壓縮裝飾器
    /// </summary>
    public class GZipFileDecorator : FileDecorator
    {
        public GZipFileDecorator(IFilePrecess fileProcess) : base(fileProcess)
        {

        }

        public override byte[] Read(string path)
        {
            byte[] compressedBytes = _filePrecess.Read(path);

            return Decompress(compressedBytes);
        }

        private byte[] Decompress(byte[] compressedBytes)
        {
            byte[] outputBytes = null;
            MemoryStream input = new MemoryStream(compressedBytes);
            outputBytes = Decompress(input).ToArray();

            return outputBytes;
        }

        private MemoryStream Decompress(Stream compressed)
        {
            MemoryStream decompressed = new MemoryStream();

            using (var zip = new GZipStream(compressed, CompressionMode.Decompress, true))
            {
                zip.CopyTo(decompressed);
            }

            decompressed.Seek(0, SeekOrigin.Begin);

            return decompressed;
        }

        public override void Write(string path, byte[] data)
        {
            // 執行壓模Compress(代表BMW的圖案)
            byte[] outputBytes = Compress(data);

            // 存檔(包裝進倉儲給他丟進倉庫)
            _filePrecess.Write(path, outputBytes);
        }

        private byte[] Compress(byte[] data)
        {
            byte[] outputBytes = null;
            MemoryStream input = new MemoryStream(data);
            outputBytes = Compress(input).ToArray();

            return outputBytes;
        }

        private MemoryStream Compress(Stream decompressed)
        {
            MemoryStream compressed = new MemoryStream();
            using (var zip = new GZipStream(compressed, CompressionLevel.Fastest, true))
            {
                decompressed.CopyTo(zip);
            }

            compressed.Seek(0, SeekOrigin.Begin);
            return compressed;
        }
    }
}