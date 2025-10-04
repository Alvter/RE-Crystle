using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;

public class LoadChart : MonoBehaviour
{
    [System.Serializable]
    public class Note
    {
        public bool isFakenote;
        public bool isSkynote;
        public int skyjudgeNumber;
        public int notetype;
        public float startJudgeTime;
        public float endJudgeTime;
        public float xOffset;
        public int speedGroup;
        public List<Draginfo> draginfolist;
    }

    [System.Serializable]
    public class Note2
    {
        public bool isFakenote;
        public int notetype;
        public float xOffset;
        public int speedGroup;
        public List<Draginfo> draginfolist;

        public float st;
        public float et;
        public float longt;
        public bool isPlayAudio = false;
        public bool isPlay = false;
        public bool isAdd = false;
    }

    [System.Serializable]
    public class Trace
    {
        public List<Note2> noteLists = new();
        
    }

    [System.Serializable]
    public class Line
    {
        public List<Note2> noteLists = new();
    }

    [System.Serializable]
    public class Draginfo
    {
        public bool isFakenote;
        public bool isSkynote;
        public int skyjudgeNumber;
        public float startJudgeTime;
        public float xOffset;
        public int speedGroup;
    }

    [System.Serializable]
    public class SpeedGroupInGames
    {
        public int speedGroupNumber;
        public List<float> time;
        public List<float> speed;
    }

    [System.Serializable]
    public class SpeedEvents
    {
        public float end;
        public float endTime;
        public float start;
        public float startTime;
        //public string condition = "undo";
        //public float st;
        //public float et;
        //public float set;
        //public float se;
    }

    [System.Serializable]
    public class ChartInfos
    {
        public string levelName;
        public string bpm;
        public string composer;
        public string designer;
        public string level;
        public string chartID;
    }

    [System.Serializable]
    public class ConnenctList
    {
        public List<Connenct> connencts;
    }

    [System.Serializable]
    public class Connenct
    {
        public float startJudgeTime;
        public int speedGroup;

        public float x;
        public float y;
        public float z = 3000f;
        public Vector3 v3 = new();
    }

    [System.Serializable]
    public class Chart
    {
        public ChartInfos chartInfos;
        public List<Note> noteLists;
        public Trace[] traceList = new Trace[4];
        public Line line;
        public List<SpeedGroupInGames> speedGroupInGames;
        public List<List<Connenct>> connenctList = new();
        public List<List<SpeedEvents>> speedEventList = new();
    }

    public static string chartName = "ra";
    public static Chart chart;
    public TextAsset textAsset;
    public string json;
    public UnityEvent ChartLoadOver;
    public AudioSource music;
    public bool isOver = false;

    public static float sx = 320f;//x���򳤶�
    public static float sy = 180f;//y�������
    public static float ns = 150f;//note�ٶ�
    //public static float scale = 35.5f;//��ͼ����

    void Awake()
    {
        textAsset = Resources.Load<TextAsset>("Chart/" + chartName + "/" + chartName);
        json = textAsset.text;
        chart = JsonUtility.FromJson<Chart>(json);

        music.clip = Resources.Load<AudioClip>("Chart/" + chartName + "/" + chartName);

        Application.targetFrameRate = 1000;
    }

    void Update()
    {
        if (chart != null && !isOver)
        {
            isOver = true;
            ChartLoadOver.Invoke();
        }
    }

}
