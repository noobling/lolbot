// Decompiled with JetBrains decompiler
// Type: BananaLib.RiotObjects.Platform.Summoner
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using RtmpSharp.IO;
using System;
using System.Collections.Generic;

namespace BananaLib.RiotObjects.Platform
{
  [SerializedName("com.riotgames.platform.summoner.Summoner")]
  [Serializable]
  public class Summoner : BaseSummoner
  {
    [SerializedName("revisionId")]
    public double RevisionId { get; set; }

    [SerializedName("revisionDate")]
    public DateTime RevisionDate { get; set; }

    [SerializedName("lastGameDate")]
    public DateTime LastGameDate { get; set; }

    [SerializedName("socialNetworkUserIds")]
    public List<object> SocialNetworkUserIds { get; set; }

    [SerializedName("previousSeasonHighestTier")]
    public string PreviousSeasonHighestTier { get; set; }

    [SerializedName("previousSeasonHighestTeamReward")]
    public int PreviousSeasonHighestTeamReward { get; set; }

    [SerializedName("tutorialFlag")]
    public bool TutorialFlag { get; set; }

    [SerializedName("helpFlag")]
    public bool HelpFlag { get; set; }

    [SerializedName("displayEloQuestionaire")]
    public bool DisplayEloQuestionaire { get; set; }

    [SerializedName("nameChangeFlag")]
    public bool NameChangeFlag { get; set; }

    [SerializedName("advancedTutorialFlag")]
    public bool AdvancedTutorialFlag { get; set; }
  }
}
