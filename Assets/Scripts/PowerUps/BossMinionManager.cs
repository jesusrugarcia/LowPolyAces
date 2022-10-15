using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinionManager : MonoBehaviour
{
    public BossManager bossManager;
    
    void OnDestroy()
    {
        bossManager.summonedMinions --;
    }
}
