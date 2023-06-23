using System;
using System.Collections.Generic;

namespace DA.Watchdog.Model
{
    public partial class Check
    {
        public Guid CheckId { get; set; }
        public Guid ObservableId { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool Success { get; set; }

        public virtual Observable Observable { get; set; }
    }
}
