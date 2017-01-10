using RtmpSharp.IO;
using System;

namespace BananaLib.RiotObjects.Platform
{
	[SerializedName("com.riotgames.platform.trade.api.contract.TradeContractDTO")]
	[Serializable]
	internal class TradeContractDTO
	{
		[SerializedName("requesterInternalSummonerName")]
		public string RequesterInternalSummonerName
		{
			get;
			set;
		}

		[SerializedName("requesterChampionId")]
		public double RequesterChampionId
		{
			get;
			set;
		}

		[SerializedName("state")]
		public string State
		{
			get;
			set;
		}

		[SerializedName("responderChampionId")]
		public double ResponderChampionId
		{
			get;
			set;
		}

		[SerializedName("responderInternalSummonerName")]
		public string ResponderInternalSummonerName
		{
			get;
			set;
		}
	}
}
