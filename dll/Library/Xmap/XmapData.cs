using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.Xmap
{
	// Token: 0x020000EA RID: 234
	public class XmapData
	{
		
		private XmapData()
		{
			this.GroupMaps = new List<GroupMap>();
			this.MyLinkMaps = null;
			this.IsLoading = false;
			this.IsLoadingCapsule = false;
		}

		public static XmapData Instance()
		{
			if (XmapData._Instance == null)
			{
				XmapData._Instance = new XmapData();
			}
			return XmapData._Instance;
		}

		public void LoadLinkMaps()
		{
			this.IsLoading = true;
		}

		public void Update()
		{
			if (this.IsLoadingCapsule)
			{
				if (!this.IsWaitInfoMapTrans())
				{
					this.LoadLinkMapCapsule();
					this.IsLoadingCapsule = false;
					this.IsLoading = false;
				}
				return;
			}
			this.LoadLinkMapBase();
			if (XmapData.CanUseCapsuleVip())
			{
				XmapController.UseCapsuleVip();
				this.IsLoadingCapsule = true;
				return;
			}
			if (XmapData.CanUseCapsuleNormal())
			{
				XmapController.UseCapsuleNormal();
				this.IsLoadingCapsule = true;
				return;
			}
			this.IsLoading = false;
		}

		public void LoadGroupMapsFromFile(string path)
		{
			this.GroupMaps.Clear();
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					if (!text.StartsWith("#") && !text.Equals(""))
					{
						List<int> idMaps = Array.ConvertAll<string, int>(streamReader.ReadLine().Trim().Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s)).ToList<int>();
						this.GroupMaps.Add(new GroupMap(text, idMaps));
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
			this.RemoveMapsHomeInGroupMaps();
		}

		private void RemoveMapsHomeInGroupMaps()
		{
			int cgender = global::Char.myCharz().cgender;
			foreach (GroupMap groupMap in this.GroupMaps)
			{
				if (cgender != 0)
				{
					if (cgender != 1)
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(22);
					}
					else
					{
						groupMap.IdMaps.Remove(21);
						groupMap.IdMaps.Remove(23);
					}
				}
				else
				{
					groupMap.IdMaps.Remove(22);
					groupMap.IdMaps.Remove(23);
				}
			}
		}

		private void LoadLinkMapCapsule()
		{
			this.AddKeyLinkMaps(TileMap.mapID);
			string[] mapNames = GameCanvas.panel.mapNames;
			for (int i = 0; i < mapNames.Length; i++)
			{
				int idMapFromName = XmapData.GetIdMapFromName(mapNames[i]);
				if (idMapFromName != -1)
				{
					int[] info = new int[]
					{
						i
					};
					this.MyLinkMaps[TileMap.mapID].Add(new MapNext(idMapFromName, TypeMapNext.Capsule, info));
				}
			}
		}

		private void LoadLinkMapBase()
		{
			this.MyLinkMaps = new Dictionary<int, List<MapNext>>();
			this.LoadLinkMapsFromFile("Xmap\\LinkMapsXmap.txt");
			this.LoadLinkMapsAutoWaypointFromFile("Xmap\\AutoLinkMapsWaypoint.txt");
			this.LoadLinkMapsHome();
			this.LoadLinkMapSieuThi();
			this.LoadLinkMapToCold();
		}

		private void LoadLinkMapsFromFile(string path)
		{
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					if (!text.StartsWith("#") && !text.Equals(""))
					{
						int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s));
						int num = array.Length - 3;
						int[] array2 = new int[num];
						Array.Copy(array, 3, array2, 0, num);
						this.LoadLinkMap(array[0], array[1], (TypeMapNext)array[2], array2);
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		private void LoadLinkMapsAutoWaypointFromFile(string path)
		{
			try
			{
				StreamReader streamReader = new StreamReader(path);
				string text;
				while ((text = streamReader.ReadLine()) != null)
				{
					text = text.Trim();
					if (!text.StartsWith("#") && !text.Equals(""))
					{
						int[] array = Array.ConvertAll<string, int>(text.Split(new char[]
						{
							' '
						}), (string s) => int.Parse(s));
						for (int i = 0; i < array.Length; i++)
						{
							if (i != 0)
							{
								this.LoadLinkMap(array[i], array[i - 1], TypeMapNext.AutoWaypoint, null);
							}
							if (i != array.Length - 1)
							{
								this.LoadLinkMap(array[i], array[i + 1], TypeMapNext.AutoWaypoint, null);
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				GameScr.info1.addInfo(ex.Message, 0);
			}
		}

		private void LoadLinkMapsHome()
		{
			int cgender = global::Char.myCharz().cgender;
			int num = 21 + cgender;
			int num2 = 7 * cgender;
			this.LoadLinkMap(num2, num, TypeMapNext.AutoWaypoint, null);
			this.LoadLinkMap(num, num2, TypeMapNext.AutoWaypoint, null);
		}

		private void LoadLinkMapSieuThi()
		{
			int cgender = global::Char.myCharz().cgender;
			int idMapNext = 24 + cgender;
			int[] array = new int[2];
			array[0] = 10;
			int[] info = array;
			this.LoadLinkMap(84, idMapNext, TypeMapNext.NpcMenu, info);
		}

		private void LoadLinkMapToCold()
		{
			if (global::Char.myCharz().taskMaint.taskId > 30)
			{
				int[] array = new int[2];
				array[0] = 12;
				int[] info = array;
				this.LoadLinkMap(19, 109, TypeMapNext.NpcMenu, info);
			}
		}

		public List<MapNext> GetMapNexts(int idMap)
		{
			if (this.CanGetMapNexts(idMap))
			{
				return this.MyLinkMaps[idMap];
			}
			return null;
		}

		public bool CanGetMapNexts(int idMap)
		{
			return this.MyLinkMaps.ContainsKey(idMap);
		}

		private void LoadLinkMap(int idMapStart, int idMapNext, TypeMapNext type, int[] info)
		{
			this.AddKeyLinkMaps(idMapStart);
			MapNext item = new MapNext(idMapNext, type, info);
			this.MyLinkMaps[idMapStart].Add(item);
		}

		private void AddKeyLinkMaps(int idMap)
		{
			if (!this.MyLinkMaps.ContainsKey(idMap))
			{
				this.MyLinkMaps.Add(idMap, new List<MapNext>());
			}
		}

		private bool IsWaitInfoMapTrans()
		{
			return !Pk9rXmap.IsShowPanelMapTrans;
		}

		public static int GetIdMapFromPanelXmap(string mapName)
		{
			return int.Parse(mapName.Split(new char[]
			{
				':'
			})[0]);
		}

		public static Waypoint FindWaypoint(int idMap)
		{
			for (int i = 0; i < TileMap.vGo.size(); i++)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
				if (XmapData.GetTextPopup(waypoint.popup).Equals(TileMap.mapNames[idMap]))
				{
					return waypoint;
				}
			}
			return null;
		}

		public static int GetPosWaypointX(Waypoint waypoint)
		{
			if (waypoint.maxX < 60)
			{
				return 15;
			}
			if ((int)waypoint.minX > TileMap.pxw - 60)
			{
				return TileMap.pxw - 15;
			}
			return (int)(waypoint.minX + 30);
		}

		public static int GetPosWaypointY(Waypoint waypoint)
		{
			return (int)waypoint.maxY;
		}

		public static bool IsMyCharDie()
		{
			return global::Char.myCharz().statusMe == 14 || global::Char.myCharz().cHP <= 0;
		}

		public static bool CanNextMap()
		{
			return !global::Char.isLoadingMap && !global::Char.ischangingMap && !Controller.isStopReadMessage;
		}

		private static int GetIdMapFromName(string mapName)
		{
			int cgender = global::Char.myCharz().cgender;
			if (mapName.Equals("Về nhà"))
			{
				return 21 + cgender;
			}
			if (mapName.Equals("Trạm tàu vũ trụ"))
			{
				return 24 + cgender;
			}
			if (mapName.Contains("Về chỗ cũ: "))
			{
				mapName = mapName.Replace("Về chỗ cũ: ", "");
				if (TileMap.mapNames[Pk9rXmap.IdMapCapsuleReturn].Equals(mapName))
				{
					return Pk9rXmap.IdMapCapsuleReturn;
				}
				if (mapName.Equals("Rừng đá"))
				{
					return -1;
				}
			}
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (mapName.Equals(TileMap.mapNames[i]))
				{
					return i;
				}
			}
			return -1;
		}

		private static string GetTextPopup(PopUp popUp)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < popUp.says.Length; i++)
			{
				stringBuilder.Append(popUp.says[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString().Trim();
		}

		private static bool CanUseCapsuleNormal()
		{
			return !XmapData.IsMyCharDie() && Pk9rXmap.IsUseCapsuleNormal && XmapData.HasItemCapsuleNormal();
		}

		private static bool HasItemCapsuleNormal()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == 193)
				{
					return true;
				}
			}
			return false;
		}

		private static bool CanUseCapsuleVip()
		{
			return !XmapData.IsMyCharDie() && Pk9rXmap.IsUseCapsuleVip && XmapData.HasItemCapsuleVip();
		}

		private static bool HasItemCapsuleVip()
		{
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			for (int i = 0; i < arrItemBag.Length; i++)
			{
				if (arrItemBag[i] != null && arrItemBag[i].template.id == 194)
				{
					return true;
				}
			}
			return false;
		}

		private const int ID_MAP_HOME_BASE = 21;

		private const int ID_MAP_TTVT_BASE = 24;

		private const int ID_ITEM_CAPSUAL_VIP = 194;

		private const int ID_ITEM_CAPSUAL_NORMAL = 193;

		private const int ID_MAP_TPVGT = 19;

		private const int ID_MAP_TO_COLD = 109;

		public List<GroupMap> GroupMaps;

		public Dictionary<int, List<MapNext>> MyLinkMaps;

		public bool IsLoading;

		private bool IsLoadingCapsule;

		private static XmapData _Instance;
	}
}
