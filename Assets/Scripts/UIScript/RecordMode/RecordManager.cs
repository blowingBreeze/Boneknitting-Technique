using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager:MonoBehaviour
{
    private RecordController m_RecordController;

    private void Awake()
    {
        m_RecordController = new RecordController();
    }

    public RecordController GetRecordController()
    {
        return m_RecordController;
    }



}
