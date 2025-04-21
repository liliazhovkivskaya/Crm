namespace CrmService.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }

        public string Discription { get; set; }

        public LogType LogType { get; set; }

        public string? Exception { get; set; }

    }

    public enum LogType
    {
        LogIngo = 0,
        LogError = 1
    }
}
