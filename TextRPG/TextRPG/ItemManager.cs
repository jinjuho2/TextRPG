using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TextRPG   
{
    public class ItemManager 
    {
        private static ItemManager instance;
        public List<Item> items = new List<Item>();
        


        public static ItemManager Instance()
        {
            if (instance == null)
            {
                instance = new ItemManager();
            }
            return instance;
        }
        public void Item()
        {
            items.Add((new Item("무쇠 갑옷", 0, 9, "무쇠로 만들어져 튼튼한 갑옷 입니다.", false, false, 1000,"Armor")));
            items.Add((new Item("청동 도끼", 5, 0, "어디선가 사용했던거 같은 도끼입니다.", false, false, 1000,"Weapone")));
            items.Add((new Item("수련자 갑옷", 0, 5, "수련에 도움을 주는 갑옷입니다.", false, false, 600, "Armor")));
            items.Add((new Item("스파르타의 창", 7, 0, "스파르타의 전사들이 사용했다는 전설의 창입니다.", false, false, 2000, "Weapone")));
            items.Add((new Item("스파르타의 갑옷", 0, 15, "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.", false, false, 2000, "Armor")));
            items.Add((new Item("낡은 검", 2, 0, "쉽게 볼 수 있는 낡은 검 입니다.", false, false, 400, "Weapone")));
        }
        
 

        public void ShowPlayerItemList()
        {
            bool hasItem = false;
            foreach (var item in items)
            {
                if (item.isHave == true)
                {
                    Console.WriteLine(item.name);
                    hasItem = true;
                }
               
            }
             if (!hasItem)
                Console.WriteLine("소지한 아이템이 없습니다.");
        }
        public void ShowShopItemList()
        {
            
            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {items[i].ToString2()}");
            }
        }

        public void IsEquip()
        {
            while (true)
            {
                Console.Clear();
                var ownedItems = items.Where(item => item.isHave).ToList();

                for (int i = 0; i < ownedItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {ownedItems[i].ToString()}");
                }
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("장착할 아이템 번호를 입력하세요");
                Console.WriteLine("0. 뒤로 가기");
                int input = int.Parse(Console.ReadLine());

                 if (input == 0)
                {
                    Player.Instance.ShowInventory();
                    break;
                }
                    
                   

                else if (input < 1 || ownedItems.Count < input)
                {
                    Console.Write("잘못된 입력입니다.");
                    System.Threading.Thread.Sleep(1000);
                    continue;
                }

                Item selectedItem = ownedItems[input - 1];
                if (!selectedItem.isEquipped)
                {
                    foreach (var item in items)
                    {
                        if (item != selectedItem && item.isEquipped && (item.Type == selectedItem.Type))
                        {
                            item.isEquipped = false;
                            Player.Instance.EquippedItemNames.Remove(item.name);
                            Console.WriteLine($"'{item.name}' 의 장착이 해제되었습니다.");
                        }
                    }
                    selectedItem.isEquipped = true;
                    Player.Instance.EquippedItemNames.Add(selectedItem.name);
                    Console.WriteLine($"'{selectedItem.name}' 을(를) 장착했습니다");
                }

                else if (selectedItem.isEquipped == true)
                {
                    selectedItem.isEquipped = false;
                    Player.Instance.EquippedItemNames.Remove(selectedItem.name);
                    Console.WriteLine($"'{selectedItem.name}' 을(를) 해제했습니다.");
                }

                
                System.Threading.Thread.Sleep(1000);
                
            }


        }

    }
}
