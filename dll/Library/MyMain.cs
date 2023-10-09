using Library.Xmap;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vietpad.InputMethod;

namespace Library
{
    public class MyMain : IActionListener
    {
        public static bool isUseUnikey;

        public static bool chat(string text)
        {
            return Pk9rXmap.Chat(text);
        }

        public static void init()
        {
            Time.timeScale = 2f;
            VietKeyHandler.InputMethod = InputMethods.Telex;
            VietKeyHandler.SmartMark = true;
            VietKeyHandler.VietModeEnabled = false;
        }

        public static void paint(mGraphics g)
        {
            paintInfor(g);
            LibraryChar.paint(g);
            ListCharsInMap.paint(g);
            Boss.Paint(g);
        }

        private static void paintInfor(mGraphics g)
        {
            try
            {
                GUIStyle gUIStyleMain = StringHandle.guiStyle(0, 7, FontStyle.Bold, Color.green);
                GUIStyle gUIStyleBorder = StringHandle.guiStyle(0, 7, FontStyle.Bold, Color.black);
                List<string> list = new List<string>();
                list.Add($"{TileMap.mapID}. {TileMap.mapName} [{TileMap.zoneID}] [{GameScr.gI().numPlayer[TileMap.zoneID]}/{GameScr.gI().maxPlayer[TileMap.zoneID]}]");
                list.Add($"{DateTime.Now}");
                list.Add($"X: {Char.myCharz().cx}- Y: {Char.myCharz().cy}");
                int numY = 104;
                if (mGraphics.zoomLevel == 2)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        g.drawString(list[i], 24.5f, (numY + i * 8) - 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 24.5f, (numY + i * 8) + 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 25.5f, (numY + i * 8) - 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 25.5f, (numY + i * 8) + 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 25f, (numY + i * 8) - 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 25f, (numY + i * 8) + 0.5f, gUIStyleBorder);
                        g.drawString(list[i], 24.5f, (numY + i * 8), gUIStyleBorder);
                        g.drawString(list[i], 25.5f, (numY + i * 8), gUIStyleBorder);
                        g.drawString(list[i], 25f, (numY + i * 8), gUIStyleMain);
                    }
                    return;
                }
            }
            catch (Exception e)
            {
                Until.writeLogError("paintInfor.txt", e.Message);
            }
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
            if (keyCode == 'o')
            {
                GameCanvas.panel.setTypeZone();
                GameCanvas.panel.show();
                return;
            }
            if (keyCode == 'm')
            {
                openMenu();
                return;
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
                Until.writeLogError("hintChat.txt", ex.Message);
            }
            LoadMap.update();
            LibraryChar.update();
            ListCharsInMap.update();
            Boss.Update();
        }

        public static void onUpdateTouchGameScr()
        {
            ListCharsInMap.updateTouch();
            Boss.UpdateTouch();
        }

        private static void openMenu()
        {
            MyVector myVector = new MyVector();
            //myVector.addElement(new Command("Menu\n Training\nMob", FunctionTrainMob.gI(), 10000, null));
            //myVector.addElement(new Command("Menu\n Săn boss", FunctionBoss.gI(), 10001, null));
            //myVector.addElement(new Command("Menu\n Yardart", FunctionBoss.gI(), 10002, null));
            //myVector.addElement(new Command("Menu\n Đệ tử", FunctionPet.gI(), 10003, null));
            //myVector.addElement(new Command("Menu\n Auto\nSkills", FunctionSkill.gI(), 10004, null));
            myVector.addElement(new Command("Chức năng\nHiển thị", LibraryChar.gI(), 10005, null));
            myVector.addElement(new Command("Chức năng\nkhác", gI(), 10006, null));
            GameCanvas.menu.startAt(myVector, 0);
        }

        public void perform(int idAction, object p)
        {
            switch (idAction)
            {
                case 10006:
                    MyVector myVector = new MyVector();
                    myVector.addElement(new Command("Unikey\n" + (isUseUnikey ? "Bật" : "Tắt"), gI(), 10201, null));
                    GameCanvas.menu.startAt(myVector, 0);
                    break;
                case 10201:
                    isUseUnikey = !isUseUnikey;
                    VietKeyHandler.VietModeEnabled = isUseUnikey;
                    GameScr.info1.addInfo((isUseUnikey ? "Bật" : "Tắt") + " Unikey", 0);
                    break;
            }
        }

        public static MyMain gI()
        {
            if (_Instance == null)
            {
                _Instance = new MyMain();
            }
            return _Instance;
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
