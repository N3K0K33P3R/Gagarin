using System.Text;

namespace MonoFlashLib.Engine
{
	public enum NCTypes
	{
		New,
		AddPlayer,
		Shoot,
		Move,
		CreateExplosion,
		CreateBrick
	}


	public class NetCommand
	{
		private readonly string[] args;
		public           string   command;
		public           NCTypes  type;

		public NetCommand(string command)
		{
			string[] temp = command.Split(' ');
			type = (NCTypes)int.Parse(temp[0]);
			args = new string[temp.Length - 1];

			if (temp.Length > 1)
			{
				for (var i = 1; i < temp.Length; i++)
				{
					args[i - 1] = temp[i];
				}
			}
			else
			{
				args = new string[0];
			}

			this.command = command;
		}

		public NetCommand(NCTypes type, params string[] list)
		{
			this.type = type;
			args      = list;
			var temp = new StringBuilder();
			temp.Append((int)type);

			foreach (string item in list)
			{
				temp.Append(" " + item);
			}

			temp.Append("|");
			command = temp.ToString();
		}

		public NetCommand(NCTypes type, params int[] list)
		{
			this.type = type;
			args      = new string[list.Length];

			for (var i = 0; i < list.Length; i++)
			{
				args[i] = list[i].ToString();
			}

			var temp = new StringBuilder();
			temp.Append((int)type);

			foreach (int item in list)
			{
				temp.Append(" " + item);
			}

			temp.Append("|");
			command = temp.ToString();
		}

		public int GetArgAsInt(int id)
		{
			if (id < args.Length)
			{
				int res = int.Parse(args[id]);
				return res;
			}

			return -1;
		}

		public string GetArg(int id)
		{
			if (id < args.Length)
			{
				return args[id];
			}

			return "";
		}
	}
}