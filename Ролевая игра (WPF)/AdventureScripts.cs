using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Ролевая_игра__WPF_
{
    public class AdventureScripts
    {
        private List<int> Choices = new List<int>();
        private MainWindow MainWindow;
        public int OrderOf { get; private set; } = 0;
        public AdventureScripts(int CaseOfAdventure, MainWindow MainForm)
        {
            MainWindow = MainForm;

            if (Choices.Count == 0)
            {
                Начало_приключения();
            }
            else
            {
                Воспроизведение_Шагов();
            }
        }

        public void Choices_Add(int value)
        {
            Choices.Add(value);
        }

        public int Choices_GetLast => Choices.Last();


        private void Начало_приключения()
        {
            if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == true)
            {
                MainWindow.ConsoleWriteLine($"СОПИ: Герой-одиночка был призван в этот мир могущественными волшебниками королевства Мелромарк, " +
                    $"дабы спасти всех в королевстве от предсказанного уничтожения нашествиями волн монстров из другого измерения. " +
                    $"Вы были призваны в тёмной комнате отдающей сыростью, в окружении магов, внемлющих вашим первым словам. Что стоит им сказать?");
            }
            else if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == false)
            {
                MainWindow.ConsoleWriteLine($"СОПИ: Героиня-одиночка была призвана в этот мир могущественными волшебниками королества Мелромарк, " +
                    $"дабы спасти всех в королевстве от предсказанного уничтожения нашествиями волн монстров из другого измерения. " +
                    $"Вы были призваны в тёмной комнате отдающей сыростью, в окружении магов, внемлющих вашим первым словам. Что стоит им сказать?");
            }
            else if (MainWindow.Персонажи.Count == 1)
            {
                MainWindow.ConsoleWriteLine($"СОПИ: Двое героев были призваны в этот мир могущественными волшебниками королества Мелромарк, " +
                    $"дабы спасти всех в королевстве от предсказанного уничтожения нашествиями волн мостров из другого измерения. " +
                    $"Вы вдвоём были призваны в тёмной комнате отдающей сыростью, в окружении магов, внемлющих вашим первым словам. Что должны сказать герои?");
            }
            else
            {
                MainWindow.ConsoleWriteLine($"СОПИ: Трое героев были призваны в этот мир могущественными волшебниками королества Мелромарк, " +
                    $"дабы спасти всех в королевстве от предсказанного уничтожения нашествиями волн мостров из другого измерения. " +
                    $"Вы втроём были призваны в тёмной комнате отдающей сыростью, в окружении магов, внемлющих вашим первым словам. Что должны сказать герои?");
            }

            MainWindow.Change_Button_Choice_1("Привет!");
            MainWindow.Change_Button_Choice_2("Как дела?");
            MainWindow.Change_Button_Choice_3("Кто вы?");

        }

        public void Воспроизведение_Шагов()
        {
            if (Choices.Count == 1 && Choices[0] == 1)
            {
                if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == true)
                {
                    MainWindow.ConsoleWriteLine($"- Герой, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герой, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжен это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == false)
                {
                    MainWindow.ConsoleWriteLine($"- Героиня, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, героиня, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжна это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count >= 1)
                {
                    MainWindow.ConsoleWriteLine($"- Герои, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герои, спасите нас!");
                    MainWindow.Change_Button_Choice_1("Как мы \nдолжны это \nсделать?");
                }

                MainWindow.Block_Button_Choice_2();
                MainWindow.Block_Button_Choice_3();
            }
            if (Choices.Count == 1 && Choices[0] == 2)
            {
                if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == true)
                {
                    MainWindow.ConsoleWriteLine($"- Герой, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герой, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжен это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == false)
                {
                    MainWindow.ConsoleWriteLine($"- Героиня, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, героиня, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжна это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count >= 1)
                {
                    MainWindow.ConsoleWriteLine($"- Герои, наше королество в опасности, на землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герои, спасите нас!");
                    MainWindow.Change_Button_Choice_1("Как мы \nдолжны это \nсделать?");
                }

                MainWindow.Block_Button_Choice_2();
                MainWindow.Block_Button_Choice_3();
            }
            if (Choices.Count == 1 && Choices[0] == 3)
            {
                if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == true)
                {
                    MainWindow.ConsoleWriteLine($"- Мы - маги королевства Мелромарк и оно в опасности. На землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герой, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжен это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count == 0 && MainWindow.Персонажи[0].Пол == false)
                {
                    MainWindow.ConsoleWriteLine($"- Мы - маги королевства Мелромарк и оно в опасности. На землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, героиня, спаси нас!");
                    MainWindow.Change_Button_Choice_1("Как я \nдолжна это \nсделать?");
                }
                else if (MainWindow.Персонажи.Count >= 1)
                {
                    MainWindow.ConsoleWriteLine($"- Мы - маги королевства Мелромарк и оно в опасности. На землях Мелромарка лежит тяжелая ноша, сражение с волнами монстров. " +
                        $"Мы пытались отбить волны своими силами, но треть населения нашего королевства была жестоко убита. Пожалуйста, герои, спасите нас!");
                    MainWindow.Change_Button_Choice_1("Как мы \nдолжны это \nсделать?");
                }

                MainWindow.Block_Button_Choice_2();
                MainWindow.Block_Button_Choice_3();
            }
            if (Choices.Count == 2)
            {
                MainWindow.ConsoleWriteLine($"- Единственное что мы можем вам дать это несколько рун, " +
                    $"усиливающие ваши способности и немного целебных и магических снадобий. Используйте их с умом!");

                MainWindow.ПереченьПредметов.Add(new Предметы.Малое_Зелье_Лечения());
                MainWindow.ПереченьПредметов.Add(new Предметы.Среднее_Зелье_Лечения());
                MainWindow.ПереченьПредметов.Add(new Предметы.Большое_Зелье_Лечения());
                MainWindow.ПереченьПредметов.Add(new Предметы.Бутылек_Маны());
                MainWindow.ПереченьПредметов.Add(new Предметы.Фласка_маны());
                MainWindow.ПереченьПредметов.Add(new Предметы.Банка_маны());

                MainWindow.ConsoleWriteLine("СОПИ: Предметы получены: Руна Здоровья (Ур. 1 из 3). " +
                    "Описание: Увеличивает количество здоровья у персонажей. Обладает множителем \"x1\"");
                MainWindow.ConsoleWriteLine("СОПИ: Предметы получены: Руна Эффективности использования заклинаний (Ур. 1 из 3). " +
                    "Описание: Уменьшает количество затрат маны на использование заклинаний у персонажей. Обладает множителем \"x1.2\"");
                MainWindow.ConsoleWriteLine("СОПИ: Предметы получены: Руна Урона (Ур. 1 из 3). " +
                    "Описание: Увеличивает количество наносимого урона персонажами. Обладает множителем \"x1\"");

                MainWindow.Change_Button_Choice_1("Спасибо!");
            }
            if (Choices.Count == 3)
            {
                MainWindow.ConsoleWriteLine($"- А теперь, пожалуйста, чтобы мы могли оценить насколько вы сильны, сразитесь с одним из нашей стражи при короле. Пройдёмте...");

                MainWindow.ConsoleWriteLine($"СОПИ: Вы следуете за магами в тренировочный зал. Выходя из подвала, " +
                    $"вы впервые видите как роскошно выглядит поместье 21 века, вместе с широкими красными " +
                    $"коврами и прекрасными ухоженными садами, среди не менее величественных картин и гобеленов.");

                MainWindow.Change_Button_Choice_1("Продолжить \nследовать");
            }
            if (Choices.Count == 4)
            {
                MainWindow.ConsoleWriteLine("СОПИ: Вы следуете за магами почти сквозь всё поместье к огромному " +
                    "тренировочному залу где вас уже поджидала стража. Увидев подходящих магов самый первый из их ряда " +
                    "вышел на один шаг вперед, предъявляя свою кандидатуру для вашей проверки.");

                MainWindow.ConsoleWriteLine("Вы будете сражаться с главой нашей стражи, покажите что умеете и пожалуйста, не убейте его. Он нам ещё нужен.");

                MainWindow.ConsoleWriteLine("СОПИ: Ваша задача - опустить здоровье противника до 50%");

                MainWindow.Change_Button_Choice_1("[Принять \nвызов]");
            }
            if (Choices.Count == 5)
            {
                MainWindow.IsBattleMode = true;

                MainWindow.Change_Button_Choice_1("Атака");
                MainWindow.Change_Button_Choice_2("Использовать \nзаклинание");
                MainWindow.Change_Button_Choice_3("Защита");
                MainWindow.Button_GiveUp.IsEnabled = true;

                MainWindow.СписокТекущихВрагов.Add(new Враги.ГлаваСтражи());

                for (int i = 0; i < MainWindow.Персонажи.Count; i++)
                {
                    Персонаж Персонаж = MainWindow.Персонажи.ElementAt(i);
                    MainWindow.ПорядокАтаки.Add(Персонаж);
                }
                foreach (Враги Враг in MainWindow.СписокТекущихВрагов)
                {
                    MainWindow.ПорядокАтаки.Add(Враг);
                }

                Battle(MainWindow.СписокТекущихВрагов, MainWindow.ПорядокАтаки, MainWindow.СписокГероевВЗащите);
            }
            if (Choices.Count == 6)
            {
                MainWindow.ConsoleWriteLine("- Достаточно, мы увидели что хотели. Мы можем не переживать за своё королевство. В награду " +
                    "наши лекари вылечат вас, восполнят запасы маны и лично мы выдаём вам улучшенные руны. А сейчас идите отдыхайте. Поговорим о делах завтра.");

                foreach (Персонаж персонаж in MainWindow.Персонажи)
                {
                    персонаж.ИзменениеСостоянияЗдоровья("лечение", 1000);
                }
                MainWindow.ConsoleWriteLine("СОПИ: Здоровье всех персонажей восстановлено.");

                foreach (Персонаж персонаж in MainWindow.Персонажи)
                {
                    if (персонаж is Персонаж_с_магией)
                    {
                        (персонаж as Персонаж_с_магией).ИзменениеСостоянияМаны("восполнение", 1000);
                    }
                }
                MainWindow.ConsoleWriteLine("СОПИ: Мана всех персонажей восполнена.");

                MainWindow.SetRuneRank(2);
                MainWindow.ConsoleWriteLine("СОПИ: Показатели рун улучшены. ");
                MainWindow.ConsoleWriteLine("СОПИ: Предметы улучшены: Руна Здоровья (Ур. 2 из 3). " +
                    "Описание: Увеличивает количество здоровья у персонажей. Обладает множителем \"x2\"");
                MainWindow.ConsoleWriteLine("СОПИ: Предметы улучшены: Руна Эффективности использования заклинаний (Ур. 2 из 3). " +
                    "Описание: Уменьшает количество затрат маны на использование заклинаний у персонажей. Обладает множителем \"x1\"");
                MainWindow.ConsoleWriteLine("СОПИ: Предметы улучшены: Руна Урона (Ур. 2 из 3). " +
                    "Описание: Увеличивает количество наносимого урона персонажами. Обладает множителем \"x2\"");

                MainWindow.Block_Button_Choice_2();
                MainWindow.Block_Button_Choice_3();

                MainWindow.Change_Button_Choice_1("[Пойти отдыхать]");
                MainWindow.Change_Button_Choice_2("");
                MainWindow.Change_Button_Choice_3("");
            }
            if (Choices.Count == 7)
            {
                MainWindow.ConsoleWriteLine("\n");
                MainWindow.ConsoleWriteLine("\n");
                MainWindow.ConsoleWriteLine("Демоверсия окончена.");

                GameOver();
            }
            if (Choices.Count == 8)
            {
                MainWindow.Close();
            }
        }
        public void Battle(List<Враги> СписокТекущихВрагов, List<object> ПорядокАтаки, bool[] СписокГероевВЗащите)
        {
            if (MainWindow.ПорядокАтаки[OrderOf] is Персонаж_с_магией)
                MainWindow.UnBlock_Button_Choice_2();
            else
            {
                MainWindow.Block_Button_Choice_2();
            }
            MainWindow.UnBlock_Button_Choice_3();

            MainWindow.ПорядокАтаки = ПорядокАтаки;
            if (AreEnemiesAlive(СписокТекущихВрагов))
            {
                if (AreHeroesAlive(MainWindow.Персонажи))
                {
                    if (ПорядокАтаки[OrderOf] is Враги)
                    {
                        if ((ПорядокАтаки[OrderOf] as Враги).IsFrozen == false)
                        {
                            Random random = new Random();
                            int СлучайныйПерсонаж = random.Next(0, MainWindow.Персонажи.Count);
                            if (СписокГероевВЗащите[СлучайныйПерсонаж] == true)
                            {
                                MainWindow.Персонажи[СлучайныйПерсонаж].ИзменениеСостоянияЗдоровья("урон", (uint)Math.Round(0.5 * (ПорядокАтаки[OrderOf] as Враги).СилаАтаки));
                            }
                            else
                            {
                                MainWindow.Персонажи[СлучайныйПерсонаж].ИзменениеСостоянияЗдоровья("урон", (uint)(ПорядокАтаки[OrderOf] as Враги).СилаАтаки);
                            }

                            if (СписокГероевВЗащите[СлучайныйПерсонаж])
                            {
                                MainWindow.ConsoleWriteLine($"CОПИ: {(ПорядокАтаки[OrderOf] as Враги).ИмяВрага} нанёс {MainWindow.Персонажи[СлучайныйПерсонаж].Имя} " +
                                $"{0.5 * (ПорядокАтаки[OrderOf] as Враги).СилаАтаки} единиц урона (а должен был: {(ПорядокАтаки[OrderOf] as Враги).СилаАтаки} единиц урона). " +
                                $"У {MainWindow.Персонажи[СлучайныйПерсонаж].Имя} осталось {MainWindow.Персонажи[СлучайныйПерсонаж].Очки_Здоровья} единиц здоровья.");
                            }
                            else
                            {
                                MainWindow.ConsoleWriteLine($"CОПИ: {(ПорядокАтаки[OrderOf] as Враги).ИмяВрага} нанёс {MainWindow.Персонажи[СлучайныйПерсонаж].Имя} " +
                                $"{(ПорядокАтаки[OrderOf] as Враги).СилаАтаки} единиц урона. У {MainWindow.Персонажи[СлучайныйПерсонаж].Имя} осталось " +
                                $"{MainWindow.Персонажи[СлучайныйПерсонаж].Очки_Здоровья} единиц здоровья.");

                            }
                        }
                        IncreaseOrderOf();

                        if (MainWindow.ПорядокАтаки[OrderOf] is Персонаж_с_магией)
                            MainWindow.UnBlock_Button_Choice_2();
                        else
                        {
                            MainWindow.Block_Button_Choice_2();
                        }
                        MainWindow.UnBlock_Button_Choice_3();
                    }
                }
                else
                {
                    GameOver(); //все герои умерли
                }
            }
            else
            {
                MainWindow.IsBattleMode = false;
                Choices_Add(1);
                Воспроизведение_Шагов();
            }
        }
        public bool AreEnemiesAlive(List<Враги> СписокТекущихВрагов)
        {
            if (СписокТекущихВрагов[0].ЗдоровьеВрага < 0.5 * СписокТекущихВрагов[0].МаксимальноеЗдоровье && СписокТекущихВрагов[0] is Враги.ГлаваСтражи)
            {
                MainWindow.IsBattleMode = false;
                MainWindow.СписокТекущихВрагов.Clear();
            }

            if (СписокТекущихВрагов.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool AreHeroesAlive(List<Персонаж> Персонажи)
        {
            int КоличествоУмершихГероев = 0;

            for (int i = 0; i < Персонажи.Count; i++)
            {
                Персонаж персонаж = Персонажи[i];
                if (персонаж.Состояние[4] == true)
                {
                    КоличествоУмершихГероев++;
                }
            }

            if (КоличествоУмершихГероев == 0)
            {
                return true;
            }
            else if (КоличествоУмершихГероев == 1 && Персонажи.Count != КоличествоУмершихГероев)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void IncreaseOrderOf()
        {

            if(MainWindow.ПорядокАтаки[OrderOf] is Персонаж_с_магией)
            {
                MainWindow.UnBlock_Button_Choice_2();
            }
            else
            {
                MainWindow.Block_Button_Choice_2();
            }

            if (OrderOf == MainWindow.ПорядокАтаки.Count - 1)
            {
                OrderOf = 0;
                for (int i = 0; i < MainWindow.СписокГероевВЗащите.Length; i++)
                {
                    MainWindow.СписокГероевВЗащите[i] = false;
                }
                foreach (var Существо in MainWindow.ПорядокАтаки)
                {
                    if(Существо is Враги)
                    {
                        (Существо as Враги).IsFrozen = false;
                    }
                }
            }
            else
            {
                OrderOf++;
            }
        }
        public void GameOver()
        {
            MainWindow.Button_Choice_2.Visibility = Visibility.Hidden;
            MainWindow.Button_Choice_3.Visibility = Visibility.Hidden;
            MainWindow.Button_GiveUp.Visibility = Visibility.Hidden;
            MainWindow.Button_Inventory.Visibility = Visibility.Hidden;

            int КоличествоМёртвыхГероев = 0;
            foreach (Персонаж персонаж in MainWindow.Персонажи)
            {
                if (персонаж.Состояние[4] == true)
                {
                    КоличествоМёртвыхГероев++;
                }
            }
            if (КоличествоМёртвыхГероев == MainWindow.Персонажи.Count)
            {
                MainWindow.ConsoleWriteLine("СОПИ: Игра окончена, все герои умерли");
            }
            else
            {
                MainWindow.ConsoleWriteLine("СОПИ: Игра окончена.");
            }

            int ОбщийСчет = 0;

            foreach (Персонаж персонаж in MainWindow.Персонажи)
            {
                if (персонаж.Пол == true)
                {
                    MainWindow.ConsoleWriteLine($"СОПИ: В течении прохождения игры герой {персонаж.Имя} получил {персонаж.Очки_Опыта} очков опыта.");
                }
                else if (персонаж.Пол == false)
                {
                    MainWindow.ConsoleWriteLine($"СОПИ: В течении прохождения игры героиня {персонаж.Имя} получила {персонаж.Очки_Опыта} очков опыта.");
                }
                ОбщийСчет += (int)персонаж.Очки_Опыта;
            }

            MainWindow.ConsoleWriteLine($"СОПИ: Общий счет игрока составил: {ОбщийСчет} очков.");

            MainWindow.Change_Button_Choice_1("[Закрыть \nигру]");
        }
    }
}