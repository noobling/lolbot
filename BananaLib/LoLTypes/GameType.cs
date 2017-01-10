// Decompiled with JetBrains decompiler
// Type: BananaLib.LoLTypes.GameType
// Assembly: BananaLib, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 75213AF3-E339-4AEB-B3FE-095F85BC5F53
// Assembly location: C:\Users\Hesa\Desktop\eZ\BananaLib.dll

using System.ComponentModel;

namespace BananaLib.LoLTypes
{
  public enum GameType
  {
    [Description("RANKED_TEAM_GAME")] RankedTeamGame,
    [Description("RANKED_GAME")] RankedGame,
    [Description("NORMAL_GAME")] NormalGame,
    [Description("GROUPFINDER")] TeamBuilder,
    [Description("CUSTOM_GAME")] CustomGame,
    [Description("TUTORIAL_GAME")] TutorialGame,
    [Description("PRACTICE_GAME")] PracticeGame,
    [Description("RANKED_GAME_SOLO")] RankedGameSolo,
    [Description("COOP_VS_AI")] CoopVsAi,
    [Description("RANKED_GAME_PREMADE")] RankedGamePremade,
  }
}
