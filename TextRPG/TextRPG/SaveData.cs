using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class SaveData
    {
        public Player PlayerData { get; set; }
        public List<Item> Items { get; set; }

        public SaveData() { }

        public SaveData(Player player, List<Item> items)
        {
            PlayerData = player;
            Items = items;
        }
    }
}
