using System.Net.Mail;

namespace Z1
{
    public class SmtpFacade
    {
        public void Send(string From, string To,
                            string Subject, string Body,
                            Stream? Attachment, string AttachmentMimeType)
        {
            MailMessage message = new MailMessage(From, To, Subject, Body);

            if(Attachment != null)
            {
                Attachment attachment = new Attachment(Attachment, AttachmentMimeType); 
                message.Attachments.Add(attachment);
            }

            SmtpClient smtpClient = new SmtpClient("localhost", 587);   

            smtpClient.Send(message);
            smtpClient.Dispose();
        }
    }
}