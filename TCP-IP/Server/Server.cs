using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        #region Variables e instancias globales
        static TcpListener _Listener;
        static TcpClient _Client;
        static NetworkStream _NS;
        const int _Port = 21;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            IniciarServer();

            //Creamos un socket de tipo TCPClient preparado para enviar y recibir info
            _Client = _Listener.AcceptTcpClient();

            //Creamos el NetworkStream asociado al client
            _NS = _Client.GetStream();
            //Dimensionamos el buffer
            byte[] buffer = new byte[1024];

            //Creamos un nuevo buffer, el cual usaremos para enviar datos,
            //y lo llenamos
            byte[] nouBuffer = Encoding.ASCII.GetBytes("Hola soy el server");
            //le decimos al NetworkStream que printee lo del nuevo buffer, y le pasamos
            //los demas parametros que necesita
            _NS.Write(nouBuffer, 0, nouBuffer.Length);

            Console.WriteLine(nouBuffer);


            Console.ReadKey();

            ApagarServer();            
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Instanciamos e iniciamos el server
        /// </summary>
        private static void IniciarServer()
        {
            //Instanciamos el server
            _Listener = new TcpListener(IPAddress.Any, _Port);
            //lo iniciamos
            _Listener.Start();
        }

        /// <summary>
        /// Paramos el server
        /// </summary>
        private static void ApagarServer()
        {
            _NS.Close();
            _Client.Close();
            _Listener.Stop();
        }
        #endregion
    }
}
