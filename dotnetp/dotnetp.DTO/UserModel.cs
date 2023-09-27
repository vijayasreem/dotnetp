namespace dotnetp
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public double Income { get; set; }
        public int Age { get; set; }
        public int CreditScore { get; set; }
        public string BankAccountNumber { get; set; }
        public string RoutingNumber { get; set; }
        public double AvailableBalance { get; set; }
        public double PaymentAmount { get; set; }
        public string VendorName { get; set; }
        public bool PaymentApprovalRequired { get; set; }
    }
}