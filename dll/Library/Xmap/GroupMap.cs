using System;
using System.Collections.Generic;

namespace Library.Xmap
{
	// Token: 0x020000E4 RID: 228
	public struct GroupMap
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x0000810E File Offset: 0x0000630E
		public GroupMap(string nameGroup, List<int> idMaps)
		{
			this.NameGroup = nameGroup;
			this.IdMaps = idMaps;
		}

		// Token: 0x0400130C RID: 4876
		public string NameGroup;

		// Token: 0x0400130D RID: 4877
		public List<int> IdMaps;
	}
}
