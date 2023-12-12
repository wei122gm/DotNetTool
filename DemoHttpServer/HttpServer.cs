using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

namespace DemoHttpServer
{
    /// <summary>
    /// Http服务
    /// </summary>
    public class HttpServer
    {
        /// <summary>
        /// Tcp服务
        /// </summary>
        TcpListener tcpListener = null;
        /// <summary>
        /// 本地目录路径
        /// </summary>
        string path = string.Empty;
        /// <summary>
        /// 存放本地目录的绝对Uri地址
        /// </summary>
        string pathAbsoluteUri = string.Empty;

        /// <summary>
        /// 请求文件的完整路径
        /// </summary>
        /// <remarks>方法参数为本地文件完整路径;返回值为文件的内容,为空时读取请求文件的内容</remarks>
        public event Func<string, byte[]> RequestFileFullPath;

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="localEndPoint">ip地址和端口</param>
        public void Start(string path, IPEndPoint localEndPoint)
        {
            this.path = path;
            /* 取本地目录的绝对uri地址,
            主要用于浏览器请求的uri路径和本地目录拼接
            示例1:
            本地目录路径: E:\Test\DemoHttpServer\bin\Debug
            绝对Uri地址:file:///E:/Test/DemoHttpServer/bin/Debug/

            示例2:注意,空格会变成 %20
            本地目录路径:E:\Program Files
            绝对Uri地址:file:///E:/Program%20Files

            */
            //参考链接:https://stackoverflow.com/a/74852300/5903844
            pathAbsoluteUri = new UriBuilder("file", string.Empty)
            {
                Path = path
               .Replace("%", $"%{(int)'%':X2}")
               .Replace("[", $"%{(int)'[':X2}")
               .Replace("]", $"%{(int)']':X2}"),
            }.Uri.AbsoluteUri;

            if (tcpListener != null)
            {
                //异步的Socket，原始Socket没有任何连接
                //tcpListener.Server.Shutdown(SocketShutdown.Both);
                tcpListener.Server.Close();
            }
            tcpListener = new TcpListener(localEndPoint);
            tcpListener.Start();

            //TcpClient tcpClient = tcpListener.AcceptTcpClient();
            //Receive(tcpClient);

            //tcpListener.BeginAcceptTcpClient(OnReceive, tcpListener);//开始异步接收客户端连接

            tcpListener.BeginAcceptTcpClient(OnReceive2, tcpListener);//开始异步接收客户端连接
        }

        void OnReceive2(IAsyncResult ar)
        {
            TcpListener tcpServer = ar.AsyncState as TcpListener;
            TcpClient tcpClient = tcpServer.EndAcceptTcpClient(ar);
            tcpServer.BeginAcceptTcpClient(OnReceive, tcpServer);//重新开始异步接收客户端连接
            string text = string.Empty;
            NetworkStream networkStream = tcpClient.GetStream();//获取网络流
            do
            {
                byte[] data = new byte[tcpClient.ReceiveBufferSize];
                int length = networkStream.Read(data, 0, data.Length);//读取流的内容
                text = string.Concat(text, Encoding.ASCII.GetString(data, 0, length));//拼接文本
            } while (networkStream.DataAvailable);//判断流中是否有可用内容
            string[] texts = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (texts.Length == 0)//断开连接，或者已经读取完成
            {
                networkStream.Close();
                tcpClient.Close();
                return;
            }
            //注意:需要空行隔开头部消息<headers>，和内容<entity-body>
            /*
            请求报文的格式
            <method> <path> <HTTP version>       GET / HTTP/1.1
            <headers>                            Host: localhost:8080
                                                 Connection: keep-alive
            <entity-body>

            空白行代表结束
            */

            /*
            响应报文的格式
            <HTTP version> <status code> <reason-phrase>                 HTTP/1.1 200 OK
            <headers>                                                    Content-Type: text/html

            <entity-body>                                                <!DOCTYPE html><html><head>< ……

            */

            string path = texts[0].Split(' ')[1];//获取uri路径 示例: GET / HTTP/1.1 获取中间的'/'
            if (path == "/")//默认路径时访问的首页文件
            {
                path += "index.html";
            }
            path = new Uri(pathAbsoluteUri + path).LocalPath;//拼接路径,获取本地文件地址

            byte[] bytes = RequestFileFullPath?.Invoke(path);
            if (bytes == null && File.Exists(path) == false)
            {
                //文件不存在,返回404
                byte[] response = Encoding.ASCII.GetBytes("HTTP/1.1 404 Not Found".ToString());
                networkStream.Write(response, 0, response.Length);
            }
            else
            {
                string suffixName = new System.IO.FileInfo(path).Extension;//获取文件扩展名
                string contentType = GetContentType(suffixName.ToLower());//将扩展名转为MIME类型,参考IIS MIME类型
                StringBuilder sb = new StringBuilder(200);
                sb.AppendLine("HTTP/1.1 200 OK");//响应获取成功
                sb.AppendLine("Content-Type: " + contentType);//内容对应的类型
                sb.AppendLine();//需要空行隔开
                byte[] bytes1 = Encoding.ASCII.GetBytes(sb.ToString());//头部的字节
                byte[] bytes2 = bytes ?? File.ReadAllBytes(path);//内容的字节

                networkStream.Write(bytes1, 0, bytes1.Length);//发送到网页浏览器
                networkStream.Write(bytes2, 0, bytes2.Length);//发送到网页浏览器
            }

            networkStream.Close();//关闭流
            tcpClient.Close();//关闭连接
        }

        /// <summary>
        /// 接收
        /// </summary>
        /// <param name="ar"></param>
        void OnReceive(IAsyncResult ar)
        {
            TcpListener tcpServer = ar.AsyncState as TcpListener;
            TcpClient tcpClient = tcpServer.EndAcceptTcpClient(ar);
            tcpServer.BeginAcceptTcpClient(OnReceive, tcpServer);//重新开始异步接收客户端连接
            byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
            TcpData tcpData = new TcpData()
            {
                TcpClient = tcpClient,
                Bytes = bytes
            };
            //开始异步读取数据
            tcpClient.Client.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, OnReadData, tcpData);
        }

        void OnReadData(IAsyncResult ar)
        {
            TcpData tcpData = ar.AsyncState as TcpData;
            TcpClient tcpClient = tcpData.TcpClient;
            try
            {
                int length = tcpData.TcpClient.Client.EndReceive(ar);
                if (length <= 0)
                {
                    tcpClient.Close();
                    return;
                }
                //byte[] bytes = new byte[tcpClient.ReceiveBufferSize];
                //TcpData tcpReadData = new TcpData()
                //{
                //    TcpClient = tcpClient,
                //    Bytes = bytes,
                //};
                //tcpClient.Client.BeginReceive(bytes, 0, bytes.Length, SocketFlags.None, OnReadData, tcpReadData);
                string text = Encoding.ASCII.GetString(tcpData.Bytes, 0, length);
                string[] texts = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (texts.Length == 0)
                {
                    tcpClient.Close();
                    return;
                }
                string path = texts[0].Split(' ')[1];
                if (path == "/")
                {
                    path += "index.html";
                }
                path = new Uri(pathAbsoluteUri + path).LocalPath;
                if (File.Exists(path))
                {
                    string suffixName = new System.IO.FileInfo(path).Extension;
                    string contentType = GetContentType(suffixName.ToLower());
                    StringBuilder sb = new StringBuilder(200);
                    sb.AppendLine("HTTP/1.1 200 OK");
                    sb.AppendLine("Content-Type: " + contentType);
                    sb.AppendLine();//需要空行隔开
                    byte[] bytes1 = Encoding.ASCII.GetBytes(sb.ToString());
                    byte[] bytes2 = File.ReadAllBytes(path);

                    tcpClient.GetStream().Write(bytes1, 0, bytes1.Length);
                    tcpClient.GetStream().Write(bytes2, 0, bytes2.Length);
                }
                else
                {
                    StringBuilder sb = new StringBuilder(100);
                    sb.AppendLine("HTTP/1.1 404 Not Found");
                    byte[] response = Encoding.ASCII.GetBytes(sb.ToString());
                    tcpClient.GetStream().Write(response, 0, response.Length);
                }
                tcpClient.Close();
            }
            catch
            {
            }

        }

        void Receive(TcpClient tcpClient)
        {
            //注意：需要空行隔开头部消息<headers>，和内容<entity-body>
            /*
           请求报文的格式
           <method> <path> <HTTP version>       GET / HTTP/1.1
           <headers>                            Host: localhost:8080
                                                Connection: keep-alive
           <entity-body>

           空白行代表结束
           */

            /*
            响应报文的格式
            <HTTP version> <status code> <reason-phrase>
            <headers>

            <entity-body>

            */

            while (true)
            {
                string text = string.Empty;
                NetworkStream networkStream = tcpClient.GetStream();
                do
                {
                    byte[] data = new byte[tcpClient.ReceiveBufferSize];
                    int length = networkStream.Read(data, 0, data.Length);
                    text = string.Concat(text, Encoding.ASCII.GetString(data, 0, length));
                } while (networkStream.DataAvailable);
                string[] texts = text.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                if (texts.Length == 0)
                {
                    try { tcpClient.Close(); } catch { }
                    tcpClient = tcpListener.AcceptTcpClient();
                    continue;
                }
                string path = texts[0].Split(' ')[1];
                if (path == "/")
                {
                    path += "index.html";
                }
                path = new Uri(pathAbsoluteUri + path).LocalPath;
                if (File.Exists(path))
                {
                    string suffixName = new System.IO.FileInfo(path).Extension;
                    string contentType = GetContentType(suffixName.ToLower());
                    StringBuilder sb = new StringBuilder(200);
                    sb.AppendLine("HTTP/1.1 200 OK");
                    sb.AppendLine("Content-Type: " + contentType);
                    sb.AppendLine();//需要空行隔开
                    byte[] bytes1 = Encoding.ASCII.GetBytes(sb.ToString());
                    byte[] bytes2 = File.ReadAllBytes(path);

                    networkStream.Write(bytes1, 0, bytes1.Length);
                    networkStream.Write(bytes2, 0, bytes2.Length);
                }
                else
                {
                    StringBuilder sb = new StringBuilder(100);
                    sb.AppendLine("HTTP/1.1 404 Not Found");
                    byte[] response = Encoding.ASCII.GetBytes(sb.ToString());
                    networkStream.Write(response, 0, response.Length);
                }
                networkStream.Close();
                try { tcpClient.Close(); } catch { }
                tcpClient = tcpListener.AcceptTcpClient();
            }
        }

        /// <summary>
        /// 获取内容类型
        /// </summary>
        /// <param name="suffixName">后缀名,带'.'</param>
        /// <returns></returns>
        string GetContentType(string suffixName)
        {
            switch (suffixName)
            {
                case ".css": return "text/css";
                case ".html": return "text/html";
                case ".ico": return "image/x-icon";
                case ".jpe": return "image/jpeg";
                case ".jpeg": return "image/jpeg";
                case ".jpg": return "image/jpeg";
                case ".js": return "application/javascript";
                case ".json": return "application/json";
                case ".map": return "text/plain";
                case ".png": return "image/png";
                default: return suffixName.TrimStart('.');
            }
        }

        /// <summary>
        /// tcp数据
        /// </summary>
        class TcpData
        {
            /// <summary>
            /// 客户端类
            /// </summary>
            public TcpClient TcpClient { get; set; }
            /// <summary>
            /// 接收的数据
            /// </summary>
            public byte[] Bytes { get; set; }
        }

        public void Dispose()
        {
            try { tcpListener.Server.Close(); } catch { }
            tcpListener = null;
        }
    }
}
