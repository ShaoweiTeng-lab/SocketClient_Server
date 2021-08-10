using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net.Sockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Net;
using System.Text;

public class Server : MonoBehaviour
{
    Socket serverSocket; //伺服器端socket
    Socket clientSocket; //客戶端socket
    IPEndPoint ipEnd; //偵聽埠
    string recvStr; //接收的字串
    string sendStr; //傳送的字串
    byte[] recvData = new byte[1024]; //接收的資料，必須為位元組
    byte[] sendData = new byte[1024]; //傳送的資料，必須為位元組
    int recvLen; //接收的資料長度
    Thread connectThread; //連線執行緒
    // Start is called before the first frame update
    void Start()
    {
        InitSocket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    ///初始化
    /// </summary>
    void InitSocket()
    {
        //定義偵聽埠,偵聽任何IP
        ipEnd = new IPEndPoint(IPAddress.Any, 5566);
        //定義套接字型別,在主執行緒中定義
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //連線
        serverSocket.Bind(ipEnd);
        //開始偵聽,最大10個連線
        serverSocket.Listen(10); 

        //開啟一個執行緒連線，必須的，否則主執行緒卡死
        connectThread = new Thread(new ThreadStart(SocketReceive));
        connectThread.Start();
    }
    void SocketReceive()
    {
        //連線
        SocketConnet();
        //進入接收迴圈
        while (true)
        {
            //對data清零
            recvData = new byte[1024];
            //獲取收到的資料的長度
            recvLen = clientSocket.Receive(recvData);
            //如果收到的資料長度為0，則重連並進入下一個迴圈
            if (recvLen == 0)
            {
                SocketConnet();
                continue;
            }
            //輸出接收到的資料
            recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
            print(recvStr);
            //將接收到的資料經過處理再發送出去
            sendStr = "From Server: " + recvStr;
            SocketSend(sendStr);
        }
    }
    /// <summary>
    /// 連線客戶端
    /// </summary>
    void SocketConnet()
    {
        if (clientSocket != null)
            clientSocket.Close();
        //控制檯輸出偵聽狀態
        Debug.Log("Waiting for a client");
        //一旦接受連線，建立一個客戶端
        clientSocket = serverSocket.Accept();
        //獲取客戶端的IP和埠
        IPEndPoint ipEndClient = (IPEndPoint)clientSocket.RemoteEndPoint;
        //輸出客戶端的IP和埠
        Debug.Log("Connect with " + ipEndClient.Address.ToString() + ":" + ipEndClient.Port.ToString());
        //連線成功則傳送資料
        sendStr = "Welcome to my server";
        SocketSend(sendStr);
    }
    /// <summary>
    /// 傳遞資料
    /// </summary>
    /// <param name="sendStr"></param>
    void SocketSend(string sendStr)
    {
        //清空傳送快取
        sendData = new byte[1024];
        //資料型別轉換
        sendData = Encoding.ASCII.GetBytes(sendStr);
        //傳送
        clientSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }
    /// <summary>
    ///關閉伺服器
    /// </summary>
    void SocketQuit()
    {
        //關閉執行緒
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        //最後關閉伺服器
        if (serverSocket != null)
            serverSocket.Close();
        print("diconnect");
    }
    void OnApplicationQuit()
    {
        SocketQuit();
    }
}
