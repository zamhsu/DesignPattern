namespace StructuralPatterns.Fecade
{
    class Program
    {
        static void Main(string[] args)
        {
            // 門面/外觀模式
            Console.WriteLine("Fecade (門面/外觀模式)");

            // 目的：讓User透過高層的"夾層"來呼叫子系統，
            // 降低User對於子系統的依賴，也更輕鬆的操作複雜的子系統。

            // 夾層的實作可以是Class也可以是Interface，主要目的必須隔離User直接操作子系統。
            // 使用理由：隔離User對於子系統的依賴，避免發生日後子系統修改導致User也必須跟著修改(連動性問題)。

            byte[] bytes = { 3, 2, 5, 4 };
            int i = IntConverter.ByteToInt(bytes);

            Console.WriteLine(i);

            string text = "123456";
            int i2 = IntConverter.StringToInt(text);

            Console.WriteLine(i2);

            Console.ReadLine();
        }
    }

    public class IntConverter
    {
        public static int ByteToInt(byte[] b)
        {
            // 現在的實作
            return b[0] + b[1] * 256 + b[2] * 256 * 256 + b[3] * 256 * 256 * 256;

            // 原本的實作
            //return BitConverter.ToInt32(b, 0);
        }

        public static int StringToInt(string text)
        {
            _ = int.TryParse(text, out int result);

            return result;
        }
    }
}