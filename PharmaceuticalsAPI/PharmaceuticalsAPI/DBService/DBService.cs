using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace PharmaceuticalsAPI.DBService;

public class DBService : IDBService
{
    public string RegisterPharmacy(string email, string phoneNumber, string password, string confirmPassword)
    {
        using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
        conn.Open();
        using SqlCommand cmd = new($"select Email from Pharmacies where Email = '{email}'", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return JsonSerializer.Serialize(new {res = "Such email is already registered in another pharmacy.", ok = false});
        }
        else
        {
            if (password == confirmPassword)
            {
                conn.Close();
                var emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                if (!emailRegex.IsMatch(email))
                {
                    return JsonSerializer.Serialize(new { res = "Email is not valid.", ok = false });
                }
                var passwordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,20}$");
                if (!passwordRegex.IsMatch(password))
                {
                    return JsonSerializer.Serialize(new { res = "Password must contain at least 1 lowercase letter, 1 uppercase letter, 1 number, 1 special character and must be between 8 and 20 characters long.", ok = false });
                }
                var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
                conn.Open();
                string code = new Random().Next(0, 999999).ToString("000000");
                using SqlCommand cmd2 = new($"insert into Pharmacies (Name, PhoneNumber, Email, Password, IsEmailVerified, IsPharmacyVerified, Address, City, EmailCode) values ('', '+{phoneNumber}', '{email}', '{passwordHash}', 0, 0, '', '', {code});", conn);
                cmd2.ExecuteNonQuery();
                MailMessage mail = new();
                SmtpClient SmtpServer = new("smtp.gmail.com");
                mail.From = new MailAddress("rasimbabayev9g19@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Verify yourself";
                Random rand = new();
                mail.Body = $"<p>This <b>{code}</b> is your code for verification.</p>";
                mail.IsBodyHtml = true;

                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Port = 587;
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                SmtpServer.Credentials = new System.Net.NetworkCredential("rasimbabayev9g19@gmail.com", "gitobwidmpsvwsdv");
                SmtpServer.EnableSsl = true;
                SmtpServer.SendMailAsync(mail);
                return JsonSerializer.Serialize(new { res = "Registered successfully!", ok = true });
            }
            else
            {
                return JsonSerializer.Serialize(new { res = "Passwords do not match.", ok = false });
            }
        }
    }

    public string GetPharmacy(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        var emailFromToken = tokenS.Claims.First(claim => claim.Type == "Email").Value;
        var passwordFromToken = tokenS.Claims.First(claim => claim.Type == "Password").Value;
        bool isExpired = tokenS.ValidTo < DateTime.UtcNow;
        if(isExpired)
        {
            return JsonSerializer.Serialize(new {res= "Token is expired.", ok=false});
        }
        else
        {
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            using SqlCommand cmd = new($"select Password from Pharmacies where Email = '{emailFromToken}'", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (BCrypt.Net.BCrypt.Verify(passwordFromToken, reader.GetString(0)))
                {
                    reader.Close();
                    using SqlCommand cmd2 = new($"select PharmacyId, Name, Address, PhoneNumber, Email, City, IsPharmacyVerified from Pharmacies where Email = '{emailFromToken}'", conn);
                    using SqlDataReader reader2 = cmd2.ExecuteReader();
                    if (reader2.Read())
                    {
                        var pharmacyId = reader2.GetInt32(0);
                        var name = reader2.GetString(1);
                        var address = reader2.GetString(2);
                        var phoneNumber = reader2.GetString(3);
                        var email = reader2.GetString(4);
                        var city = reader2.GetString(5);
                        var IsVerified = reader2.GetBoolean(6);
                        var pharmacy = new Pharmacy()
                        {
                            Id = pharmacyId,
                            Name = name,
                            Address = address,
                            PhoneNumber = phoneNumber,
                            Email = email,
                            City = city,
                            IsVerified = IsVerified
                        };
                        reader2.Close();
                        using SqlCommand cmd3 = new($"select P.ProductId, P.Name from Products as P join PharmacyProduct as PP on P.ProductId = PP.ProductId where PP.PharmacyId = {pharmacyId}", conn);
                        using SqlDataReader reader3 = cmd3.ExecuteReader();
                        var pharmaceuticals = new List<Pharmaceuticals>();
                        while (reader3.Read())
                        {
                            pharmaceuticals.Add(new Pharmaceuticals()
                            {
                                Id = reader3.GetInt32(0),
                                Name = reader3.GetString(1)
                            });
                        }
                        return JsonSerializer.Serialize(new { res = pharmacy, pharmaceuticals = pharmaceuticals.ToArray(), ok = true });
                    }
                }
            }
        }
        return JsonSerializer.Serialize(new { res = "Error", ok = false });
    }

    public string LoginPharmacy(string email, string password, string issuer, string audience, byte[] key, string ipaddress)
    {
        using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
        conn.Open();
        using SqlCommand cmd = new($"select IsBanned from BanList where BanIpAddress = '{ipaddress}'", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            if (reader.GetBoolean(0))
            {
                return JsonSerializer.Serialize(new { res = "You are banned from this service.", ok = false });
            }
        }
        reader.Close();
        using SqlCommand cmd2 = new($"select IsEmailVerified from Pharmacies where Email = '{email}'", conn);
        using SqlDataReader reader2 = cmd2.ExecuteReader();
        if (reader2.Read())
        {
            if (!reader2.GetBoolean(0))
            {
                reader2.Close();
                using SqlCommand cmd3 = new($"select count(*) from BanList where BanIpAddress = '{ipaddress}'", conn);
                using SqlDataReader reader3 = cmd3.ExecuteReader();
                if (reader3.Read())
                {
                    if (reader3.GetInt32(0) == 0)
                    {
                        reader3.Close();
                        using SqlCommand cmd4 = new($"insert into BanList (BanIpAddress, Tries, IsBanned) values ('{ipaddress}', 1, 0)", conn);
                        cmd4.ExecuteNonQuery();
                        
                    }
                    else
                    {
                        if(reader3.GetInt32(0) == 3)
                        {
                            reader3.Close();
                            using SqlCommand cmd5 = new($"update BanList set IsBanned = 1 where BanIpAddress = '{ipaddress}'", conn);
                            cmd5.ExecuteNonQuery();
                            return JsonSerializer.Serialize(new { res = "You are banned from this service.", ok = false });
                        }
                        else
                        {
                            reader3.Close();
                            using SqlCommand cmd5 = new($"update BanList set Tries = Tries + 1 where BanIpAddress = '{ipaddress}'", conn);
                            cmd5.ExecuteNonQuery();
                        }
                        
                    }
                }
                return JsonSerializer.Serialize(new { res = "Email is not verified.", ok = false });
            }
        }
        conn.Close();
        conn.Open();
        using SqlCommand cmd6 = new($"select Password from Pharmacies where Email = '{email}'", conn);
        using SqlDataReader reader6 = cmd6.ExecuteReader();
        if (reader6.Read())
        {
            if (!BCrypt.Net.BCrypt.Verify(password, reader6.GetString(0)))
            {
                return JsonSerializer.Serialize(new { res = "Password is incorrect.", ok = false });
            }
        }
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim("Email", email),
                new Claim("Password", password),
                new Claim(JwtRegisteredClaimNames.Jti,
                    Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(1.5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        return JsonSerializer.Serialize(new { res = $"Logged in successfully! {email} {password}", ok = true, token = tokenString });
    }

    public string VerifyEmail(string email, string code)
    {
        try
        {
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            using SqlCommand cmd = new($"select EmailCode from Pharmacies where Email = '{email}'", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                if (reader.GetInt32(0) == Convert.ToInt32(code))
                {
                    reader.Close();
                    using SqlCommand cmd2 = new($"update Pharmacies set IsEmailVerified = 1 where Email = '{email}'", conn);
                    cmd2.ExecuteNonQuery();
                    return JsonSerializer.Serialize(new { res = "Email verified successfully!", ok = true });
                }
                else
                {
                    return JsonSerializer.Serialize(new { res = "Code is incorrect.", ok = false });
                }
            }
            else
            {
                return JsonSerializer.Serialize(new { res = "Email is not registered.", ok = false });
            }
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public string LoginWithToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        var emailFromToken = tokenS.Claims.First(claim => claim.Type == "Email").Value;
        var passwordFromToken = tokenS.Claims.First(claim => claim.Type == "Password").Value;

        bool isExpired = tokenS.ValidTo < DateTime.UtcNow;
        if(isExpired)
        {
            return JsonSerializer.Serialize(new {res = "Token is expired.", ok=false});
        }
        using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
        conn.Open();
        using SqlCommand cmd = new($"select Password from Pharmacies where Email = '{emailFromToken}'", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            if (BCrypt.Net.BCrypt.Verify(passwordFromToken, reader.GetString(0)))
            {
                return JsonSerializer.Serialize(new { res = "Logged in successfully!", ok = true });
            }
        }
        return JsonSerializer.Serialize(new { res = "Error", ok = false });
    }

    public string UpdatePharmacy(string pharmacyId, string name, string address, string city, string phoneNumber, string token)
    {
        try
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token);
            var tokenS = handler.ReadToken(token) as JwtSecurityToken;
            bool isExpired = tokenS.ValidTo < DateTime.UtcNow;
            if(isExpired)
            {
                return JsonSerializer.Serialize(new {res = "Token is expired.", ok=false});
            }
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            using SqlCommand cmd = new($"update Pharmacies set Name = '{name}', Address = '{address}', City = '{city}', PhoneNumber = '{phoneNumber}' where PharmacyId = {pharmacyId}", conn);
            cmd.ExecuteNonQuery();
            return JsonSerializer.Serialize(new { res = "Updated successfully!", ok = true });
        }
        catch (Exception e)
        {
            return JsonSerializer.Serialize(new { res = e.Message, ok = false });
        }
    }

    public string AddPharmaceuticalToPharmacy(string name, string pharmacyId, string token)
    {
        using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
        conn.Open();
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        bool isExpired = tokenS.ValidTo < DateTime.UtcNow;
        if (isExpired)
        {
            return JsonSerializer.Serialize(new { res = "Token is expired.", ok = false });
        }
        using SqlCommand cmd = new($"select ProductId, Name from Products where Name = '{name}'", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var pharmaceutical = new Pharmaceuticals()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
            reader.Close();
            using SqlCommand cmd2 = new($"insert into PharmacyProduct (PharmacyId, ProductId) values ({pharmacyId}, {pharmaceutical.Id})", conn);
            cmd2.ExecuteNonQuery();
            return JsonSerializer.Serialize(new { res = "Added successfully!", ok = true });
        }
        return JsonSerializer.Serialize(new { res = "Error.", ok = false });
    }

    public string RemovePharmaceuticalFromPharmacy(string name, string pharmacyId, string token)
    {
        using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
        conn.Open();
        var handler = new JwtSecurityTokenHandler();
        var jsonToken = handler.ReadToken(token);
        var tokenS = handler.ReadToken(token) as JwtSecurityToken;
        bool isExpired = tokenS.ValidTo < DateTime.UtcNow;
        if (isExpired)
        {
            return JsonSerializer.Serialize(new { res = "Token is expired.", ok = false });
        }
        using SqlCommand cmd = new($"select ProductId, Name from Products where Name = '{name}'", conn);
        using SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var pharmaceutical = new Pharmaceuticals()
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1)
            };
            reader.Close();
            using SqlCommand cmd2 = new($"delete from PharmacyProduct where PharmacyId = {pharmacyId} and ProductId = {pharmaceutical.Id}", conn);
            cmd2.ExecuteNonQuery();
            return JsonSerializer.Serialize(new { res = "Deleted successfully!", ok = true });
        }
        return JsonSerializer.Serialize(new { res = "Error.", ok = false });
    }

    public object GetPharmacyPharmaceuticals(string pharmacyId)
    {
        try
        {
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            var pharmaceuticals = new List<Pharmaceuticals>();
            using SqlCommand cmd = new($"select P.ProductId, P.Name from Products as P join PharmacyProduct as PP on P.ProductId = PP.ProductId where PP.PharmacyId = {pharmacyId}", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pharmaceuticals.Add(new Pharmaceuticals()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            conn.Close();
            return JsonSerializer.Serialize(new {res = pharmaceuticals.ToArray(), ok=true});
        }
        catch (Exception e)
        {
            return JsonSerializer.Serialize(new { res = e.Message, ok = false});
        }
    }

    public object GetPharmaceuticals(string includes)
    {
        try
        {
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            var pharmaceuticals = new List<Pharmaceuticals>();

            using SqlCommand cmd = new($"select * from Products where Name like '%' + '{includes}' + '%'", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                pharmaceuticals.Add(new Pharmaceuticals()
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1)
                });
            }
            conn.Close();
            
            return pharmaceuticals;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public object GetPharmacies(string city, string pharmaceutical)
    {
        try {
            using SqlConnection conn = new("Data Source=SQL8004.site4now.net;Initial Catalog=db_aa4553_piyadb;User Id=db_aa4553_piyadb_admin;Password=Fl1ck_Maga");
            conn.Open();
            var pharmacies = new List<Pharmacy>();
            using SqlCommand cmd = new($"declare @ProductName nvarchar(max) = '{pharmaceutical}'; declare @PharmacyCity nvarchar(max) = '{city}';select P.PharmacyId, P.Name as PharmacyName, P.PhoneNumber, P.Email, P.Address, P.City, PR.ProductId, PR.Name as ProductName from Pharmacies as P join PharmacyProduct as PP on P.PharmacyId = PP.PharmacyId join Products as PR on PP.ProductId = PR.ProductId where PR.Name = @ProductName and P.City = @PharmacyCity and P.IsPharmacyVerified = 1;", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var pharmacyId = reader.GetInt32(0);
                var pharmacyName = reader.GetString(1);
                var phoneNumber = reader.GetString(2);
                var email = reader.GetString(3);
                var address = reader.GetString(4);
                var sqlcity = reader.GetString(5);
                var productId = reader.GetInt32(6);
                var productName = reader.GetString(7);
                var pharmacy = new Pharmacy()
                {
                    Id = pharmacyId,
                    Name = pharmacyName,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Address = address,
                };
                pharmacies.Add(pharmacy);
            }
            conn.Close();
            return pharmacies;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }
}
