using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    static public int getRandomInArrayDistribution(List<float> distribution)
    {
        float sum = 0;
        foreach(var d in distribution)
        {
            sum += d;
        }
        var rand = Random.Range(0, sum);
        for(int i = 0; i < distribution.Count; i++)
        {
            if (rand < distribution[i])
            {
                return i;
            }
            else
            {
                rand -= distribution[i];
            }
        }
        
        return distribution.Count-1;
    }
}
