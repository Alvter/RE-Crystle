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

        // 遍历所有折线
        for (int i = 0; i < chart.connenctList.Count; i++)
        {
            // 创建游戏对象，并添加 Line Renderer 组件
            GameObject lineObject = new GameObject("Line" + i);
            LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();

            // 设置线的宽度
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            // 设置 LineRenderer 的材质
            lineRenderer.material = lineMaterial;

            // 设置线的位置点
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
