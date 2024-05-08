using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace ReChargeBackend.Utility
{
    public class NotificationManager
    {
        public async static Task NotifyUser(string title, string body, string imageUrl, string deviceToken, DateTime time, CancellationToken token)
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
            ScheduleMessage(message, time, token);

        }

        public async static Task ScheduleMessage(Message message, DateTime time, CancellationToken token)
        {
            if (time <= DateTime.Now)
            {
                Console.WriteLine("Message skipped");
                return;
            }
            Console.WriteLine("Message scheduled");
            await Task.Delay(time - DateTime.Now + new TimeSpan(0, 0, 15), token);
            string response = await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge")).SendAsync(message, token);
            Console.WriteLine($"Successfully sent notification for {DateTime.Now} at {time}: " + response);

        }

    }
}
