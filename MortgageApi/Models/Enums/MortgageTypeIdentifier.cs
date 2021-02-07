using System.ComponentModel;

namespace MortgageApi.Models.Enums
{
    public enum MortgageTypeIdentifier
    {
        [Description("None")]
        None = 0,
        [Description("2 Year Fixed Rate")]
        Fixed = 1,
        [Description("Variable Rate")]
        Variable = 2
    }
}