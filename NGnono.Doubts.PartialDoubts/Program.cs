namespace NGnono.Doubts.PartialDoubts
{
    using System;

    /// <summary>
    /// 看 IL 会把 部分类编译到一起
    /// </summary>

    class Program
    {
        static void Main(string[] args)
        {
            var a = new A();
            a.PrintB();

            Console.ReadLine();
        }
    }


    //public partial abstract class BassClass
    //{
    //    //编译报错
    //}


    public partial interface IInterface
    {
         
    }


    public partial class A
    {
        public void PrintA()
        {
        }
    }

    public partial class B
    {
        partial void Pb();

        public void Run()
        {
            Pb();
        }
    }

    public partial class B
    {
        partial void Pb()
        {
            Console.WriteLine("class B.Pb()");
        }
    }

    //编译出错
    //public partial class B
    //{
    //    partial void Pb()
    //    {
    //        Console.WriteLine("class B.Pb()");
    //    } 
    //}

    public class C
    {
        public C()
        {
            var b = new B();
            b.Run();
        }
    }
}


namespace NGnono.Doubts.PartialDoubts
{
    public partial class A
    {
        public void PrintB()
        {
        }
    }
}
