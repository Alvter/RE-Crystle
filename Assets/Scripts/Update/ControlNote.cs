using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class ControlNote : MonoBehaviour
{
    public bool isStart = false;

    public Transform RealTraceList;//显示轨道父对象
    public Transform WaitTraceList;//预等轨道父对象

    public Transform RealLine;
    public Transform WaitLine;

    public GameObject Tail;
    public GameObject Hold_tail1;
    public GameObject Hold_tail2;

    float tailHeight;
    float traceTail_1;
    float traceTail_2;

    SpriteRenderer spriteRenderer;

    public void ScriptStart()
    {
        spriteRenderer = Tail.GetComponent<SpriteRenderer>();
        tailHeight = spriteRenderer.sprite.bounds.size.y * Tail.transform.lossyScale.y;//hold尾所占y方向单位长度

        spriteRenderer = Hold_tail1.GetComponent<SpriteRenderer>();
        traceTail_1 = spriteRenderer.sprite.bounds.size.y * Tail.transform.lossyScale.y;//hold尾所占y方向单位长度

        spriteRenderer = Hold_tail2.GetComponent<SpriteRenderer>();
        traceTail_2 = spriteRenderer.sprite.bounds.size.y * Tail.transform.lossyScale.y;//hold尾所占y方向单位长度

        isStart = !isStart;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isStart) return;

        for (int i = 0; i < 5; i++)
        {
            if (i == 4)
            {
                for (int m = 0; m < RealLine.childCount; m++)
                {
                    var note = chart.line.noteLists[m];

                    if (note.isPlay) continue;

                    Transform noteTsf = RealLine.GetChild(m);

                    float sttime = note.st;
                    float edtime = note.et;
                    float lgtime = note.longt;

                    float resTime = sttime - realTime;

                    Vector3 localPosition;

                    float nz = ns * Function.noteZpos(note.speedGroup, sttime);
                    localPosition = new Vector3(noteTsf.position.x, -155, nz);

                    if (note.notetype == 2)
                    {
                        noteTsf.GetChild(0).localScale = new Vector2(1, (Function.noteZpos(note.speedGroup, edtime) - Function.noteZpos                                               (note.speedGroup, sttime)) * ns / tailHeight / 10);
                    }

                    noteTsf.localPosition = localPosition;
                }
            }
            else
            {
                if (RealTraceList.GetChild(i).childCount <= 0) continue;

                for (int m = 0; m < RealTraceList.GetChild(i).childCount; m++)
                {
                    var note = chart.traceList[i].noteLists[m];

                    if (note.isPlay) continue;

                    Transform noteTsf = RealTraceList.GetChild(i).GetChild(m);

                    float sttime = note.st;
                    float edtime = note.et;
                    float lgtime = note.longt;

                    float resTime = sttime - realTime;

                    Vector3 localPosition;

                    float nz = ns * Function.noteZpos(note.speedGroup, sttime);
                    localPosition = new Vector3(0, 0, nz);

                    if (note.notetype == 2)
                    {
                        noteTsf.GetChild(0).localScale = new Vector2(1, (Function.noteZpos(note.speedGroup, edtime) - Function.noteZpos                                               (note.speedGroup, sttime)) * ns / traceTail_1 / 10);
                        noteTsf.GetChild(1).localScale = new Vector2(1, (Function.noteZpos(note.speedGroup, edtime) - Function.noteZpos                                               (note.speedGroup, sttime)) * ns / traceTail_2 / 10);
                    }

                    noteTsf.localPosition = localPosition;
                }
            }
        }
    }
}
