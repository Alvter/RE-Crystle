using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static LoadChart;
using static Updater;

public class LineConnector : MonoBehaviour
{
    public bool isStart = false;

    public Material lineMaterial;

    public Transform ConnectLineList;

    private float lineWidth = 10f;

    private int index = 0;

    public void ScriptStart()
    {
        isStart = true;

        // ������������
        for (int i = 0; i < chart.connenctList.Count; i++)
        {
            // ������Ϸ���󣬲���� Line Renderer ���
            GameObject lineObject = new GameObject("Line" + i);
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            // �����ߵĿ��
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // ���� LineRenderer �Ĳ���
            lineRenderer.material = lineMaterial;

            // �����ߵ�λ�õ�
            for (int m = 0; m < chart.connenctList[i].Count; m++)
            {

                lineRenderer.positionCount = chart.connenctList[i].Count;

                lineRenderer.SetPosition(m, chart.connenctList[i][m].v3);
            }

            lineObject.transform.SetParent(ConnectLineList);
        }
    }

    void Update()
    {
        if (!isStart) return;

        int k = index;
        for (int i = k; i < chart.connenctList.Count; i++)
        {
            GameObject line = ConnectLineList.GetChild(i).gameObject;
            LineRenderer lineRenderer = line.GetComponent<LineRenderer>();

            for (int m = 0; m < chart.connenctList[i].Count; m++)
            {
                lineRenderer.SetPosition(m, chart.connenctList[i][m].v3);
            }

            if (realTime > chart.connenctList[i][chart.connenctList[i].Count - 1].startJudgeTime)
            {
                ConnectLineList.GetChild(i).gameObject.SetActive(false);
                //if (k < chart.connenctList.Count - 1) index++;
            }
        }
    }
}
