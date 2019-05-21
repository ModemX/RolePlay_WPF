using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_игра__WPF_
{
    internal class Заклинания
    {
        public abstract class Заклинание
        {
            public virtual string Название { get; }
            public virtual uint Стоимость { get; }
        }

        public class Лечение : Заклинание
        {
            new public static string Название { get; } = "Заклинание Лечения";
            public override uint Стоимость { get; } = 15;
            public uint Сила_Воздействия { get; } = 25;

            public Лечение(Персонаж_с_магией Источник, Персонаж Цель, double Множитель_Стоимости)
            {
                Источник.ИзменениеСостоянияМаны("расходование", (uint)(Стоимость * Множитель_Стоимости));
                Цель.ИзменениеСостоянияЗдоровья("лечение", Сила_Воздействия);
            }
        }

        public class Воскрешение : Заклинание
        {
            new public static string Название { get; } = "Заклинание Воскрешения";
            public override uint Стоимость { get; } = 100;

            public Воскрешение(Персонаж_с_магией Источник, Персонаж Цель, double Множитель_Стоимости)
            {
                Источник.ИзменениеСостоянияМаны("расходовние", (uint)(Стоимость * Множитель_Стоимости));
                Цель.ИзменениеСостоянияЗдоровья("лечение", 50);
            }
        }

        public class Огненный_Шар : Заклинание
        {
            new public static string Название { get; } = "Заклинание Огненного Шара";
            public override uint Стоимость { get; } = 40;
            public Огненный_Шар(Персонаж_с_магией Источник, Враги Цель, double Множитель_Стоимости)
            {
                Источник.ИзменениеСостоянияМаны("расходовние", (uint)(Стоимость * Множитель_Стоимости));
                Цель.ОтнятьЗдоровье(50);
            }
        }

        public class Заморозка : Заклинание
        {
            new public static string Название { get; } = "Заклинание Заморозки";
            public override uint Стоимость { get; } = 20;
            public Заморозка(Персонаж_с_магией Источник, Враги Цель, double Множитель_Стоимости)
            {
                Источник.ИзменениеСостоянияМаны("расходовние", (uint)(Стоимость * Множитель_Стоимости));
                Цель.IsFrozen = true;
            }
        }
    }
}
