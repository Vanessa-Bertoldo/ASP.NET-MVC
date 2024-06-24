namespace web.students.Logging
{
    public interface ICustomLogger
    {
        void Log(string message);
    }

    public class MockLogger : ICustomLogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"Log: {message}");
        }
    }
}
