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
        static void Main(string[] args)
        {
            //Instanciamos el server
            TcpListener Listener = new TcpListener(IPAddress.Any, 21);
            //lo iniciamos
            Listener.Start();

            //Creamos un socket de tipo TCPClient preparado para enviar y recibir info
            TcpClient client = Listener.AcceptTcpClient();

            //Creamos el NetworkStream asociado al client
            NetworkStream NS = client.GetStream();
            //Dimensionamos el buffer
            byte[] buffer = new byte[1024];

            //Creamos un nuevo buffer, el cual usaremos para enviar datos,
            //y lo llenamos
            byte[] nouBuffer = Encoding.ASCII.GetBytes("Hola soy el server");
            //le decimos al NetworkStream que printee lo del nuevo buffer, y le pasamos
            //los demas parametros que necesita
            NS.Write(nouBuffer, 0, nouBuffer.Length);

            Console.WriteLine(nouBuffer);
            Console.ReadKey();
            //Paramos el server
            NS.Close();
            client.Close();
            Listener.Stop();
        }
    }
}
