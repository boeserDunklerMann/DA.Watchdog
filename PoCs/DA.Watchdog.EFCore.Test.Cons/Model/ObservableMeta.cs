using System;
using System.Collections.Generic;

namespace DA.Watchdog.EFCore.Test.Cons.Model
{
    public partial class ObservableMeta
    {
        public Guid ObservableId { get; set; }
        public DateTime? CreationDate { get; set; }
        public bool? Active { get; set; }

        public virtual Observable Observable { get; set; }
    }
}
