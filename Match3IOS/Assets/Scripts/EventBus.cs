using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventBus
{
    public static Action OnBoardChanged;
    public static Action OnGoalReached;

    public static Action<string> OnChipDestroyd;

    public static Action OnGoldChanged;
    public static Action OnChosenAnotherBackground;
    public static Action OnBoughtPowerUp;
}
