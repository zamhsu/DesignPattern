namespace BehavioralPatterns.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            // 命令
            Console.WriteLine("Command (命令)");

            // 目的：將一個請求封裝成一個物件，讓你能夠使用各種不同的訊息、佇列、紀錄以及支援復原功能加以參數化。

            // 在程式運行的過程中很像一直在打指令，一行指令會執行一個或多個動作。
            // 撰寫程式的過程中也罷執行動作封裝成一個物件，然後依序執行

            List<CheckResult> results = new List<CheckResult>();
            Invoker invoker = Client.CreateInvoker();

            string[] fakeData = new string[] { "9650001", "9750001", "9650002" };
            Console.WriteLine("已輸入：");

            foreach (var item in fakeData)
            {
                Console.WriteLine(item);
                results.Add(invoker.Action(item));
            }

            foreach (var item in results)
            {
                Console.WriteLine($"Source: {item.Source}, Result: {item.Result}");
            }

            Console.ReadLine();
        }
    }

    // 第一步，建立要執行的指令
    public interface IFormatChecker
    {
        CheckResult CheckLength(string source);
        CheckResult CheckHead(string source);
    }

    // 第二步，執行checker動作的主要執行人和實作checker內容
    public class FormatChecker : IFormatChecker
    {
        public CheckResult CheckLength(string source)
        {
            bool result = source.Length == 7;
            return new CheckResult() { Source = source, Result = result };
        }

        public CheckResult CheckHead(string source)
        {
            string head = source.Substring(0, 3);
            bool result = head == "965";
            return new CheckResult() { Source = source, Result = result };
        }
    }

    // 第三步，抽出checker的執行操作(execute)
    public abstract class CheckCommand
    {
        protected FormatChecker Checker { get; private set; }
        protected CheckCommand(FormatChecker checker)
        {
            Checker = checker;
        }
        public abstract CheckResult Execute(string source);
    }

    // 依照check內的方法寫出對應檢查的類別
    public class CheckLengthCommand : CheckCommand
    {
        public CheckLengthCommand(FormatChecker checker) : base(checker)
        {
        }

        public override CheckResult Execute(string source)
        {
            return Checker.CheckLength(source);
        }
    }

    // 依照check內的方法寫出對應檢查的類別
    public class CheckHeadCommand : CheckCommand
    {
        public CheckHeadCommand(FormatChecker checker) : base(checker)
        {
        }

        public override CheckResult Execute(string source)
        {
            return Checker.CheckHead(source);
        }
    }

    // 第四步，建立執行動作的工作站，所有指令都交由Invoker來執行
    public class Invoker
    {
        private List<CheckCommand> _commands = new List<CheckCommand>();

        public void AddCommand(CheckCommand command)
        {
            _commands.Add(command);
        }

        public void RemoveCommand(CheckCommand command)
        {
            _commands.Remove(command);
        }

        public CheckResult Action(string source)
        {
            CheckResult result = null;
            foreach (var command in _commands)
            {
                result = command.Execute(source);
                if (result.Result == false)
                {
                    break;
                }
            }

            return result;
        }
    }

    public class Client
    {
        // user端將指令加入Invoker中
        public static Invoker CreateInvoker()
        {
            FormatChecker checker = new FormatChecker();

            Invoker invoker = new Invoker();
            invoker.AddCommand(new CheckLengthCommand(checker));
            invoker.AddCommand(new CheckHeadCommand(checker));

            return invoker;
        }
    }

    public class CheckResult
    {
        public string Source { get; set; } = null!;
        public bool Result { get; set; }
    }
}