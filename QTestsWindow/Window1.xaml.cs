using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QTestsWindow
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public List<Person> Persons { get; set; }
        public string selectedName { get; set; }
        public Window1()
        {
            Persons = new List<Person>();
            Persons.Add(new Person("xyf",16));
            Persons.Add(new Person("白骨金", 16));
            Persons.Add(new Person("猪八戒", 16));
            InitializeComponent();
            //listBox.SetBinding(ListBox.SelectedItemProperty, new Binding("selectedName"));
            DataContext = this;
            //listBox.ItemsSource = this;
            lstProducts.Items.Add("aafdf");
            lstProducts.Items.Add("aafdf");
            lstProducts.Items.Add("aafdf");
            lstProducts.Items.Add("aafdf");
        }

        public class Person
        {
            public Person(string n, int a)
            {
                Name = n;
                Age = a;
            }
            public string Name { get; set; }
            public int Age { get; set; }
        }
    }
}
