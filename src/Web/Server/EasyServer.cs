using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class EasyServer
    {
        public void ListenTcp(string ip = "127.0.0.1",int port = 11000)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            byte[] buffer = null;
            string receiveString = null;

            IPAddress localIP = IPAddress.Parse(ip);
            int localPort = port;
            TcpListener listener = new TcpListener(localIP, localPort);//用本地IP和端口实例化Listener 
            listener.Start();//开始监听 
            Console.WriteLine("开始监听！");
            while (true)
            {
                client = listener.AcceptTcpClient();//接受一个Client 
                /*接收数据*/
                buffer = new byte[client.ReceiveBufferSize];
                stream = client.GetStream();//获取网络流 
                stream.Read(buffer, 0, buffer.Length);//读取网络流中的数据
                receiveString = Encoding.Default.GetString(buffer).Trim('\0');//转换成字符串 
                Console.WriteLine(receiveString);

                /*###################################################
                //发送数据
                string sendString = null;//要发送的字符串 
                byte[] sendData = null;//要发送的字节数组 
                sendString = "已收到您的信息！";//获取要发送的字符串 
                sendData = Encoding.Default.GetBytes(sendString);//获取要发送的字节数组
                stream = client.GetStream();//获取网络流 
                stream.Write(sendData, 0, sendData.Length);//将数据写入网络流

                //###################################################*/
                //发送数据
                string sendString = null;//要发送的字符串 
                byte[] sendData = null;//要发送的字节数组 
                sendString = "StartSendFile";//获取要发送的字符串 
                sendData = Encoding.Default.GetBytes(sendString);//获取要发送的字节数组
                stream = client.GetStream();//获取网络流 
                stream.Write(sendData, 0, sendData.Length);//将数据写入网络流

                //###################################################

                //接受数据
                buffer = new byte[client.ReceiveBufferSize];
                stream = client.GetStream();//获取网络流 
                stream.Read(buffer, 0, buffer.Length);//读取网络流中的数据
                receiveString = Encoding.Default.GetString(buffer).Trim('\0');//转换成字符串 
                Console.WriteLine(receiveString);

                //###################################################

                stream.Close();//关闭流 
                client.Close();//关闭Client 

            }
        }

        public void ListenTcpGetFile(string ip = "127.0.0.1", int port = 11000)
        {
            TcpClient client = null;
            NetworkStream stream = null;
            string receiveString = null;

            IPAddress localIP = IPAddress.Parse(ip);
            int localPort = port;
            TcpListener listener = new TcpListener(localIP, localPort);//用本地IP和端口实例化Listener 
            listener.Start();//开始监听 
            Console.WriteLine("开始监听！");
            while (true)
            {
                Stopwatch watch = new Stopwatch();
                client = listener.AcceptTcpClient();//接受一个Client 
                if (client.Connected)
                {
                    watch.Start();
                    int size = 0;
                    int len = 0;                   
                    stream = client.GetStream();//获取网络流 
                    if (stream != null)
                    {
                        string fileName = new Random().NextDouble().ToString();
                        FileStream fs = new FileStream(string.Format(AppDomain.CurrentDomain.BaseDirectory + "files/{0}.rar", fileName), FileMode.Create, FileAccess.Write);

                        byte[] buffer = new byte[512];
                        while ((size = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fs.Write(buffer, 0, size);
                            len += size;
                        }
                        fs.Flush();
                        stream.Flush();

                        fs.Close();
                        stream.Close();//关闭流 
                        client.Close();//关闭Client 
                        watch.Stop();
                        Console.WriteLine("文件接受成功!传输用时：{0}毫秒。", watch.ElapsedMilliseconds);
                    }
                }         
            }
        }

        public void ListenUdp()
        {
            UdpClient client = null;
            string receiveString = null;
            byte[] receiveData = null;
            //实例化一个远程端点，IP和端口可以随意指定，等调用client.Receive(ref remotePoint)时会将该端点改成真正发送端端点 
            IPEndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);

            while (true)
            {
                client = new UdpClient(11000);
                receiveData = client.Receive(ref remotePoint);//接收数据 
                receiveString = Encoding.Default.GetString(receiveData);
                Console.WriteLine(receiveString);
                client.Close();//关闭连接 
            } 
        }
    }
}
