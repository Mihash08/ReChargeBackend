using FirebaseAdmin;
using FirebaseAdmin.Messaging;

namespace ReChargeBackend.Utility
{
    public class NotificationManager
    {
        public async static void NotifyUser(string title, string body, string imageUrl, string deviceToken)
        {
            var message = new Message()
            {
                Notification = new Notification()
                {
                    Title = title,
                    Body = body,
                    ImageUrl = imageUrl
                },
                Token = deviceToken

            };

            //await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge-firebase"))(message);

        }

    }
}
