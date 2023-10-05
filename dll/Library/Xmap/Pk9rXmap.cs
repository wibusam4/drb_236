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
				if (Pk9rXmap.IsXmapRunning)
				{
					XmapController.FinishXmap();
					GameScr.info1.addInfo("Đã huỷ Xmap", 0);
				}
				else
				{
					XmapController.ShowXmapMenu();
				}
			}
			else if (Pk9rXmap.IsGetInfoChat<int>(text, "/xmp"))
			{
				if (Pk9rXmap.IsXmapRunning)
				{
					XmapController.FinishXmap();
					GameScr.info1.addInfo("Đã huỷ Xmap", 0);
				}
				else
				{
					XmapController.StartRunToMapId(Pk9rXmap.GetInfoChat<int>(text, "/xmp"));
				}
			}
			else if (text == "/csb")
			{
				Pk9rXmap.IsUseCapsuleNormal = !Pk9rXmap.IsUseCapsuleNormal;
				GameScr.info1.addInfo("Sử dụng capsule thường Xmap: " + (Pk9rXmap.IsUseCapsuleNormal ? "Bật" : "Tắt"), 0);
			}
			else
			{
				if (!(text == "/csdb"))
				{
					return false;
				}
				Pk9rXmap.IsUseCapsuleVip = !Pk9rXmap.IsUseCapsuleVip;
				GameScr.info1.addInfo("Sử dụng capsule đặc biệt Xmap: " + (Pk9rXmap.IsUseCapsuleVip ? "Bật" : "Tắt"), 0);
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
				Pk9rXmap.Chat("xmp");
			}
			else
			{
				Pk9rXmap.Chat("csb");
			}
			return true;
		}

		public static void Update()
		{
			if (XmapData.Instance().IsLoading)
			{
				XmapData.Instance().Update();
			}
			if (Pk9rXmap.IsXmapRunning)
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
				global::Char.myCharz().isTeleport = false;
				if (teleport.type == 0)
				{
					Controller.isStopReadMessage = false;
					global::Char.ischangingMap = true;
				}
				Teleport.vTeleport.removeElement(teleport);
				return true;
			}
			return false;
		}

		public static void SelectMapTrans(int selected)
		{
			if (Pk9rXmap.IsMapTransAsXmap)
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
			Pk9rXmap.IsMapTransAsXmap = false;
			if (Pk9rXmap.IsShowPanelMapTrans)
			{
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				return;
			}
			Pk9rXmap.IsShowPanelMapTrans = true;
		}

		public static void FixBlackScreen()
		{
			Controller.gI().loadCurrMap(0);
			Service.gI().finishLoadMap();
			global::Char.isLoadingMap = false;
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
