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
        List<Предметы> ПереченьПредметов;

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

            ПереченьПредметов.Select(x => x.Название).OrderBy(x => x);

            foreach (Предметы.Зелье Предмет in ПереченьПредметов)
            {
                if (Предмет is Предметы.Малое_Зелье_Лечения)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Малое_Зелье_Лечения).Название);
                }
                else if (Предмет is Предметы.Среднее_Зелье_Лечения)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Среднее_Зелье_Лечения).Название);
                }
                else if (Предмет is Предметы.Большое_Зелье_Лечения)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Большое_Зелье_Лечения).Название);
                }
                else if (Предмет is Предметы.Бутылек_Маны)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Бутылек_Маны).Название);
                }
                else if (Предмет is Предметы.Фласка_маны)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Фласка_маны).Название);
                }
                else if (Предмет is Предметы.Банка_маны)
                {
                    Список_Предметов.Items.Add((Предмет as Предметы.Банка_маны).Название);
                }
            }
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
                ПереченьПредметов.Remove(new Предметы.Малое_Зелье_Лечения());
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
                ПереченьПредметов.Remove(new Предметы.Среднее_Зелье_Лечения());
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
                ПереченьПредметов.Remove();
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
                ПереченьПредметов.Remove(new Предметы.Бутылек_Маны());
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
                ПереченьПредметов.Remove(new Предметы.Фласка_маны());
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
                ПереченьПредметов.Remove(new Предметы.Банка_маны());
                ReloadInventoryList();
            }
        }

        private void ReloadInventoryList()
        {
            Список_Предметов.Items.Clear();

            ПереченьПредметов.Select(x => x.Название).OrderBy(x => x);

            foreach (object Предмет in ПереченьПредметов)
            {
                Список_Предметов.Items.Add((Предмет as Предметы.Зелье).Название);
            }
        }
    }
}
