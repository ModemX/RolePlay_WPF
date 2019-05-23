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
    public partial class AddHero : Window
    {
        uint RecievedID;

        bool IsAgeAcceptable = false;
        bool IsNameAcceptable = false;
        bool IsMagicChecked = false;

        Персонаж Новый_персонаж = null;
        Персонаж_с_магией Новый_персонаж_с_магией = null;

        public string Имя { get; private set; }
        public string Раса { get; private set; }
        public uint Возраст { get; private set; }
        public bool Пол { get; private set; }

        private double[] МножительСилыРун;

        public AddHero(int ID, double[] МножительСилыРун)
        {
            InitializeComponent();
            RecievedID = (uint)ID;
            GroupBox_Race.Visibility = Visibility.Hidden;
            GroupBox_HaveMagic.Visibility = Visibility.Hidden;
            this.МножительСилыРун = МножительСилыРун;
        }

        public Персонаж Получить_Персонажа() => Новый_персонаж;
        public Персонаж_с_магией Получить_Персонажа_с_магией() => Новый_персонаж_с_магией;

        private void Button_Accept_Click(object sender, RoutedEventArgs e)
        {
            if (Hero_DoHaveMagic.IsChecked == true)
            {
                Новый_персонаж_с_магией = new Персонаж_с_магией(RecievedID, Имя, Пол, Возраст, Раса, МножительСилыРун);
            }
            else
            {
                Новый_персонаж = new Персонаж(RecievedID, Имя, Пол, Возраст, Раса, МножительСилыРун);
            }
            DialogResult = true;
            Close();
        }

        void TextBlock_Clear(object sender, MouseEventArgs e)
        {
            TextBlock_Description.Text = "";
        }
        void TextBlock_Change(string Text)
        {
            TextBlock_Description.Text = Text;
        }

        private void RadioButton_Hero_Male_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Race.Visibility = Visibility.Visible;
            RadioButton_Race_Elf.Content = "Эльф";
            Пол = true;
            CheckAllRequaredFields();
        }
        private void RadioButton_Hero_Female_Click(object sender, RoutedEventArgs e)
        {
            GroupBox_Race.Visibility = Visibility.Visible;
            RadioButton_Race_Elf.Content = "Эльфийка";
            Пол = false;
            CheckAllRequaredFields();
        }

        //проверка ввода возраста
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e) 
        {
            bool AgeHasInvalidInput = false;

            if (TextBox_Age.Text.Length == 0)
                AgeHasInvalidInput = true;

            foreach (char symbol in TextBox_Age.Text)
            {
                if (!char.IsDigit(symbol))
                {
                    AgeHasInvalidInput = true;
                    break;
                }
            }

            if (AgeHasInvalidInput == false)
            {
                IsAgeAcceptable = true;
                Возраст = Convert.ToUInt32(TextBox_Age.Text);
            }
            else
                IsAgeAcceptable = false;

            CheckAllRequaredFields();
        }
        //проверка ввода имени
        private void TextBox_NameOfHero_TextChanged(object sender, TextChangedEventArgs e)
        {
            bool NameHasInvalidInput = false;
            char PreviousSymbol = 'a';
            foreach (char symbol in TextBox_NameOfHero.Text)
            {
                if (!char.IsLetter(symbol) || symbol == ' ' && PreviousSymbol == ' ')
                {
                    NameHasInvalidInput = true;
                    break;
                }
                PreviousSymbol = symbol;
            }
            if (NameHasInvalidInput == false)
            {
                IsNameAcceptable = true;
                Имя = TextBox_NameOfHero.Text;
            }
            else
                IsNameAcceptable = false;
            CheckAllRequaredFields();
        }

        void CheckAllRequaredFields()
        {
            if (IsNameAcceptable && IsAgeAcceptable && IsMagicChecked)
                Button_Accept.IsEnabled = true;
            else
                Button_Accept.IsEnabled = false;
        }

        private void RadioButton_Race_Human_Checked(object sender, RoutedEventArgs e)
        {
            GroupBox_HaveMagic.Visibility = Visibility.Visible;
            Раса = "человек";
            CheckAllRequaredFields();
        }
        private void RadioButton_Race_Dwarf_Checked(object sender, RoutedEventArgs e)
        {
            GroupBox_HaveMagic.Visibility = Visibility.Visible;
            Раса = "гном";
            CheckAllRequaredFields();
        }
        private void RadioButton_Race_Elf_Checked(object sender, RoutedEventArgs e)
        {
            GroupBox_HaveMagic.Visibility = Visibility.Visible;
            if (Пол == true)
                Раса = "эльф";
            else
                Раса = "эльфийка";
            CheckAllRequaredFields();
        }
        private void RadioButton_Race_Ork_Checked(object sender, RoutedEventArgs e)
        {
            GroupBox_HaveMagic.Visibility = Visibility.Visible;
            Раса = "орк";
            CheckAllRequaredFields();
        }
        private void RadioButton_Race_Goblin_Checked(object sender, RoutedEventArgs e)
        {
            GroupBox_HaveMagic.Visibility = Visibility.Visible;
            Раса = "гоблин";
            CheckAllRequaredFields();
        }
        private void Hero_DoHaveMagic_Click(object sender, RoutedEventArgs e)
        {
            IsMagicChecked = true;
            CheckAllRequaredFields();
        }
        private void Hero_DoNotHaveMagic_Click(object sender, RoutedEventArgs e)
        {
            IsMagicChecked = true;
            CheckAllRequaredFields();
        }

        private void TextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_Change("К истории о противостоянии присоединяется новый герой! \nО ком будут слагать эпические легенды?");
        }
        private void RadioButton_Hero_Male_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_Change($"Благородный сэр с гордым именем {TextBox_NameOfHero.Text} и в сияющих доспехах");
        }
        private void RadioButton_Hero_Female_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_Change($"Благородная мэм с гордым именем {TextBox_NameOfHero.Text} и в сияющих доспехах");
        }
        private void Button_Accept_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_Change($"Готовы отправится на поиски приключений?");
        }
        private void RadioButton_Race_Human_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Пол)
            {
                TextBlock_Change($"Храбрый герой, с благородным именем {Имя}, оказался человеком! \n" +
                $"Самым средним и не выдающимся представителем мифического мира");
            }
            else
            {
                TextBlock_Change($"Храбрая героиня, с благородным именем {Имя}, оказалась человеком! \n" +
                $"Самым средним и не выдающимся представителем мифического мира");

            }
        }
        private void RadioButton_Race_Dwarf_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Пол)
            {
                TextBlock_Change($"Не позволяйте себе быть обманутым его ростом, он много кому сможет задать жару");

            }
            else
            {
                TextBlock_Change($"Не позволяйте себе быть обманутым её ростом, она много кому сможет задать жару");

            }
        }
        private void RadioButton_Race_Elf_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Пол)
            {
                TextBlock_Change($"Высокие (и чаще высокомерные) мастера театра боевых действий с острыми ушками и не менее острым языком");

            }
            else
            {
                TextBlock_Change($"Высокие (и чаще высокомерные) мастерицы театра боевых действий с острыми ушками и не менее острым языком");

            }
        }
        private void RadioButton_Race_Ork_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Пол)
            {
                TextBlock_Change($"Орки, могучие и свирепые берсерки с жаждой крови, но обделенные интеллектом");

            }
            else
            {
                TextBlock_Change($"Орки, могучие и свирепые берсерки с жаждой крови, но обделенные интеллектом");

            }
        }
        private void RadioButton_Race_Goblin_MouseEnter(object sender, MouseEventArgs e)
        {
            if (Пол)
            {
                TextBlock_Change($"Гоблины славятся своими умениями как инженеры (хоть их изобретения так и норовят развалиться " +
                    $"или взорваться), так и как торговцы, за счет своей хитрости");
            }
            else
            {
                TextBlock_Change($"Гоблины славятся своими умениями как инженеры (хоть их изобретения так и норовят развалиться " +
                    $"или взорваться), так и как торговцы, за счет своей хитрости");
            }
        }
        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock_Change($"Будет ли {Имя} обладать магией?");
        }
    }
}