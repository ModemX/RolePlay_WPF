using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Data;
using System.IO;

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
            AddHeroWindow = new AddHero(Персонажи.Count);
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
                Button_Save.IsEnabled = true;
                //Button_Inventory.IsEnabled = true;
                ТекущийПерсонаж = Персонажи.Count - 1;
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
            if (ТекущийПерсонаж == Персонажи.Count - 1)
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

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.TableName = "Персонажи";
                dt.Columns.Add("ID");
                dt.Columns.Add("Имя");
                dt.Columns.Add("Раса");
                dt.Columns.Add("Возраст");
                dt.Columns.Add("Пол");
                dt.Columns.Add("Обладает_магией");
                dt.Columns.Add("Ослаблен");
                dt.Columns.Add("Болен");
                dt.Columns.Add("Отравлен");
                dt.Columns.Add("Парализован");
                dt.Columns.Add("Мёртв");
                dt.Columns.Add("Может_говорить");
                dt.Columns.Add("Может_двигаться");
                dt.Columns.Add("Максимальное_здоровье");
                dt.Columns.Add("Очки_здоровья");
                dt.Columns.Add("Очки_опыта");
                dt.Columns.Add("Максимальная_мана");
                dt.Columns.Add("Очки_маны");
                ds.Tables.Add(dt);

                foreach (Персонаж_с_магией персонаж in Персонажи)
                {
                    DataRow строка = ds.Tables["Персонажи"].NewRow();
                    строка["ID"] = персонаж.ID;
                    строка["Имя"] = персонаж.Имя;
                    строка["Раса"] = персонаж.Раса;
                    строка["Возраст"] = персонаж.Возраст;
                    строка["Пол"] = персонаж.Пол;
                    if(персонаж.Максимальная_мана == 0)
                        строка["Обладает_магией"] = "false";
                    else
                        строка["Обладает_магией"] = "true";
                    строка["Ослаблен"] = персонаж.Состояние[0];
                    строка["Болен"] = персонаж.Состояние[1];
                    строка["Отравлен"] = персонаж.Состояние[2];
                    строка["Парализован"] = персонаж.Состояние[3];
                    строка["Мёртв"] = персонаж.Состояние[4];
                    строка["Может_говорить"] = персонаж.Может_говорить;
                    строка["Может_двигаться"] = персонаж.Может_двигаться;
                    строка["Максимальное_здоровье"] = персонаж.Максимальное_здоровье;
                    строка["Очки_здоровья"] = персонаж.Очки_Здоровья;
                    строка["Очки_опыта"] = персонаж.Очки_Опыта;
                    строка["Максимальная_мана"] = персонаж.Максимальная_мана;
                    строка["Очки_маны"] = персонаж.Очки_Опыта;
                    ds.Tables["Персонажи"].Rows.Add(строка);
                }
                ds.WriteXml("SaveFile.svfl");
                MessageBox.Show("Игра сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            catch (Exception ex)
            {
                StreamWriter streamWriter = new StreamWriter("ErrorLog.log");
                streamWriter.Write(ex);
                MessageBox.Show($"Произошла ошибка при сохранении. В директории с игрой был создан лог файл для отладки", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                streamWriter.Close();
            }
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            if (Персонажи.Count == 0)
            {
                DoLoadFile();
            }
            else
            {
                var DialogResult = MessageBox.Show("Вы уверены что хотите загрузить игру? Текущая игра будет безвозвратно утеряна", "Подтвердить действие?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if(DialogResult == MessageBoxResult.Yes)
                {
                    DoLoadFile();
                }
            }
        }
        void DoLoadFile()
        {
            if (File.Exists("SaveFile.svfl")) // если существует данный файл
            {
                DataSet ds = new DataSet(); // создаем новый пустой кэш данных
                ds.ReadXml("SaveFile.svfl"); // записываем в него XML-данные из файла

                foreach (DataRow строка in ds.Tables["Персонажи"].Rows)
                {
                    
                    //if (строка["Обладает_магией"] == true)
                    //    Персонаж_с_магией ЗагруженныйПерсонаж = new Персонаж_с_магией(ЗагруженныйПерсонаж.ID);
                    //else
                    //    Персонаж ЗагруженныйПерсонаж = new Персонаж(ЗагруженныйПерсонаж.ID);
                    //ЗагруженныйПерсонаж.ID = ;
                    //ЗагруженныйПерсонаж.Имя = ;
                    //ЗагруженныйПерсонаж.Раса = ;
                    //ЗагруженныйПерсонаж.Возраст = ;
                    //ЗагруженныйПерсонаж.Пол = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //ЗагруженныйПерсонаж. = ;
                    //Персонажи.Add()
                }
            }
            else
            {
                MessageBox.Show("XML файл не найден.", "Ошибка.");
            }
        }
    }
}
