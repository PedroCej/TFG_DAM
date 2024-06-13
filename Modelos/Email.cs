using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Threading;

namespace ProyectoTFG.Modelos
{
    class Email
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPass;

        public Email(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
        {
            _smtpServer = smtpServer;
            _smtpPort = smtpPort;
            _smtpUser = smtpUser;
            _smtpPass = smtpPass;
        }

        public void SendEmail(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("APPcidencias", _smtpUser));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            message.Body = new TextPart("plain") { Text = body };

            using var client = new SmtpClient();
            client.Connect(_smtpServer, _smtpPort, true);
            client.Authenticate(_smtpUser, _smtpPass);
            client.Send(message);
            client.Disconnect(true);
        }

        public void RegisterUser(string email)
        {
            var subject = "Bienvenido a la APPcidencias del CIFP nº1 Cuenca";
            var body = "Gracias por registrarte en APPcidencias. \tEsperamos que disfrutes usando nuestra aplicación. Ya puedes enviar las incidencias que registres en nuestro centro para que nos pongamos a solucionarlas. \nGracias por tu colaboración.";
            var emailThread = new Thread(() => SendEmail(email, subject, body));
            emailThread.Start();
        }
    }
}
