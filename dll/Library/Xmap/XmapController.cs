using System;
using System.Collections.Generic;

namespace Library.Xmap
{
	// Token: 0x020000E9 RID: 233
	public class XmapController : IActionListener
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0008FF3C File Offset: 0x0008E13C
		public static void Update()
		{
			if (XmapController.IsWaiting() || XmapData.Instance().IsLoading)
			{
				return;
			}
			if (XmapController.IsWaitNextMap)
			{
				XmapController.Wait(200);
				XmapController.IsWaitNextMap = false;
				return;
			}
			if (XmapController.IsNextMapFailed)
			{
				XmapData.Instance().MyLinkMaps = null;
				XmapController.WayXmap = null;
				XmapController.IsNextMapFailed = false;
				return;
			}
			if (XmapController.WayXmap == null)
			{
				if (XmapData.Instance().MyLinkMaps == null)
				{
					XmapData.Instance().LoadLinkMaps();
					return;
				}
				XmapController.WayXmap = XmapAlgorithm.FindWay(TileMap.mapID, XmapController.IdMapEnd);
				XmapController.IndexWay = 0;
				if (XmapController.WayXmap == null)
				{
					GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
					XmapController.FinishXmap();
					return;
				}
			}
			if (TileMap.mapID == XmapController.WayXmap[XmapController.WayXmap.Count - 1] && !XmapData.IsMyCharDie())
			{
				XmapController.FinishXmap();
				return;
			}
			if (TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay])
			{
				if (XmapData.IsMyCharDie())
				{
					Service.gI().returnTownFromDead();
					XmapController.IsWaitNextMap = (XmapController.IsNextMapFailed = true);
				}
				else if (XmapData.CanNextMap())
				{
					XmapController.NextMap(XmapController.WayXmap[XmapController.IndexWay + 1]);
					XmapController.IsWaitNextMap = true;
				}
				XmapController.Wait(500);
				return;
			}
			if (TileMap.mapID == XmapController.WayXmap[XmapController.IndexWay + 1])
			{
				XmapController.IndexWay++;
				return;
			}
			XmapController.IsNextMapFailed = true;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00008256 File Offset: 0x00006456
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				XmapController.ShowPanelXmap((List<int>)p);
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x00008267 File Offset: 0x00006467
		private static void Wait(int time)
		{
			XmapController.IsWait = true;
			XmapController.TimeStartWait = mSystem.currentTimeMillis();
			XmapController.TimeWait = (long)time;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00008280 File Offset: 0x00006480
		private static bool IsWaiting()
		{
			if (XmapController.IsWait && mSystem.currentTimeMillis() - XmapController.TimeStartWait >= XmapController.TimeWait)
			{
				XmapController.IsWait = false;
			}
			return XmapController.IsWait;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x000900A4 File Offset: 0x0008E2A4
		public static void ShowXmapMenu()
		{
			XmapData.Instance().LoadGroupMapsFromFile("Xmap\\GroupMapsXmap.txt");
			MyVector myVector = new MyVector();
			foreach (GroupMap groupMap in XmapData.Instance().GroupMaps)
			{
				myVector.addElement(new Command(groupMap.NameGroup, XmapController._Instance, 1, groupMap.IdMaps));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x00090134 File Offset: 0x0008E334
		public static void ShowPanelXmap(List<int> idMaps)
		{
			Pk9rXmap.IsMapTransAsXmap = true;
			int count = idMaps.Count;
			GameCanvas.panel.mapNames = new string[count];
			GameCanvas.panel.planetNames = new string[count];
			for (int i = 0; i < count; i++)
			{
				string str = TileMap.mapNames[idMaps[i]];
				GameCanvas.panel.mapNames[i] = idMaps[i].ToString() + ": " + str;
				GameCanvas.panel.planetNames[i] = "Xmap by Phucprotein";
			}
			GameCanvas.panel.setTypeMapTrans();
			GameCanvas.panel.show();
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000082A6 File Offset: 0x000064A6
		public static void StartRunToMapId(int idMap)
		{
			XmapController.IdMapEnd = idMap;
			Pk9rXmap.IsXmapRunning = true;
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000082B4 File Offset: 0x000064B4
		public static void FinishXmap()
		{
			Pk9rXmap.IsXmapRunning = false;
			XmapController.IsNextMapFailed = false;
			XmapData.Instance().MyLinkMaps = null;
			XmapController.WayXmap = null;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000082D3 File Offset: 0x000064D3
		public static void SaveIdMapCapsuleReturn()
		{
			Pk9rXmap.IdMapCapsuleReturn = TileMap.mapID;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000901D4 File Offset: 0x0008E3D4
		private static void NextMap(int idMapNext)
		{
			List<MapNext> mapNexts = XmapData.Instance().GetMapNexts(TileMap.mapID);
			if (mapNexts != null)
			{
				foreach (MapNext mapNext in mapNexts)
				{
					if (mapNext.MapID == idMapNext)
					{
						XmapController.NextMap(mapNext);
						return;
					}
				}
			}
			GameScr.info1.addInfo("Lỗi tại dữ liệu", 0);
		}

		private static void NextMap(MapNext mapNext)
		{
			
			switch (mapNext.Type)
			{
				case TypeMapNext.AutoWaypoint:
					XmapController.NextMapAutoWaypoint(mapNext);
					return;
				case TypeMapNext.NpcMenu:
					XmapController.NextMapNpcMenu(mapNext);
					return;
				case TypeMapNext.NpcPanel:
					XmapController.NextMapNpcPanel(mapNext);
					return;
				case TypeMapNext.Position:
					XmapController.NextMapPosition(mapNext);
					return;
				case TypeMapNext.Capsule:
					XmapController.NextMapCapsule(mapNext);
					return;
				default:
					return;
			}
		}

		private static void NextMapAutoWaypoint(MapNext mapNext)
		{
			Waypoint waypoint = XmapData.FindWaypoint(mapNext.MapID);
			if (waypoint != null)
			{
				int posWaypointX = XmapData.GetPosWaypointX(waypoint);
				int posWaypointY = XmapData.GetPosWaypointY(waypoint);
				XmapController.MoveMyChar(posWaypointX, posWaypointY);
				XmapController.RequestChangeMap(waypoint);
			}
			if (waypoint == null && (mapNext.MapID == 46 || mapNext.MapID == 47) && global::Char.myCharz().cx < 600)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(610, global::Char.myCharz().cy);
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000902DC File Offset: 0x0008E4DC
		private static void NextMapNpcMenu(MapNext mapNext)
		{
			int num = mapNext.Info[0];
			Service.gI().openMenu(num);
			for (int i = 1; i < mapNext.Info.Length; i++)
			{
				int num2 = mapNext.Info[i];
				Service.gI().confirmMenu((short)num, (sbyte)num2);
			}
		}

		private static void NextMapNpcPanel(MapNext mapNext)
		{
			int num = mapNext.Info[0];
			int num2 = mapNext.Info[1];
			int selected = mapNext.Info[2];
			Service.gI().openMenu(num);
			Service.gI().confirmMenu((short)num, (sbyte)num2);
			Service.gI().requestMapSelect(selected);
		}

		private static void NextMapPosition(MapNext mapNext)
		{
			int x = mapNext.Info[0];
			int y = mapNext.Info[1];
			XmapController.MoveMyChar(x, y);
			Service.gI().requestChangeMap();
			Service.gI().getMapOffline();
		}

		private static void NextMapCapsule(MapNext mapNext)
		{
			XmapController.SaveIdMapCapsuleReturn();
			int selected = mapNext.Info[0];
			Service.gI().requestMapSelect(selected);
		}

		public static void UseCapsuleNormal()
		{
			Pk9rXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, -1, 193);
		}

		public static void UseCapsuleVip()
		{
			Pk9rXmap.IsShowPanelMapTrans = false;
			Service.gI().useItem(0, 1, -1, 194);
		}

		public static void HideInfoDlg()
		{
			InfoDlg.hide();
		}

		private static void MoveMyChar(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			if (!ItemTime.isExistItem(4387))
			{
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y + 1;
				Service.gI().charMove();
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
			}
		}

		private static void RequestChangeMap(Waypoint waypoint)
		{
			if (waypoint.isOffline)
			{
				Service.gI().getMapOffline();
				return;
			}
			Service.gI().requestChangeMap();
		}

		private const int TIME_DELAY_NEXTMAP = 200;

		private const int TIME_DELAY_RENEXTMAP = 500;

		private const int ID_ITEM_CAPSULE_VIP = 194;

		private const int ID_ITEM_CAPSULE = 193;

		private const int ID_ICON_ITEM_TDLT = 4387;

		private static readonly XmapController _Instance = new XmapController();

		// Token: 0x04001323 RID: 4899
		private static int IdMapEnd;

		// Token: 0x04001324 RID: 4900
		private static List<int> WayXmap;

		// Token: 0x04001325 RID: 4901
		private static int IndexWay;

		// Token: 0x04001326 RID: 4902
		private static bool IsNextMapFailed;

		// Token: 0x04001327 RID: 4903
		private static bool IsWait;

		// Token: 0x04001328 RID: 4904
		private static long TimeStartWait;

		// Token: 0x04001329 RID: 4905
		private static long TimeWait;

		// Token: 0x0400132A RID: 4906
		private static bool IsWaitNextMap;
	}
}
