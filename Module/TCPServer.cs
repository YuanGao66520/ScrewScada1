using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Module
{
   
    public class TCPServer
    {
        public string TriggerString { get; set; }

        public int Connected { get; set; }

        //创建套接字
        Socket sock = null;

        //创建负责监听客户端连接的线程
        Thread threadListen = null;

        //创建URL与Socket的字典集合
        //创建URL与Socket的字典集合
        Dictionary<string, Socket> DicSocket = new Dictionary<string, Socket>();

        public event EventHandler<EventMessage> OnReceiveMessage; //定义一个委托类型的事件  
        public event EventHandler<EventArgs> OnDisConnect; //当连接断开时 
        public event EventHandler<string> OnConnected; //当连接时 
        public bool OpenServer(int netPort)
        {
            
            //创建负责监听的套接字，注意其中参数：IPV4 字节流 TCP
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPAddress address = IPAddress.Any;

            //根据IPAddress以及端口号创建IPE对象
            IPEndPoint endpoint = new IPEndPoint(address, netPort);

            try
            {
                sock.Bind(endpoint);
            }
            catch (Exception ex)
            {
                
                return false;
            }

            sock.Listen(1);

            threadListen = new Thread(ListenConnecting);
            threadListen.IsBackground = true;
            threadListen.Start();
            return true;
        }
        #region 监听线程
        /// <summary>
        /// 监听线程
        /// </summary>
        private void ListenConnecting()
        {
            while (true)
            {
                Socket sockClient = sock.Accept();
                //一旦监听到一个客户端的连接，将会创建一个与该客户端连接的套接字

                string client = sockClient.RemoteEndPoint.ToString();
                DicSocket.Add(client, sockClient);
                OnConnected(this, client);
               
                //开启接受线程
                Thread thr = new Thread(ReceiveMsg);
                thr.IsBackground = true;
                thr.Start(sockClient);

            }

        }
        #endregion

        #region 接收线程
        byte[] arrMsgRec = new byte[1024];
        /// <summary>
        /// 接收线程
        /// </summary>
        /// <param name="sockClient"></param>
        private void ReceiveMsg(object sockClient)
        {
            Socket sckclient = sockClient as Socket;
            while (true)
            {
                //定义一个2M缓冲区
                

                int length = -1;

                try
                {
                    length = sckclient.Receive(arrMsgRec);
                }
                catch (Exception)
                {
                    string str = sckclient.RemoteEndPoint.ToString();

                    //从列表中移除URL
                    DicSocket.Remove(str);
                    OnDisConnect(this, new EventArgs());
                   
                    break;
                }

                if (length == 0)
                {
                    string str = sckclient.RemoteEndPoint.ToString();

                    //从列表中移除URL
                    DicSocket.Remove(str);
                    OnDisConnect(this,new EventArgs());
                    
                    break;
                }
                else
                {
                    string strMsg = Encoding.UTF8.GetString(arrMsgRec, 0, length );
                    string Msg = "[接收]     " + sckclient.RemoteEndPoint.ToString() + "     " + strMsg;
                    OnReceiveMessage(this, new EventMessage()
                    {
                        Msg = strMsg,
                        SocketObj = sckclient
                    }); //触发事件
                }
            }
        }
        #endregion

        public bool SendMessage(EventMessage msg)
        {
            if (msg.SocketObj == null) return false;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(msg.Msg);
            List<byte> listbyte = new List<byte>();
            listbyte.AddRange(buffer);
            //将泛型集合转换为数组
            byte[] newBuffer = listbyte.ToArray();
            msg.SocketObj.Send(newBuffer);
            return true;
        }
        

    }

    public class EventMessage
    {
        public string  Msg { get; set; }
        public Socket SocketObj { get; set; } 
    }

}
