using System;
using System.Collections.Generic;

namespace Library.Xmap
{
	// Token: 0x020000E8 RID: 232
	public class XmapAlgorithm
	{
		// Token: 0x060009FA RID: 2554 RVA: 0x0008FD74 File Offset: 0x0008DF74
		public static List<int> FindWay(int idMapStart, int idMapEnd)
		{
			List<int> wayPassedStart = XmapAlgorithm.GetWayPassedStart(idMapStart);
			return XmapAlgorithm.FindWay(idMapEnd, wayPassedStart);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0008FD90 File Offset: 0x0008DF90
		private static List<int> FindWay(int idMapEnd, List<int> wayPassed)
		{
			int num = wayPassed[wayPassed.Count - 1];
			if (num == idMapEnd)
			{
				return wayPassed;
			}
			if (!XmapData.Instance().CanGetMapNexts(num))
			{
				return null;
			}
			List<List<int>> list = new List<List<int>>();
			foreach (MapNext mapNext in XmapData.Instance().GetMapNexts(num))
			{
				List<int> list2 = null;
				if (!wayPassed.Contains(mapNext.MapID))
				{
					List<int> wayPassedNext = XmapAlgorithm.GetWayPassedNext(wayPassed, mapNext.MapID);
					list2 = XmapAlgorithm.FindWay(idMapEnd, wayPassedNext);
				}
				if (list2 != null)
				{
					list.Add(list2);
				}
			}
			return XmapAlgorithm.GetBestWay(list);
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0008FE48 File Offset: 0x0008E048
		private static List<int> GetBestWay(List<List<int>> ways)
		{
			if (ways.Count == 0)
			{
				return null;
			}
			List<int> list = ways[0];
			for (int i = 1; i < ways.Count; i++)
			{
				if (XmapAlgorithm.IsWayBetter(ways[i], list))
				{
					list = ways[i];
				}
			}
			return list;
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00008231 File Offset: 0x00006431
		private static List<int> GetWayPassedStart(int idMapStart)
		{
			return new List<int>
			{
				idMapStart
			};
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000823F File Offset: 0x0000643F
		private static List<int> GetWayPassedNext(List<int> wayPassed, int idMapNext)
		{
			return new List<int>(wayPassed)
			{
				idMapNext
			};
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0008FE90 File Offset: 0x0008E090
		private static bool IsWayBetter(List<int> way1, List<int> way2)
		{
			bool flag = XmapAlgorithm.IsBadWay(way1);
			bool flag2 = XmapAlgorithm.IsBadWay(way2);
			return (!flag || flag2) && ((!flag && flag2) || way1.Count < way2.Count);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000824E File Offset: 0x0000644E
		private static bool IsBadWay(List<int> way)
		{
			return XmapAlgorithm.IsWayGoFutureAndBack(way);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0008FED0 File Offset: 0x0008E0D0
		private static bool IsWayGoFutureAndBack(List<int> way)
		{
			List<int> list = new List<int>
			{
				27,
				28,
				29
			};
			for (int i = 1; i < way.Count - 1; i++)
			{
				if (way[i] == 102 && way[i + 1] == 24 && list.Contains(way[i - 1]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
