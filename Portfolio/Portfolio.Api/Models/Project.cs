using System;

namespace Portfolio.Api.Models
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string About { get; set; }
        public string Implementation { get; set; }
        public string Conclusion { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}