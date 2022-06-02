using StructuralPatterns.Decorator;
using System.Text;

namespace CreationalPatterns.Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            // 建造者
            Console.WriteLine("Builder (建造者)");

            // 目的：將一個複雜物件的建構與它的表示分離，使得同樣的建構過程可以創建出不同的配置或佈局。

            // 買了兩盒的鋼彈模型，打開第一步就是先看『組裝順序說明書』與『零件說明圖規格書』與『實體零件』
            // 在Main主程式中FileProcessDirector扮演著組裝順序的說明書，事實上可能第一盒鋼彈與第二盒鋼彈的組裝順序不同，我們會寫新FileProcessDirector類別來去符合第二盒鋼彈的組裝順序。
            // 在new FileProcessDirector完畢後，就是已經得到一台實體的鋼彈了。

            string path = "1.txt";
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            string newLine = Environment.NewLine;
            string expected = "測試 文字" + newLine + "GZip" + newLine + "加密" + newLine + "Base64" + newLine + "檔案存取";
            byte[] writeBytes = Encoding.UTF8.GetBytes(expected);

            // DESBuilder代表第一盒鋼彈
            FileProcessBuilder writeBuilder = new DESBuilder();
            
            // XXXBuilder代表第二盒鋼彈
            //FileProcessBuilder writeBuilder = new XXXBuilder();
            
            //FileProcessDirector代表零件組裝順序說明書
            FileProcessDirector writeDirector = new FileProcessDirector(writeBuilder);

            IFileProcess writeProcess = writeBuilder.GetFileProcess();

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 建造規格
    /// </summary>
    public abstract class FileProcessBuilder
    {
        // 零件說明圖與規格書，說明盒內具有哪些零件各幾個
        
        protected IFileProcess _fileProcess;

        protected FileProcessBuilder()
        {
            _fileProcess = new FileProcess();
        }

        public abstract void BuildEncode();
        public abstract void BuildCrypto();
        public abstract void BuildCompress();

        public IFileProcess GetFileProcess()
        {
            return _fileProcess;
        }
    }

    /// <summary>
    /// 實際建造的動作
    /// </summary>
    public class DESBuilder : FileProcessBuilder
    {
        // 實體零件的產生動作，就是把它從零件架上剪下來的動作

        public override void BuildCompress()
        {
            _fileProcess = new GZipFileDecorator(_fileProcess);
        }

        public override void BuildCrypto()
        {
            _fileProcess = new DESCryptoFileDecorator(_fileProcess);
        }

        public override void BuildEncode()
        {
            _fileProcess = new Base64FileDecorator(_fileProcess);
        }
    }

    /// <summary>
    /// 建造的流程
    /// </summary>
    public class FileProcessDirector
    {
        // 零件組裝順序說明書

        public FileProcessDirector(FileProcessBuilder builder)
        {
            // 執行組裝
            builder.BuildEncode();   // 步驟1
            builder.BuildCompress(); // 步驟2
            builder.BuildCrypto();   // 步驟3
        }
    }
}