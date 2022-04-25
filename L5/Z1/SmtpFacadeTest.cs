using Microsoft.VisualStudio.TestTools.UnitTesting;
using Z1;

namespace SMTPFacadeTest
{
    [TestClass]
    public class SmtpFacadeTest
    {

        [TestMethod]
        [ExpectedException(typeof(System.Net.Mail.SmtpException))]
        public void Sending_email_should_fail_due_to_inaccessible_port_on_host()
        {
            SmtpFacade smtpFacade = new SmtpFacade();
            string from = "from@gmail.com";
            string to = "to@gmail.com";

            smtpFacade.Send(from, to, "Subject", "Body", null, "text/plain");
        }
    }
}