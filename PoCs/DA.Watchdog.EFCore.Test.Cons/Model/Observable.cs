using System;
using System.Collections.Generic;

namespace DA.Watchdog.EFCore.Test.Cons.Model
{
    public partial class Observable
    {
        public Observable()
        {
            Check = new HashSet<Check>();
        }

        public Guid ObservableId { get; set; }
        public string Name { get; set; }
        public string Remarks { get; set; }

        public virtual ObservableMeta ObservableMeta { get; set; }
        public virtual ICollection<Check> Check { get; set; }
    }
}
