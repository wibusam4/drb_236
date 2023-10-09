using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using Vietpad.InputMethod;

namespace Library
{
    public class Until
    {
        public static VietKeyHandler vietKeyHandler = new VietKeyHandler();

        public static void teleportMyChar(IMapObject obj)
        {
            teleportTo(obj.getX(), obj.getY());
        }

        public static void toVietnamese(ref string str, int inputType, int caresPos, char keyChar)
        {
            if (inputType == TField.INPUT_TYPE_ANY && !str.StartsWith("/")) str = vietKeyHandler.toVietnamese(str, caresPos);
        }

        public static void writeLogError(string path, string message)
        {
            StreamWriter streamWriter = new StreamWriter("/Data/Errors/" + path);
            streamWriter.WriteLine(string.Format("{0}: {1}", DateTime.Now, message));
            streamWriter.Close();
        }

        public static bool isTick(int tick)
        {
            return GameCanvas.gameTick * (int)Time.timeScale % tick == 0;
        }

        public static bool isMeInNRDMap()
        {
            return TileMap.mapID >= 85 && TileMap.mapID <= 91;
        }

        public static void teleportTo(int x, int y)
        {
            if (isUsingTDLT())
            {
                Char.myCharz().cx = x;
                Char.myCharz().cy = y;
                Service.gI().charMove();
                return;
            }
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y + 1;
            Service.gI().charMove();
            Char.myCharz().cx = x;
            Char.myCharz().cy = y;
            Service.gI().charMove();
        }

        public static void teleportInNRDMap(int position)
        {
            if (position == 0)
            {
                teleportTo(60, getYGround(60));
                return;
            }
            if (position != 2)
            {
                teleportTo(TileMap.pxw - 60, getYGround(TileMap.pxw - 60));
                return;
            }
            for (int i = 0; i < GameScr.vNpc.size(); i++)
            {
                Npc npc = (Npc)GameScr.vNpc.elementAt(i);
                if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
                {
                    Char.myCharz().npcFocus = npc;
                    teleportTo(npc.cx, npc.cy - 3);
                    return;
                }
            }
        }

        public static int getYGround(int x)
        {
            int num = 50;
            int i = 0;
            while (i < 30)
            {
                i++;
                num += 24;
                if (TileMap.tileTypeAt(x, num, 2))
                {
                    if (num % 24 != 0)
                    {
                        num -= num % 24;
                        break;
                    }
                    break;
                }
            }
            return num;
        }

        public static bool isUsingTDLT()
        {
            return ItemTime.isExistItem(4387);
        }

        public static void showCat(string text)
        {
            GameScr.info1.addInfo(text, 0);
        }

        public static bool isMeWearingActivationSet(int idSet)
        {
            int num = 0;
            for (int i = 0; i < 5; i++)
            {
                Item item = Char.myCharz().arrItemBody[i];
                if (item == null)
                {
                    return false;
                }
                if (item.itemOption == null)
                {
                    return false;
                }
                for (int j = 0; j < item.itemOption.Length; j++)
                {
                    if (item.itemOption[j].optionTemplate.id == idSet)
                    {
                        num++;
                        break;
                    }
                }
            }
            return num == 5;
        }

        public static bool isMeWearingTXHSet()
        {
            return Char.myCharz().cgender == 0 && isMeWearingActivationSet(127);
        }

        public static bool isMeWearingPikkoroDaimaoSet()
        {
            return Char.myCharz().cgender == 1 && isMeWearingActivationSet(132);
        }

        public static bool isMeWearingCadicSet()
        {
            return Char.myCharz().cgender == 2 && isMeWearingActivationSet(134);
        }

        public static short getNRSDId()
        {
            if (isMeInNRDMap())
            {
                return (short)(2400 - TileMap.mapID);
            }
            return 0;
        }

        public static string statusMenu(bool Bool)
        {
            if (!Bool)
            {
                return "Đang Tắt";
            }
            return "Đang Bật";
        }

        public class Threading
        {
            public void wait(int time)
            {
                isWait = true;
                timeStartWait = mSystem.currentTimeMillis();
                timeWait = time;
            }

            public bool isWaiting()
            {
                if (isWait && mSystem.currentTimeMillis() - timeStartWait >= timeWait)
                {
                    isWait = false;
                }
                return isWait;
            }

            private bool isWait;

            private long timeStartWait;

            private long timeWait;
        }
    }
}
