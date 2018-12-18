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
        static void Main(string[] args)
        {
            //Creamos el TCPClient asociado el server
            TcpClient client = new TcpClient("127.0.0.1", 21);

            //Convertimos un mensaje a un array de bytes
            byte[] dades = Encoding.ASCII.GetBytes("Hola soy el cliente");

            //Instanciamos un NetworkStream para leer y escribir a partir de GetStream
            NetworkStream NS = client.GetStream();

            //Enviamos el mensaje al server
            NS.Write(dades, 0, dades.Length);

            //Preparamos el socket para que escuche la respuesta del server
            var dadaResposta = new byte[256];
            string resposta = string.Empty;
            Int32 bytes = NS.Read(dadaResposta, 0, dadaResposta.Length);
            resposta = Encoding.ASCII.GetString(dadaResposta, 0, bytes);
            Console.WriteLine(resposta);
            Console.ReadKey();
        }
    }
}
