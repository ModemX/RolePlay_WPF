﻿using System;
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
    /// Логика взаимодействия для Attack.xaml
    /// </summary>
    public partial class Attack : Window
    {
        List<Враги> СписокТекущихВрагов;
        Персонаж АтакующийПерсонаж;
        public int ЗначениеАтаки { get; private set; }
        public int SelectedEnemy { get; private set; }
        public bool IsEnemyKilled { get; private set; }

        public Attack(List <Враги> СписокНаследованныхВрагов, Персонаж НаследованныйАтакующийПерсонаж)
        {
            InitializeComponent();
            СписокТекущихВрагов = СписокНаследованныхВрагов;
            foreach (Враги Враг in СписокТекущихВрагов)
            {
                ComboBox_EnemyList.Items.Add(Враг.ИмяВрага);
            }
            ComboBox_EnemyList.SelectedIndex = 0;
            SelectedEnemy = 0;
            Button_Attack.IsEnabled = true;
            АтакующийПерсонаж = НаследованныйАтакующийПерсонаж;
            ЗначениеАтаки = АтакующийПерсонаж.ПолучитьЗначениеАтаки();
        }

        private void ComboBox_EnemyList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Button_Attack.IsEnabled = true;
            SelectedEnemy = ComboBox_EnemyList.SelectedIndex;
        }

        private void Button_Attack_Click(object sender, RoutedEventArgs e)
        {
            СписокТекущихВрагов[SelectedEnemy].ОтнятьЗдоровье(ЗначениеАтаки);
            DialogResult = true;
            Close();
            if (ЗначениеАтаки > СписокТекущихВрагов[SelectedEnemy].ЗдоровьеВрага)
                IsEnemyKilled = true;
        }

        public List<Враги> GetСписокТекущихВрагов() => СписокТекущихВрагов;
    }
}
