﻿using UnityEngine;

public class HandCtrl : MonoBehaviour {
    public static float[] values = new float[20];
    public GameObject[] Hands;//保存模型引用
    public void MoveHand(HandCtrlData handdata)
    {

        for (int i = 0; i < 15; i++)
        {
            values[i] = handdata.HandData[i];//传递获取的手套数据
        }
        //0,1,4,6,7,10,13号骨骼
        Hands[0].transform.localRotation
               = Quaternion.Euler(70f * values[0], 0f, -15f);
        Hands[1].transform.localRotation
            = Quaternion.Euler(85f * values[1], 0f, 0f);
        Hands[4].transform.localRotation
            = Quaternion.Euler(110f * values[4], 0f, 0f);
        Hands[6].transform.localRotation
            = Quaternion.Euler(90f * values[6], 0f, 0f);
        Hands[7].transform.localRotation
            = Quaternion.Euler(110f * values[7], 0f, 0f);
        Hands[10].transform.localRotation
            = Quaternion.Euler(110f * values[10], 0f, 0f);
        Hands[13].transform.localRotation
            = Quaternion.Euler(110f * values[13], 0f, 0f);

        //5,8,11,14号骨骼
        Hands[5].transform.localRotation
            = Quaternion.Euler(110f * values[4] * 0.667f, 0f, 0f);
        Hands[8].transform.localRotation
            = Quaternion.Euler(110f * values[7] * 0.667f, 0f, 0f);
        Hands[11].transform.localRotation
            = Quaternion.Euler(110f * values[10] * 0.667f, 0f, 0f);
        Hands[14].transform.localRotation
            = Quaternion.Euler(110f * values[13] * 0.667f, 0f, 0f);

        //3,9,12号骨骼
        Hands[3].transform.localRotation
            = Quaternion.Euler(85 * values[3], 0f, 28f * (1 - values[5]) - 15f);

        float ringmid = -20f * (1 - values[8]) + 10f;
        Hands[9].transform.localRotation
            = Quaternion.Euler(90 * values[9], 0f, ringmid);

        float ringlittle = -25f * (1 - values[11]) + 8f;
        Hands[12].transform.localRotation
            = Quaternion.Euler(90 * values[12], 0f, ringmid + ringlittle);

        //2号骨骼
        float thumb_y = values[2] * 1.3f - values[0] * 0.3f;
        if (thumb_y > 1) thumb_y = 1;
        else if (thumb_y < 0) thumb_y = 0;
        float thumb_x = 1 - values[2];
        Hands[2].transform.localRotation
            = Quaternion.Euler(30f * thumb_y,
            -5f * thumb_x - 15f,
            -30f * values[2] + 20f);

    }
}