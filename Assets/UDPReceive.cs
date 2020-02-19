using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class UDPReceive : MonoBehaviour
{
    int LOCA_LPORT = 3333;
    static UdpClient udp;
    Thread thread;
    public SerialHandler serialHandler;
    

    void Start ()
    {
        udp = new UdpClient(LOCA_LPORT);
        //udp.Client.ReceiveTimeout = 1000;
        thread = new Thread(new ThreadStart(ThreadMethod));
        thread.Start(); 
    }

    void Update ()
    {
    }

    void OnApplicationQuit()
    {
        thread.Abort();
    }

    private static void ThreadMethod()
    {
        while(true)
        {
            IPEndPoint remoteEP = null;
            byte[] data = udp.Receive(ref remoteEP);
            float recx = BitConverter.ToSingle(data, 0);
            //string text = Encoding.ASCII.GetString(data);
            Debug.Log(recx);
            //string s1 = recx.ToString("f1");
            //serialHandler.Write(s1 + "\n");//受け取ったデータをシリアル通信でマイコンへ送信する部分をかく
        }
    } 
}