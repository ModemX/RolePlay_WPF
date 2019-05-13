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
        List<Персонаж> Персонажи;
        List<Предметы.Зелье> ПереченьПредметов;
        List<string> НазванияПредметов = new List<string>();

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
                Button_Use.IsEnabled = true;
            }
        }
        private void Выпадающий_Список_Персонажей_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Список_Предметов.SelectedItem != null && Выпадающий_Список_Персонажей.SelectedIndex != -1)
            {
                Button_Use.IsEnabled = true;
            }
        }

        private void Button_Use_Click(object sender, RoutedEventArgs e)
        {
            if ((string)Список_Предметов.SelectedItem == "Малое зелье лечения [+25 ОЗ]")
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
            else if ((string)Список_Предметов.SelectedItem == "Среднее зелье лечения [+50 ОЗ]")
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
            else if ((string)Список_Предметов.SelectedItem == "Большое зелье лечения [+75 ОЗ]")
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
            else if ((string)Список_Предметов.SelectedItem == "Бутылек Маны [+25 ОМ]")
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
            else if ((string)Список_Предметов.SelectedItem == "Фласка маны [+50 ОМ]")
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
            else if ((string)Список_Предметов.SelectedItem == "Банка маны [+75 ОМ]")
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

            foreach (Предметы.Зелье Предмет in ПереченьПредметов)
            {
                Список_Предметов.Items.Add((Предмет as Предметы.Зелье).Название);
                НазванияПредметов.Add(Предмет.Название);
            }

            ПереченьПредметов.Select(x => x.Название).OrderBy(x => x);
        }

        int SearchForItem(string Название_искомого_объекта)
        {
            for (int i = 0; i < НазванияПредметов.Count; i++) 
            {
                if (НазванияПредметов[i] == Название_искомого_объекта)
                    return i;
            }
            return -1;
        }

        public List<Предметы.Зелье> GetInventory() => ПереченьПредметов;
    }
}
