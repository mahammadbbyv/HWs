namespace PharmaceuticalsAPI.DBService
{
    public interface IDBService
    {
        public string RegisterPharmacy(string email, string phoneNumber, string password, string confirmPassword);
        public string LoginPharmacy(string email, string password, string issuer, string audience, byte[] key, string ipaddress);
        public string VerifyEmail(string email, string password);
        public string GetPharmacy(string token);
        public string UpdatePharmacy(string pharmacyId, string name, string phoneNumber, string address, string city, string token);
        public string AddPharmaceuticalToPharmacy(string name, string pharmacyId, string token);
        public string RemovePharmaceuticalFromPharmacy(string name, string pharmacyId, string token);
        public object GetPharmacyPharmaceuticals(string pharmacyId);
        public string LoginWithToken(string token);
        public object GetPharmaceuticals(string includes);
        public object GetPharmacies(string city, string pharmaceutical);
    }
}
