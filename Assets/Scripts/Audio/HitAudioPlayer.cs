using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class HitAudioPlayer : MonoBehaviour
{
    public AudioSource tapAudio;
    public AudioSource flickAudio;
    public AudioSource dragAudio;

    public bool isStart = false;

    public int[] noteIndex = new int[5] { 0, 0, 0, 0, 0 };

    public float audioTime;
    public void ScriptStart()
    {
        isStart = !isStart;
    }

    void Update()
    {
        if (!isStart) return;

        audioTime = realTime + 0.05f;

        int k;

        for (int i = 0; i < chart.traceList.Length; i++)
        {
            k = noteIndex[i];
            if (chart.traceList[i].noteLists.Count <= 0) continue;
            var note = chart.traceList[i].noteLists[k];

            if (note.isFakenote)
            {
                if (k < chart.traceList[i].noteLists.Count - 1) noteIndex[i]++;
                continue;
            }
            if (audioTime - note.st < 0 || note.isPlayAudio) continue;

            switch (note.notetype)
            {
                case 0:
                    tapAudio.PlayOneShot(tapAudio.clip);
                    break;
                case 1:
                    flickAudio.PlayOneShot(flickAudio.clip);
                    break;
                case 2:
                    tapAudio.PlayOneShot(tapAudio.clip);
                    break;
                case 3:
                    dragAudio.PlayOneShot(dragAudio.clip);
                    break;
                default:
                    break;
            }
            
            note.isPlayAudio = true;
            if (k < chart.traceList[i].noteLists.Count - 1) noteIndex[i]++;
        }

        k = noteIndex[4];

        if (chart.line.noteLists.Count <= 0) return;
        var notes = chart.line.noteLists[k];

        if (notes.isFakenote)
        {
            if (k < chart.line.noteLists.Count - 1) noteIndex[4]++;
            return;
        }
        if (audioTime - notes.st < 0 || notes.isPlayAudio) return;

        switch (notes.notetype)
        {
            case 0:
                tapAudio.PlayOneShot(tapAudio.clip);
                break;
            case 1:
                flickAudio.PlayOneShot(flickAudio.clip);
                break;
            case 2:
                tapAudio.PlayOneShot(tapAudio.clip);
                break;
            case 3:
                dragAudio.PlayOneShot(dragAudio.clip);
                break;
            default:
                break;
        }

        notes.isPlayAudio = true;
        if (k < chart.line.noteLists.Count - 1) noteIndex[4]++;
    }
}
