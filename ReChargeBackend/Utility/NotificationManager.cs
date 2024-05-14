using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace ReChargeBackend.Utility
{
    public class NotificationManager
    {
        public async static Task ScheduleNotificationToUser(string title, string body, string imageUrl, string deviceToken, DateTime time)
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
            Console.WriteLine($"Scheduling for {time}");
            ScheduleMessage(message, time);
        }

        public async static Task SendNotification(string title, string body, string imageUrl, string deviceToken)
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
            SendMessage(message);
        }
        public async static Task ScheduleMessage(Message message, DateTime time)
        {
            if (time <= DateTime.Now)
            {
                Console.WriteLine("Message skipped");
                return;
            }
            Console.WriteLine("Message scheduled");
            await Task.Delay(time - DateTime.Now);
            string response = await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge")).SendAsync(message);
            Console.WriteLine($"Successfully sent notification for {DateTime.Now} at {time}: " + response);

        }
        public async static Task SendMessage(Message message)
        {
            string response = await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge")).SendAsync(message);
            Console.WriteLine($"Successfully sent notification: " + response);
        }

    }
}
