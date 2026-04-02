using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab7
{
    /// <summary>
    /// Класс, представляющий данные о событии мониторинга
    /// </summary>
    public class MetricData(string metricName, double value, double threshold, DateTime timestamp)
    {
        public string MetricName { get; } = metricName ?? throw new ArgumentNullException(nameof(metricName));
        public double Value { get; } = value;
        public double Threshold { get; } = threshold;
        public DateTime Timestamp { get; } = timestamp;

        public override string ToString()
        {
            return $"Metric: {MetricName}, Value: {Value} (Threshold: {Threshold})";
        }
    }

    /// <summary>
    /// Класс аргументов события
    /// </summary>
    public class MetricEventArgs(string eventType, MetricData data) : EventArgs
    {
        public string EventType { get; } = eventType ?? throw new ArgumentNullException(nameof(eventType));
        public MetricData Data { get; } = data ?? throw new ArgumentNullException(nameof(data));
    }

    public delegate void MetricEventHandler(MetricEventArgs e);

    /// <summary>
    /// Субъект (Subject). Вместо интерфейса ISubject и методов Attach/Detach
    /// использует стандартное событие .NET.
    /// </summary>
    public class EventMonitor
    {
        public event MetricEventHandler? OnMetricExceeded;

        public void CheckMetric(string metricName, double value, double threshold)
        {
            Console.WriteLine($"[Monitor]: Checking {metricName} ({value} vs {threshold})");

            if (value > threshold)
            {
                MetricData eventData = new MetricData(metricName, value, threshold, DateTime.Now);

                OnMetricExceeded?.Invoke(new MetricEventArgs(eventType: metricName + "_Exceeded", data: eventData));
            }
        }
    }
}
