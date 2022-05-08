namespace CreationalPatterns.AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 簡單工廠
            Console.WriteLine("Abstract Factory (抽象工廠)");

            // 目的：為一個產品族提供了統一的建立介面。當需要這個產品族的某一系列的時候，
            // 可以從抽象工廠中選出相應的系列建立一個具體的工廠類。
            // 工廠方法的升級版
            // 工廠是抽象的，產品也是抽象的。
            // 抽象工廠(Abstract Factory)：提供建立產品的介面，包含建立產品的方法，可建立多個不同產品
            // 具體工廠(Concreate Factory)：主要實現抽象工廠的多個方法，具體實作產品

            // 例如：大部分汽車公司有轎車和休旅車(不同汽車類型，抽象工廠)
            // 然後各品牌依照這兩種車型生產汽車(不同廠牌，具體工廠)

            // Benz
            CarFactory benzCarFactory = new BenzFactory();
            Car benzSedan = benzCarFactory.CreateSedan();
            Car benzSuv = benzCarFactory.CreateSuv();
            Console.WriteLine($"Brand: {benzSedan.Name()}, HorsePower: {benzSedan.HorsePower()}");
            Console.WriteLine($"Brand: {benzSuv.Name()}, HorsePower: {benzSuv.HorsePower()}");

            Console.WriteLine("------------------------------------");

            // BMW
            CarFactory bmwCarFactory = new BmwFactory();
            Car bmwSedan = bmwCarFactory.CreateSedan();
            Car bmwSuv = bmwCarFactory.CreateSuv();
            Console.WriteLine($"Brand: {bmwSedan.Name()}, HorsePower: {bmwSedan.HorsePower()}");
            Console.WriteLine($"Brand: {bmwSuv.Name()}, HorsePower: {bmwSuv.HorsePower()}");

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 抽象產品(汽車)
    /// </summary>
    public abstract class Car
    {
        public abstract string Name();
        public abstract double HorsePower();
        public abstract string Description();
    }

    /// <summary>
    /// 具體產品(轎車)
    /// </summary>
    public class Sedan : Car
    {
        private string _name;
        private double _horsePower;
        private string _description;

        public Sedan(string name, double horsePower, string description)
        {
            _name = name;
            _horsePower = horsePower;
            _description = description;
        }

        public override string Name()
        {
            return _name;
        }

        public override double HorsePower()
        {
            return _horsePower;
        }

        public override string Description()
        {
            return _description;
        }
    }

    /// <summary>
    /// 具體產品(休旅車)
    /// </summary>
    public class Suv : Car
    {
        private string _name;
        private double _horsePower;
        private string _description;

        public Suv(string name, double horsePower, string description)
        {
            _name = name;
            _horsePower = horsePower;
            _description = description;
        }

        public override string Name()
        {
            return _name;
        }

        public override double HorsePower()
        {
            return _horsePower;
        }

        public override string Description()
        {
            return _description;
        }
    }

    /// <summary>
    /// 抽象工廠(汽車工廠)
    /// </summary>
    public abstract class CarFactory
    {
        public abstract Sedan CreateSedan();
        public abstract Suv CreateSuv();
    }

    /// <summary>
    /// 具體工廠(Benz)
    /// </summary>
    public class BenzFactory : CarFactory
    {
        private readonly string brand = "Benz";

        public override Sedan CreateSedan()
        {
            Sedan sedan = new Sedan($"{brand} C63 AMG", 480, "Good Benz Sedan");

            return sedan;
        }

        public override Suv CreateSuv()
        {
            Suv suv = new Suv($"{brand} GLE 63 AMG", 440, "Good Benz SUV");

            return suv;
        }
    }

    /// <summary>
    /// 具體工廠(BMW)
    /// </summary>
    public class BmwFactory : CarFactory
    {
        private readonly string brand = "BMW";

        public override Sedan CreateSedan()
        {
            Sedan sedan = new Sedan($"{brand} M5", 617, "Good BMW Sedan");

            return sedan;
        }

        public override Suv CreateSuv()
        {
            Suv suv = new Suv($"{brand} X6 M", 617, "Good BMW SUV");

            return suv;
        }
    }

}