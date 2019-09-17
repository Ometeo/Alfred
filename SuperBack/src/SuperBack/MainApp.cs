using AlfredUtilities;
using Autofac;
using SuperBack.Sensor;
using System;

namespace SuperBack
{
    public class MainApp : AlfredBase
    {
        private ContainerBuilder containerBuilder = null;

        public IContainer Value { get; private set; }

        public ISensorService SensorService { get; private set; }

        public MainApp()
        {
            Console.WriteLine("* Create Alfred main app.");
            containerBuilder = new ContainerBuilder();
        }

        public MainApp Init()
        {
            Console.WriteLine("* Init Alfred.");
            containerBuilder.RegisterType<SimpleSensorService>().As<ISensorService>();

            Console.WriteLine("* Build Alfred DI container.");
            Value = containerBuilder.Build();
            
            return this;
        }

        public void Run()
        {
            using (var scope = Value.BeginLifetimeScope())
            {
                Console.WriteLine("* Create Alfred sensor service.");
                SensorService = scope.Resolve<ISensorService>();
            }
        }

        protected override void DisposeManagedObjects()
        {
            Console.WriteLine("* Dispose Managed objects in main app");
            Value.Dispose();          
        }

        protected override void DisposeUnmanagedObjects()
        {
            Console.WriteLine("* Dispose Unmanaged objects in main app");
        }
    }
}
