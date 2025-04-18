using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;

namespace TextRPG
{
    public static class SaveManager
    {
        private static string savePath = "save.json";

        public static void SavePlayer()
        {
            var saveData = new SaveData
            {
                PlayerData = Player.Instance,
                Items = ItemManager.Instance().items
            };

            var json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(savePath, json);
            Console.WriteLine("저장 완료!");
        }

        public static void LoadPlayer()
        {
            Console.Write("1.새로하기 , 2.로드하기\n원하시는 행동을 입력해주세요.\n>>");
            if (int.TryParse(Console.ReadLine(), out int input))
            {
                if (input == 2) //로드하기
                {
                    if (File.Exists(savePath))
                    {
                        string json = File.ReadAllText(savePath);
                        var saveData = JsonSerializer.Deserialize<SaveData>(json);

                        if (saveData != null)
                        {
                            var loadedPlayer = saveData.PlayerData;
                            var loadedItems = saveData.Items;

                            // 기존 싱글톤 인스턴스에 데이터 복사
                            Player.Instance.name = loadedPlayer.name;
                            Player.Instance.atk = loadedPlayer.atk;
                            Player.Instance.def = loadedPlayer.def;
                            Player.Instance.hp = loadedPlayer.hp;
                            Player.Instance.gold = loadedPlayer.gold;
                            Player.Instance.lv = loadedPlayer.lv;
                            Player.Instance.clearEA = loadedPlayer.clearEA;
                            Player.Instance.OwnedItemNames = loadedPlayer.OwnedItemNames;
                            Player.Instance.EquippedItemNames = loadedPlayer.EquippedItemNames;

                            // 아이템 상태 복원
                            foreach (var item in ItemManager.Instance().items)
                            {
                                var loadedItem = loadedItems.FirstOrDefault(x => x.name == item.name);
                                if (loadedItem != null)
                                {
                                    item.isHave = loadedItem.isHave;
                                    item.isEquipped = loadedItem.isEquipped;
                                }
                            }

                            Console.WriteLine("로드 완료!");
                        }
                        else
                        {
                            Console.WriteLine("로드 실패: 파일 형식이 올바르지 않습니다.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("저장 파일이 없습니다.");
                        Console.WriteLine("새로 시작합니다.");
                        System.Threading.Thread.Sleep(1000);
                        Console.Clear();
                        LoadPlayer();
                    }
                }
                else if (input == 1) // 새로하기
                {
                    Console.Write("플레이어 이름을 입력하세요: ");
                    string getName = Console.ReadLine();
                    Console.WriteLine("입력 완료");
                    Player.Instance.Init(getName, 10, 5, 100, 1500, 1, 0);
                }
            }
        }
        

    }
}

