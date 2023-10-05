using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Library
{
    public class LoadMap
    {
        public static void loadMap(int position)
        {
            if (Until.isMeInNRDMap())
            {
                Until.teleportInNRDMap(position);
                return;
            }
            loadWaypointsInMap();
            switch (position)
            {
                case 0:
                    if (wayPointMapLeft[0] != 0 && wayPointMapLeft[1] != 0)
                    {
                        Until.teleportTo(wayPointMapLeft[0], wayPointMapLeft[1]);
                    }
                    else
                    {
                        Until.teleportTo(60, Until.getYGround(60));
                    }
                    break;
                case 1:
                    if (wayPointMapRight[0] != 0 && wayPointMapRight[1] != 0)
                    {
                        Until.teleportTo(wayPointMapRight[0], wayPointMapRight[1]);
                    }
                    else
                    {
                        Until.teleportTo(TileMap.pxw - 60, Until.getYGround(TileMap.pxw - 60));
                    }
                    break;
                case 2:
                    if (wayPointMapCenter[0] != 0 && wayPointMapCenter[1] != 0)
                    {
                        Until.teleportTo(wayPointMapCenter[0], wayPointMapCenter[1]);
                    }
                    else
                    {
                        Until.teleportTo(TileMap.pxw / 2, Until.getYGround(TileMap.pxw / 2));
                    }
                    break;
            }
            if (TileMap.mapID != 7 && TileMap.mapID != 14 && TileMap.mapID != 0)
            {
                Service.gI().requestChangeMap();
                return;
            }
            Service.gI().getMapOffline();
        }

        private static void loadWaypointsInMap()
        {
            resetSavedWaypoints();
            int num = TileMap.vGo.size();
            if (num != 2)
            {
                for (int i = 0; i < num; i++)
                {
                    Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
                    if (waypoint.maxX < 60)
                    {
                        wayPointMapLeft[0] = (int)(waypoint.minX + 15);
                        wayPointMapLeft[1] = (int)waypoint.maxY;
                    }
                    else if ((int)waypoint.maxX > TileMap.pxw - 60)
                    {
                        wayPointMapRight[0] = (int)(waypoint.maxX - 15);
                        wayPointMapRight[1] = (int)waypoint.maxY;
                    }
                    else
                    {
                        wayPointMapCenter[0] = (int)(waypoint.minX + 15);
                        wayPointMapCenter[1] = (int)waypoint.maxY;
                    }
                }
                return;
            }
            Waypoint waypoint2 = (Waypoint)TileMap.vGo.elementAt(0);
            Waypoint waypoint3 = (Waypoint)TileMap.vGo.elementAt(1);
            if ((waypoint2.maxX < 60 && waypoint3.maxX < 60) || ((int)waypoint2.minX > TileMap.pxw - 60 && (int)waypoint3.minX > TileMap.pxw - 60))
            {
                wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
                wayPointMapLeft[1] = (int)waypoint2.maxY;
                wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
                wayPointMapRight[1] = (int)waypoint3.maxY;
                return;
            }
            if (waypoint2.maxX < waypoint3.maxX)
            {
                wayPointMapLeft[0] = (int)(waypoint2.minX + 15);
                wayPointMapLeft[1] = (int)waypoint2.maxY;
                wayPointMapRight[0] = (int)(waypoint3.maxX - 15);
                wayPointMapRight[1] = (int)waypoint3.maxY;
                return;
            }
            wayPointMapLeft[0] = (int)(waypoint3.minX + 15);
            wayPointMapLeft[1] = (int)waypoint3.maxY;
            wayPointMapRight[0] = (int)(waypoint2.maxX - 15);
            wayPointMapRight[1] = (int)waypoint2.maxY;
        }

        private static void resetSavedWaypoints()
        {
            wayPointMapLeft = new int[2];
            wayPointMapCenter = new int[2];
            wayPointMapRight = new int[2];
        }

        private static int[] wayPointMapLeft;

        private static int[] wayPointMapCenter;

        private static int[] wayPointMapRight;
    }
}
