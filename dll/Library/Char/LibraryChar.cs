using Library;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public class LibraryChar : IActionListener
    {
        public static LibraryChar gI()
        {
            if (_Instance == null)
            {
                _Instance = new LibraryChar();
            }
            return _Instance;
        }

        public void perform(int idAction, object p)
        {
            if (idAction == 10005)
            {
                MyVector myVector = new MyVector();
                myVector.addElement(new Command("Thông tin\n đối thủ\n" + StringHandle.statusMenu(enableShowInforChar), gI(), 20301, null));
                myVector.addElement(new Command("Thông tin\n quái focus\n" + StringHandle.statusMenu(enableShowInforMob), gI(), 20302, null));
                myVector.addElement(new Command("Danh sách\n nhân vật\n" + StringHandle.statusMenu(ListCharsInMap.isEnabled), gI(), 20303, null));
                myVector.addElement(new Command("Danh sách\n boss\n" + StringHandle.statusMenu(Boss.isEnabled), gI(), 20304, null));
                GameCanvas.menu.startAt(myVector, 0);
                return;
            }
            //if (idAction == 10204)
            //{
            //    GameCanvas.gI().keyPressedz(98);
            //    GameScr.info1.addInfo("Hãy chọn đối tượng để teleport tới", 0);
            //    return;
            //}
            switch (idAction)
            {
                case 20301:
                    enableShowInforChar = !enableShowInforChar;
                    Until.showCat("Hiển thị thông tin đối thủ: " + StringHandle.status(enableShowInforChar));
                    return;
                case 20302:
                    enableShowInforMob = !enableShowInforMob;
                    Until.showCat("Hiển thị thông tin quái: " + StringHandle.status(enableShowInforMob));
                    return;
                case 20303:
                    ListCharsInMap.isEnabled = !ListCharsInMap.isEnabled;
                    Until.showCat("Hiển thị danh sách nhân vật: " + StringHandle.status(ListCharsInMap.isEnabled));
                    return;
                case 20304:
                    Boss.isEnabled = !Boss.isEnabled;
                    Until.showCat("Hiển thị danh sách boss: " + StringHandle.status(Boss.isEnabled));
                    return;
                default:
                    return;
            }
        }

        public static bool chat(string text)
        {
            //if (text == "/alg")
            //{
            //    FunctionLogin.enableAutoLogin = !FunctionLogin.enableAutoLogin;
            //    GameScr.info1.addInfo("[ThanhLc] Auto Login :" + StringHandle.Status(FunctionLogin.enableAutoLogin), 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/kmt"))
            //{
            //    Library.CharID = StringHandle.GetInfoChat<int>(text, "/kmt");
            //    Library.enableLockCharFocus = !Library.enableLockCharFocus;
            //    GameScr.info1.addInfo("[ThanhLc] Khóa mục tiêu id [" + Library.CharID.ToString() + "] :" + StringHandle.Status(Library.enableLockCharFocus), 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/c"))
            //{
            //    Library.runSpeed = StringHandle.GetInfoChat<int>(text, "/c");
            //    GameScr.info1.addInfo("[ThanhLc] Tốc độ chạy đã được chỉnh thành " + Library.runSpeed.ToString(), 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/s"))
            //{
            //    Time.timeScale = (float)StringHandle.GetInfoChat<int>(text, "/s");
            //    GameScr.info1.addInfo("[ThanhLc] Tốc độ game đã được chỉnh thành " + Time.timeScale.ToString(), 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/l"))
            //{
            //    Char.myCharz().cx -= StringHandle.GetInfoChat<int>(text, "/l");
            //    GameScr.info1.addInfo("[ThanhLc] Dịch trái", 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/r"))
            //{
            //    Char.myCharz().cx += StringHandle.GetInfoChat<int>(text, "/r");
            //    GameScr.info1.addInfo("[ThanhLc] Dịch phải", 0);
            //}
            //else if (StringHandle.IsGetInfoChat<int>(text, "/d"))
            //{
            //    Char.myCharz().cy += StringHandle.GetInfoChat<int>(text, "/d");
            //    GameScr.info1.addInfo("[ThanhLc] Dịch xuống", 0);
            //}
            //else
            //{
            //    if (!StringHandle.IsGetInfoChat<int>(text, "/u"))
            //    {
            //        return false;
            //    }
            //    Char.myCharz().cy -= StringHandle.GetInfoChat<int>(text, "/u");
            //    GameScr.info1.addInfo("[ThanhLc] Dịch lên", 0);
            //}
            return true;
        }

        public static void update()
        {
            if (mSystem.currentTimeMillis() - lastTimeADS > 60000L)
            {
                GameScr.info1.addInfo("TOOLWIBU.ME - Mấy ní chơi game zui zẻ!", 0);
                lastTimeADS = mSystem.currentTimeMillis();
            }
            Char.myCharz().cspeed = runSpeed;
            CharEffect.update();
        }

        
        public static void paint(mGraphics g)
        {
            CharEffect.paint(g);
        }


        private static LibraryChar _Instance;

        public static long lastTimeADS;

        public static int runSpeed = 6;

        public static bool enableShowInforChar;

        public static bool enableShowInforMob;

        public static bool enableShowPositionMob;

        public static bool enableShowNameMob;

        public static bool enableShowIDItem;

        public static bool enableShowNameItem;
    }
}
