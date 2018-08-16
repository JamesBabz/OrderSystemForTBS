using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using DAL.Entities;

namespace BLL
{
    public class sendMail
    {
        private Employee _newEmployee;

        //Smpt server
        public const string OUTLOOK_SERVER = "smtp-mail.outlook.com";

        public const string GMAIL_SERVER = "smtp.gmail.com";


        //Connecting port
        public const int PORT = 587;

        //sends email with given information
        public void mailTo(string mail, string password, string name)
        {


            SmtpClient mailServer = new SmtpClient();

            //Provide your email id with your password.
            //Enter the app-specfic password if two-step authentication is enabled.
            NetworkCredential MyCredentials = new NetworkCredential("passwordrecovery@tbs.dk", "91Maskin");

            mailServer.Host = OUTLOOK_SERVER;
            mailServer.Port = 587; //for local
            //mailServer.Port = 25;//for online
            mailServer.EnableSsl = true;
            mailServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailServer.UseDefaultCredentials = false;
            mailServer.Credentials = MyCredentials;


            //Senders email.
            string from = "passwordrecovery@tbs.dk";
            //Receiver email
            string to = mail;


            MailMessage msg = new MailMessage();

            msg.From = new MailAddress(from);
            msg.To.Add(new MailAddress(to));
            msg.IsBodyHtml = true;
            msg.Headers.Add("Content-Type", "content=text/html; charset=\"UTF-8\"");

            //Subject of the email.
            msg.Subject = "Ny adgangskode";

            //Specify the body of the email here.
            msg.Body =
                "Hej " + "<b>" + name + "</b>" +
                "<br>" +
                "<br>" +
                "<br>" +
                "Her er din nye kode. Som skal bruges næste gang du logger ind. " +
                "du vil her angive din egen adgangskode til fremadrettet brug." +
                "<br>" +
                "<br>" +
                "<br>" +
                "Adgangskode: " + "<b>" + password +
                "<br>" +
                "<br>" +
                "<br>" +
                "<img src='https://i.imgur.com/VxHTDDr.png'>" +
                "<br>" +
                "<b>" + "Denne mail kan ikke besvares, ved spørgsmål kontakt administrationen" + "</b>";

            mailServer.Send(msg);
        }

    }
}


