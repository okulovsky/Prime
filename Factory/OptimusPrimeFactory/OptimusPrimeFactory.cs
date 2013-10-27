﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BookSleeve;
using OptimusPrime.OprimusPrimeCore;
using OptimusPrime.OprimusPrimeCore.Extension;
using OptimusPrime.OprimusPrimeCore.Helpers;
using OptimusPrime.OprimusPrimeCore.Logger;

namespace OptimusPrime.Factory
{
    public partial class OptimusPrimeFactory : IFactory
    {
        /// <summary>
        /// Коллекция потоков, содержащих все сервисы.
        /// </summary>
        private readonly List<Thread> _threads;

        /// <summary>
        /// Список коллекций данных, порожденных топологическими элементами.
        /// </summary>
        private RedisConnection _connection;

        /// <summary>
        /// Коллекция псевдонимов имен.
        /// </summary>
        private readonly Dictionary<string, string> _pseudoNames; 

        /// <summary>
        /// Коллекция сервисов.
        /// </summary>
        private static IList<IOptimusPrimeService> Services { get; set; }
        

        public OptimusPrimeFactory()
        {
            Services = new List<IOptimusPrimeService>();
            _threads = new List<Thread>();
            _pseudoNames = new Dictionary<string, string>();
        }

        public void Start()
        {
            _connection = new RedisConnection("localhost", allowAdmin: true);

            Task openTask = _connection.Open();
            _connection.Wait(openTask);

            Task flushDbTask = _connection.Server.FlushAll();
            _connection.Wait(flushDbTask);

            foreach (var service in Services)
            {
                var serviceThread = new Thread(service.Actuation);
                serviceThread.Start();
                _threads.Add(serviceThread);
            }
        }

        public void Stop()
        {
            foreach (var thread in _threads)
                thread.Abort();
        }

        public string DumpDb()
        {
            var db = new Dictionary<string, object[]>();
            foreach (var service in Services)
                foreach (var output in service.OptimusPrimeOut)
                {
                    var range = _connection.Lists.Range(output.Service.DbPage, output.Name, 0, -1);
                    var bytes = _connection.Wait(range);
                    var result = bytes.Select(SerializeExtension.Deserialize<object>).ToArray();

                    db.Add(output.Name, result);
                }
            var filePath = PathHelper.GetFilePath();
            var logData = new LogData(_pseudoNames, db);
            var data = logData.Serialize();
            File.WriteAllBytes(filePath, data);
            return filePath;
        }
    }
}