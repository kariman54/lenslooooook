
using lensLook.Dal.Models;
using lensLook.Pl.Settings;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;



namespace lensLook.Pl.Helper
{
    public class EmailSettings : IEmailSettings
    {
        private readonly MailSettings _mail;

        //To Inject All Thing Of  Mailsetting in AppSetting 
        public EmailSettings(IOptions<MailSettings> Mail)
        {
            _mail = Mail.Value;
        }

        #region Old Functionality
        //public static  void SendEmail(Email Model)
        //{
        //	var client = new SmtpClient("smtp.gmail.com", 587);   // LeagcyEmail   //host and Port;
        //	client.EnableSsl = true;
        //	client.Credentials = new NetworkCredential("ahmedalaayassin6@gmail.com", "dunlftgznketuptb");
        //	 client.Send("ahmedalaayassin6@gmail.com",Model.To,Model.Subject,Model.Body);





        //}
        #endregion



        public void SendEmail(Email model)
        {

            var Mail = new MimeMessage     // it`s Used By Package
            {
                Sender = MailboxAddress.Parse(_mail.Email),  // Represent Of Email Persone Have Send the Link To Reset The Password
                Subject = model.Subject,
            };




            Mail.To.Add(MailboxAddress.Parse(model.To));       // this Persone U Recipt The Request 


            var builder = new BodyBuilder();   // To Make builder Of Persone 


            builder.HtmlBody = model.Body; // thats Body 

            Mail.Body = builder.ToMessageBody();

            Mail.From.Add(new MailboxAddress(_mail.DisplayName, _mail.Email));

            using var smtpProvieder = new SmtpClient();
            smtpProvieder.Connect(_mail.Host, _mail.Port, SecureSocketOptions.StartTls);

            smtpProvieder.Authenticate(_mail.Email, _mail.Password);
            smtpProvieder.Send(Mail);
            smtpProvieder.Disconnect(true);
        }



    }
}
