using System;
using System.Collections.Generic;
using System.Text;

namespace MWapplicationXamarin
{
    public class Task
    {
        public Task(string id, string description, string priority, double value)
        {
            Id = id;
            Description = description;
            Priority = priority;
            Value = value;
        }

        public string Id { get; set; }
        public string Description { get; set; }
        public string Priority { get; set; }
        public double Value { get; set; }

    }
}
