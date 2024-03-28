namespace DataAccess.Models
{
    public partial class Person
    {
        public Person()
        {
            StudentParents = new HashSet<Student>();
        }

        public long PersonId { get; set; }
        public string FullName { get; set; } = null!;
        public string? UserAvatar { get; set; }
        public string Phone { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string Address { get; set; } = null!;
        public DateTime? Dob { get; set; }

        public virtual Account? Account { get; set; }
        public virtual Student? StudentStudentNavigation { get; set; }
        public virtual Tutor? Tutor { get; set; }
        public virtual Staff? Staff { get; set; }
        public virtual ICollection<Student> StudentParents { get; set; }
    }
}
