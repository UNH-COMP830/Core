
using System.ComponentModel;

namespace GameSlam.Core.Enums
{
    /// <summary>
    /// How to map Enum to EF
    /// code: http://stackoverflow.com/questions/34557574/how-to-create-a-table-corresponding-to-enum-in-ef6-code-first
    /// </summary>
    public enum SystemTypeEnum
    {
        Windows = 1,
        Linux = 2,
        OSX = 3
    }

    public enum CategoryEnum
    {
        [Description("Action")]
        Action = 1,
        [Description("Shooter")]
        Shooter = 2,
        [Description("Puzzle")]
        Puzzle = 3,
        [Description("Sports")]
        Sports = 4,
        [Description("Music")]
        Music = 5,
        [Description("Multiplayer")]
        Multiplayer = 6,
        [Description("Adventure & RPG")]
        AdventureAndRPG = 7,
        [Description("Strategy & Defence")]
        StrategyAndDefence = 8,
        [Description("Tutorials")]
        Tutorials = 9
    }

    public enum ApprovalStatusEnum : int
    {
        [Description("Waiting For Approval")]
        PendingApproval = 1,
        [Description("Approved")]
        Approved = 2,
        [Description("Declined")]
        Declined = 3
    }
  
}
