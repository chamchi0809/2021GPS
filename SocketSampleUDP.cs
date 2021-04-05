using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System;

public class SocketSampleUDP : MonoBehaviour
{

    // 접속할 곳의 IP 어드레스.
    private string m_address = "";

    // 접속할 곳의 포트 번호.
    private const int m_port = 50765;

    // 통신용 변수.
    private Socket m_socket = null;

    // 상태. 
    private State m_state;

    // 상태 정의. 
    private enum State
    {
        SelectHost = 0,
        CreateListener,
        ReceiveMessage,        
        SendMessage,        
    }


    // Use this for initialization
    void Start()
    {
        m_state = State.SelectHost;

        IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
        System.Net.IPAddress hostAddress = hostEntry.AddressList[0];
        Debug.Log(hostEntry.HostName);
        m_address = "127.0.0.1";
    }

    // Update is called once per frame
    void Update()
    {
        switch (m_state)
        {
            case State.CreateListener:
                CreateListener();
                break;

            case State.ReceiveMessage:
                ReceiveMessage();
                break;

            case State.SendMessage:
                SendMessage(new Vector3(1,2,3));
                break;

            default:
                break;
        }
    }

    // 소켓 생성.
    void CreateListener()
    {
        Debug.Log("[UDP]Start communication.");

        // 소켓을 생성합니다.
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        // 사용할 포트 번호를 할당합니다.
        m_socket.Bind(new IPEndPoint(IPAddress.Any, m_port));

        m_state = State.ReceiveMessage;
    }

    // 다른 단말에서 보낸 메시지 수신.
    void ReceiveMessage()
    {
        byte[] buffer = new byte[12];
        IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
        EndPoint senderRemote = (EndPoint)sender;

        if (m_socket.Poll(0, SelectMode.SelectRead))
        {
            int recvSize = m_socket.ReceiveFrom(buffer, SocketFlags.None, ref senderRemote);
            if (recvSize > 0)
            {
                Vector3 value = Vector3.zero;
                value.x = BitConverter.ToSingle(buffer, 0);
                value.y = BitConverter.ToSingle(buffer, 4);
                value.z = BitConverter.ToSingle(buffer, 8);
                Debug.Log(value);                
            }
        }
    }

    // 대기 종료.
    void CloseListener()
    {
        // 대기를 종료합니다.
        if (m_socket != null)
        {
            m_socket.Close();
            m_socket = null;
        }
        

        Debug.Log("[UDP]End communication.");
    }

    // 클라이언트와의 접속, 송신, 접속 종료.
    void SendMessage(Vector3 value)
    {
        Debug.Log("[UDP]Start communication.");

        // 서버에 접속.
        m_socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

        // 메시지 송신.
        byte[] buffer = new byte[12];
        Buffer.BlockCopy(BitConverter.GetBytes(value.x), 0, buffer, 0, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(value.y), 0, buffer, 4, 4);
        Buffer.BlockCopy(BitConverter.GetBytes(value.z), 0, buffer, 8, 4);
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse(m_address), m_port);
        m_socket.SendTo(buffer, buffer.Length, SocketFlags.None, endpoint);


        // 접속 종료.
        m_socket.Shutdown(SocketShutdown.Both);
        m_socket.Close();
        

        Debug.Log("[UDP]End communication.");
    }


    void OnGUI()
    {
        if (m_state == State.SelectHost)
        {
            OnGUISelectHost();
        }
    }

    void OnGUISelectHost()
    {
        if (GUI.Button(new Rect(20, 40, 150, 20), "Launch server."))
        {
            m_state = State.CreateListener;
        }

        // 클라이언트를 선택했을 때의 접속할 서버 주소를 입력합니다.
        m_address = GUI.TextField(new Rect(20, 100, 200, 20), m_address);
        if (GUI.Button(new Rect(20, 70, 150, 20), "Connect to server"))
        {
            m_state = State.SendMessage;
        }
    }
}