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
        static string _ServerMessage = "Hola soy el server";
        static string _Resposta = string.Empty;
        #endregion

        #region Main
        static void Main(string[] args)
        {
            IniciarServer();

            Instances();

            Conversion_Send();

            ReciveRespuesta();

            Printar();

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
            //Lo iniciamos
            _Listener.Start();
        }

        /// <summary>
        /// Preparamos el socket para poder enviar y recibir info y redimensionamos el buffer
        /// </summary>
        private static void Instances()
        {
            //Creamos un socket de tipo TCPClient preparado para enviar y recibir info
            _Client = _Listener.AcceptTcpClient();
            //Creamos el NetworkStream asociado al client
            _NS = _Client.GetStream();
            //Dimensionamos el buffer
            byte[] buffer = new byte[1024];
            
        }

        /// <summary>
        /// Hacemos la conversion y el envio de los datos
        /// </summary>
        private static void Conversion_Send()
        {
            //Creamos un nuevo buffer, el cual usaremos para enviar datos,
            //y lo llenamos con el mensaje creado previamente, ademas pasamos
            //el mensaje a bytes
            byte[] nouBuffer = Encoding.ASCII.GetBytes(_ServerMessage);

            //le decimos al NetworkStream que envie lo del nuevo buffer, y le pasamos
            //los demas parametros que necesita
            _NS.Write(nouBuffer, 0, nouBuffer.Length);
        }

        private static void ReciveRespuesta()
        {
            var dadaResposta = new byte[256];
            Int32 bytes = _NS.Read(dadaResposta, 0, dadaResposta.Length);
            _Resposta = Encoding.ASCII.GetString(dadaResposta, 0, bytes);
        }

        /// <summary>
        /// Printamos todo lo que vamos a enviar y lo que recibimos
        /// </summary>
        private static void Printar()
        {
            //Printamos el mensaje que recibimos del cliente
            Console.WriteLine("Recibes: {0}", _Resposta);
            //Printamos el mensaje que recibimos del server
            Console.WriteLine("Envias: {0}", _ServerMessage);
        }

        /// <summary>
        /// Paramos el server
        /// </summary>
        private static void ApagarServer()
        {
            //Cerramos el NetworkStream
            _NS.Close();
            //Cerramos el TCPClient
            _Client.Close();
            //Paramos el TCPListener
            _Listener.Stop();
        }
        #endregion
    }
}
