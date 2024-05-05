using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;

namespace ReChargeBackend.Utility
{
    public class NotificationManager
    {
        public async static void NotifyUser(string title, string body, string imageUrl, string deviceToken)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("firebase/serviceAccountKey.json")
            }, "recharge");

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

            string response = await FirebaseMessaging.GetMessaging(FirebaseApp.GetInstance("recharge")).SendAsync(message);
            Console.WriteLine("Successfully sent notification: " + response);

        }

    }
}
