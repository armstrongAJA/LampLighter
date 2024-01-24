using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Exit
{
    public int exitIndex;
    public int entranceIndex;
    public string newLevelName;
    public bool isLevelExit;
    public Collider2D exitCollider;
    public int sceneTransitionMovementDirectionHorizontal;//1 or -1, 0 to disable
    public int sceneTransitionMovementDirectionVertical;//1 or -1, 0 to disable

}
