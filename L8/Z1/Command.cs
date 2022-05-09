using System;
using System.Net;

namespace Command
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class FileDownloader : ICommand
    {
        public abstract void Execute();
        public FileDownloader(Uri _address, string _fileName)
        {
            address = _address;
            fileName = _fileName;
            webClient = new();
        }

        protected Uri address;
        protected string fileName;
        protected WebClient webClient;
    }

    public class FtpFileDownloader : FileDownloader
    {
        public FtpFileDownloader(Uri address, string fileName) : base(address, fileName) { }

        public override void Execute()
        {
            webClient.DownloadFile(address, fileName);
        }
    }

    public class HttpFileDownloader : FileDownloader
    {
        public HttpFileDownloader(Uri address, string fileName) : base(address, fileName) { }

        public override void Execute()
        {
            webClient.DownloadFile(address, fileName);
        }
    }

    public class FileWithRandomContentCreator : ICommand
    {
        public FileWithRandomContentCreator(string _fileName)
        {
            fileName = _fileName;
        }

        public void Execute()
        {
            RandomFileFiller receiver = new();
            receiver.CreateAndFillFileWithRandomData(fileName);
        }

        private string fileName;
    }

    public class RandomFileFiller
    {
        public void CreateAndFillFileWithRandomData(string fileName)
        {
            StreamWriter streamWriter = File.CreateText(fileName);
            string data = GenerateRandomData();
            streamWriter.Write(data);
        }

        private string GenerateRandomData()
        {
            Random random = new Random();
            int length = 100;
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class FileCopier : ICommand
    {
        public FileCopier(string _from, string _to)
        {
            from = _from;
            to = _to; 
        }

        public void Execute()
        {
            File.Copy(from, to);
        }

        private readonly string from;
        private readonly string to;
    }

    public class Invoker
    {
        public Invoker()
        {
            var t1 = new Thread(WaitForCommandToExecuteFromQueue);
            var t2 = new Thread(WaitForCommandToExecuteFromQueue);
            commands = new();

            t1.Start();
            t2.Start();
        }
        public void Execute(ICommand command)
        {
            commands.Enqueue(command);
        }

        private void WaitForCommandToExecuteFromQueue()
        {
            while(true)
            {
                if(commands.Count == 0)
                {
                    Thread.Sleep(50);
                }
                else
                {
                    lock(commands)
                    {
                        if(commands.Count > 0)
                        {
                            Console.WriteLine("here");
                            ICommand command = commands.Dequeue();
                            try
                            {
                                command.Execute();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine(ex.Message.ToString());
                            }
                        }
                    }
                }
            }
        }

        private Queue<ICommand> commands;
        public static void Main(string[] param)
        {
        }
    }

}
