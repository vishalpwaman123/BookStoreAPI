using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;

namespace MSMQ
{
    public class SendMail
    {

        /// <summary>
        /// This Method is used for sending Mail
        /// </summary>
        /// <param name="email">It contains Email id</param>
        /// <param name="token">It contains JWT Token</param>
        public void ReceiveMessageFromQueue(string Body, string Label)
        {
            try
            {

                // Created new instance of MailMessage
                MailMessage mail = new MailMessage();

                // It is an New instence of smtpClient
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);

                // It contains Sender mail Address
                mail.From = new MailAddress("1001thebeast1001@gmail.com");

                // It Contains the Reciver mail Address
                mail.To.Add(new MailAddress("1001thebeast1001@gmail.com"));

                // It is an Subject Of mail
                mail.Subject = "Reset PassWord ";

                // It is an body of Mail
                mail.Body = "Subject: " + Label + "\n " + Body;

                smtpClient.Credentials = new System.Net.NetworkCredential("1001thebeast1001@gmail.com", "8806787166");
                smtpClient.EnableSsl = true;

                // Send Mail
                smtpClient.Send(mail);
                Console.WriteLine("link has been sent to your mail!!!\n\n\n");
            }
            catch (Exception exception)
            {
                Console.Write(exception.ToString());
            }
        }


    }
}
