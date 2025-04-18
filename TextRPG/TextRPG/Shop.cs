using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG
{
    public class Shop : MenuBase
    {
        public override void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{Player.Instance.gold} G");
            Console.WriteLine();
            Console.WriteLine("[아이템 목록]");
            ItemManager.Instance().ShowShopItemList();
            Console.WriteLine();
            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 메인 메뉴");
            HandleInput();
        }
        public override void HandleInput()
        {
            Console.Write("원하는 행동을 입력하세요: ");
            while (true)
            {
                int input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        BuyItem();
                        break;
                    case 2:
                        SellItem();
                        break;
                    case 0:
                        GameManager.MainMenu();
                        break;
                    default:
                        InvalidInput();
                        ShowMenu();
                        break;
                }
            }
        }
        private void BuyItem()
        {
            var items = ItemManager.Instance().items;

            while (true)
            {
                Console.Clear();
                for (int i = 0; i < items.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {items[i].ToString2()}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("0. 뒤로 가기");
                Console.Write("구매할 아이템을 입력하세요.: ");
                int input = int.Parse(Console.ReadLine());

                if (input == 0)
                {
                    ShowMenu();
                    return;
                }

                else if (input < 1 || items.Count < input)
                {
                    InvalidInput();
                    Wait();
                    continue;
                }

                Item selectedItem = items[input - 1];

                if (!selectedItem.isHave && Player.Instance.gold >= selectedItem.price)
                {
                    selectedItem.isHave = true;
                    Player.Instance.OwnedItemNames.Add(selectedItem.name);
                    Console.WriteLine($"'{selectedItem.name}' 을(를) 구매했습니다");
                    Player.Instance.gold -= selectedItem.price;
                    Wait();
                }

                else if (!selectedItem.isHave && Player.Instance.gold < selectedItem.price)
                {
                    Console.WriteLine($"골드가 부족합니다.");
                    Wait();
                }

                else
                {
                    Console.WriteLine($"'{selectedItem.name}' 을(를) 이미 구매하였습니다.");
                    Wait();
                }
            }
        }

        private void SellItem()
        {
            while (true)
            {
                Console.Clear();

                var ownedItems = ItemManager.Instance().items.Where(item => item.isHave).ToList();

                if (ownedItems.Count == 0)
                {
                    Console.WriteLine("소지한 아이템이 없습니다.");
                    Wait();
                    ShowMenu();
                    return;
                }
                
                Console.WriteLine("[소지한 아이템 목록]");
                for (int i = 0; i < ownedItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ownedItems[i].ToString3()}");
                }

                Console.WriteLine();
                Console.WriteLine("0. 뒤로 가기");
                Console.Write("판매할 아이템 번호를 입력하세요: ");

                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    if (input == 0)
                    {
                        ShowMenu();
                        return;
                    }

                    else if (input >= 1 && input <= ownedItems.Count)
                    {
                        Item selectedItem = ownedItems[input - 1];
                        if (selectedItem.isEquipped)
                        {
                            Console.WriteLine($"'{selectedItem.name}'은(는) 장착 중인 아이템입니다. 판매할 수 없습니다.");
                            Wait();
                            continue;
                        }
                        selectedItem.isHave = false;
                        Player.Instance.gold += (int)(selectedItem.price * 0.85f);
                        Player.Instance.OwnedItemNames.Remove(selectedItem.name);
                        Console.WriteLine($"'{selectedItem.name}' 을(를) 판매했습니다.");
                        Wait();
                        continue;
                    }

                    else
                    {
                        InvalidInput();
                        Wait();
                        continue;

                    }
                }

                else
                {
                    Console.WriteLine("숫자를 입력해주세요.");
                    Wait();
                    continue;
                }
            }
        }
    }
}
