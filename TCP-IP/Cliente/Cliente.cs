using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Cliente
{
    class Cliente
    {
        #region Variables e instancias Globales
        static TcpClient _Client;
        static NetworkStream _NS;
        const string _ServerIP = "127.0.0.1";
        const int _Port = 21;
        static string _ClientMessage = "Hola soy el cliente";
        static string _Resposta = string.Empty;
        static string _UserMessage = "";
        #endregion

        #region Main
        static void Main(string[] args)
        {
            Instances();

            GetUserMessage();

            Send();

            ReciveRespuesta();

            Printar();

            Console.ReadKey();
        }
        #endregion

        #region Metodos
        /// <summary>
        /// Instanciamos el TCPClient y el NetworkStream
        /// </summary>
        private static void Instances()
        {
            //Creamos el TCPClient asociado el server
            _Client = new TcpClient(_ServerIP, _Port);

            //Instanciamos un NetworkStream para leer y escribir a partir de GetStream
            _NS = _Client.GetStream();
        }

        /// <summary>
        /// Convertimos los datos y los mandamos
        /// </summary>
        private static void Send()
        {
            //Convertimos un mensaje a un array de bytes
            byte[] dades = Encoding.ASCII.GetBytes(_ClientMessage);

            //Enviamos el mensaje al server
            _NS.Write(dades, 0, dades.Length);

            byte[] dades2 = Encoding.ASCII.GetBytes(_UserMessage);
            _NS.Write(dades2, 0, dades2.Length);
        }

        /// <summary>
        /// Printamos todo lo que vamos a enviar y lo que recibimos
        /// </summary>
        private static void Printar()
        {
            Console.WriteLine("Envias: {0}", _ClientMessage);
            Console.WriteLine("Recibes: {0}", _Resposta);
            
        }

        /// <summary>
        /// Preparamos el socket para que escuche la respuesta del server
        /// </summary>
        private static void ReciveRespuesta()
        {            
            var dadaResposta = new byte[256];
            Int32 bytes = _NS.Read(dadaResposta, 0, dadaResposta.Length);
            _Resposta = Encoding.ASCII.GetString(dadaResposta, 0, bytes);
        }

        private static void GetUserMessage()
        {
            Console.Write("Envia: ");
            _UserMessage = Console.ReadLine();
        }
        #endregion
    }
}
