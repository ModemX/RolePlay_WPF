using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Ролевая_игра__WPF_
{
    public partial class MainWindow : Window
    {
        private AddHero AddHeroWindow;
        private List<object> Персонажи = new List<object>();
        private string ConsoleBuffer;
        private int ТекущийПерсонаж;
        private bool? IsRecentlyAddedHeroMage;

        public MainWindow()
        {
            InitializeComponent();
            Console.Text = "Добро пожаловать, герой. Я твой рассказчик, Система Одностороннего Повествования \nИстории, но можешь называть меня СОПИ ♥. \nДля начала предалагаю начать с создания твоего героя.\n";
            ТекущийПерсонаж = 0;
        }

        private void MainWindow_Button_AddHero_Click(object sender, RoutedEventArgs e)
        {
            AddHeroWindow = new AddHero(Персонажи.Count - 1);
            AddHeroWindow.ShowDialog();
            bool? dialogResult = AddHeroWindow.DialogResult;
            bool flag = true;
            if (dialogResult.GetValueOrDefault() == flag & dialogResult.HasValue)
            {
                Персонаж ВозможныйПерсонаж = AddHeroWindow.Получить_Персонажа();
                if (ВозможныйПерсонаж != null)
                {
                    Персонажи.Add(AddHeroWindow.Получить_Персонажа());
                    bool пол = AddHeroWindow.Пол;
                    if (пол)
                    {
                        Console.Text = $"Появился новый герой! Он {((Персонажи[Персонажи.Count - 1] as Персонаж) as Персонаж).Раса} в возрасте " +
                            $"{Персонаж.Лет_Лета_Год((Персонажи[Персонажи.Count - 1] as Персонаж).Возраст)} и зовут его {(Персонажи[Персонажи.Count - 1] as Персонаж).Имя}\n";
                    }
                    else
                    {
                        Console.Text = $"Появилась новая героиня! Она {(Персонажи[Персонажи.Count - 1] as Персонаж).Раса} в возрасте " +
                            $"{Персонаж.Лет_Лета_Год((Персонажи[Персонажи.Count - 1] as Персонаж).Возраст)} и зовут её {(Персонажи[Персонажи.Count - 1] as Персонаж).Имя}\n";
                    }
                    IsRecentlyAddedHeroMage = new bool?(false);
                }
                else //Персонажи с магией
                {
                    Персонажи.Add(AddHeroWindow.Получить_Персонажа_с_магией());
                    bool пол2 = AddHeroWindow.Пол;
                    if (пол2)
                    {
                        Console.Text = $"Появился новый герой! Он {(Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Раса} с познаниями в магии и в возрасте " +
                            $"{Персонаж.Лет_Лета_Год((Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Возраст)}, а зовут его {(Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Имя}\n";
                    }
                    else
                    {
                        Console.Text = $"Появилась новая героиня! Она {(Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Раса} с познаниями в магии и в возрасте " +
                            $"{Персонаж.Лет_Лета_Год((Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Возраст)}, а зовут её {(Персонажи[Персонажи.Count - 1] as Персонаж_с_магией).Имя}\n";
                    }
                    IsRecentlyAddedHeroMage = new bool?(true);
                }
                MainWindow_Button_ShowInfo.IsEnabled = true;
                ТекущийПерсонаж = Персонажи.Count-1;
            }
            if (Персонажи.Count >= 2)
            {
                MainWindow_Button_SwitchHero.IsEnabled = true;
            }
        }

        private void MainWindow_Button_ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MainWindow_Button_ShowInfo.Content == "Вернуться к \nповествованию")
            {
                MainWindow_Button_ShowInfo.Content = "Состояние текущего \nгероя";
                Console.Text = ConsoleBuffer;
            }
            else
            {
                MainWindow_Button_ShowInfo.Content = "Вернуться к \nповествованию";
                ConsoleBuffer = Console.Text;
                if (IsRecentlyAddedHeroMage == false)
                {
                    Console.Text = (Персонажи[ТекущийПерсонаж] as Персонаж).ToString();
                }
                else //Персонажи с магией
                {
                    Console.Text = (Персонажи[ТекущийПерсонаж] as Персонаж_с_магией).ToString();
                }
            }
        }

        private void MainWindow_Button_SwitchHero_Click(object sender, RoutedEventArgs e)
        {
            if (ТекущийПерсонаж == Персонажи.Count-1)
            {
                ТекущийПерсонаж = 0;
                Console.Text += $"Инициативу принимает {(Персонажи[ТекущийПерсонаж] as Персонаж).Имя}\n";
            }
            else
            {
                ТекущийПерсонаж++;
                Console.Text += $"Инициативу принимает {(Персонажи[ТекущийПерсонаж] as Персонаж).Имя}\n";
            }
        }
    }
}
