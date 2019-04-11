using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Console;


/// 教学网址：
/// https://www.cnblogs.com/linJie1930906722/p/6103455.html?utm_source=itdadao&utm_medium=referral

namespace QTestsConsole
{
    class Program
    {

        private const string InventoryFileName = "inventory.json";
        static void Main(string[] args)
        {
            //CreateJson();
            //ConverObject();
            //SerializeJson();
            //DeserializeJson();
            dd();
            ReadKey();


        }


        private static void cc()
        {

            var jsonString = @"{""Name"":""小苹果"",""Company"":""韩国公司"",   ""Entered"":""2016-11-26 00:14""}";

            dynamic json = Newtonsoft.Json.Linq.JToken.Parse(jsonString) as dynamic;

            string name = json.Name;
            string company = json.Company;
            DateTime entered = json.Entered;
            Console.WriteLine("name:" + name);
            Console.WriteLine("company:" + company);
            Console.WriteLine("entered:" + entered);
        }



        private static void dd()
        {
            //Newtonsoft.Json.Linq.JObject jsonObject = new Newtonsoft.Json.Linq.JObject {{"Entered", DateTime.Now}};
            Newtonsoft.Json.Linq.JObject jsonObject = new Newtonsoft.Json.Linq.JObject();
            jsonObject.Add("Entered", DateTime.Now);
            dynamic album = jsonObject;

            album.AlbumName = "非主流歌曲";

            foreach (var item in jsonObject)  //循环输出动态的值  JObject（基类为JContainer、JObject和JArray）是一个集合，实现了IEnumerable接口，因此你还可以轻松地在运行时循环访问
            {
                Console.WriteLine(item.Key + "的值为：" + item.Value.ToString());
            }
        }

        private static void bb()
        {
            string jsonString1 = @"[{""Name"":""小苹果"",""Age"":""20""},{""Name"":""演员"",""Age"":""2""}]";
            Newtonsoft.Json.Linq.JArray userAarray1 = Newtonsoft.Json.Linq.JArray.Parse(jsonString1) as Newtonsoft.Json.Linq.JArray;
            List<User> userListModel = userAarray1.ToObject<List<User>>();
            foreach (var userModel1 in userListModel)
            {
                Console.WriteLine("Name:" + userModel1.Name);
                Console.WriteLine("Age:" + userModel1.Age);
            }

            Console.WriteLine("");
            string jsonString = @"[{""Name"":""小苹果"",""Age"":""20""}]";
            Newtonsoft.Json.Linq.JArray userAarray = Newtonsoft.Json.Linq.JArray.Parse(jsonString) as Newtonsoft.Json.Linq.JArray;
            Newtonsoft.Json.Linq.JObject jObject = userAarray[0] as Newtonsoft.Json.Linq.JObject;
            User userModel = jObject.ToObject<User>();
            Console.WriteLine("Name:" + userModel.Name);
            Console.WriteLine("Age:" + userModel.Age);
        }


        private static void aa()
        {
            UserType album = new UserType()
            {
                Type = "普通用户",
                UserListModel = new List<User>()
                {
                    new User()
                    {
                        Name="张三",
                        Age = 20
                    },
                    new User()
                    {
                        Name="李四",
                        Age = 30
                    }
                }
            };

            // serialize to string            
            string json2 = Newtonsoft.Json.JsonConvert.SerializeObject(album, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine("序列化结果");
            Console.WriteLine("");
            Console.WriteLine(json2);

            UserType userType = Newtonsoft.Json.JsonConvert.DeserializeObject<UserType>(json2);
            Console.WriteLine("");
            Console.WriteLine("反序列化：");
            Console.WriteLine("Type:" + userType.Type);
            Console.WriteLine("");
            foreach (var userModel in userType.UserListModel)
            {
                Console.WriteLine("Name:" + userModel.Name);
                Console.WriteLine("Age:" + userModel.Age);
            }
        }


        public class UserType
        {
            public string Type { get; set; }
            public List<User> UserListModel { get; set; }
        }

        public class User
        {
            public string Name { set; get; }
            public int Age { set; get; }
        }



        public static void SerializeJson()
        {
            using (StreamWriter writer = File.CreateText("abc.json"))
            {
                JsonSerializer serializer = JsonSerializer.Create(new JsonSerializerSettings { Formatting = Formatting.Indented });
                serializer.Serialize(writer, GetInventoryObject());
            }
        }

        public static void DeserializeJson()
        {
            using (StreamReader reader = File.OpenText(InventoryFileName))
            {
                JsonSerializer serializer = JsonSerializer.Create();
                var inventory = serializer.Deserialize(reader, typeof(Inventory)) as Inventory;
                foreach (var item in inventory.InventoryItems)
                {
                    WriteLine(item);
                }
            }
        }


        public static void ConverObject()
        {
            Inventory inventory = GetInventoryObject();
            string json = JsonConvert.SerializeObject(inventory, Formatting.Indented);
            WriteLine(json);
            WriteLine();
            Inventory newInventory = JsonConvert.DeserializeObject<Inventory>(json);
            foreach(var product in newInventory.InventoryItems)
            {
                WriteLine(newInventory);
            }
        }

        public static Inventory GetInventoryObject() =>
            new Inventory
            {
                InventoryItems = new Product[]
                {
                    new Product
                    {
                        ProductID = 100,
                        ProductName = "Product Thing",
                        SupplierID = 10
                    },
                    new BookProduct
                    {
                        ProductID = 101,
                        ProductName = "How To Use Your New Product Thing",
                        SupplierID = 10,
                        ISBN = "1245324652346"
                    }
                }
            };


        public static void CreateJson()
        {
            var book1 = new JObject();
            book1["title"] = "Professional C# 6 and .NET 5 Core";
            book1["publisher"] = "Wrox Press";

            var book2 = new JObject();
            book2["title"] = "Professional C# and .NET 4.5.1";
            book2["publisher"] = "Wrox Press";

            var books = new JArray();
            books.Add(book1);
            books.Add(book2);

            var json = new JObject();
            json["books"] = books;
            WriteLine(json);
        }
    }
}
