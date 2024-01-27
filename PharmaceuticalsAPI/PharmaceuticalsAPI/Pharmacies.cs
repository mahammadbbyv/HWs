namespace PharmaceuticalsAPI
{
    public class Pharmacy
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set;}
        public string PhoneNumber { get; set;}
        public string Email { get; set; }
        public string City { get; set; }
        public bool IsVerified { get; set; }
    }
}
