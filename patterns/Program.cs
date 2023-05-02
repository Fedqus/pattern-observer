using System;
using System.Collections.Generic;

namespace patterns
{
    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class Weather : ISubject
    {
        private List<IObserver> _subscribers = new List<IObserver>();
        private double _temperature;

        public double Temperature
        {
            get => _temperature;
            set {
                _temperature = value;
                Notify();
            }
        }
        public void Attach(IObserver observer)
        {
            _subscribers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _subscribers.Remove(observer);
        }

        public void Notify()
        {
            Console.WriteLine($"The temperature has changed by {Temperature} degrees");
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(this);
            }
        }
    }

    public class User : IObserver
    {
        public User(string name, double comfortableTemperature)
        {
            Name = name;
            ComfortableTemperature = comfortableTemperature;
        }

        public string Name { get; }
        public double ComfortableTemperature { get; }

        public void Update(ISubject subject)
        {
            var weather = subject as Weather;
            if (weather.Temperature >= ComfortableTemperature)
            {
                Console.WriteLine($"{Name} is going outside");
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Weather weather = new Weather();

            User userA = new User("Vlad", 26);
            User userB = new User("Alex", 23);
            User userC = new User("Daniel", 20);

            weather.Attach(userA);
            weather.Attach(userB);
            weather.Attach(userC);

            weather.Temperature = 24;

            Console.ReadLine();
        }
    }
}
