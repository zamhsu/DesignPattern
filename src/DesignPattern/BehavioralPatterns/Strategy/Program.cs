namespace BehavioralPatterns.Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            // 策略模式
            Console.WriteLine("Strategy (策略模式)");

            // 目的：定義一組演算法，將每個算法都封裝起來，並且讓他們之間可以互換。
            // 看起來和簡單工廠很像，但是著重的點不同
            // 簡單工廠注重生成物件
            // 策略模式關注行為的封裝

            string todayBean = "衣索比亞耶加雪菲";

            CoffeeBrewType brewType = CoffeeBrewType.PourOver;
            ICoffeStrategy coffeStrategy;

            switch (brewType)
            {
                case CoffeeBrewType.PourOver:
                    coffeStrategy = new PourOver();
                    break;
                case CoffeeBrewType.ColdBrew:
                    coffeStrategy = new ColdBrew();
                    break;
                case CoffeeBrewType.Espresso:
                    coffeStrategy = new Espresso();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            string coffee = coffeStrategy.Brew(todayBean);

            Console.WriteLine(coffee);

            Console.ReadLine();
        }
    }

    public interface ICoffeStrategy
    {
        string Brew(string coffeeBean);
    }

    public class PourOver : ICoffeStrategy
    {
        public string Brew(string coffeeBean)
        {
            return $"使用 {coffeeBean} 的咖啡豆製作手沖咖啡";
        }
    }

    public class ColdBrew : ICoffeStrategy
    {
        public string Brew(string coffeeBean)
        {
            return $"使用 {coffeeBean} 的咖啡豆製作冷萃咖啡";
        }
    }

    public class Espresso : ICoffeStrategy
    {
        public string Brew(string coffeeBean)
        {
            return $"使用 {coffeeBean} 的咖啡豆製作濃縮咖啡";
        }
    }

    public enum CoffeeBrewType
    {
        PourOver,
        ColdBrew,
        Espresso
    }
}