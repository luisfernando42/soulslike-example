using System.Collections.Generic;

public abstract class AnimationKeys
{
    public static string VERTICAL { get; private set; } = "Vertical";
    public static string HORIZONTAL { get; private set; } = "Horizontal";
    public static string INTERACTING { get; private set; } = "IsInteracting";
    public static string COMBO { get; private set; } = "CanCombo";


    public static Dictionary<AnimationsEnum, string> animations =
        new()
        {
            { AnimationsEnum.rolling, "Rolling" },
            { AnimationsEnum.backstab, "Backstab" },
            { AnimationsEnum.backstep, "Backstep" },
            { AnimationsEnum.falling, "Falling" },
            { AnimationsEnum.land, "Landing" },
            { AnimationsEnum.empty, "Empty" },
            { AnimationsEnum.damage1, "TakeDamage_01" },
            { AnimationsEnum.death1, "Death_01" },
        };

}
