using Library.Xmap;
using System;
using System.Collections.Generic;
using UnityEngine;
using Vietpad.InputMethod;
using UnityEngine;

namespace Library
{
    public class MyMain
    {
        public static bool chat(string text)
        {
            return Pk9rXmap.Chat(text);
        }

        public static void init()
        {
            Time.timeScale = 2f;
            VietKeyHandler.InputMethod = InputMethods.Telex;
            VietKeyHandler.SmartMark = true;
        }

        public static void paint(mGraphics g)
        {
            paintInfor(g);
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
            catch(Exception e)
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
            if (keyCode == 'm')
            {
                GameCanvas.panel.setTypeZone();
                GameCanvas.panel.show();
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
