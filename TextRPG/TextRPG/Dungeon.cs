using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG
{
    internal class Dungeon : MenuBase
    {

        public Random rand = new Random();

        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1. 쉬움 | 방어력 5 이상 권장");
            Console.WriteLine("2. 보통 | 방어력 11이상 권장");
            Console.WriteLine("3. 어려움 | 방어력 17 이상 권장");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>");
            HandleInput();
        }

        public override void HandleInput()
        {
            while (true)
            {
                
                try
                {
                    int input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            EnterDungeon("쉬움", 5, 1000, 20, 36);
                            break;
                        case 2:
                            EnterDungeon("보통", 11, 1700, 35, 51);
                            break;
                        case 3:
                            EnterDungeon("어려움", 17, 2500, 50, 66);
                            break;
                        case 0:
                            Console.WriteLine("나가기");
                            GameManager.MainMenu();
                            break;
                        default:
                            Console.WriteLine("잘못된 입력입니다.");
                            Console.WriteLine("다시 입력해주세요.");
                            Wait();
                            ShowMenu();
                            break;
                    }
                }


                catch
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Wait();
                    ShowMenu();
                    continue;
                }
            }

        }

        private void EnterDungeon(string DungeonName, int requiredDef, int baseGold, int minHp, int maxHp)
        {
            Console.Clear();
            Console.WriteLine("던전으로 들어갑니다.");
            Wait(2000);
            Console.Clear();
            Console.WriteLine("던전 탐험중입니다.");
            Wait(2000);
            Console.Clear();

            bool isSuccess = Player.Instance.def >= requiredDef || rand.Next(0, 5) > 2; // 던전 성공 여부
            int lostHp = rand.Next(minHp + (requiredDef - Player.Instance.def), maxHp + (requiredDef - Player.Instance.def));

            Player.Instance.hp -= lostHp;
            Console.WriteLine($"[{DungeonName} 던전 탐험 결과]");

            if (isSuccess)
            {
                Player.Instance.clearEA++;
                Console.WriteLine($"{DungeonName} 던전 클리어!");
                Console.WriteLine($"체력 {Player.Instance.hp + lostHp} -> {Player.Instance.hp}");
                Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += baseGold + (baseGold * (Player.Instance.atk * 2) / 100)}");
                Player.Instance.LevelUp();
            }

            else
            {
                Console.WriteLine($"{DungeonName} 던전 실패");
                Console.WriteLine($"체력 {Player.Instance.hp + lostHp} -> {Player.Instance.hp}");
            }

            Player.Instance.Dead();
            Console.WriteLine();
            Console.WriteLine("0. 메인 메뉴로 돌아가기");
            Console.Write(">> ");
            if (int.TryParse(Console.ReadLine(), out int input) && input == 0)
            {
                GameManager.MainMenu();
            }
        }
        //public void ShowDungeon()
        //{
        //    SelectDungeon input = (SelectDungeon)int.TryParse(Console.ReadLine(), out int input));
        //    int recDef = 0;
        //    int back = 5;
        //    switch (input)
        //    {
        //        case SelectDungeon.Easy:
        //            recDef = 5;
        //            if (Player.Instance.def < recDef)
        //            {
        //                if (rand.Next(0, 5) > 2)  // 던전 성공
        //                {
        //                    Console.Clear();
        //                    int losthp = rand.Next((20 + (recDef - Player.Instance.def)), (36 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Player.Instance.clearEA += 1;
        //                    Console.WriteLine("축하합니다!!");
        //                    Console.WriteLine("쉬운던전을 클리어 하였습니다.");
        //                    Console.WriteLine();
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                    Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (1000 + (1000 * (Player.Instance.atk * 2) / 100))}");
        //                }
        //                else
        //                {
        //                    Console.Clear();
        //                    int losthp = rand.Next((20 + (recDef - Player.Instance.def)), (36 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Console.WriteLine("던전 실패");
        //                    Console.WriteLine();
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} : {Player.Instance.hp}");
        //                }
        //            }
        //            else
        //            {
        //                Console.Clear();
        //                Player.Instance.clearEA += 1;
        //                int losthp = rand.Next((20 + (recDef - Player.Instance.def)), (36 + (recDef - Player.Instance.def)));
        //                Player.Instance.hp -= losthp;
        //                Console.WriteLine("쉬운던전 클리어");
        //                Console.WriteLine("[탐험 결과]");
        //                Console.WriteLine($"체력 {Player.Instance.hp + losthp } -> {Player.Instance.hp}");
        //                Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (1000 + (1000 * (Player.Instance.atk * 2) / 100))}");
        //            }
        //            Player.Instance.Dead();
        //            Player.Instance.LevelUp();
        //            Console.WriteLine("0. 메인 메뉴");
        //            back = int.Parse( Console.ReadLine() );

        //            if (back == 0 )
        //            GameManager.MainMenu();
        //            break;

        //        case SelectDungeon.Normal:
        //            recDef = 11;
        //            if (Player.Instance.def < recDef)
        //            {
        //                if (rand.Next(0, 5) > 2)  // 던전 성공
        //                {
        //                    Console.Clear();
        //                    Player.Instance.clearEA += 1;
        //                    int losthp = rand.Next((35 + (recDef - Player.Instance.def)), (51 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Console.WriteLine("보통 던전 클리어");
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                    Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (1700 + (1700 * (Player.Instance.atk * 2) / 100))}");
        //                }
        //                else
        //                {
        //                    Console.Clear();
        //                    int losthp = rand.Next((35 + (recDef - Player.Instance.def)), (51 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Console.WriteLine("던전 실패");
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                }
        //            }
        //            else
        //            {
        //                Console.Clear();
        //                Player.Instance.clearEA += 1;
        //                int losthp = rand.Next((35 + (recDef - Player.Instance.def)), (51 + (recDef - Player.Instance.def)));
        //                Player.Instance.hp -= losthp;
        //                Console.WriteLine("보통 던전 클리어");
        //                Console.WriteLine("[탐험 결과]");
        //                Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (1700 + (1700 * (Player.Instance.atk * 2) / 100))}");
        //            }
        //            Player.Instance.Dead();
        //            Player.Instance.LevelUp();
        //            Console.WriteLine("0. 메인 메뉴");
        //            back = int.Parse(Console.ReadLine());

        //            if (back == 0)
        //                GameManager.MainMenu();
        //            break;

        //        case SelectDungeon.Hard:
        //            recDef = 17;
        //            if (Player.Instance.def < recDef)
        //            {
        //                if (rand.Next(0, 5) > 2)  // 던전 성공
        //                {
        //                    Console.Clear();
        //                    Player.Instance.clearEA += 1;
        //                    int losthp = rand.Next((50 + (recDef - Player.Instance.def)), (66 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Console.WriteLine("하드 던전 클리어");
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                    Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (2500 + (2500 * (Player.Instance.atk * 2) / 100))}");
        //                }
        //                else
        //                {
        //                    Console.Clear();
        //                    int losthp = rand.Next((50 + (recDef - Player.Instance.def)), (66 + (recDef - Player.Instance.def)));
        //                    Player.Instance.hp -= losthp;
        //                    Console.WriteLine("던전 실패");
        //                    Console.WriteLine("[탐험 결과]");
        //                    Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                }
        //            }
        //            else
        //            {
        //                Console.Clear();
        //                Player.Instance.clearEA += 1;
        //                int losthp = rand.Next((50 + (recDef - Player.Instance.def)), (66 + (recDef - Player.Instance.def)));
        //                Player.Instance.hp -= losthp;
        //                Console.WriteLine("하드 던전 클리어");
        //                Console.WriteLine("[탐험 결과]");
        //                Console.WriteLine($"체력 {Player.Instance.hp + losthp} -> {Player.Instance.hp}");
        //                Console.WriteLine($"Gold {Player.Instance.gold} -> {Player.Instance.gold += (2500 + (2500 * (Player.Instance.atk * 2) / 100))}");
        //            }
        //            Player.Instance.Dead();
        //            Player.Instance.LevelUp();
        //            Console.WriteLine("0. 메인 메뉴");
        //            back = int.Parse(Console.ReadLine());

        //            if (back == 0)
        //                GameManager.MainMenu();
        //            break;
        //    }


    }
}

