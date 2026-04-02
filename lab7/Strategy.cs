using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    public interface IFormatStrategy
    {
        string Format(string message, DateTime timestamp);
    }

    public class TextFormatStrategy : IFormatStrategy

    {
        public string Format(string message, DateTime timestamp) =>
            $"[{timestamp:yyyy-MM-dd HH:mm:ss}] {message}";
    }

    public class JsonFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) =>
            $"{{\"timestamp\":\"{timestamp:O}\",\"message\":\"{message}\"}}";
    }
    public class HtmlFormatStrategy : IFormatStrategy
    {
        public string Format(string message, DateTime timestamp) => 
            $"<div class='event'><span class='time'>[{timestamp:HH:mm:ss}]</span> <span class='message'>{message}</span></div>";
        
    }
}