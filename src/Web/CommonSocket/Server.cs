using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CommonSocket
{
    /// <summary>
    /// 
    /// </summary>
    public class Server
    {
        private int m_numConnections;
        private int m_receiveBufferSize;
        private BufferManager m_bufferManager;
        private const int opsToPreAlloc = 2;
        private Socket listenSocket;
        SocketAsyncEventArgsPool m_readWritePool;
        int m_totalBytesRead;           // counter of the total # bytes received by the server
        int m_numConnectedSockets;      // the total number of clients connected to the server 
        Semaphore m_maxNumberAcceptedClients;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numConnections">the maximum number of connections the sample is designed to handle simultaneously</param>
        /// <param name="receiveBufferSize">buffer size to use for each socket I/O operation</param>
        public Server(int numConnections, int receiveBufferSize)
        {
            m_totalBytesRead = 0;
            m_numConnectedSockets = 0;
            m_numConnections = numConnections;
            m_receiveBufferSize = receiveBufferSize;
            m_bufferManager = BufferManager.CreateBufferManager(receiveBufferSize*numConnections*opsToPreAlloc,
                                                receiveBufferSize);
            m_readWritePool = SocketAsyncEventArgsPool.valueOf(numConnections);
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections); 
        }

        public void Init()
        {
            //
            SocketAsyncEventArgs readWriteEventArg;

            for (int i = 0; i < m_numConnections; i++)
            {
                readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += readWriteEventArg_Completed;
                readWriteEventArg.UserToken = new AsyncUserToken();

                // assign a byte buffer from the buffer pool to the SocketAsyncEventArg object
                m_bufferManager.SetBuffer(readWriteEventArg);

                // add SocketAsyncEventArg to the pool
                m_readWritePool.Push(readWriteEventArg);
            }
        }

        void readWriteEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
