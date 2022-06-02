namespace BehavioralPatterns.ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            // 責任鏈
            Console.WriteLine("Chain of Responsibility (責任鏈)");

            // 目的：透過物件的方式去取代if的撰寫方式。
            // 運用情境：需求是必須經過層層把關而且必定是依序檢查不得跳躍的情境下就是適合的情境。

            // 依照ChainContext中GetCheckers裡一直new的順序檢查，從第一個開始到最後一個。
            // Length -> Head -> FirstDate -> SecondDate

            var checker = ChainContext.GetCheckers();
            List<CheckResult> results = new List<CheckResult>();

            foreach (var item in FakeDataSource.Data)
            {
                results.Add(checker.Check(item));
            }

            foreach (var item in results)
            {
                Console.WriteLine($"Source : {item.Source}, Result : {item.Result}");
            }

            Console.ReadLine();
        }
    }

    public static class FakeDataSource
    {
        public static List<string> Data
        {
            get
            {
                List<string> result = new List<string>()
                {
                    "001test0000012022050120220502",
                    "002test0000022022050120220502 Too Long",
                    "003test00000320220501NotADate"
                };

                return result;
            }
        }
    }

    public class CheckResult
    {
        public string Source { get; set; } = null!;
        public bool Result { get; set; }
    }

    public abstract class FormatChecker
    {
        protected FormatChecker _successor;
        
        protected abstract bool InternalCheck(string source);

        public CheckResult Check(string source)
        {
            // 檢查結果為true，若沒有後續，代表檢查結束，如果有後續則繼續
            // 檢查結果為false，跳出不再檢查

            if (InternalCheck(source))
            {
                if (_successor != null)
                {
                    return _successor.Check(source);
                }
                else
                {
                    return new CheckResult() 
                    { 
                        Source = source, 
                        Result = true 
                    };
                }
            }
            else
            {
                return new CheckResult()
                {
                    Source = source,
                    Result = false
                };
            }
        }

        protected FormatChecker(FormatChecker successor)
        {
            _successor = successor;
        }
    }

    /// <summary>
    /// 長度檢查
    /// </summary>
    internal class LengthChecker: FormatChecker
    {
        public LengthChecker(FormatChecker successor) : base(successor)
        {

        }

        protected override bool InternalCheck(string source)
        {
            return source.Length == 29;
        }
    }

    /// <summary>
    /// 開頭檢查
    /// </summary>
    internal class HeadChecker : FormatChecker
    {
        public HeadChecker(FormatChecker successor) : base(successor)
        {

        }

        protected override bool InternalCheck(string source)
        {
            string head = source.Substring(0, 3);
            return int.TryParse(head, out _);
        }
    }

    /// <summary>
    /// 第一個日期檢查
    /// </summary>
    internal class FirstDateChecker : FormatChecker
    {
        public FirstDateChecker(FormatChecker successor) : base(successor)
        {

        }

        protected override bool InternalCheck(string source)
        {
            string dateString = source.Substring(13, 8);
            return double.TryParse(dateString, out _);
        }
    }

    /// <summary>
    /// 第二個日期檢查
    /// </summary>
    internal class SecondDateChecker : FormatChecker
    {
        public SecondDateChecker(FormatChecker successor) : base(successor)
        {

        }

        protected override bool InternalCheck(string source)
        {
            string dateString = source.Substring(21, 8);
            return double.TryParse(dateString, out _);
        }
    }

    public class ChainContext
    {
        public static FormatChecker GetCheckers()
        {
            // 依序檢查 Length -> Head -> FirstDate -> SecondDate
            return new LengthChecker(new HeadChecker(new FirstDateChecker(new SecondDateChecker(null))));
        }
    }
}