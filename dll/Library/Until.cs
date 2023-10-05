using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Library
{
    public class Until
    {
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

        public class Threading
        {
            public void wait(int time)
            {
                this.isWait = true;
                this.timeStartWait = mSystem.currentTimeMillis();
                this.timeWait = (long)time;
            }

            public bool isWaiting()
            {
                if (this.isWait && mSystem.currentTimeMillis() - this.timeStartWait >= this.timeWait)
                {
                    this.isWait = false;
                }
                return this.isWait;
            }

            private bool isWait;

            private long timeStartWait;

            private long timeWait;
        }
    }
}
