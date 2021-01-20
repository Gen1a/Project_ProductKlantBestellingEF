using System;

namespace EntityFrameworkRepository.Interfaces
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public DateTime AutoTimeCreation { get; set; }
        public long AutoUpdateCount { get; set; }
        public DateTime AutoTimeUpdate { get; set; }
        public string AutoUpdatedBy { get; set; }
    }
}
