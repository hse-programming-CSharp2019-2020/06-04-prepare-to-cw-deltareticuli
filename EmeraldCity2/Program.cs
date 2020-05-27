using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmeraldLib;
using System.Xml.Serialization;

namespace EmeraldCity2
{
    class Program
    {
        const string path = @"../../../EmeraldCity/bin/Debug/out.ser";
        static void Main(string[] args)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("404");
                return;
            }

            Street[] streets;

            XmlSerializer ser = new XmlSerializer(typeof(Street[]));
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    streets = ser.Deserialize(fs) as Street[];
                }

                if (streets == null)
                {
                    Console.WriteLine("Произошла ошибка десериализации");
                    return;
                }

                Console.WriteLine(string.Join<Street>("\n", streets.Where(street => ~street % 2 > 0 && +street)));
            }
            catch (IOException)
            {
                Console.WriteLine("Произошла ошибка ввода/вывода при работе с файлом");
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла непредвиденная ошибка при работе с файлом");
            }
        }
    }
}
