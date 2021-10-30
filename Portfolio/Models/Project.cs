using System;

namespace Portfolio.Models
{
    public class Project
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string About { get; init; }
        public string Implementation { get; init; }
        public string Conclusion { get; init; }
    }
}