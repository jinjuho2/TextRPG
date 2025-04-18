using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Rest : MenuBase

    {
        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("휴식하기");
            Console.WriteLine(" 500 G를 내면 체력을 회복할 수 있는 곳입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine();
            Console.WriteLine("[행동 목록]");
            Console.WriteLine("1. 휴식하기");
            Console.WriteLine("0. 나가기");
            Console.Write("원하는 행동을 입력하세요: ");
            HandleInput();
        }

        public override void HandleInput()
        {
            int input;
            if (int.TryParse(Console.ReadLine(), out input))
            {

                switch (input)
                {
                    case 1:
                        DoRest();
                        break;
                    case 0:
                        GameManager.MainMenu();
                        break;
                    default:
                        InvalidInput();
                        Wait();
                        ShowMenu();
                        HandleInput();
                        break;
                }
            }
            else
            {
                Console.WriteLine("숫자를 입력해주세요.");
                Wait();
                ShowMenu();
                HandleInput(); 
            }
        }
        public void DoRest()
        {

            if (Player.Instance.gold >= 500)
            {
                Player.Instance.gold -= 500;
                Player.Instance.hp = 100;
                Console.WriteLine("체력을 회복했습니다.");
                Wait();
                GameManager.MainMenu();
            }
            else if (Player.Instance.gold <= 500)
            {
                Console.WriteLine("골드가 부족합니다.");
                Wait();
                GameManager.MainMenu();
            }
        }
    }
}
