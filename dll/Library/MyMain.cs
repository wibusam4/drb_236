using Library.Xmap;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Library
{
    public class MyMain
    {
        public static bool chat(string text)
        {
            return Pk9rXmap.Chat(text);
        }

        public static void paint(mGraphics g)
        {
        }

        public static void updateKey(int keyCode)
        {
            if (keyCode == 106)
            {
                LoadMap.loadMap(0);
                return;
            }
            if (keyCode == 107)
            {
                LoadMap.loadMap(2);
                return;
            }
            if (keyCode == 108)
            {
                LoadMap.loadMap(1);
            }
        }

        public static void update()
        {
            try
            {
                HintCommand.gI.update();
            }
            catch (Exception ex)
            {
                Until.writeLogError("hintChat", ex.Message);
            }
        }

        public void perform(int idAction, object p)
        {
        }

        public static MyMain gI()
        {
            if (MyMain._Instance == null)
            {
                MyMain._Instance = new MyMain();
            }
            return MyMain._Instance;
        }

        public void onChatFromMe(string text, string to)
        {
        }

        public void onCancelChat()
        {
            throw new NotImplementedException();
        }

        public static MyMain _Instance;
    }
}
