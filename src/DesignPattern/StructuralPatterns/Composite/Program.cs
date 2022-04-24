namespace CreationalPatterns.SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 組合
            Console.WriteLine("Composite (組合)");

            // 目的：將物件組合成樹狀結構以表示part-whole hierarchies，
            // 使客戶端能夠一致地對待單個物件和物件的組合。

            // 檔案系統就是一種Composite pattern的實現
            // 進入檔案系統基本上只會看到資料夾(Directory)和檔案(file)這兩種物件
            // 一個資料夾可以包含任意數量的檔案或是資料夾
            // 這麼一來就形成了樹狀的結構，而我們都可以對其進行刪除或是重新命名的動作
            // 這就是定義所說的一致性對待
            // 只要遇到階層或是樹狀的結構都可以評估看看是否需要套用Composite pattern

            // 開發部碼農2名
            Employee pg1 = new Employee() { Name = "碼農1號", Department = "開發部", Designation = "碼農" };
            Employee pg2 = new Employee() { Name = "碼農2號", Department = "開發部", Designation = "碼農" };

            // 開發部PM1名
            CompositeEmployee pm1 = new CompositeEmployee() { Name = "PM1號", Department = "開發部", Designation = "專案經理" };

            // PM有2名碼農
            pm1.AddEmployee(pg1);
            pm1.AddEmployee(pg2);

            // 開發部主管
            CompositeEmployee manager = new CompositeEmployee() { Name = "主管", Department = "開發部", Designation = "主管" };

            // 主管有1名PM
            manager.AddEmployee(pm1);

            // PM和底下員工
            Console.WriteLine("----- PM和底下員工 ----");
            pm1.DisplayDetails();
            Console.WriteLine();

            // 主管和底下員工
            Console.WriteLine("----- 主管和底下員工 ----");
            manager.DisplayDetails();
            Console.WriteLine();

            Console.ReadLine();
        }
    }

    public interface IEmployee
    {
        string? Name { get; set; }
        string? Department { get; set; }
        string? Designation { get; set; }
        void DisplayDetails();
    }

    // 葉節點
    public class Employee : IEmployee
    {
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }

        public void DisplayDetails()
        {
            Console.WriteLine($"\t{Name} 隸屬於 {Department} 頭銜:{Designation}");
        }
    }

    // 非葉節點
    public class CompositeEmployee : IEmployee
    {
        public string? Name { get; set; }
        public string? Department { get; set; }
        public string? Designation { get; set; }

        // 下屬容器
        private List<IEmployee> subordinateList = new List<IEmployee>();

        // 新增員工
        public void AddEmployee(IEmployee employee)
        {
            subordinateList.Add(employee);
        }

        // 移除員工
        public void RemoveEmployee(IEmployee employee)
        {
            subordinateList.Remove(employee);
        }

        public void DisplayDetails()
        {
            Console.WriteLine($"\n{Name} 隸屬於 {Department} 頭銜:{Designation}");

            foreach (var employee in subordinateList)
            {
                employee.DisplayDetails();
            }
        }
    }
}