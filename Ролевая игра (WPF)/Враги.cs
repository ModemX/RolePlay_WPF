using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_игра__WPF_
{
    public abstract class Враги
    {
        public abstract string ИмяВрага { get; protected set; }
        public abstract int ЗдоровьеВрага { get; protected set; }
        public abstract int СилаАтаки { get; protected set; }
        public abstract int МаксимальноеЗдоровье { get;}

        public void ОтнятьЗдоровье(int СилаАтакиПерсонажа) => ЗдоровьеВрага -= СилаАтакиПерсонажа;
        public class ГлаваСтражи : Враги
        {
            public override string ИмяВрага { get; protected set; } = "Глава стражи";
            public override int ЗдоровьеВрага { get; protected set; } = 500;
            public override int СилаАтаки { get; protected set; } = 10;
            public override int МаксимальноеЗдоровье { get; } = 500;
        }
    }


}
