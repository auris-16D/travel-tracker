namespace TravelTracker.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("treks")]
    public class Trek
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public long StartTime { get; set; }

        public long FinishTime { get; set; }

        public string Area { get; set; }

        public double StartPointLatitude { get; set; }

        public double StartPointLongitude { get; set; }

        public double FinishPointLatitude { get; set; }

        public double FinishPointLongitude { get; set; }

        public Weather Weather { get; set; }

        public string Summary { get; set; }

        public virtual Owner Owner { get; set; }
    }

    [Table("owners")]
    public class Owner
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Trek> Treks { get; set; }
    }
}
