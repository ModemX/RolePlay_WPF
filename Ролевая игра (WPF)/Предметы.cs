using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_игра__WPF_
{
    public class Предметы
    {
        public abstract class Зелье
        {
            public virtual string Название { get; }
        }

        public abstract class Зелье_Лечения : Зелье
        {
            public virtual uint Количество_Восполняемого_Здоровья { get; }
        }
        public class Малое_Зелье_Лечения : Зелье_Лечения
        {
            public override string Название { get; } = "Малое зелье лечения [+25 ОЗ]";
            public override uint Количество_Восполняемого_Здоровья { get; } = 25;
        }
        public class Среднее_Зелье_Лечения : Зелье_Лечения
        {
            public override string Название { get; } = "Среднее зелье лечения [+50 ОЗ]";
            public override uint Количество_Восполняемого_Здоровья { get; } = 50;
        }
        public class Большое_Зелье_Лечения : Зелье_Лечения
        {
            public override string Название { get; } = "Большое зелье лечения [+75 ОЗ]";
            public override uint Количество_Восполняемого_Здоровья { get; } = 75;
        }

        public class Зелье_маны : Зелье
        {
            public virtual uint Количество_Восполняемой_Маны { get; }
        }
        public class Бутылек_Маны : Зелье_маны
        {
            public override string Название { get; } = "Бутылек Маны [+25 ОМ]";
            public override uint Количество_Восполняемой_Маны { get; } = 25;
        }
        public class Фласка_маны : Зелье_маны
        {
            public override string Название { get; } = "Фласка маны [+50 ОМ]";
            public override uint Количество_Восполняемой_Маны { get; } = 25;
        }
        public class Банка_маны : Зелье_маны
        {
            public override string Название { get; } = "Банка маны [+75 ОМ]";
            public override uint Количество_Восполняемой_Маны { get; } = 25;
        }
    }
}
