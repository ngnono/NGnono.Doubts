using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace NGnono.Doubts.IODoubts.FileIO
{
    internal class ConfigManager
    {
        /// <summary>
        /// 相似词路径
        /// </summary>
        public string SimilarFilePath
        {
            get { return ConfigurationManager.AppSettings["similarfilepath"]; }
        }
    }


    internal class LoadFiles
    {

        #region methods

        /// <summary>
        /// get files
        /// </summary>
        /// <param name="searchPattern"></param>
        /// <returns></returns>
        private static string[] GetFiles(string searchPattern)
        {
            var files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern);


            return files;
        }

        private Hashtable Load(string searchPattern, Action<List<string>, Hashtable> func)
        {
            var files = GetFiles(searchPattern);

            var h = new Hashtable();
            foreach (var file in files)
            {
                using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    var byts = ReadLine(fs);
                    func(byts,h);
                }
            }

            return h;
        }

        private List<string> ReadLine(Stream stream)
        {
            var rows = new List<string>();

            using (var sr = new StreamReader(stream, Encoding.Default))
            {
                //第一种读法
                string str;
                while ((str = sr.ReadLine()) != null)
                {
                    rows.Add(str);
                }

            }

            return rows;
        }


        private static void SafeRead(Stream stream, byte[] data)
        {
            int offset = 0;
            int remaining = data.Length;
            // 只要有剩余的字节就不停的读  
            while (remaining > 0)
            {
                int read = stream.Read(data, offset, remaining);
                if (read <= 0)
                    throw new EndOfStreamException("文件读取到" + read.ToString() + "失败！");
                // 减少剩余的字节数  
                remaining -= read;
                // 增加偏移量  
                offset += read;
            }
        }

        private static byte[] ReadFully(Stream stream)
        {
            // 初始化一个32k的缓存  
            byte[] buffer = new byte[32768];
            using (MemoryStream ms = new MemoryStream())
            { //返回结果后会自动回收调用该对象的Dispose方法释放内存  
                // 不停的读取  
                while (true)
                {
                    int read = stream.Read(buffer, 0, buffer.Length);
                    // 直到读取完最后的3M数据就可以返回结果了  
                    if (read <= 0)
                        return ms.ToArray();
                    ms.Write(buffer, 0, read);
                }
            }
        }

        private static byte[] Read4Buffer(Stream stream, int bufferLen)
        {
            // 如果指定的无效长度的缓冲区，则指定一个默认的长度作为缓存大小  
            if (bufferLen < 1)
            {
                bufferLen = 0x8000;
            }
            // 初始化一个缓存区  
            byte[] buffer = new byte[bufferLen];
            int read = 0;
            int block;
            // 每次从流中读取缓存大小的数据，知道读取完所有的流为止  
            while ((block = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                // 重新设定读取位置  
                read += block;
                // 检查是否到达了缓存的边界，检查是否还有可以读取的信息  
                if (read == buffer.Length)
                {
                    // 尝试读取一个字节  
                    int nextByte = stream.ReadByte();
                    // 读取失败则说明读取完成可以返回结果  
                    if (nextByte == -1)
                    {
                        return buffer;
                    }
                    // 调整数组大小准备继续读取  
                    byte[] newBuf = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuf, buffer.Length);
                    newBuf[read] = (byte)nextByte;
                    buffer = newBuf;// buffer是一个引用（指针），这里意在重新设定buffer指针指向一个更大的内存  
                    read++;
                }
            }
            // 如果缓存太大则使用ret来收缩前面while读取的buffer，然后直接返回  
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);

            return ret;
        }


        #endregion
    }
}
