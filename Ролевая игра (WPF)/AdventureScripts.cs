using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ролевая_игра__WPF_
{
    public class AdventureScripts
    {
        private int[] Choices = new int[999];
        MainWindow MainWindow;
        public AdventureScripts(uint CaseOfAdventure, MainWindow MainForm)
        {
            MainWindow = MainForm;
            switch (CaseOfAdventure)
            {
                case 0:
                    {
                        if (Choices[0] == 0)
                            Начало_приключения();
                        else
                            goto case 1;
                    } break;
                case 1: { } break;
                case 2: { } break;
                case 3: { } break;
                case 4: { } break;
                case 5: { } break;
                case 6: { } break;
                case 7: { } break;
                case 8: { } break;
                case 9: { } break;
                case 10: { } break;
                case 11: { } break;
                default:
                    break;
            }
        }

        public int Choices_AddLast_GetLast
        {
            get
            {
                for (int i = Choices.Length; i >= 0; i--)
                {
                    if (Choices[i] != 0)
                    {
                        return Choices[i];
                    }
                }

                return -1;
            }
            set
            {
                for (int i = Choices.Length; i >= 0; i--)
                {
                    if (Choices[i] != 0)
                    {
                        Choices[i + 1] = value;
                    }
                }
            }
        }

        public void Начало_приключения()
        {
            MainWindow.ConsoleWriteLine("Проверка");
        }
    }
}
