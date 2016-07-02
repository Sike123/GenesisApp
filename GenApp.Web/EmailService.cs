using System.Configuration;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;


namespace GenApp.Web
{
    public class EmailService:IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridAsync(message);
        }

        private async Task ConfigSendGridAsync(IdentityMessage message)
        {
            var myMessage= new SendGridMessage();
            myMessage.AddTo(message.Destination);
            myMessage.From= new System.Net.Mail.MailAddress("pawangrg54@hotmail.com","pawan");
            myMessage.Subject = "Test Subject";
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credential= new NetworkCredential(ConfigurationManager.AppSettings["mailAccount"],
                ConfigurationManager.AppSettings["mailPassword"]);

            //Create a web transport for sending mail 
            var transportWeb = new SendGrid.Web(credential);

            //send the email
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}