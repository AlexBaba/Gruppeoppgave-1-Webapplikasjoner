using System;
using System.Security.Cryptography;
using System.Text;


public class PasswordHandler
{
    private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
    private static SHA256Managed hasher = new SHA256Managed();

    private static int DEFAULT_SALT_LENGTH = 16;

    //ROUNDS burde være ca 100 000 for å bli regnet som sikkert
    private static int ROUNDS = 1;


    //Create new entry:
    //0. String password = ...;
    //1. Create salt -> string salt = generateSalt(16);
    //2. Create salted hash -> string saltedHash = generateHash(salt + password);
    //3. set dbSalt = salt
    //4. set dbSaltedHash = saltedHash

    //Comparing:
    //0. String password = ...;
    //1. Create salt -> string salt = dbSalt
    //2. Create salted hash -> string saltedHash = generateHash(dbSalt + password);
    //3. Check if saltedHash == dbSaltedHash(password stored in db)


    public static string createSaltedHash(string salt, string password)
    {
        return generateHash(salt + password);
    }


    public static string generateHash(string str)
    {

        //Copy bytes from str
        byte[] bytes = new byte[str.Length * sizeof(char)];
        System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);

        //Runs the hash algorithm several times
        for (int i = 0; i < ROUNDS; i++)
            bytes = hasher.ComputeHash(bytes);

        //Removes invalid ASCII characters
        return Convert.ToBase64String(bytes);
    }

    public static string generateSalt()
    {
        //Fills saltBuffer with random bytes
        byte[] saltBuffer = new byte[DEFAULT_SALT_LENGTH];
        rng.GetBytes(saltBuffer);

        //Removes string terminator, and uses less space
        return Convert.ToBase64String(saltBuffer);
    }

}
