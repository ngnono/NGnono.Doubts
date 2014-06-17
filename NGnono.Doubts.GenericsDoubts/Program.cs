using System;
using System.Collections.Generic;

namespace NGnono.Doubts.GenericsDoubts
{
    internal struct StructDict
    {
        //public override int GetHashCode()
        //{
        //    return 1;
        //}
        public string Name { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Run();

            Console.ReadLine();
        }

        /// <summary>
        /// 值类型 字典问题
        /// </summary>
        public static void Run()
        {
            var dict = new Dictionary<StructDict, bool>();

            var a = new StructDict();
            a.Name = "121";
            var b = new StructDict();//hashcode 与a一致
            b.Name = "121";
            var c = new StructDict();
            c.Name = "aaaa";
            var d = new StructDict();//
            var e = new StructDict();//hashcode 与d一致


            Console.WriteLine(a.GetHashCode());
            Console.WriteLine(b.GetHashCode());
            Console.WriteLine(c.GetHashCode());
            Console.WriteLine(d.GetHashCode());
            Console.WriteLine(e.GetHashCode());

            dict.Add(a, true);
            dict.Add(b, true);
            dict.Add(c, true);
            dict.Add(d, true);
            dict.Add(e, true);
        }


        public void Run1()
        {
            var record = new Record();

            var i = record.Get<int>("int");
            var s = record.Get<string>("str");
            var l = record.Get<long>("long");
            var d = record.Get<float>("float");

            Console.WriteLine(i);
            Console.WriteLine(s);
            Console.WriteLine(l);
            Console.WriteLine(d);

        }
    }

    public static class RecordExtensions
    {

        private static class Cache<T>
        {
            public static Func<IRecord, string, T> Get;
        }

        static RecordExtensions()
        {
            Cache<string>.Get = (record, field) => record.GetString(field);
            Cache<int>.Get = (record, field) => record.GetInt(field);
            Cache<long>.Get = (record, field) => record.GetLong(field);
        }

        public static T Get<T>(this IRecord record, string field)
        {
            return Cache<T>.Get(record, field);
        }
    }

    public interface IRecord
    {
        string GetString(string field);
        int GetInt(string field);
        long GetLong(string field);
    }

    public class Record : IRecord
    {
        public string GetString(string field)
        {
            return "str";
        }

        public int GetInt(string field)
        {
            return 1;
        }

        public long GetLong(string field)
        {
            return 11L;
        }
    }
}
