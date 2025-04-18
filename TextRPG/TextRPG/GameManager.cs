using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        public static Shop shop = new Shop();
        public static Rest rest = new Rest();
        public static Dungeon dungeon = new Dungeon();

        public enum SelectAction
        {
            Status = 1,
            Inventory,
            Shop,
            Dungeon,
            Rest,
            Exit,
        }


        public static void MainMenu()
        {
             

            while (true)
            {
                Console.Clear();
                Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.\r\n이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
                Console.WriteLine("1. 상태 보기\r\n2. 인벤토리\r\n3. 상점\r\n4. 던전 입장\r\n5. 휴식하기\r\n6. 게임 종료\r\n\r\n원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                
                try
                { 
                    SelectAction input = (SelectAction)int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case SelectAction.Status:
                            Player.Instance.ShowStatus();
                            break;
                        case SelectAction.Inventory:
                            Player.Instance.ShowInventory();
                            break;
                        case SelectAction.Shop:
                            shop.RunMenu();
                            break;
                        case SelectAction.Dungeon:
                            dungeon.RunMenu();
                            break;
                        case SelectAction.Rest:
                            rest.RunMenu();
                            break;
                        case SelectAction.Exit:
                            Console.WriteLine("게임 저장중...");
                            SaveManager.SavePlayer();
                            Console.WriteLine("아무 키나 누르면 종료됩니다...");
                            Console.ReadKey();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.Write("다시 입력해주세요.");
                            Thread.Sleep(1000);
                            continue;

                    }
                }
                catch
                {
                    Console.WriteLine("숫자를 정확히 입력해주세요.");
                    Thread.Sleep(1000);
                }
                
            }
        }

        static void Main(string[] args)
        {
            ItemManager.Instance().Item();
            SaveManager.LoadPlayer();
            Thread.Sleep(3000);
            MainMenu();
        }
    }
}
