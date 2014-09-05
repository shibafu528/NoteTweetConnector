using System;

namespace NoteTweetConnector
{
    class MainClass
    {
        private const String CONSUMER_KEY = "E8DeKvJHpAlCEGa9hI657tME5";
        private const String CONSUMER_SECRET = "tieRk1ccVu4PoWUXLOG6IFKTQ3GYSXZpzvcRq2Z9uG3nBYoeU7";

        //なんとかして
        private const String ACCESS_TOKEN = "";
        private const String ACCESS_TOKEN_SECRET = "";

        public static void Main(string[] args)
        {
            var notepad = new NotepadManager();

            var tokens = CoreTweet.Tokens.Create(CONSUMER_KEY, CONSUMER_SECRET, ACCESS_TOKEN, ACCESS_TOKEN_SECRET);

            var textBuffer = "";
            foreach (var status in tokens.Statuses.HomeTimeline())
            {
                if (status.User.IsProtected)
                    continue;
                textBuffer += formatTweet(status) + "\n";
            }
            notepad.setText(textBuffer);

            var stream = tokens.Streaming.StartStream(CoreTweet.Streaming.StreamingType.User);
            foreach (var message in stream)
            {
                if (message is CoreTweet.Streaming.StatusMessage)
                {
                    var status = (message as CoreTweet.Streaming.StatusMessage).Status;
                    if (!status.User.IsProtected)
                        textBuffer = formatTweet(status) + "\n" + textBuffer;
                }
                notepad.setText(textBuffer);
            }
        }

        private static String formatTweet(CoreTweet.Status status)
        {
            return string.Format("[{0}] {1}: {2}", status.CreatedAt.ToLocalTime(), status.User.ScreenName, status.Text);
        }
    }
}
