using System;
using System.Collections.Generic;
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

namespace Ролевая_игра__WPF_
{
    /// <summary>
    /// Логика взаимодействия для Magic.xaml
    /// </summary>
    public partial class Magic : Window
    {
        private List<Персонаж> СписокПерсонажей;
        private List<Враги> СписокВрагов;
        private List<object> ПорядокАтаки;
        private int OrderOf;
        private double[] МножительСилыРун;
        public string ConsoleOutput { get; private set; }
        public Magic(List<Персонаж> СписокПерсонажей, List<Враги> СписокВрагов, List<object> ПорядокАтаки, int OrderOf, double[] МножительСилыРун)
        {
            InitializeComponent();

            this.СписокПерсонажей = СписокПерсонажей;
            this.СписокВрагов = СписокВрагов;
            this.ПорядокАтаки = ПорядокАтаки;
            this.OrderOf = OrderOf;
            this.МножительСилыРун = МножительСилыРун;

            ComboBox_ВыборЗаклинания.Items.Add(Заклинания.Лечение.Название);
            ComboBox_ВыборЗаклинания.Items.Add(Заклинания.Воскрешение.Название);
            ComboBox_ВыборЗаклинания.Items.Add(Заклинания.Огненный_Шар.Название);
            ComboBox_ВыборЗаклинания.Items.Add(Заклинания.Заморозка.Название);

            ComboBox_ВыборЗаклинания.SelectedIndex = 0;
            TextBlock_Подсказки.Text = "Лечит персонажа на 25 единиц здоровья";
        }

        private void ComboBox_ВыборЗаклинания_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 0)
            {
                ComboBox_ЦельЗаклинания.Items.Clear();
                foreach (Персонаж персонаж in СписокПерсонажей)
                {
                    if (персонаж.Состояние[4] == false)
                    {
                        ComboBox_ЦельЗаклинания.Items.Add(персонаж.Имя);
                    }
                }
                TextBlock_Подсказки.Text = "Лечит персонажа на 25 единиц здоровья";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ВыборЗаклинания.SelectedIndex == 0 && ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Вылечит {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 25 единиц здоровья. " +
                        $"Сейчас {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ.";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if (ComboBox_ВыборЗаклинания.SelectedIndex == 1)
            {
                ComboBox_ЦельЗаклинания.Items.Clear();

                foreach (Персонаж персонаж in СписокПерсонажей)
                {
                    if (персонаж.Состояние[4] == true)
                    {
                        ComboBox_ЦельЗаклинания.Items.Add(персонаж.Имя);
                    }
                }
                TextBlock_Подсказки.Text = "Воскресит персонажа и вылечит его на 50 единиц здоровья";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ВыборЗаклинания.SelectedIndex == 0 && ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Воскресит {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} и вылечит его на 25 единиц здоровья.";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if(ComboBox_ВыборЗаклинания.SelectedIndex == 2)
            {
                ComboBox_ЦельЗаклинания.Items.Clear();

                foreach (Враги Враг in СписокВрагов)
                {
                    ComboBox_ЦельЗаклинания.Items.Add(Враг.ИмяВрага);
                }

                TextBlock_Подсказки.Text = "Создаст и запустит огненный шар который нанесет 50 единиц урона по врагу";

                Button_Использовать.IsEnabled = false;

                if (ComboBox_ВыборЗаклинания.SelectedIndex == 0 && ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Создаст и запустит огненный шар который нанесет 50 единиц урона по {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага}." +
                        $"Сейчас {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ЗдоровьеВрага}/{СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].МаксимальноеЗдоровье}";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if(ComboBox_ВыборЗаклинания.SelectedIndex == 3)
            {
                ComboBox_ЦельЗаклинания.Items.Clear();

                foreach (Враги Враг in СписокВрагов)
                {
                    ComboBox_ЦельЗаклинания.Items.Add(Враг.ИмяВрага);
                }
                TextBlock_Подсказки.Text = "Заморозит врага на 1 ход. (Выбранный враг пропустит ход)";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ВыборЗаклинания.SelectedIndex == 0 && ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Заморозит {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} на 1 ход. (Выбранный {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} пропустит ход)";
                    Button_Использовать.IsEnabled = true;
                }
            }
        }

        private void ComboBox_ЦельЗаклинания_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 0)
            {
                TextBlock_Подсказки.Text = "Лечит персонажа на 25 единиц здоровья";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ВыборЗаклинания.SelectedIndex == 0 && ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Вылечит {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 25 единиц здоровья. " +
                        $"Сейчас {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ.";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if (ComboBox_ВыборЗаклинания.SelectedIndex == 1)
            {
                TextBlock_Подсказки.Text = "Воскресит персонажа и вылечит его на 50 единиц здоровья";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Воскресит {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} и вылечит его на 25 единиц здоровья.";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if(ComboBox_ВыборЗаклинания.SelectedIndex == 2)
            {
                TextBlock_Подсказки.Text = "Создаст и запустит огненный шар который нанесет 50 единиц урона по врагу";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Создаст и запустит огненный шар который нанесет 50 единиц урона по {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага}. " +
                        $"Сейчас {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ЗдоровьеВрага}/{СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].МаксимальноеЗдоровье}";
                    Button_Использовать.IsEnabled = true;
                }
            }
            else if(ComboBox_ВыборЗаклинания.SelectedIndex == 3)
            {
                TextBlock_Подсказки.Text = "Заморозит врага на 1 ход. (Выбранный враг пропустит ход)";
                Button_Использовать.IsEnabled = false;
                if (ComboBox_ЦельЗаклинания.SelectedIndex != -1)
                {
                    TextBlock_Подсказки.Text = $"Заморозит {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} на 1 ход. (Выбранный {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} пропустит ход)";
                    Button_Использовать.IsEnabled = true;
                }
            }
        }

        private void Button_Использовать_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 0)
            {
                new Заклинания.Лечение(ПорядокАтаки[OrderOf] as Персонаж_с_магией, СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex], МножительСилыРун[1]);
                if ((ПорядокАтаки[OrderOf] as Персонаж).Пол == true)
                {
                    ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж).Имя} вылечил {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 25 единиц здоровья. " +
                        $"Сейчас у {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} " +
                        $"{СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ";
                }
                else
                {
                    ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж).Имя} вылечила {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 25 единиц здоровья. " +
                        $"Сейчас у {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} " +
                        $"{СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ";
                }
            }
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 1)
            {
                new Заклинания.Воскрешение(ПорядокАтаки[OrderOf] as Персонаж_с_магией, СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex], МножительСилыРун[1]);
                if ((ПорядокАтаки[OrderOf] as Персонаж).Пол == true)
                {
                    ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж).Имя} воскресил и вылечил {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 50 единиц здоровья. " +
                        $"Сейчас у {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} " +
                        $"{СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ";
                }
                else
                {
                    ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж).Имя} воскресила и вылечила {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} на 50 единиц здоровья. " +
                        $"Сейчас у {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Имя} " +
                        $"{СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Очки_Здоровья} ОЗ / {СписокПерсонажей[ComboBox_ЦельЗаклинания.SelectedIndex].Максимальное_здоровье} ОЗ";
                }
            }
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 2)
            {
                new Заклинания.Огненный_Шар(ПорядокАтаки[OrderOf] as Персонаж_с_магией, СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex], МножительСилыРун[1]);
                ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж_с_магией).Имя} запустил огненный шар в " +
                    $"{СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} и нанёс ему 50 единиц урона. " +
                    $"Сейчас у {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ИмяВрага} " +
                    $"{СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].ЗдоровьеВрага} ОЗ / {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex].МаксимальноеЗдоровье} ОЗ";
            }
            if (ComboBox_ВыборЗаклинания.SelectedIndex == 3)
            {
                Заклинания.Заморозка Заклинание = new Заклинания.Заморозка(ПорядокАтаки[OrderOf] as Персонаж_с_магией, СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex], МножительСилыРун[1]);
                ConsoleOutput = $"{(ПорядокАтаки[OrderOf] as Персонаж_с_магией).Имя} заморозил {СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex]}. " +
                    $"{СписокВрагов[ComboBox_ЦельЗаклинания.SelectedIndex]} попускает ход.";
            }
            DialogResult = true;
        }
    }
}
