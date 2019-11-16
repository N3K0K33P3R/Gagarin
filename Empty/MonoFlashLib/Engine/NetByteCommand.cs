using System;
using System.Collections.Generic;
using System.Text;

namespace MonoFlashLib.Engine
{
    internal enum TypeOfArg
    {
        Tint,
        Tbyte,
        TString,
        Tfloat,
        Tend
    }

    public class NetByteCommand
    {
        private readonly List<byte> args;


        public NetByteCommand()
        {
            args = new List<byte>();
        }

        public NetByteCommand(byte[] args)
        {
            this.args = new List<byte>(args);
        }

        public void AddArgument(int argument)
        {
            args.Add((byte) TypeOfArg.Tint);
            args.AddRange(BitConverter.GetBytes(argument));
        }

        public void AddArgument(byte argument)
        {
            args.Add((byte) TypeOfArg.Tbyte);
            args.AddRange(BitConverter.GetBytes(argument));
        }

        public void AddArgument(float argument)
        {
            args.Add((byte) TypeOfArg.Tfloat);
            args.AddRange(BitConverter.GetBytes(argument));
        }

        public void AddArgument(string argument)
        {
            args.Add((byte) TypeOfArg.TString);
            var data = Encoding.UTF8.GetBytes(argument);
            args.AddRange(data);
        }

        public byte[] GetBytes()
        {
            args.Add((byte) TypeOfArg.Tend);
            return args.ToArray();
        }

        public void End()
        {
            args.Add((byte) TypeOfArg.Tend);
        }

        public void Clear()
        {
            args.Clear();
        }
    }
}