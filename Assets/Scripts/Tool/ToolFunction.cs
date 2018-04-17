using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
public class FIleDialogStruct
{
    public int structSize = 0;
    public IntPtr dlgOwner = IntPtr.Zero;
    public IntPtr instance = IntPtr.Zero;
    public String filter = null;
    public String customFilter = null;
    public int maxCustFilter = 0;
    public int filterIndex = 0;
    public String file = null;
    public int maxFile = 0;
    public String fileTitle = null;
    public int maxFileTitle = 0;
    public String initialDir = null;
    public String title = null;
    public int flags = 0;
    public short fileOffset = 0;
    public short fileExtension = 0;
    public String defExt = null;
    public IntPtr custData = IntPtr.Zero;
    public IntPtr hook = IntPtr.Zero;
    public String templateName = null;
    public IntPtr reservedPtr = IntPtr.Zero;
    public int reservedInt = 0;
    public int flagsEx = 0;

    //打开文件选择对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetOpenFileName([In, Out] FIleDialogStruct ofn);


    // 打开文件保存对话框
    [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
    public static extern bool GetSaveFileName([In, Out] FIleDialogStruct ofd);
}

public class ToolFunction
{
    /// <summary>
    /// 将毫秒时间转换成 mm:ss:sss
    /// </summary>
    public static string TranslateToMSM(float fTimeInMiloSecond)
    {
        string ret;
        int nMinute = (int)(fTimeInMiloSecond / 60000) % 60;
        int nSecond = (int)(fTimeInMiloSecond / 1000) % 60;
        int nMilloSecond = (int)fTimeInMiloSecond % 1000;
        ret = string.Format("{0:D2} : {1:D2} : {2:D3}", nMinute, nSecond, nMilloSecond);
        return ret;
    }

    /// <summary>
    /// 弹出文件选择对话框，返回文件路径
    /// </summary>
    /// <param name="filter">文件过滤字符串，使用逗号分隔</param>
    /// <param name="title">窗口标题</param>
    /// <param name="extension">默认添加后缀</param>
    /// <returns>文件路径</returns>
    public static string OpenFilePath(string filter, string title, string extension)
    {
        FIleDialogStruct pth = new FIleDialogStruct();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = filter;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath;  // default path  
        pth.title = title;
        pth.defExt = extension;
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (FIleDialogStruct.GetOpenFileName(pth))
        {
            return pth.file;//选择的文件路径;  
        }
        else
        {
            return null;
        }
    }

    public static string SaveFilePath(string filter, string title, string extension)
    {
        FIleDialogStruct pth = new FIleDialogStruct();
        pth.structSize = System.Runtime.InteropServices.Marshal.SizeOf(pth);
        pth.filter = filter;
        pth.file = new string(new char[256]);
        pth.maxFile = pth.file.Length;
        pth.fileTitle = new string(new char[64]);
        pth.maxFileTitle = pth.fileTitle.Length;
        pth.initialDir = Application.dataPath;  // default path  
        pth.title = title;
        pth.defExt = extension;
        pth.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;
        if (FIleDialogStruct.GetSaveFileName(pth))
        {
            return pth.file;//选择的文件路径;  
        }
        else
        {
            return null;
        }
    }
}
