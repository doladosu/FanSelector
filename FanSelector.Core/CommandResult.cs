namespace FanSelector.Core
{
    /// <summary>
    /// Simple result class for command handlers to return 
    /// </summary>
    public class CommandResult
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}