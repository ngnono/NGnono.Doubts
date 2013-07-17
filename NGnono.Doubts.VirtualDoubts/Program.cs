using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGnono.Doubts.VirtualDoubts
{
    class Program
    {
        static void Main(string[] args)
        {
            Test1();
        }


        private static void Test1()
        {

            Action<BaseClass> P = impl =>
                {
                    impl.Print1();
                    impl.Print2();
                };

            BaseClass a = new BaseClass();

            P(a);
            a = new AChildClass();
            P(a);
            a = new BChildClass();
            P(a);


            Console.ReadKey();
        }
    }

    #region test2

    public abstract class RootClass
    {
        public abstract void Print1();

        public virtual void Print2()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",RootClass.Print2");
        }

        public virtual void Print4()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",RootClass.Print2");
        }

        public void Print3()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",RootClass.Print3");
        }
    }

    public class ARootClass :RootClass
    {
        public override void Print1()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",ARootClass.Print1");
        }

        public override void Print2()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",ARootClass.Print2");
        }

        //public override void Print3()
        //{
        //    //编译异常
        //}

        public new void Print4()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",ARootClass.Print4");
        }

        public new void Print3()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",ARootClass.Print3");
        }

        public void Print5()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",ARootClass.Print5");
        }
    }

    public class BRootClass :RootClass
    {
        public override void Print1()
        {
            throw new NotImplementedException();
        }
    }

    #endregion


    #region test1

    public class BaseClass
    {
        public virtual void Print1()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",BaseClass.Print1");
        }

        public void Print2()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",BaseClass.Print2");
        }
    }

    public class AChildClass : BaseClass
    {
        public override void Print1()
        {

            Console.WriteLine("this.tostring: " + this.ToString() + ",AChildClass.Print1");

            Print2();
        }

        public new void Print2()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",AChildClass.Print2");
        }
    }

    public class BChildClass : BaseClass
    {
        public new void Print1()
        {
            Console.WriteLine("this.tostring: " + this.ToString() + ",BChildClass.Print1");
        }


        //public override void Print2()
        //{
        // 编译出错
        //}
    }

    #endregion
}
