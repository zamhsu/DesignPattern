namespace BehavioralPatterns.Memento
{
    class Program
    {
        // 協助保管備份的執行個體，不可讓其他人修改
        static MementoCaretaker _caretaker;

        // 備份的目標主要是Model
        static Model _model;

        static void Main(string[] args)
        {
            // 備忘錄
            Console.WriteLine("Memento (備忘錄)");

            // 目的：完整複製物件內的狀態，儲存至其他地方，達到復原機制。

            // 例如上一頁或上一步的功能，或是儲存當前使用者畫面佈局和資料的狀態，
            // 下次回來時可以恢復之前的狀態

            _model = new Model();

            _model.class1.x = 10;
            _model.class1.class2.a = 5;

            // 備份
            CreateCaretaker();

            Console.WriteLine("備份資料");
            Console.WriteLine($"class1.x: {_model.class1.x}");
            Console.WriteLine($"class1.class2.a: {_model.class1.class2.a}");

            // 修改資料
            _model.class1.x = 2;
            _model.class1.class2.a = 0;

            Console.WriteLine("修改資料");
            Console.WriteLine($"class1.x: {_model.class1.x}");
            Console.WriteLine($"class1.class2.a: {_model.class1.class2.a}");

            // 還原
            _model.SetMemento(_caretaker.Memento);

            Console.WriteLine("還原");
            Console.WriteLine($"class1.x: {_model.class1.x}");
            Console.WriteLine($"class1.class2.a: {_model.class1.class2.a}");

            _caretaker = null;

            Console.ReadLine();
        }

        private static void CreateCaretaker()
        {
            // 備份檔管理者先產生執個體
            _caretaker = new MementoCaretaker();
            // 複製要備份的資料
            _caretaker.Memento = _model.CreateMemento();
        }
    }

    /// <summary>
    /// 備份檔管理者
    /// </summary>
    public class MementoCaretaker
    {
        private ModelMemento _memento = null!;
        public ModelMemento Memento
        {
            get
            {
                return _memento;
            }
            set
            {
                _memento = value;
            }
        }
    }

    class Model
    {
        public Class1 class1;
        public Class3 class3;
        public Model()
        {
            class1 = new Class1();
            class3 = new Class3();
        }

        // 執行複製的方法
        public ModelMemento CreateMemento()
        {
            // 需要執行的欄位為class1, class3
            return new ModelMemento(class1, class3);
        }

        // 執行還原的方法
        public void SetMemento(ModelMemento memento)
        {
            class1 = (Class1)memento.class1.Clone();
            class3 = (Class3)memento.class3.Clone();
        }
    }

    /// <summary>
    /// 備份檔
    /// </summary>
    public class ModelMemento
    {
        // ModelMemento可視為一張複寫紙，將Model內所需複製的欄位填入ModelMemento
        public Class1 class1 { get; private set; }
        public Class3 class3 { get; private set; }

        // 資料在這裡複製
        public ModelMemento(Class1 _class1, Class3 _class3)
        {
            class1 = (Class1)_class1.Clone();
            class3 = (Class3)_class3.Clone();
        }
    }

    public class Class1 : ICloneable
    {
        public int x { get; set; }
        public int y { get; set; }
        public Class2 class2 { get; set; }
        public Class1()
        {
            class2 = new Class2();
        }

        public object Clone()
        {
            Class1 obj = (Class1)this.MemberwiseClone();
            if (class2 != null)
            {
                // 內部物件
                // 實作深層複製，等於new新的執行個體，然後把直放進去
                obj.class2 = (Class2)this.class2.Clone();
            }

            return obj;
        }
    }

    public class Class2 : ICloneable
    {
        public int a { get; set; }
        public int b { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class Class3 : ICloneable
    {
        public int k { get; set; }
        public int j { get; set; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}