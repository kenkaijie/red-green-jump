using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager: MonoBehaviour
{
    public List<ScrollingBackground> BackgroundObjects;

    public void SetMotionState(bool doMovement)
    {
        foreach (var background in BackgroundObjects)
        {
            background.SetMotionState(doMovement);
        }
    }
}