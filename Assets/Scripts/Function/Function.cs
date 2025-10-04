using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Updater;
using static LoadChart;

public class Function : MonoBehaviour
{
    public static int[] speedIndex;

    public void ScriptStart()
    {
        for (int i = 0; i < chart.speedGroupInGames.Count; i++)
        {
            speedIndex = new int[chart.speedGroupInGames.Count];
            speedIndex[i] = 0;
        }
    }

    public static float noteZpos(int group, float t)
    {
        if (t > realTime)
        {
            return getZpos(group, t, realTime);
        }
        else
        {
            return getZpos(group, realTime, t) * -1;
        }
    }

    public static List<SpeedEvents> speedEvent;
    public static float getZpos(int group, float t, float rt)
    {
        speedEvent = chart.speedEventList[group];
        int index = speedIndex[group];

        float posz = 0;//y坐标

        for (int i = index; i < speedEvent.Count; i++)
        {
            float startTime = speedEvent[i].startTime;//事件开始时间
            float _startTime = startTime;

            float endTime = speedEvent[i].endTime;//事件结束时间
            float _endTime = endTime;

            float startSpeed = speedEvent[i].start;//事件开始速度
            float endSpeed = speedEvent[i].end;//事件结束速度

            float noteTime = t;//note开始时间

            if (rt > _endTime)
            {
                if (index < speedEvent.Count - 1) speedIndex[group]++;
            }

            if (rt < endTime)
            {
                //startTime = Mathf.Max(0, Mathf.Min(rt, startTime));
                if (rt > startTime)
                {
                    startTime = rt;
                    startSpeed = (float)Mathf.Lerp(startSpeed, endSpeed, (rt - _startTime) / (_endTime - _startTime));
                }
                if (endTime > noteTime)
                {
                    endSpeed = (float)Mathf.Lerp(startSpeed, endSpeed, (noteTime - _startTime) / (_endTime - _startTime));
                    endTime = noteTime;
                }
                if (startTime > noteTime)
                {
                    return posz;
                }
                posz += (startSpeed + endSpeed) * (float)(endTime - startTime) / 2;
            }

        }
        return posz;
    }
}
