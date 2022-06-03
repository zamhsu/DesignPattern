namespace StructuralPatterns.Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            // 橋接
            Console.WriteLine("Bridge (橋接)");

            // 目的：將抽象與實作解耦合，使得兩邊可以獨立的變化。
            // 橋接模式可以說是Design Pattern的根源，在所有的Pattern中都會有依賴抽象的概念
            // Bridge橋接模式就是實踐抽象的概念，在類別中留下位置等執行個體來注入。

            // 在這BMI的例子當中，BMI整體的計算過程與所需要的參數都是固定，唯一變動的部分只有評語的結果有所不同
            // 這時候就把評語的地方由外面注入，這樣就達到Bridge Pattern的概念，換言之就是讓他可以變動抽換。

            Human human = new Human(new ManComment());
            human.Weight = 60;
            human.Height = 1.75;

            Console.WriteLine("性別：男");
            Console.WriteLine("體重：" + human.Weight);
            Console.WriteLine("身高：" + human.Height);
            Console.WriteLine("BMI：" + human.BMI);
            Console.WriteLine("評價：" + human.GetResult());

            Console.ReadLine();
        }
    }

    public class Human
    {
        // 預留位置由外面來決定BMI結果判定
        private IBMIComment _comment;
        public Human(IBMIComment comment)
        {
            _comment = comment;
        }

        public double Weight { get; set; }
        public double Height { get; set; }

        private bool _calculated = false;
        private double _bmi = 0;
        public double BMI
        {  
            get
            {
                if (!_calculated)
                {
                    GetBMIValue();
                }

                return _bmi;
            }
        }

        public string Result
        {
            get
            {
                return GetResult();
            }
        }

        private void GetBMIValue()
        {
            _calculated = true;
            if (Weight > 0 && Height > 0)
            {
                _bmi = Weight / Math.Pow(Height, 2);
            }
            else
            {
                _bmi = -1;
            }
        }

        // 預留位置由外面來決定BMI結果判定
        public string GetResult()
        {
            return _comment.GetResult(BMI);
        }
    }

    public interface IBMIComment
    {
        string GetResult(double bmi);
    }

    public class ManComment : IBMIComment
    {
        public string GetResult(double bmi)
        {
            if (bmi > 25)
            {
                return "太胖";
            }
            else if (bmi < 20)
            {
                return "太瘦";
            }
            else
            {
                return "適中";
            }
        }
    }

    public class WomanComment : IBMIComment
    {
        public string GetResult(double bmi)
        {
            if (bmi > 22)
            {
                return "太胖";
            }
            else if (bmi < 18)
            {
                return "太瘦";
            }
            else
            {
                return "適中";
            }
        }
    }
}