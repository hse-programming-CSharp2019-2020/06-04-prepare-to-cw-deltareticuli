using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EmeraldLib;

namespace EmeraldCity
{
    class Program
    {
        static Random rnd = new Random();
        static Street[] ReadFile(int n)
        {
            List<Street> res = new List<Street>(n);
            try
            {
                FileStream fileStream = new FileStream("data.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.GetEncoding("Windows-1251")))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        string[] lineSplitted = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        if (lineSplitted.Length < 2)
                            return null;
                        string name = lineSplitted[0];
                        int[] houses;
                        try
                        {
                            houses = lineSplitted.Skip(1).Select(int.Parse).ToArray();
                            if (res.Count < n)
                                res.Add(new Street { Name = name, Houses = houses });
                            if (houses.Any(x => x > 100 || x < 0))
                                return null;
                        }
                        catch (Exception ex) when (ex is FormatException || ex is OverflowException)
                        {
                            return null;
                        }
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Произошла ошибка ввода/вывода при работе с файлом");
                return null;
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла непредвиденная ошибка");
                return null;
            }
            return res.ToArray();
        }

        static Street[] CreateArray(int n)
        {
            Street[] res = new Street[n];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = new Street { Name = CreateName(5), Houses = CreateHouses() };
            }
            return res;
        }

        static string CreateName(int n)
        {
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                res.Append((char)rnd.Next('a', 'z' + 1));
            }
            return res.ToString();
        }

        static int[] CreateHouses()
        {
            int[] res = new int[rnd.Next(2, 11)];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = rnd.Next(1, 101);
            }
            return res;
        }

        static int ReadNum(string msg, int left = int.MinValue, int right = int.MaxValue)
        {
            int res;
            do
            {
                Console.WriteLine(msg);
            } while (!int.TryParse(Console.ReadLine(), out res) || res < left || res > right);
            return res;
        }

        static void Main(string[] args)
        {
            int n = ReadNum("Введите N: ", 0);
            Street[] streets = ReadFile(n) ?? CreateArray(n);

            Console.WriteLine(string.Join<Street>("\n", streets));

            Serialize(streets);
        }

        private static void Serialize(Street[] streets)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Street[]));

            using (FileStream fs = new FileStream("out.ser", FileMode.Create))
            {
                ser.Serialize(fs, streets);
            }
        }
    }
}
