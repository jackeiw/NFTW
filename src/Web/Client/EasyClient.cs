using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class EasyClient
    {
        public void ConnectTcp(string ip = "127.0.0.1", int port = 11000)
        {
            string sendString = null;//要发送的字符串 
            byte[] sendData = null;//要发送的字节数组 
            TcpClient client = null;//TcpClient实例 
            NetworkStream stream = null;//网络流 

            IPAddress remoteIP = IPAddress.Parse(ip);//远程主机IP 
            int remotePort = port;//远程主机端口 

            while (true)//死循环 
            {
                sendString = Console.ReadLine();//获取要发送的字符串 
                sendData = Encoding.Default.GetBytes(sendString);//获取要发送的字节数组 
                client = new TcpClient();//实例化TcpClient 
                try
                {
                    client.Connect(remoteIP, remotePort);//连接远程主机 
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("连接超时，服务器没有响应！{0}", ex.Message);//连接失败 
                    Console.ReadKey();
                    return;
                }
                stream = client.GetStream();//获取网络流 
                stream.Write(sendData, 0, sendData.Length);//将数据写入网络流
                

                stream.Close();//关闭网络流 
                client.Close();//关闭客户端 
            } 
        }

        public void ConnectTcpSendFile(string ip = "127.0.0.1", int port = 11000)
        {           
            byte[] sendData = null;//要发送的字节数组 
            TcpClient client = null;//TcpClient实例 
            NetworkStream stream = null;//网络流 

            IPAddress remoteIP = IPAddress.Parse(ip);//远程主机IP 
            int remotePort = port;//远程主机端口 

            while (true)//死循环 
            {
                client = new TcpClient();//实例化TcpClient 
                try
                {
                    client.Connect(remoteIP, remotePort);//连接远程主机 
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("连接超时，服务器没有响应！{0}", ex.Message);//连接失败 
                    Console.ReadKey();
                    return;
                }
                stream = client.GetStream();//获取网络流 

                //stream.Write(sendData, 0, sendData.Length);//将数据写入网络流
                FileStream fileStream = new FileStream(string.Format(AppDomain.CurrentDomain.BaseDirectory + "fly.rar"), FileMode.Open, FileAccess.Read);
                sendData = new byte[fileStream.Length];
                int blockSize = 10240;
                int from = 0;
                int count = 0;
                double index = Math.Ceiling((double)((double)fileStream.Length / blockSize));
                for (int i = 0; i < index; i++)
                {
                    from = i * blockSize;
                    if (i == index - 1)
                    {
                        count = (int)(fileStream.Length % blockSize);
                    }
                    else
                    {
                        count = blockSize;
                    }
                    fileStream.Read(sendData, from, count); 
                }

                /*while(fileStream.CanRead)
                {
                    if (fileStream.Position > to)
                    {
                        from = index * blockSize;
                        to = (index + 1) * blockSize;
                    }
                    fileStream.Read(sendData, from, to + 1);                    
                    index++;
                }*/
                
                stream.Write(sendData, 0, sendData.Length);//将数据写入网络流

                fileStream.Flush();
                stream.Flush();

                fileStream.Close();
                stream.Close();//关闭网络流 
                client.Close();//关闭客户端 
                Console.WriteLine("是否继续发送！k，结束。");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar == 'k')
                {
                    break;
                }                
            }
            Console.WriteLine("发送结束！");
            Console.ReadLine();
        }

        public void ConnectUdp()
        {
            string sendString = null;//要发送的字符串 
            byte[] sendData = null;//要发送的字节数组 
            UdpClient client = null;

            IPAddress remoteIP = IPAddress.Parse("127.0.0.1");
            int remotePort = 11000;
            IPEndPoint remotePoint = new IPEndPoint(remoteIP, remotePort);//实例化一个远程端点 

            while (true)
            {
                sendString = Console.ReadLine();
                sendData = Encoding.Default.GetBytes(sendString);

                client = new UdpClient();
                client.Send(sendData, sendData.Length, remotePoint);//将数据发送到远程端点 
                client.Close();//关闭连接 
            } 
        }
    }
}
