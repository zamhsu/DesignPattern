namespace CreationalPatterns.RegistryOfSingletons
{
    class Program
    {
        static void Main(string[] args)
        {
            // Registry of Singletons
            Console.WriteLine("Registry of Singletons");

            // 目的：共同管理單例的Instance。

            // 系統運作中，單例的Instance非常多
            // 變成須要有一個共通的地方來管理會使得取出Instance更加簡便。

            var o1 = SingletonRegistry.GetInstance<Class1>();
            var o2 = SingletonRegistry.GetInstance<Class1>();

            Console.WriteLine("是否相同");
            Console.WriteLine(object.ReferenceEquals(o1, o2));

            Console.ReadLine();
        }
    }

    public class SingletonRegistry
    {
        private static Dictionary<string, object> registry = new Dictionary<string, object>();

        public static T GetInstance<T>() where T : class, new()
        {
            Type type = typeof(T);
            string key = type.Name;

            if (!registry.ContainsKey(key))
            {
                registry[key] = new T();
            }

            return (T)registry[key];
        }
    }

    public class Class1
    {
        public string Name
        {
            get { return "Class 1 Name"; }
        }
    }
}