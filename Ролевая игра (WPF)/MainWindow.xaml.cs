using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Ролевая_игра__WPF_
{
    public partial class MainWindow : Window
    {
        private AddHero AddHeroWindow;
        private Inventory InventoryWindow;
        public List<Персонаж> Персонажи = new List<Персонаж>();
        public List<bool> Обладает_магией = new List<bool>();
        public List<Предметы.Зелье> ПереченьПредметов = new List<Предметы.Зелье>();
        private string ConsoleBuffer;
        private int ТекущийПерсонаж;
        public int NextAdventureIs = 0;
        private AdventureScripts adventureScripts;
        private bool[] AdventureScriptsNonRepeat = new bool[12];
        /// <summary>
        /// Здоровье: [x1] [x2] [x3]
        /// Эффективность использования заклинаний: [x1.2] [x1] [x0.75]   
        /// Урон: [x1] [x1,5] [x2]
        /// </summary>
        public double[] МножительСилыРун = new double[3] { 1, 1.2, 1 };
        public bool IsBattleMode { get; set; } = false;
        public List<Враги> СписокТекущихВрагов = new List<Враги>();
        Attack AttackDialog;

        public MainWindow()
        {
            InitializeComponent();
            Console.Text = "Добро пожаловать, герой. Я твой рассказчик, Система Одностороннего Повествования Истории, но можешь называть меня СОПИ ♥. \n" +
                "Для начала предалагаю начать с создания твоего героя или загрузить свое прохождение из прошлой жизни.\n";
            ТекущийПерсонаж = 0;

            Menu_Grid.Visibility = Visibility.Visible;
            Adventure_Grid.Visibility = Visibility.Hidden;
        }

        //~MainWindow()
        //{
        //    Close();
        //    InventoryWindow.Close();
        //    AddHeroWindow.Close();
        //    AttackDialog.Close();
        //}

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
                    Обладает_магией.Add(false);
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
                }
                else //Персонажи с магией
                {
                    Персонажи.Add(AddHeroWindow.Получить_Персонажа_с_магией());
                    Обладает_магией.Add(true);
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
                }
                MainWindow_Button_ShowInfo.IsEnabled = true;
                Button_Save.IsEnabled = true;
                Button_Inventory.IsEnabled = true;
                MainWindow_Button_GoForAdventure.IsEnabled = true;
                ТекущийПерсонаж = Персонажи.Count - 1;
            }
            if (Персонажи.Count == 1 || Персонажи.Count == 2)
            {
                MainWindow_Button_SwitchHero.IsEnabled = true;

            }
            if (Персонажи.Count == 3)
            {
                MainWindow_Button_SwitchHero.IsEnabled = true;
                MainWindow_Button_AddHero.IsEnabled = false;
            }
        }

        private void MainWindow_Button_ShowInfo_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MainWindow_Button_ShowInfo.Content == "Вернуться к \nповествованию")
            {
                MainWindow_Button_ShowInfo.Content = "Состояние текущего \nгероя";
                Button_Inventory.IsEnabled = true;
                Button_Load.IsEnabled = true;
                Button_Save.IsEnabled = true;
                Console.Text = ConsoleBuffer;
                MainWindow_Button_SwitchHero.IsEnabled = true;
                MainWindow_Button_GoForAdventure.IsEnabled = true;

            }
            else
            {
                MainWindow_Button_ShowInfo.Content = "Вернуться к \nповествованию";

                Button_Inventory.IsEnabled = false;
                Button_Load.IsEnabled = false;
                Button_Save.IsEnabled = false;
                MainWindow_Button_SwitchHero.IsEnabled = false;
                MainWindow_Button_GoForAdventure.IsEnabled = false;


                ConsoleBuffer = Console.Text;
                if (Обладает_магией[ТекущийПерсонаж] == false)
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
            //Сохранение информации о персонажах
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable
                {
                    TableName = "Персонажи"
                };
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

                for (int i = 0; i < Персонажи.Count; i++)
                {
                    if (Обладает_магией[i] == false)
                    {
                        Персонаж персонаж = Персонажи[i] as Персонаж;
                        DataRow строка = ds.Tables["Персонажи"].NewRow();
                        строка["ID"] = персонаж.ID;
                        строка["Имя"] = персонаж.Имя;
                        строка["Раса"] = персонаж.Раса;
                        строка["Возраст"] = персонаж.Возраст;
                        строка["Пол"] = персонаж.Пол;
                        строка["Обладает_магией"] = "False";
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
                        ds.Tables["Персонажи"].Rows.Add(строка);
                    }
                    else
                    {
                        Персонаж_с_магией персонаж = Персонажи[i] as Персонаж_с_магией;
                        DataRow строка = ds.Tables["Персонажи"].NewRow();
                        строка["ID"] = персонаж.ID;
                        строка["Имя"] = персонаж.Имя;
                        строка["Раса"] = персонаж.Раса;
                        строка["Возраст"] = персонаж.Возраст;
                        строка["Пол"] = персонаж.Пол;
                        строка["Обладает_магией"] = "True";
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
                        строка["Очки_маны"] = персонаж.Очки_Маны;
                        ds.Tables["Персонажи"].Rows.Add(строка);
                    }
                }
                //сохранение информации о тексте в консоли
                dt = new DataTable
                {
                    TableName = "Консоль"
                };
                dt.Columns.Add("Консоль_Текст");
                ds.Tables.Add(dt);
                DataRow СтрокаКонсоли = ds.Tables["Консоль"].NewRow();
                СтрокаКонсоли["Консоль_Текст"] = Console.Text;
                ds.Tables["Консоль"].Rows.Add(СтрокаКонсоли);

                //сохранение информации о предметах в инвентаре
                dt = new DataTable
                {
                    TableName = "Инвентарь"
                };
                dt.Columns.Add("Название");
                dt.Columns.Add("Числовое_значение"); //кол-во предметов либо значение восстановления (HP/MP)
                ds.Tables.Add(dt);
                foreach (Предметы.Зелье Предмет in ПереченьПредметов)
                {
                    DataRow Строка = ds.Tables["Инвентарь"].NewRow();
                    Строка["Название"] = Предмет.Название;

                    if (Предмет is Предметы.Зелье_Лечения)
                    {
                        if (Предмет is Предметы.Малое_Зелье_Лечения)
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Малое_Зелье_Лечения).Количество_Восполняемого_Здоровья;
                        }
                        else if (Предмет is Предметы.Среднее_Зелье_Лечения)
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Среднее_Зелье_Лечения).Количество_Восполняемого_Здоровья;
                        }
                        else
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Большое_Зелье_Лечения).Количество_Восполняемого_Здоровья;
                        }
                    }
                    else
                    {
                        if (Предмет is Предметы.Бутылек_Маны)
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Бутылек_Маны).Количество_Восполняемой_Маны;
                        }
                        else if (Предмет is Предметы.Фласка_маны)
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Фласка_маны).Количество_Восполняемой_Маны;
                        }
                        else
                        {
                            Строка["Числовое_значение"] = (Предмет as Предметы.Банка_маны).Количество_Восполняемой_Маны;
                        }
                    }

                    ds.Tables["Инвентарь"].Rows.Add(Строка);
                }

                ds.WriteXml("SaveFile.svfl");
                MessageBox.Show($"Игра сохранена", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                MessageBoxResult DialogResult = MessageBox.Show("Вы уверены что хотите загрузить игру? Текущая игра будет безвозвратно утеряна", "Подтвердить действие?", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    DoLoadFile();
                }
            }
            Button_Inventory.IsEnabled = true;
            MainWindow_Button_SwitchHero.IsEnabled = true;
            MainWindow_Button_GoForAdventure.IsEnabled = true;
        }

        private void DoLoadFile()
        {
            if (File.Exists("SaveFile.svfl"))
            {
                Персонажи.Clear();
                ПереченьПредметов.Clear();

                //загрузка персонажей
                DataSet ds = new DataSet();
                ds.ReadXml("SaveFile.svfl");
                foreach (DataRow строка in ds.Tables["Персонажи"].Rows)
                {
                    if (строка[5] as string == "True")
                    {
                        Персонажи.Add(new Персонаж_с_магией(
                            Convert.ToUInt32(строка["ID"]),
                            Convert.ToString(строка["Имя"]),
                            Convert.ToBoolean(строка["Пол"]),
                            Convert.ToUInt32(строка["Возраст"]),
                            Convert.ToString(строка["Раса"]),
                            Convert.ToBoolean(строка["Ослаблен"]),
                            Convert.ToBoolean(строка["Болен"]),
                            Convert.ToBoolean(строка["Отравлен"]),
                            Convert.ToBoolean(строка["Парализован"]),
                            Convert.ToBoolean(строка["Мёртв"]),
                            Convert.ToBoolean(строка["Может_говорить"]),
                            Convert.ToBoolean(строка["Может_двигаться"]),
                            Convert.ToUInt32(строка["Максимальное_здоровье"]),
                            Convert.ToUInt32(строка["Очки_Здоровья"]),
                            Convert.ToUInt32(строка["Очки_Опыта"]),
                            Convert.ToUInt32(строка["Максимальная_мана"]),
                            Convert.ToUInt32(строка["Очки_Маны"])
                            ));
                    }
                    else
                    {
                        Персонажи.Add(new Персонаж(
                            Convert.ToUInt32(строка["ID"]),
                            Convert.ToString(строка["Имя"]),
                            Convert.ToBoolean(строка["Пол"]),
                            Convert.ToUInt32(строка["Возраст"]),
                            Convert.ToString(строка["Раса"]),
                            Convert.ToBoolean(строка["Ослаблен"]),
                            Convert.ToBoolean(строка["Болен"]),
                            Convert.ToBoolean(строка["Отравлен"]),
                            Convert.ToBoolean(строка["Парализован"]),
                            Convert.ToBoolean(строка["Мёртв"]),
                            Convert.ToBoolean(строка["Может_говорить"]),
                            Convert.ToBoolean(строка["Может_двигаться"]),
                            Convert.ToUInt32(строка["Максимальное_здоровье"]),
                            Convert.ToUInt32(строка["Очки_Здоровья"]),
                            Convert.ToUInt32(строка["Очки_Опыта"])
                            ));
                    }
                }

                //загрузка консоли
                DataRow СтрокиКонсоли = ds.Tables["Консоль"].Rows[0];
                Console.Text = Convert.ToString(СтрокиКонсоли["Консоль_Текст"]);

                ТекущийПерсонаж = Персонажи.Count - 1;
                foreach (Персонаж Перс in Персонажи)
                {
                    if (Перс is Персонаж_с_магией)
                    {
                        Обладает_магией.Add(true);
                    }
                    else
                    {
                        Обладает_магией.Add(false);
                    }
                }

                //загрузка предметов
                if (ds.Tables["Инвентарь"] != null)
                {
                    foreach (DataRow строка in ds.Tables["Инвентарь"].Rows)
                    {
                        if (строка[0] as string == "[+25 ОЗ] Малое зелье лечения")
                        {
                            ПереченьПредметов.Add(new Предметы.Малое_Зелье_Лечения());
                        }
                        else if (строка[0] as string == "[+50 ОЗ] Среднее зелье лечения")
                        {
                            ПереченьПредметов.Add(new Предметы.Среднее_Зелье_Лечения());
                        }
                        else if (строка[0] as string == "[+75 ОЗ] Большое зелье лечения")
                        {
                            ПереченьПредметов.Add(new Предметы.Большое_Зелье_Лечения());
                        }
                        else if (строка[0] as string == "[+25 ОМ] Бутылек Маны")
                        {
                            ПереченьПредметов.Add(new Предметы.Бутылек_Маны());
                        }
                        else if (строка[0] as string == "[+50 ОМ] Фласка маны")
                        {
                            ПереченьПредметов.Add(new Предметы.Фласка_маны());
                        }
                        else if (строка[0] as string == "[+75 ОМ] Банка маны")
                        {
                            ПереченьПредметов.Add(new Предметы.Банка_маны());
                        }
                    }
                }

                InventoryWindow = new Inventory(Персонажи, ПереченьПредметов);

                MainWindow_Button_ShowInfo.IsEnabled = true;
                Button_Save.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Файл сохранения не найден. Убедитесь что файл сохранения располагается в директории с игрой", "Ошибка.", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Inventory_Click(object sender, RoutedEventArgs e)
        {
            InventoryWindow = new Inventory(Персонажи, ПереченьПредметов);
            InventoryWindow.ShowDialog();
            ПереченьПредметов = InventoryWindow.GetInventory();
        }

        private void MainWindow_Button_GoForAdventure_Click(object sender, RoutedEventArgs e)
        {
            Menu_Grid.Visibility = Visibility.Hidden;
            Adventure_Grid.Visibility = Visibility.Visible;

            adventureScripts = new AdventureScripts(NextAdventureIs, this);
        }

        public string Get_Button_Choice_1()
        {
            return (string)Button_Choice_1.Content;
        }

        public void Change_Button_Choice_1(string Text)
        {
            Button_Choice_1.Content = Text;
        }

        public string Get_Button_Choice_2()
        {
            return (string)Button_Choice_2.Content;
        }

        public void Change_Button_Choice_2(string Text)
        {
            Button_Choice_2.Content = Text;
        }

        public string Get_Button_Choice_3()
        {
            return (string)Button_Choice_3.Content;
        }

        public void Change_Button_Choice_3(string Text)
        {
            Button_Choice_3.Content = Text;
        }

        private void Button_Choice_1_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBattleMode)
            {
                adventureScripts.Choices_Add(1);
                adventureScripts.Воспроизведение_Шагов();
            }
            else
            {
                int OrderOf = adventureScripts.OrderOf;
                List<object> ПорядокАтаки = new List<object>();

                foreach (Персонаж Персонаж in Персонажи)
                {
                    Персонаж.МножительСилыРун = МножительСилыРун;
                    ПорядокАтаки.Add(Персонаж);
                }
                foreach (Враги Враг in СписокТекущихВрагов)
                {
                    ПорядокАтаки.Add(Враг);
                }

                AttackDialog = new Attack(СписокТекущихВрагов, Персонажи[OrderOf]);
                AttackDialog.ShowDialog();
                if (AttackDialog.DialogResult == true)
                {
                    СписокТекущихВрагов = AttackDialog.GetСписокТекущихВрагов();
                    if (Персонажи[OrderOf].Пол == true)
                    {
                        ConsoleWriteLine($"CОПИ: {Персонажи[OrderOf].Имя} нанёс {AttackDialog.ЗначениеАтаки} единиц урона здоровью цели: " +
                        $"{СписокТекущихВрагов[AttackDialog.SelectedEnemy].ИмяВрага}. Текущие значение здоровья врага стало " +
                        $"{СписокТекущихВрагов[AttackDialog.SelectedEnemy].ЗдоровьеВрага} единиц здоровья.");
                    }
                    else
                    {
                        ConsoleWriteLine($"CОПИ: {Персонажи[OrderOf].Имя} нанесла {AttackDialog.ЗначениеАтаки} единиц урона здоровью цели: " +
                        $"{СписокТекущихВрагов[AttackDialog.SelectedEnemy].ИмяВрага}. Текущие значение здоровья врага стало " +
                        $"{СписокТекущихВрагов[AttackDialog.SelectedEnemy].ЗдоровьеВрага} единиц здоровья.");

                    }
                    Random random = new Random();
                    Персонажи[OrderOf].Добавить_ОчкиОпыта((uint)random.Next(100, 150));

                    adventureScripts.IncreaseOrderOf();
                }
                adventureScripts.Battle(СписокТекущихВрагов, ПорядокАтаки);

            }
        }

        private void Button_Choice_2_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBattleMode)
            {
                adventureScripts.Choices_Add(2);
                adventureScripts.Воспроизведение_Шагов();
            }
        }

        private void Button_Choice_3_Click(object sender, RoutedEventArgs e)
        {
            if (!IsBattleMode)
            {
                adventureScripts.Choices_Add(3);
                adventureScripts.Воспроизведение_Шагов();
            }
        }

        public void Block_Button_Choice_1()
        {
            Button_Choice_1.Visibility = Visibility.Hidden;
        }

        public void UnBlock_Button_Choice_1()
        {
            Button_Choice_1.Visibility = Visibility.Visible;
        }

        public void Block_Button_Choice_2()
        {
            Button_Choice_2.Visibility = Visibility.Hidden;
        }

        public void UnBlock_Button_Choice_2()
        {
            Button_Choice_2.Visibility = Visibility.Visible;
        }

        public void Block_Button_Choice_3()
        {
            Button_Choice_3.Visibility = Visibility.Hidden;
        }

        public void UnBlock_Button_Choice_3()
        {
            Button_Choice_3.Visibility = Visibility.Visible;
        }

        public void ConsoleWriteLine(string Text)
        {
            Console.Text += '\n';
            Console.Text += '\n';
            Console.Text += Text;
            ScrollViewer.ScrollToEnd();
        }

        private void Button_GiveUp_Click(object sender, RoutedEventArgs e)
        {
            IsBattleMode = false;
            adventureScripts.Choices_Add(1);
            adventureScripts.Choices_Add(1);

            if (Персонажи.Count == 1 && Персонажи[0].Пол == true)
            {
                ConsoleWriteLine("- Стража! У нас предатель и трус! Убить героя!");
            }
            else if (Персонажи.Count == 1 && Персонажи[0].Пол == false)
            {
                ConsoleWriteLine("- Стража! У нас предательница и трусиха! Убить героиню!");
            }
            else
            {
                ConsoleWriteLine("- Стража! У нас предатели и трусы! Убить героев!");
            }

            foreach (Персонаж персонаж in Персонажи)
            {
                персонаж.ИзменениеСостоянияЗдоровья("урон", 1000);
            }

            adventureScripts.GameOver();
        }
    }
}
