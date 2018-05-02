using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart : MonoBehaviour
{

    public LineRenderer m_Curve;
    public Camera m_ChartCamera;

    public TextMesh AxieXMax;
    public TextMesh AxieXMin;
    public TextMesh AxieYMax;
    public TextMesh AxieYMin;
    public TextMesh ShowData;

    public float LeftBorder;
    public float RightBorder;
    public float UpBorder;
    public float BottemBorder;

    private int m_nMaxCount = 500;
    private List<Vector3> m_Points;
    public int m_nAxisXLenth = 2;
    public int m_nAxisYLenth = 1;



    float m_fMaxX;
    float m_fMinX;
    int m_nMaxXIndex;
    int m_nMinXIndex;

    float m_fMaxY;
    float m_fMinY;
    int m_nMaxYIndex;
    int m_nMinYIndex;

    private float m_fKx;
    private float m_fKy;

    enum PeakType { MAXX, MINX, MAXY, MINY };

    float time;
    // Use this for initialization
    void Start()
    {
        m_Points = new List<Vector3>();
        m_fMaxX = 0.0f;
        m_fMinX = 0f;
        m_nMaxXIndex = 0;
        m_nMinXIndex = 0;
        m_fMaxY = 0.0f;
        m_fMinY = 0f;
        m_nMaxYIndex = 0;
        m_nMinYIndex = 0;
        m_fKx = 0f;
        m_fKy = 0f;
        time = 0f;
    }

    private void Update()
    {
        time += Time.deltaTime;
        float y = Random.Range(0, 10000 * Time.deltaTime);
        UpdateCurveData(time, y, 0);
    }

    // Update is called once per frame
    public void UpdateCurveData(float x, float y, float z)
    {
        m_Curve.positionCount = m_Points.Count > m_nMaxCount ? m_nMaxCount : m_Points.Count;
        m_Curve.SetPositions(AddPoint(x, y, z));
        AxieXMax.text = string.Format("{0:F2}/xx", m_fMaxX);
        AxieXMin.text = string.Format("{0:F2}", m_fMinX);
        AxieYMax.text = string.Format("{0:F2}/xx", m_fMaxY);
        AxieYMin.text = string.Format("{0:F2}", m_fMinY);
        ShowDataOnMouse();
    }


    public Vector3[] AddPoint(float x, float y, float z = 0f)
    {
        int tDrawPointCount = 0;
        Vector3[] DrawPoint;

        if (m_Points.Count >= m_nMaxCount)
        {
            MovieListForward();
            m_Points[m_Points.Count - 1] = new Vector3(x, y, z);
            tDrawPointCount = m_nMaxCount;
            DrawPoint = new Vector3[tDrawPointCount];
        }
        else
        {
            m_Points.Add(new Vector3(x, y, z));  //先添加则下面要减一
            tDrawPointCount = m_Points.Count;
            DrawPoint = new Vector3[tDrawPointCount];
        }

        if (x > m_fMaxX)
        {
            m_fMaxX = x;
            m_nMaxXIndex = m_Points.Count - 1;
        }
        else if (x < m_fMinX)
        {
            m_fMinX = x;
            m_nMinXIndex = m_Points.Count - 1;
        }

        if (y > m_fMaxY)
        {
            m_fMaxY = y;
            m_nMaxYIndex = m_Points.Count - 1;
        }
        else if (y < m_fMinY)
        {
            m_fMinY = y;
            m_nMinYIndex = m_Points.Count - 1;
        }


        if (m_nMaxXIndex < 0)
        {
            CaculateNewPeakValue(PeakType.MAXX);
        }
        if (m_nMinXIndex < 0)
        {
            CaculateNewPeakValue(PeakType.MINX);
        }
        if (m_nMaxYIndex < 0)
        {
            CaculateNewPeakValue(PeakType.MAXY);
        }
        if (m_nMinYIndex < 0)
        {
            CaculateNewPeakValue(PeakType.MINY);
        }


        m_fKx = m_nAxisXLenth / (m_fMaxX - m_fMinX);
        m_fKy = m_nAxisYLenth / (m_fMaxY - m_fMinY);

        float fminX = m_fMinX * m_fKx;
        float fminY = m_fMinY * m_fKy;

        for (int DrawIndex = tDrawPointCount - 1, PointIndex = m_Points.Count - 1; DrawIndex >= 0 && PointIndex >= 0; --DrawIndex, --PointIndex)
        {
            DrawPoint[DrawIndex].x = m_Points[PointIndex].x * m_fKx - fminX;
            DrawPoint[DrawIndex].y = m_Points[PointIndex].y * m_fKy - fminY;
            DrawPoint[DrawIndex].z = m_Points[PointIndex].z;
        }

        return DrawPoint;
    }

    private void CaculateNewPeakValue(PeakType peakType)
    {
        switch (peakType)
        {
            case PeakType.MAXX:
                {
                    float temp = m_Points[0].x;
                    int tempIndex = 0;
                    for (int i = 0; i < m_Points.Count; ++i)
                    {
                        if (m_Points[i].x > temp)
                        {
                            temp = m_Points[i].x;
                            tempIndex = i;
                        }
                    }
                    m_fMaxX = temp;
                    m_nMaxXIndex = tempIndex;
                }
                break;
            case PeakType.MINX:
                {
                    float temp = m_Points[0].x;
                    int tempIndex = 0;
                    for (int i = 0; i < m_Points.Count; ++i)
                    {
                        if (m_Points[i].x < temp)
                        {
                            temp = m_Points[i].x;
                            tempIndex = i;
                        }
                    }
                    m_fMinX = temp;
                    m_nMinXIndex = tempIndex;
                }
                break;
            case PeakType.MAXY:
                {
                    float temp = m_Points[0].y;
                    int tempIndex = 0;
                    for (int i = 0; i < m_Points.Count; ++i)
                    {
                        if (m_Points[i].y > temp)
                        {
                            temp = m_Points[i].y;
                            tempIndex = i;
                        }
                    }
                    m_fMaxY = temp;
                    m_nMaxYIndex = tempIndex;
                }
                break;
            case PeakType.MINY:
                {
                    float temp = m_Points[0].y;
                    int tempIndex = 0;
                    for (int i = 0; i < m_Points.Count; ++i)
                    {
                        if (m_Points[i].y < temp)
                        {
                            temp = m_Points[i].y;
                            tempIndex = i;
                        }
                    }
                    m_fMinY = temp;
                    m_nMinYIndex = tempIndex;
                }
                break;
            default:
                break;
        }
    }

    private void MovieListForward()
    {
        for (int i = 0; i < m_Points.Count - 1; ++i)
        {
            m_Points[i] = m_Points[i + 1];
        }
        --m_nMaxXIndex;
        --m_nMinXIndex;
        --m_nMaxYIndex;
        --m_nMinYIndex;
    }

    public void ShowDataOnMouse()
    {
        var mousePos = Input.mousePosition;
        var tempViewPos = m_ChartCamera.ScreenToViewportPoint(mousePos);
        var tempWordPos = m_ChartCamera.ScreenToWorldPoint(mousePos);

        Debug.Log(tempViewPos);

        if (tempViewPos.x < RightBorder&&tempViewPos.x>LeftBorder
            && tempViewPos.y > BottemBorder&&tempViewPos.y<UpBorder)
        {
            float tempX = (tempWordPos.x-transform.position.x) / m_fKx+m_fMinX;
            float tempY = (tempWordPos.y - transform.position.y) / m_fKy + m_fMinY;
            //if(tempX<=m_fMaxX&&tempY<=m_fMaxY
            //    &&tempX>=m_fMinX&&tempY>=m_fMinY)
            //{
            //    ShowData.text = string.Format("({0:F2},{1:F2})", tempX, tempY);

            //}
            //else
            //{
            //    ShowData.text = string.Format("(maxX,maxY):({0:F2},{1:F2})", m_fMaxX, m_fMaxY);
            //}
            ShowData.text = string.Format("({0:F2},{1:F2})",
                tempX < m_fMaxX ? (tempX < m_fMinX ? m_fMinX : tempX) : m_fMaxX,
                tempY < m_fMaxY ? (tempY < m_fMinY ? m_fMinY : tempY) : m_fMaxY);
        }
    }
}
