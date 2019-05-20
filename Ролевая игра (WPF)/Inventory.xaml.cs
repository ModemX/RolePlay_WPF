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
    /// Логика взаимодействия для Inventory.xaml
    /// </summary>

    public partial class Inventory : Window
    {
        private List<Персонаж> Персонажи;
        private List<Предметы.Зелье> ПереченьПредметов;
        private List<string> НазванияПредметов = new List<string>();

        public Inventory(List<Персонаж> Текущие_Персонажи, List<Предметы.Зелье> ПереченьПредметов)
        {
            InitializeComponent();
            Персонажи = Текущие_Персонажи;
            foreach (Персонаж Текущий_Персонаж in Текущие_Персонажи)
            {
                if (Текущий_Персонаж is Персонаж)
                {
                    Выпадающий_Список_Персонажей.Items.Add((Текущий_Персонаж as Персонаж).Имя);
                }
                else
                {
                    Выпадающий_Список_Персонажей.Items.Add((Текущий_Персонаж as Персонаж_с_магией).Имя);
                }
            }
            this.ПереченьПредметов = ПереченьПредметов;

            ReloadInventoryList();
        }

        private void Список_Предметов_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Список_Предметов.SelectedItem != null && Выпадающий_Список_Персонажей.SelectedIndex != -1)
            {
                if (ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_маны && Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] is Персонаж_с_магией)
                {
                    Button_Use.IsEnabled = true;
                }
                else
                if(Персонажи[Выпадающий_Список_Персонажей.SelectedIndex].Очки_Здоровья != Персонажи[Выпадающий_Список_Персонажей.SelectedIndex].Максимальное_здоровье
                   && ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_Лечения)
                {
                    Button_Use.IsEnabled = true;
                }
                else 
                if (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] is Персонаж_с_магией 
                   &&
                   (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] as Персонаж_с_магией).Очки_Маны 
                   != 
                   (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] as Персонаж_с_магией).Максимальная_мана 
                   && 
                   ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_маны)
                {
                    Button_Use.IsEnabled = true;
                }
                else
                {
                    Button_Use.IsEnabled = false;
                }
            }
        }
        private void Выпадающий_Список_Персонажей_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Список_Предметов.SelectedItem != null && Выпадающий_Список_Персонажей.SelectedIndex != -1)
            {
                if (ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_маны && Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] is Персонаж_с_магией)
                {
                    Button_Use.IsEnabled = true;
                }
                else
                if (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex].Очки_Здоровья != Персонажи[Выпадающий_Список_Персонажей.SelectedIndex].Максимальное_здоровье
                   && ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_Лечения)
                {
                    Button_Use.IsEnabled = true;
                }
                else
                if (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] is Персонаж_с_магией
                   &&
                   (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] as Персонаж_с_магией).Очки_Маны
                   !=
                   (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] as Персонаж_с_магией).Максимальная_мана
                   &&
                   ПереченьПредметов[Список_Предметов.SelectedIndex] is Предметы.Зелье_маны)
                {
                    Button_Use.IsEnabled = true;
                }
                else
                {
                    Button_Use.IsEnabled = false;
                }
            }

            Info_HP.Content = "Здоровье: " + Персонажи[Выпадающий_Список_Персонажей.SelectedIndex].Очки_Здоровья + " HP";
            if (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] is Персонаж_с_магией)
            {
                Info_MP.Content = "Мана: " + (Персонажи[Выпадающий_Список_Персонажей.SelectedIndex] as Персонаж_с_магией).Очки_Маны + " MP";
            }
            else
            {
                Info_MP.Content = "Мана: N/A";
            }
        }

        private void Button_Use_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Список_Предметов.SelectedItem == "[+25 ОЗ] Малое зелье лечения")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж).Имя)
                    {
                        Текущий_Персонаж.ИзменениеСостоянияЗдоровья("лечение", 25);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
            else if ((string)Список_Предметов.SelectedItem == "[+50 ОЗ] Среднее зелье лечения")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж).Имя)
                    {
                        Текущий_Персонаж.ИзменениеСостоянияЗдоровья("лечение", 50);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
            else if ((string)Список_Предметов.SelectedItem == "[+75 ОЗ] Большое зелье лечения")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж).Имя)
                    {
                        Текущий_Персонаж.ИзменениеСостоянияЗдоровья("лечение", 75);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
            else if ((string)Список_Предметов.SelectedItem == "[+25 ОМ] Бутылек Маны")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж_с_магией).Имя)
                    {
                        (Текущий_Персонаж as Персонаж_с_магией).ИзменениеСостоянияМаны("восполнение", 25);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
            else if ((string)Список_Предметов.SelectedItem == "[+50 ОМ] Фласка маны")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж_с_магией).Имя)
                    {
                        (Текущий_Персонаж as Персонаж_с_магией).ИзменениеСостоянияМаны("восполнение", 50);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
            else if ((string)Список_Предметов.SelectedItem == "[+75 ОМ] Банка маны")
            {
                foreach (Персонаж Текущий_Персонаж in Персонажи)
                {
                    if ((string)Выпадающий_Список_Персонажей.SelectedItem == (Текущий_Персонаж as Персонаж_с_магией).Имя)
                    {
                        (Текущий_Персонаж as Персонаж_с_магией).ИзменениеСостоянияМаны("восполнение", 75);
                    }
                }
                ПереченьПредметов.RemoveAt(SearchForItem((string)Список_Предметов.SelectedItem));
                ReloadInventoryList();
            }
        }

        private void ReloadInventoryList()
        {
            Список_Предметов.Items.Clear();

            foreach (Предметы.Зелье Предмет in ПереченьПредметов.OrderBy(it => it.Название))
            {
                Список_Предметов.Items.Add((Предмет as Предметы.Зелье).Название);
                НазванияПредметов.Add(Предмет.Название);
            }
        }

        private int SearchForItem(string Название_искомого_объекта)
        {
            for (int i = 0; i < НазванияПредметов.Count; i++)
            {
                if (НазванияПредметов[i] == Название_искомого_объекта)
                {
                    return i;
                }
            }
            return -1;
        }

        public List<Предметы.Зелье> GetInventory()
        {
            return ПереченьПредметов;
        }
    }
}
