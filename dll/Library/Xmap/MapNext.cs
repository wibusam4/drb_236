using System;

namespace Library.Xmap
{
	// Token: 0x020000E5 RID: 229
	public struct MapNext
	{
		// Token: 0x060009ED RID: 2541 RVA: 0x0000811E File Offset: 0x0000631E
		public MapNext(int mapID, TypeMapNext type, int[] info)
		{
			this.MapID = mapID;
			this.Type = type;
			this.Info = info;
		}

		// Token: 0x0400130E RID: 4878
		public int MapID;

		// Token: 0x0400130F RID: 4879
		public TypeMapNext Type;

		// Token: 0x04001310 RID: 4880
		public int[] Info;
	}
}
