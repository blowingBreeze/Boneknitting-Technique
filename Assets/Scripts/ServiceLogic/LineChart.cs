using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineChart : MonoBehaviour {

    public LineRenderer m_Curve;
    public LineRenderer m_AxisX;
    public LineRenderer m_AxisY;

    public int m_nMaxCount = 100;

    private List<Vector3> m_Points;

    readonly int m_nAxisXLenth = 1;
    readonly int m_nAxisXCount = 6;
    readonly int m_nAxisYLenth = 1;
    readonly int m_nAxisYCount = 6;

    float m_fMaxX;
    float m_fMinX;
    int m_nMaxXIndex;
    int m_nMinXIndex;

    float m_fMaxY;
    float m_fMinY;
    int m_nMaxYIndex;
    int m_nMinYIndex;

    enum PeakType { MAXX,MINX,MAXY,MINY};
    // Use this for initialization
    void Start()
    {
        m_fMaxX = 0.0f;
        m_fMinX = 0f;
        m_nMaxXIndex = 0;
        m_nMinXIndex = 0;
        m_fMaxY = 0.0f;
        m_fMinY = 0f;
        m_nMaxYIndex = 0;
        m_nMinYIndex = 0;

        m_Points = new List<Vector3>();
    }

    // Update is called once per frame
    public void UpdateCurveData()
    {
        m_Curve.positionCount = m_Points.Count > m_nMaxCount ? m_nMaxCount : m_Points.Count;
        m_Curve.SetPositions(m_Points.ToArray());
    }

    private void CaculateDrawPoint(List<Vector2> points, int nCount, float fMaxX, float fMaxY, float fz = 0f)
    { 
        //for (int DrawIndex = nCount - 1, PointIndex = points.Count - 1; DrawIndex >= 0 && PointIndex >= 0; --DrawIndex, --PointIndex)
        //{
        //    DrawArray[DrawIndex] = new Vector3(points[PointIndex].x * fKx - fMinX, points[PointIndex].y * fKy, fz);
        //}
        //return DrawArray;
    }

    public void AddPoint(float x, float y, float z = 0f)
    {
        if (m_Points.Count == m_nMaxCount)
        {
            MovieListForward();
        }

        int tDrawPointCount = m_Points.Count < m_nMaxCount ? m_Points.Count : m_nMaxCount;
        Vector3[] vecDrawPoint = new Vector3[tDrawPointCount];

        m_Points.Add(new Vector3(x, y, z));  //先添加则下面要减一
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


            float fKx = m_nAxisXLenth / (m_fMaxX - m_fMinX);
            float fKy = m_nAxisYLenth / (m_fMaxY - m_fMinY);

            float fminX = m_fMinX * fKx;
            float fminY = m_fMinY * fKy;

        //for (int DrawIndex = tDrawPointCount - 1, PointIndex = m_Points.Count - 1; DrawIndex >= 0 && PointIndex >= 0; --DrawIndex, --PointIndex)
        //{
        //    DrawArray[DrawIndex] = new Vector3(points[PointIndex].x * fKx - fMinX, points[PointIndex].y * fKy, fz);
        //}
    }

    private void CaculateNewPeakValue(PeakType peakType)
    {
        switch(peakType)
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
                        if (m_Points[i].y< temp)
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
        for(int i=0;i<m_Points.Count-1;++i)
        {
            m_Points[i] = m_Points[i + 1];
        }
        --m_nMaxXIndex;
        --m_nMinXIndex;
        --m_nMaxYIndex;
        --m_nMinYIndex;
    }
}
