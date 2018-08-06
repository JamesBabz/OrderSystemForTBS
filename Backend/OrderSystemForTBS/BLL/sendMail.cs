using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using DAL.Entities;

namespace BLL
{
    public class sendMail
    {
        private Employee _newEmployee;
        //Smpt server
        public const string GMAIL_SERVER = "smtp.gmail.com";
        //Connecting port
        public const int PORT = 587;

        //sends email with given information
        public void mailTo(string mail, string password, string name)
        {


            SmtpClient mailServer = new SmtpClient();

            //Provide your email id with your password.
            //Enter the app-specfic password if two-step authentication is enabled.
            NetworkCredential MyCredentials = new NetworkCredential("je.enemark96@gmail.com", "tzyxfsqgxandltqt");

            mailServer.Host = "smtp.gmail.com";
            mailServer.Port = 587;//for local
            //mailServer.Port = 25;//for online
            mailServer.EnableSsl = true;
            mailServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailServer.UseDefaultCredentials = false;
            mailServer.Credentials = MyCredentials;



            //Senders email.
            string from = "je.enemark96@gmail.com";
            //Receiver email
            string to = mail;


            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));

            //Subject of the email.
            msg.Subject = "Kodeord";

            //Specify the body of the email here.
            msg.Body = 
                "Hej " + name + " \n" + " Her er din nye kode, denne skal bruges næste gang du logger ind, " +
                "du vil her angive din egen adgangskode til fremadrettet brug." + " \n" +
                "Kode: " + password;

            mailServer.Send(msg);

        }
    }
}
