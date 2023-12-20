using UnityEngine;
using System.Collections;

public class StatManager : MonoBehaviour
{


    public void AdjustStat(ref int stat, int value)
    {
        stat += value;
        if (stat < 1)
        {
            stat = 1;
        }
    }

    public void AdjustStat(ref float stat, float value)
    {
        stat += value;
        if (stat < 1)
        {
            stat = 1;
        }
    }

}
