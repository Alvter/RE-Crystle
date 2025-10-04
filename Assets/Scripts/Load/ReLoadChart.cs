using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System.Text.RegularExpressions;
using static LoadChart;

public class ReLoadChart : MonoBehaviour
{
    public bool isStart = false;

    public UnityEvent ChartLoadOver;
    public void ScriptStart()
    {
        for (int i = 0; i < chart.noteLists.Count; i++)
        {
            if (chart.noteLists[i].draginfolist == null || chart.noteLists[i].draginfolist.Count <= 0) continue;

            List<Connenct> connencts = new();

            var notes = chart.noteLists[i];
            Connenct connenct = new();

            int kx = 1;
            int ky = 1;
            switch (notes.skyjudgeNumber)
            {
                case 1:
                    kx = -1;
                    break;
                case 2:
                    break;
                case 3:
                    kx = -1;
                    ky = -1;
                    break;
                case 4:
                    ky = -1;
                    break;
                default:
                    break;
            }

            connenct.x = (notes.skyjudgeNumber == 0) ? -115 + 230 * notes.xOffset : 230 * kx;
            connenct.y = (notes.skyjudgeNumber == 0) ? -155 : 110 * ky;
            connenct.v3 = new Vector3(connenct.x, connenct.y, connenct.z);

            connenct.startJudgeTime = notes.startJudgeTime;
            connenct.speedGroup = notes.speedGroup;

            connencts.Add(connenct);

            for (int m = 0; m < chart.noteLists[i].draginfolist.Count; m++)
            {
                Note newNote = new();
                connenct = new();
                Draginfo note = chart.noteLists[i].draginfolist[m];

                if (note.isSkynote)
                {
                    newNote.startJudgeTime = note.startJudgeTime;
                    newNote.endJudgeTime = note.startJudgeTime;
                    newNote.isFakenote = note.isFakenote;
                    newNote.isSkynote = note.isSkynote;
                    newNote.skyjudgeNumber = note.skyjudgeNumber;
                    newNote.speedGroup = note.speedGroup;
                    newNote.xOffset = note.xOffset;
                    newNote.notetype = 3;

                    chart.noteLists.Add(newNote);

                    kx = 1;
                    ky = 1;
                    switch (note.skyjudgeNumber)
                    {
                        case 1:
                            kx = -1;
                            break;
                        case 2:
                            break;
                        case 3:
                            kx = -1;
                            ky = -1;
                            break;
                        case 4:
                            ky = -1;
                            break;
                        default:
                            break;
                    }

                    connenct.x = 230 * kx;
                    connenct.y = 110 * ky;
                    connenct.v3 = new Vector3(connenct.x, connenct.y, connenct.z);

                    connenct.startJudgeTime = note.startJudgeTime;
                    connenct.speedGroup = note.speedGroup;

                    connencts.Add(connenct);
                }
                else
                {
                    newNote.startJudgeTime = note.startJudgeTime;
                    newNote.endJudgeTime = note.startJudgeTime;
                    newNote.isFakenote = note.isFakenote;
                    newNote.isSkynote = note.isSkynote;
                    newNote.skyjudgeNumber = note.skyjudgeNumber;
                    newNote.speedGroup = note.speedGroup;
                    newNote.xOffset = note.xOffset;
                    newNote.notetype = 3;

                    chart.noteLists.Add(newNote);

                    connenct.x = -115f + note.xOffset * 230f;
                    connenct.y = -155f;
                    connenct.v3 = new Vector3(connenct.x, connenct.y, connenct.z);

                    connenct.startJudgeTime = note.startJudgeTime;
                    connenct.speedGroup = note.speedGroup;

                    connencts.Add(connenct);
                }
            }

            chart.connenctList.Add(connencts);

        }

        //°´Ê±¼äÅÅÐò
        chart.noteLists.Sort((note1, note2) => note1.startJudgeTime.CompareTo(note2.startJudgeTime));

        for (int i = 0; i < 4; i++)
        {
            chart.traceList[i] = new();
        }

        for (int i = 0; i < chart.noteLists.Count; i++)
        {
            var note = chart.noteLists[i];
            Note2 note2 = new();

            note2.st = note.startJudgeTime;
            note2.et = (note.notetype == 2) ? note.endJudgeTime : note.startJudgeTime;
            note2.longt = note2.et - note2.st;
            note2.isFakenote = note.isFakenote;
            note2.notetype = note.notetype;
            note2.xOffset = note.xOffset;
            note2.speedGroup = note.speedGroup;
            note2.draginfolist = note.draginfolist;

            switch (note.skyjudgeNumber)
            {
                case 0:
                    chart.line.noteLists.Add(note2);
                    break;
                case 1:
                    chart.traceList[0].noteLists.Add(note2);
                    break;
                case 2:
                    chart.traceList[1].noteLists.Add(note2);
                    break;
                case 3:
                    chart.traceList[2].noteLists.Add(note2);
                    break;
                case 4:
                    chart.traceList[3].noteLists.Add(note2);
                    break;
                default:
                    break;
            }
        }



        for (int i = 0; i < chart.speedGroupInGames.Count; i++)
        {
            var speedEvent = chart.speedGroupInGames[i];

            List<SpeedEvents> tempSpeedEvents = new();

            for (int m = 0; m < speedEvent.time.Count - 1; m++)
            {
                SpeedEvents tempSpeedEvent = new();

                tempSpeedEvent.startTime = speedEvent.time[m];
                tempSpeedEvent.endTime = speedEvent.time[m + 1];
                tempSpeedEvent.start = speedEvent.speed[m];
                tempSpeedEvent.end = speedEvent.speed[m + 1];

                tempSpeedEvents.Add(tempSpeedEvent);
            }

            chart.speedEventList.Add(tempSpeedEvents);
        }

        ChartLoadOver.Invoke();
        isStart = !isStart;
    }

    void Update()
    {
        
    }
}
