namespace BehavioralPatterns.Visitor
{
    class Program
    {
        static void Main(string[] args)
        {
            // 訪問者
            Console.WriteLine("Visitor (訪問者)");

            // 目的：將某種資料結構中各元素的操作提取出來，封裝成獨立的類別(Visitor)
            // 使得每一個Visitor都能依據不同的元素，對應到不同的行為結果

            // 一般來說都是透過資料操作類別直接操作資料，
            // 使用訪問者模式後，資料可以選擇某個資料操作類別來操作

            SaleOrder saleOrder = new SaleOrder()
            {
                Id = 1,
                CustomerName = "Little boy",
                CustomerAddress = "Mars",
                CreateDate = DateTime.Now,
                Items = new List<string>() { "手機", "手機殼", "玻璃膜" }
            };

            ReturnOrder returnOrder = new ReturnOrder()
            {
                Id = 3,
                CustomerName = "Little boy",
                CustomerAddress = "Mars",
                CreateDate = DateTime.Now,
                Items = new List<string>() { "蔬菜", "水果" }
            };

            OrderCenter orderCenter = new OrderCenter() { saleOrder, returnOrder };

            Picker picker = new Picker()
            {
                Id = 1006,
                Name = "Picker 1006"
            };

            Distributor distributor = new Distributor()
            {
                Id = 1028,
                Name = "Distributor 1028"
            };

            orderCenter.Accept(picker);

            orderCenter.Accept(distributor);

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 訂單(Element)
    /// </summary>
    public abstract class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = null!;
        public string CustomerAddress { get; set; } = null!;
        public decimal Price { get; set; }
        public DateTime CreateDate { get; set; }

        public List<string> Items { get; set; } = null!;
        public abstract void Accept(Visitor visitor);
    }

    /// <summary>
    /// 銷售訂單(Element A)
    /// </summary>
    public class SaleOrder : Order
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// 退貨訂單(Element B)
    /// </summary>
    public class ReturnOrder : Order
    {
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }
    }

    /// <summary>
    /// 訪問者(Visitor)
    /// </summary>
    public abstract class Visitor
    {
        public abstract void Visit(SaleOrder saleOrder);
        public abstract void Visit(ReturnOrder returnOrder);
    }

    /// <summary>
    /// 揀貨員(Visitor A)
    /// </summary>
    public class Picker : Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public override void Visit(SaleOrder saleOrder)
        {
            Console.WriteLine("揀貨員：");
            Console.WriteLine($"開始為銷售訂單 [{saleOrder.Id}] 揀貨：");
            foreach (var item in saleOrder.Items)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"訂單 [{saleOrder.Id}] 揀貨完畢");

            Console.WriteLine("==========================");
            Console.WriteLine();
        }

        public override void Visit(ReturnOrder returnOrder)
        {
            Console.WriteLine("揀貨員：");
            Console.WriteLine($"開始為退貨訂單 [{returnOrder.Id}] 進行退貨處理：");
            foreach (var item in returnOrder.Items)
            {
                Console.WriteLine(item);
            }

            Console.WriteLine($"退貨訂單 [{returnOrder.Id}] 退貨完畢");
            Console.WriteLine("==========================");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 送貨員(Visitor B)
    /// </summary>
    public class Distributor : Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public override void Visit(SaleOrder saleOrder)
        {
            Console.WriteLine("送貨員：");
            Console.WriteLine($"開始為銷售訂單 [{saleOrder.Id}] 送貨：");

            Console.WriteLine($"一共有{saleOrder.Items.Count()}件商品。");
            Console.WriteLine($"收件人：{saleOrder.CustomerName}");
            Console.WriteLine($"地址：{saleOrder.CustomerAddress}");

            Console.WriteLine($"訂單 [{saleOrder.Id}] 送貨完畢");
            Console.WriteLine("==========================");
            Console.WriteLine();
        }

        public override void Visit(ReturnOrder returnOrder)
        {
            Console.WriteLine("送貨員：");
            Console.WriteLine($"收到來自 [{returnOrder.CustomerName}] 的退貨訂單 [{returnOrder.Id}] ，進行退貨收貨處理：");

            Console.WriteLine($"一共有{returnOrder.Items.Count()}件商品。");

            Console.WriteLine($"退貨訂單 [{returnOrder.Id}] 收貨完成");
            Console.WriteLine("==========================");
            Console.WriteLine();
        }
    }

    /// <summary>
    /// 訂單中心(Object Structure)
    /// </summary>
    public class OrderCenter : List<Order>
    {
        public void Accept(Visitor visitor)
        {
            var iterator = this.GetEnumerator();

            while (iterator.MoveNext())
            {
                iterator.Current.Accept(visitor);
            }
        }
    }
}