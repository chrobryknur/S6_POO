using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logger;
using System;

namespace LoggerTest
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void Should_return_null_logger()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.GetLogger(LogType.None);
            Type type = logger.GetType();
            Assert.IsTrue(type.Equals(typeof(NullLogger)));
            logger.Log("test");
        }

        [TestMethod]
        public void Should_return_console_logger()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.GetLogger(LogType.Console);
            Type type = logger.GetType();
            Assert.IsTrue(type.Equals(typeof(ConsoleLogger)));
            logger.Log("test");
        }

        [TestMethod]
        public void Should_return_file_logger()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.GetLogger(LogType.File, "log.txt");
            Type type = logger.GetType();
            Assert.IsTrue(type.Equals(typeof(FileLogger)));
            logger.Log("test");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception), "Can't create FileLogger without a path to a file provided!")]
        public void Should_throw_exception_as_path_is_not_provided()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            ILogger logger = loggerFactory.GetLogger(LogType.File);
            Type type = logger.GetType();
            Assert.IsTrue(type.Equals(typeof(FileLogger)));
        }
    }
}