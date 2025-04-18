using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TextRPG
{
    public abstract class MenuBase
    {
        public void RunMenu()
        {
            ShowMenu();
            HandleInput();
        }

        public abstract void ShowMenu();
        public abstract void HandleInput();

        

        public void Wait(int ms = 1000)
        {
            System.Threading.Thread.Sleep(ms);
        }

        protected void InvalidInput()
        {
            Console.WriteLine("잘못된 입력입니다.");
            Wait();
        }


    }
}
