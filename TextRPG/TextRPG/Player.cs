using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;


namespace TextRPG
{
    public class Player
    {
        private static Player instance;
        public static Player Instance
        {
            get
            {
                if (instance == null)
                    instance = new Player();
                return instance;
            }
        }

        public TextUtils text = new TextUtils();
        // 인벤토리
        public List<string> EquippedItemNames { get; set; } = new List<string>();
        public List<string> OwnedItemNames { get; set; } = new List<string>();

        // 스탯
        public string name { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int hp { get; set; }
        public int gold { get; set; }
        public int lv { get; set; }
        public int atkadd { get; set; }
        public int defadd { get; set; }
        public int clearEA { get; set; }

        public void Init(string name, int atk, int def, int hp, int gold, int lv, int clearEA)
        {
            this.name = name;
            this.atk = atk;
            this.def = def;
            this.hp = hp;
            this.gold = gold;
            this.lv = lv;
            this.clearEA = clearEA;
        }

        // ======== 상태 보기 ========
        public void ShowStatus()
        {
            Console.Clear();
            Console.WriteLine($"이름: {name}");
            Console.WriteLine($"Lv: {lv:D2}");
            Console.WriteLine($"클리어 횟수: {clearEA}");
            Console.WriteLine($"공격력: {atk + EquippedAttack()} (+{EquippedAttack()})");
            Console.WriteLine($"방어력: {def + EquippedDefense()} (+{EquippedDefense()})");
            Console.WriteLine($"체력: {hp}");
            Console.WriteLine($"Gold: {gold} G");
            Console.WriteLine("\n0. 메인 메뉴로");
            Console.Write(">> ");

            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 0)
                    GameManager.MainMenu();
                else
                    Console.WriteLine("다시 입력해주세요.");
            }

        }


        // ======== 인벤토리 보기 ========
        public void ShowInventory()
        {
            Console.Clear();
            Console.WriteLine($"[아이템 목록]");
            ItemManager.Instance().ShowPlayerItemList();
            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 메인 메뉴");
            Console.Write(">> ");

            if (int.TryParse(Console.ReadLine(), out int input))
                if (input == 1)
                    ShowEquipment();
                else if (input == 0)
                    GameManager.MainMenu();
                else
                    Console.WriteLine("다시 입력해주세요.");

        }

        // ======== 장비 보기 ========
        public void ShowEquipment()
        {
            Console.Clear();
            Console.WriteLine($"[아이템 목록]");
            ItemManager.Instance().IsEquip();
            Console.WriteLine("\n1. 인벤토리");
            Console.WriteLine("0. 메인 메뉴");
            Console.Write(">> ");

            while (true)
            {
                int input = int.Parse(Console.ReadLine());

                if (input == 1)
                {
                    ShowInventory();
                    break;
                }
                else if (input == 0)
                    GameManager.MainMenu();
                else
                    Console.WriteLine("다시 입력해주세요.");
            }
        }

        // ======== 장비 스탯 합산 ========
        public int EquippedAttack()
        {
            int totalAttack = 0;

            foreach (var item in ItemManager.Instance().items)
            {
                if (item.isEquipped)
                    totalAttack += item.atk;
            }

            return totalAttack;
        }

        public int EquippedDefense()
        {
            int totalDefense = 0;

            foreach (var item in ItemManager.Instance().items)
            {
                if (item.isEquipped)
                    totalDefense += item.def;
            }

            return totalDefense;
        }

        // ======== 레벨업 ========
        public void LevelUp()
        {
            if (lv == clearEA)
            {
                lv++;
                clearEA = 0;
                hp += 5;
                atk += 1;
                def += 1;
                text.WriteWithDelay("축하합니다. 레벨업! ++", 100);
            }
            else
            {
                Console.WriteLine($"Lv: {lv:D2}");
                Console.WriteLine($"클리어 횟수: {clearEA}");
            }
        }

        // ======== 사망 처리 ========
        public void Dead()
        {
            if (hp <= 0)
            {
                Console.WriteLine("플레이어가 사망하였습니다.");
                Console.WriteLine("아무 키나 누르면 종료됩니다...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}


