using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    public class Item
    {
        
        public string name { get; set; }
        public string description { get; set; }
        public int atk { get; set; }
        public int def { get; set; }
        public int price { get; set; }
        public bool isEquipped { get; set; }
        public bool isHave {  get; set; }
        public string Type { get; set; }

        public Item() { }
        public Item(string setName, int setAtk, int setDef, string setDescription, bool isequipped, bool ishave, int price , string type)
        {
            name = setName;
            description = setDescription;
            atk = setAtk;
            def = setDef;
            isEquipped = isequipped;
            isHave = ishave;
            this.price = price;
            Type = type;
        }

        public override string ToString() // 장비창에서 씀
        {
            string equipText = isEquipped ? "[E] " : "";
            return $"{equipText}{name} - {description} (공격력: {atk}, 방어력: {def})";
        }
        
        public string ToString2() //상점에서 씀
        {
            string buyText = isHave ? "구매 완료" : price.ToString();
            return $"{name} | (공격력: {atk}, 방어력: {def}| {description} | {price} | {buyText})";
        }
        public string ToString3() // 상점에서 씀
        {
            string equipText = isEquipped ? "[E] " : "";
            return $"{equipText}{name} | (공격력: {atk}, 방어력: {def}| {description} | {price * 0.85})";
        }

    }
}
