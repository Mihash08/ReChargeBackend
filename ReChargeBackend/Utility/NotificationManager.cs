using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace ReChargeBackend.Utility
{
    public class NotificationManager
    {
        public async static void NotifyUser(string title, string body, string imageUrl, string deviceToken, DateTime time, CancellationToken token)
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
            Console.WriteLine("Scheduling");
            ScheduleMessage(message, time, token);
            Console.WriteLine("Message scheduled");

        }

        public async static Task ScheduleMessage(Message message, DateTime time, CancellationToken token)
        {
            if (time > DateTime.Now)
            {
                await Task.Delay(time - DateTime.Now + new TimeSpan(0, 0, 15), token);
            }
            string response = await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge")).SendAsync(message, token);
            Console.WriteLine("Successfully sent notification: " + response);

        }

    }
}
