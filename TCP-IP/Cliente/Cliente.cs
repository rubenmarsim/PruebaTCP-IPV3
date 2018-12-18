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
        #endregion

        #region Main
        static void Main(string[] args)
        {
            //Creamos el TCPClient asociado el server
            _Client = new TcpClient(_ServerIP, _Port);

            //Convertimos un mensaje a un array de bytes
            byte[] dades = Encoding.ASCII.GetBytes("Hola soy el cliente");

            //Instanciamos un NetworkStream para leer y escribir a partir de GetStream
            _NS = _Client.GetStream();

            //Enviamos el mensaje al server
            _NS.Write(dades, 0, dades.Length);

            //Preparamos el socket para que escuche la respuesta del server
            var dadaResposta = new byte[256];
            string resposta = string.Empty;
            Int32 bytes = _NS.Read(dadaResposta, 0, dadaResposta.Length);
            resposta = Encoding.ASCII.GetString(dadaResposta, 0, bytes);
            Console.WriteLine(resposta);


            Console.ReadKey();
        }
        #endregion

        #region Metodos

        #endregion
    }
}
