using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class ConnectListProcess : MonoBehaviour
{
    public bool isStart = false;

    public void ScriptStart()
    {
        isStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;

        for (int i = 0; i < chart.connenctList.Count; i++)
        {
            for (int m = 0; m < chart.connenctList[i].Count; m++)
            {
                Connenct cnt = chart.connenctList[i][m];

                float sttime = cnt.startJudgeTime;
                int group = cnt.speedGroup;

                float z = Function.noteZpos(group, sttime);

                if (z <= 3000f && z >= -3000f) cnt.v3 = new Vector3(cnt.v3.x, cnt.v3.y, z * ns);
            }
        }
    }
}
