using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;


namespace HCI_Project
{
    public partial class SOS : Form
    {
        emergency e = new emergency();
        public SOS()
        {
            InitializeComponent();
            e.callfohelp();

        }
    }
    public class emergency
    {
        public void callfohelp()
        {
            string accountSid = "AC768d05b56fe0bebb508bf3c1667a2e28";
            string authToken = "0d124ddbc49eae80beb1f4c786b64da5";

            // Replace with the phone number to send the SMS message and voice call to
            string recipientPhoneNumber = "+201554736415";

            // Replace with the emergency message content
            string messageBody = "Emergency! Someone Fell on the ground Please send help immediately.";

            // Initialize TwilioClient
            TwilioClient.Init(accountSid, authToken);

            // Send SMS message
            MessageResource message = MessageResource.Create(
                to: new PhoneNumber(recipientPhoneNumber),
                from: new PhoneNumber("+12056065983"), // Replace with your Twilio phone number
                body: messageBody
            );

            Console.WriteLine("SMS message sent successfully: " + message.Sid);

            // Send voice call
            CallResource call = CallResource.Create(
                to: new PhoneNumber(recipientPhoneNumber),
                from: new PhoneNumber("+12056065983"),
                twiml: new Twiml("<Response><Say>" + messageBody + "</Say></Response>")
            );

            Console.WriteLine("Voice call initiated: " + call.Sid);

        }
    }


}