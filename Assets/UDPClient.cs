using UnityEngine;
using System.Net.Sockets;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using System.Net;

public class UDPClient : MonoBehaviour
{
    // broadcast address
    public string host = "192.168.0.255";
    public int port = 3333;
    private UdpClient client;
    //private float x;
    public ray r;

    void Start ()
    {
        var remote = new IPEndPoint(
                        IPAddress.Parse("192.168.1.6"),
                        8001);
        //client = new UdpClient();
        client = new UdpClient(8001);
        client.EnableBroadcast = true;
        //client.Connect(host, port);
        client.Connect(remote);
    }

    void Update ()
    {
    }

    public void send(string f)
    {
        //if(GUI.Button (new Rect (10,10,100,40), "Send"))
        //{
            //x = 0.1f;
            byte[] dgram = Encoding.UTF8.GetBytes(f);
            //f = r.distance3; //いずれモータ角度に直す
            //byte[] dgram = BitConverter.GetBytes(f);
            client.Send(dgram, dgram.Length);
        //}
    }

    void OnApplicationQuit()
    {
        client.Close();
    }
}