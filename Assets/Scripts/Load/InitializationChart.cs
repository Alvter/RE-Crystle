using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class InitializationChart : MonoBehaviour
{
    public bool isStart = false;

    public Transform RealTraceList;//显示判定线父对象
    public Transform WaitTraceList;//预等判定线父对象

    public GameObject RealTrace;//显示判定线父对象
    public GameObject WaitTrace;//预等判定线父对象

    public Transform RealLine;//显示判定线
    public Transform WaitLine;//预等判定线

    //note预制体
    public GameObject Tap;
    public GameObject Flick;
    public GameObject Hold;
    public GameObject Drag;
    public GameObject Tap_line;
    public GameObject Flick_line;
    public GameObject Hold_line;
    public GameObject Drag_line;

    public int[] noteIndex = new int[5] { 0, 0, 0, 0, 0 };

    public void ScriptStart()
    {

        //noteIndex = new int[chart.traceList.Length];
        //for (int i = 0; i < chart.traceList.Length; i++)
        //{
        //    noteIndex[i] = 0;
        //}

        var line = chart.line;

        for (int i = 0; i < line.noteLists.Count; i++)
        {
            var note = line.noteLists[i];
            float mx = note.xOffset;

            GameObject notes;

            mx = -115 + mx * 230;
            switch (note.notetype)
            {
                case 0://tap
                    notes = Instantiate(Tap_line, new Vector3(mx, -155, 3000), Quaternion.identity, WaitLine.transform);
                    break;
                case 1://flick
                    notes = Instantiate(Flick_line, new Vector3(mx, -155, 3000), Quaternion.identity, WaitLine.transform);
                    break;
                case 2://hold
                    notes = Instantiate(Hold_line, new Vector3(mx, -155, 3000), Quaternion.identity, WaitLine.transform);
                    break;
                case 3://drag
                    notes = Instantiate(Drag_line, new Vector3(mx, -155, 3000), Quaternion.identity, WaitLine.transform);
                    break;
                default:
                    break;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            var trace = chart.traceList[i];

            int kx = 1;
            int ky = 1;
            switch (i)
            {
                case 0:
                    kx = -1;
                    break;
                case 1:
                    break;
                case 2:
                    kx = -1;
                    ky = -1;
                    break;
                case 3:
                    ky = -1;
                    break;
                default:
                    break;
            }

            GameObject realTrace = Instantiate(RealTrace, new Vector3(230 * kx, 110 * ky, 0), Quaternion.identity, RealTraceList);//创建显示轨道
            GameObject waitTrace = Instantiate(WaitTrace, new Vector3(230 * kx, 110 * ky, 0), Quaternion.identity, WaitTraceList);//创建预等轨道;

            if (trace.noteLists.Count <= 0) continue;//判定判定线是否有note

            for (int m = 0; m < trace.noteLists.Count; m++)
            {
                var note = trace.noteLists[m];
                GameObject notes;
                switch (note.notetype)
                {
                    case 0://tap
                        notes = Instantiate(Tap, new Vector3(0, 1000, 0), Quaternion.identity, waitTrace.transform);
                        notes.transform.localScale = new Vector2(36 * kx, 36 * ky);
                        notes.transform.localPosition = new Vector3(0, 0, 3000);
                        break;
                    case 1://flick
                        notes = Instantiate(Flick, new Vector3(0, 1000, 0), Quaternion.identity, waitTrace.transform);
                        notes.transform.localScale = new Vector2(36 * kx, 36 * ky);
                        notes.transform.localPosition = new Vector3(0, 0, 3000);
                        break;
                    case 2://hold
                        notes = Instantiate(Hold, new Vector3(0, 1000, 0), Quaternion.identity, waitTrace.transform);
                        notes.transform.localScale = new Vector3(kx, ky, 1);
                        //notes.transform.GetChild(2).localScale = new Vector2(36, 36);
                        notes.transform.localPosition = new Vector3(0, 0, 3000);
                        break;
                    case 3://drag
                        notes = Instantiate(Drag, new Vector3(0, 1000, 0), Quaternion.identity, waitTrace.transform);
                        notes.transform.localScale = new Vector2(36 * kx, 36 * ky);
                        notes.transform.localPosition = new Vector3(0, 0, 3000);
                        break;
                    default:
                        break;
                }
            }
        }

        isStart = !isStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;

        for (int i = 0; i < 5; i++)
        {
            int m = noteIndex[i];
            if (i != 4)
            {
                var noteList = chart.traceList[i].noteLists;
                var note = noteList[m];
                if (!note.isAdd)
                {
                    if (WaitTraceList.GetChild(i).childCount == 0) continue;

                    Transform noteTsf = WaitTraceList.GetChild(i).GetChild(0);
                    noteTsf.parent = RealTraceList.GetChild(i);

                    note.isAdd = true;
                    if (m < noteList.Count - 1) noteIndex[i]++;
                }
                //if (note.st - realTime < 3f && !note.isAdd)
                //{
                //    if (WaitTraceList.GetChild(i).childCount == 0) continue;

                //    Transform noteTsf = WaitTraceList.GetChild(i).GetChild(0);
                //    noteTsf.parent = RealTraceList.GetChild(i);

                //    note.isAdd = true;
                //    if (m < noteList.Count - 1) noteIndex[i]++;
                //}
            }
            else
            {
                var noteList = chart.line.noteLists;
                var line = chart.line;
                var note = line.noteLists[m];
                if (!note.isAdd)
                {
                    if (WaitLine.childCount == 0) continue;

                    Transform noteTsf = WaitLine.GetChild(0);
                    noteTsf.parent = RealLine;

                    note.isAdd = true;
                    if (m < noteList.Count - 1) noteIndex[i]++;
                }
            }
        }
    }
}
