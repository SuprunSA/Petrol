using System;

namespace Petrol
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите объем цистерны (изначальный объём - 0)>>>");
            var tank = new Tank(int.Parse(Console.ReadLine()));
            while (true) Read(tank.Add, tank.Take);
        }

        static void Read(Action<int> Add, Action<int> Take)
        {
            while (true)
            {
                Console.WriteLine("Нажмите 1, чтобы долить, 2 - чтобы слить>>>");
                var key = Console.ReadKey().Key;
                switch (key)
                {
                    case ConsoleKey.D1:
                        DoSth(Add);
                        return;
                    case ConsoleKey.D2:
                        DoSth(Take);
                        return;
                    default:
                        break;
                }
            }
        }

        static void DoSth(Action<int> action)
        {
            Console.WriteLine("Введите объём >>>");
            if (int.TryParse(Console.ReadLine(), out int t))
            {
                action(t);
            }
            else Console.WriteLine("Введено некорректное значение");
        }
    }

    class Tank
    {
        public int MaxVolume { get; }
        public int CurrVolume { get; private set; }
        event Action<int> Mesg;

        public Tank(int maxVolume)
        {
            MaxVolume = maxVolume;
            Mesg += Mes;
        }

        public void Add(int vol)
        {
            if (CurrVolume + vol <= MaxVolume)
            {
                CurrVolume += vol;
                Mes(vol);
            }
            else throw new TankOverFlowException("Недостаточно места в цистерне.");
        }

        public void Take(int vol)
        {
            if (CurrVolume - vol >= 0) 
            {
                CurrVolume -= vol;
                Mesg(vol);
            }
            else throw new NotEnoughException("Недостаточно бензина в цистерне.");
        }

        public void Mes(int vol)
        {
            Console.WriteLine("Заполнено {0} л. из {1} л.", vol, MaxVolume);
        }
    }

    class TankOverFlowException : ArgumentException
    {
        public TankOverFlowException(string mes) : base(mes) { }
    }

    class NotEnoughException : ArgumentException
    {
        public NotEnoughException(string mes) : base(mes) { }
    }
}
