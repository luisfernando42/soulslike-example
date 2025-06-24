using System.Collections.Generic;
using UnityEngine;

public abstract class AnimationKeys
{
    public static string VERTICAL { get; private set; } = "Vertical";
    public static string HORIZONTAL { get; private set; } = "Horizontal";
    public static string INTERACTING { get; private set; } = "IsInteracting";


    public static Dictionary<AnimationsEnum, string> animations = 
        new() 
        { 
            { AnimationsEnum.rolling, "Rolling" },
            { AnimationsEnum.backstab, "Backstab" },
            { AnimationsEnum.backstep, "Backstep" },
        };

}
