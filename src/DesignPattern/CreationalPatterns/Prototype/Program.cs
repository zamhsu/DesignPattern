namespace CreationalPatterns.Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // 原型
            Console.WriteLine("Prototype (原型)");

            // 目的：物件複製。
            // 物件複製分成兩種，淺層複製與深層複製。

            // 淺層複製(Shallow Copy)：複製物件本身的實值型別(int、string、double....)，參考型別『沒有』複製。
            // 複製後的物件內的參考型別欄位會與原先物件共用相同物件。

            // 深層複製(Deep Copy)：完整的複製整個物件，參考型別與實值型都完整複製，與原先物件完全分離。

            // 複製物件的方式，可以藉由Net Framework的ICloneable Interface來繼承，實做Clone()的方法。
            // 實做clone方法內使用MemberwiseClone()方法來達到淺層複製。

            Class1 class1a = new Class1();
            class1a.X = 0;

            Class1 class1b = (Class1)class1a.Clone();

            Console.WriteLine("淺層複製(Shallow Copy)");
            Console.WriteLine("(修改前)class1a.X：" + class1a.X);
            class1a.X = 1000;
            Console.WriteLine("修改class1a.X為：" + class1a.X);
            Console.WriteLine("(修改前的class1a.X)class1b.X：" + class1b.X);

            Class2 class2a = new Class2();
            class2a.Data = new Class1() { X = 0 };

            Class2 class2b = (Class2)class2a.Clone();

            Console.WriteLine("深層複製(Deep Copy)");
            Console.WriteLine("(修改前)class2a.Data.X：" + class2a.Data.X);
            class2a.Data.X = 1000;
            Console.WriteLine("修改class2a.Data.X為：" + class2a.Data.X);
            Console.WriteLine("(修改前的class2a.Data.X)class2b.Data.X：" + class2b.Data.X);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 淺層複製
    /// </summary>
    public class Class1 : ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    /// <summary>
    /// 深層複製
    /// </summary>
    public class Class2 : ICloneable
    {
        public Class1 Data { get; set; }
        public string? Id { get; set; }

        public Class2()
        {
            Data = new Class1();
        }

        public object Clone()
        {
            // 只有複製實質型別
            var result = (Class2)this.MemberwiseClone();

            if (this.Data != null)
            {
                // 深層複製
                result.Data = (Class1)this.Data.Clone();
            }

            return result;
        }
    }
}