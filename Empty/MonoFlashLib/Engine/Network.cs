using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MonoFlashLib.Engine
{
	public class Network
	{
		public static void HandleConnection(
			List<NetCommand> sendBuffer,
			List<NetCommand> readBuffer,
			NetworkStream networkStream)
		{
			foreach (NetCommand data in sendBuffer)
			{
				byte[] ClientRequestBytes = Encoding.UTF8.GetBytes(data.command);
				////Console.WriteLine("[Client] Writing request {0}", data.command);
				networkStream.Write(ClientRequestBytes, 0, ClientRequestBytes.Length);
				//networkStream.WriteAsync();
			}

			sendBuffer.Clear();

			if (networkStream.DataAvailable)
			{
				var    buffer    = new byte[4096];
				int    byteCount = networkStream.Read(buffer, 0, buffer.Length);
				string response  = Encoding.UTF8.GetString(buffer, 0, byteCount);
				////Console.WriteLine("[Client] Data was {0}", response);
				string[] temp = response.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

				foreach (string item in temp)
				{
					readBuffer.Add(new NetCommand(item));
				}
			}
		}


		public static void HandleConnection(
			List<NetByteCommand> sendBuffer,
			List<NetByteCommand> readBuffer,
			NetworkStream networkStream)
		{
			foreach (NetByteCommand data in sendBuffer)
				//Console.WriteLine("[Client] Writing data");
			{
				networkStream.Write(data.GetBytes(), 0, data.GetBytes().Length);
			}

			//networkStream.WriteAsync();
			sendBuffer.Clear();

			if (networkStream.DataAvailable)
			{
				var    buffer    = new byte[4096];
				int    byteCount = networkStream.Read(buffer, 0, buffer.Length);
				string response  = Encoding.UTF8.GetString(buffer, 0, byteCount);
				//Console.WriteLine("[Client] Data was {0}", response);
				readBuffer.Add(new NetByteCommand(buffer));
			}
		}
	}
}