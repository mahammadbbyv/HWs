namespace PharmaceuticalsAPI.DBService
{
    public interface IDBService
    {
        public string RegisterPharmacy(string email, string phoneNumber, string password, string confirmPassword);
        public string LoginPharmacy(string email, string password, string issuer, string audience, byte[] key, string ipaddress);
        public string VerifyEmail(string email, string password);
        public string GetPharmacy(string token);
        public bool AddPharmaceuticalToPharmacy(string name, string pharmacyId, string key, string ipaddress);
        public object GetPharmacyPharmaceuticals(string pharmacyId);
        public string LoginWithToken(string token);
        public object GetPharmaceuticals(string includes);
        public object GetPharmacies(string city, string pharmaceutical);
    }
}
