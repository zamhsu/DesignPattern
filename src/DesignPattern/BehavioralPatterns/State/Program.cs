namespace BehavioralPatterns.State
{
    class Program
    {
        static void Main(string[] args)
        {
            // 狀態
            Console.WriteLine("State (狀態)");

            // 目的：讓一個物件在其內部狀態改變的時候，其行為也隨之改變。
            // 狀態模式需要對每一個系統可能取得的狀態創立一個狀態類的子類。
            // 當系統的狀態變化時，系統便改變所選的子類。

            // 建立每一種狀態的類別，裡面定義了該狀態要做的事和下一步驟
            // 建立存放狀態的物件(context)，具有改變狀態的權力

            // 有點像責任鏈模式，都是為了消除if-else

            Context context = new Context();
            // 自訂初始狀態
            context.CurrentState = new ToDoState();
            while (context.CurrentState != null)
            {
                context.Action();
            }

            Console.ReadLine();
        }
    }

    public abstract class JobState
    {
        public abstract void Action(Context context);
    }

    public class ToDoState : JobState
    {
        public override void Action(Context context)
        {
            // 動作

            // 設定下一步驟
            context.CurrentState = new WorkingState();
            Console.WriteLine("工作已建立在待辦清單");
        }
    }

    public class WorkingState : JobState
    {
        public override void Action(Context context)
        {
            // 動作

            // 設定下一步驟
            context.CurrentState = new TestingState();
            Console.WriteLine("工作完成");
        }
    }

    public class TestingState : JobState
    {
        public override void Action(Context context)
        {
            // 動作

            // 設定下一步驟
            context.CurrentState = new DoneState();
            Console.WriteLine("測試完成");
        }
    }

    public class DoneState : JobState
    {
        public override void Action(Context context)
        {
            // 動作

            // 沒有下一步驟
            context.CurrentState = null;
            Console.WriteLine("已完成此工作所有項目，結案");
        }
    }

    public class Context
    {
        public JobState? CurrentState { get; set; }

        public Context()
        {
            // 設定預設狀態
            CurrentState = new ToDoState();
        }

        public void Action()
        {
            if (CurrentState != null)
            {
                CurrentState.Action(this);
            }
        }
    }
}