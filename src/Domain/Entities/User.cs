namespace Domain.Entities
{
	public class User : BaseEntity
	{
        public User()
        {
            Employments = new List<Employment>();
        }

        public string? FirstName { get; set; } 
        public string? LastName { get; set; } 
        public string? Email { get; set; } 
        public Address? Address { get; set; }
        public List<Employment> Employments { get; set; }  

    }
}

