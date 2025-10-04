using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class HideNote : MonoBehaviour
{
    public Transform realTraceList;
    public Transform realLine;

    public bool isStart = false;

    public int[] noteIndex = new int[5] { 0, 0, 0, 0, 0 };

    public void ScriptStart() 
    {
        isStart = !isStart;
    }

    void Update()
    {
        if (!isStart) return;

        int m;
        for (int i = 0; i < chart.traceList.Length; i++)
        {
            m = noteIndex[i];

            if (chart.traceList[i].noteLists.Count <= 0 || realTraceList.GetChild(i).childCount <= 0) continue;
            var note = chart.traceList[i].noteLists[m];
            if (note.et > realTime) continue;
            realTraceList.GetChild(i).GetChild(m).gameObject.SetActive(false);
            if (m < chart.traceList[i].noteLists.Count - 1) noteIndex[i]++;
        }

        m = noteIndex[4];
        for (int i = m; i < chart.line.noteLists.Count; i++)
        {
            if (chart.line.noteLists.Count <= 0 || realLine.childCount <= 0) return;
            var notes = chart.line.noteLists[i];
            if (notes.et > realTime) continue;
            realLine.GetChild(i).gameObject.SetActive(false);
            if (m < chart.line.noteLists.Count - 1 && notes.notetype != 2) noteIndex[4]++;
        }
    }
}
