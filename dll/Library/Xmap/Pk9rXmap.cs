using System;

namespace Library.Xmap
{
	// Token: 0x020000E6 RID: 230
	public class Pk9rXmap
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0008FB8C File Offset: 0x0008DD8C
		public static bool Chat(string text)
		{
			if (text == "/xmp")
			{
				if (IsXmapRunning)
				{
					XmapController.FinishXmap();
					GameScr.info1.addInfo("Đã huỷ Xmap", 0);
				}
				else
				{
					XmapController.ShowXmapMenu();
				}
			}
			else if (IsGetInfoChat<int>(text, "/xmp"))
			{
				if (IsXmapRunning)
				{
					XmapController.FinishXmap();
					GameScr.info1.addInfo("Đã huỷ Xmap", 0);
				}
				else
				{
					XmapController.StartRunToMapId(GetInfoChat<int>(text, "/xmp"));
				}
			}
			else if (text == "/csb")
			{
				IsUseCapsuleNormal = !IsUseCapsuleNormal;
				GameScr.info1.addInfo("Sử dụng capsule thường Xmap: " + (IsUseCapsuleNormal ? "Bật" : "Tắt"), 0);
			}
			else
			{
				if (!(text == "/csdb"))
				{
					return false;
				}
				IsUseCapsuleVip = !IsUseCapsuleVip;
				GameScr.info1.addInfo("Sử dụng capsule đặc biệt Xmap: " + (IsUseCapsuleVip ? "Bật" : "Tắt"), 0);
			}
			return true;
		}

		public static bool HotKeys()
		{
			int keyAsciiPress = GameCanvas.keyAsciiPress;
			if (keyAsciiPress != 99)
			{
				if (keyAsciiPress != 120)
				{
					return false;
				}
				Chat("xmp");
			}
			else
			{
				Chat("csb");
			}
			return true;
		}

		public static void Update()
		{
			if (XmapData.Instance().IsLoading)
			{
				XmapData.Instance().Update();
			}
			if (IsXmapRunning)
			{
				XmapController.Update();
			}
		}

		public static void Info(string text)
		{
			if (text.Equals("Bạn chưa thể đến khu vực này"))
			{
				XmapController.FinishXmap();
			}
		}

		public static bool XoaTauBay(object obj)
		{
			Teleport teleport = (Teleport)obj;
			if (teleport.isMe)
			{
				Char.myCharz().isTeleport = false;
				if (teleport.type == 0)
				{
					Controller.isStopReadMessage = false;
					Char.ischangingMap = true;
				}
				Teleport.vTeleport.removeElement(teleport);
				return true;
			}
			return false;
		}

		public static void SelectMapTrans(int selected)
		{
			if (IsMapTransAsXmap)
			{
				XmapController.HideInfoDlg();
				XmapController.StartRunToMapId(XmapData.GetIdMapFromPanelXmap(GameCanvas.panel.mapNames[selected]));
				return;
			}
			XmapController.SaveIdMapCapsuleReturn();
			Service.gI().requestMapSelect(selected);
		}

		public static void ShowPanelMapTrans()
		{
			IsMapTransAsXmap = false;
			if (IsShowPanelMapTrans)
			{
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				return;
			}
			IsShowPanelMapTrans = true;
		}

		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			Char.isLoadingMap = false;
		}

		private static bool IsGetInfoChat<T>(string text, string s)
		{
			if (!text.StartsWith(s))
			{
				return false;
			}
			try
			{
				Convert.ChangeType(text.Substring(s.Length), typeof(T));
			}
			catch
			{
				return false;
			}
			return true;
		}

		private static T GetInfoChat<T>(string text, string s)
		{
			return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
		}

		public static bool IsXmapRunning = false;

		public static bool IsMapTransAsXmap = false;

		public static bool IsShowPanelMapTrans = true;

		public static bool IsUseCapsuleNormal = false;

		public static bool IsUseCapsuleVip = true;

		public static int IdMapCapsuleReturn = -1;
	}
}
