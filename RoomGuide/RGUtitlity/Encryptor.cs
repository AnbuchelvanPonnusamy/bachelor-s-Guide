namespace RGUtitlity
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public class Encryptor
    {
        #region Variable Declaration

        private static byte[] fromEncrypt;
        private static byte[] encrypted;
        private static byte[] toEncrypt;
        private static byte[] es;
        private static ASCIIEncoding textConverter;
        private static RC2CryptoServiceProvider rc2Crypto;
        private static byte[] initVector = Encoding.ASCII.GetBytes("gateway24");

        //// Create a new 128 bit key.
        private static byte[] keySize = Encoding.ASCII.GetBytes("ABCDEFGHIJKLMNOP");

        #endregion Variable Declaration

        #region Constructor

        public Encryptor()
        {
        }

        #endregion Constructor

        #region Encrypt

        //// <summary>
        ////
        //// </summary>
        //// <param name="TextToBeEncrypted"></param>
        //// <returns></returns>
        public static string Encrypt(string textToBeEncrypted, string dynamicKey)
        {
            try
            {
                if (textToBeEncrypted == null)
                {
                    textToBeEncrypted = string.Empty;
                }

                if ((dynamicKey != null) && (!IsValideDynamickey(dynamicKey)))
                {
                    throw new Exception("Cannot Encrypt the given string");
                }

                textConverter = new ASCIIEncoding();
                rc2Crypto = new RC2CryptoServiceProvider();
                rc2Crypto.Mode = System.Security.Cryptography.CipherMode.CBC;
                ICryptoTransform encryptor = rc2Crypto.CreateEncryptor(keySize, initVector);
                MemoryStream rc2msEncrypt = new MemoryStream();
                CryptoStream rc2csEncrypt = new CryptoStream(rc2msEncrypt, encryptor, CryptoStreamMode.Write);
                toEncrypt = textConverter.GetBytes(textToBeEncrypted);
                if (toEncrypt != null || toEncrypt.Length > 0)
                {
                    ////Write all data to the crypto stream and flush it.
                    rc2csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
                    rc2csEncrypt.FlushFinalBlock();
                }

                encrypted = rc2msEncrypt.ToArray();
                if (encrypted == null)
                {
                    return null;
                }

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Encrypt

        #region Decrypt

        ///// <summary>
        //// Decrypts the encrypted text
        //// </summary>
        //// <param name="EncryptedText"></param>
        //// <returns></returns>
        public static string Decrypt(string encryptedText, string dynamicKey)
        {
            try
            {
                if (string.IsNullOrEmpty(encryptedText))
                {
                    return null;
                }

                if (!string.IsNullOrEmpty(dynamicKey) && (!IsValideDynamickey(dynamicKey)))
                {
                    throw new Exception("Cannot Decrypt the given string");
                }

                textConverter = new ASCIIEncoding();
                //// RC2 cryptology
                rc2Crypto = new RC2CryptoServiceProvider();

                //// Get a decryptor that uses the same key and initVector as the encryptor.
                ICryptoTransform decryptor = rc2Crypto.CreateDecryptor(keySize, initVector);

                es = Convert.FromBase64String(encryptedText);

                if (es == null || es.Length == 0)
                {
                    return null;
                }

                MemoryStream rc2msDecrypt = new MemoryStream(es);
                CryptoStream rc2csDecrypt = new CryptoStream(rc2msDecrypt, decryptor, CryptoStreamMode.Read);
                fromEncrypt = new byte[es.Length];
                if (fromEncrypt == null || fromEncrypt.Length == 0)
                {
                    return null;
                }

                rc2csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                string plainText = textConverter.GetString(fromEncrypt);
                plainText = plainText.Replace("\0", string.Empty);
                return plainText;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion Decrypt

        #region GetValidDyanamicKey

        public static string GetValidDynamicKey()
        {
            string key = string.Empty;
            try
            {
                key += Guid.NewGuid().ToString().Replace("-", "X").Substring(0, 3) + "-";
                key += Guid.NewGuid().ToString().Replace("-", "X").Substring(0, 4) + "-";
                key += Guid.NewGuid().ToString().Replace("-", "X").Substring(0, 5) + "-A";
                key += Reverse(key);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return key;
        }

        #endregion GetValidDyanamicKey

        #region validate Dynamic key

        private static bool IsValideDynamickey(string dynamicKey)
        {
            bool isValid = false;
            try
            {
                if ((dynamicKey != null) &&
                     (dynamicKey == Reverse(dynamicKey)) &&
                     (dynamicKey.Length == 32) &&
                     (CharCount(dynamicKey, "-") == 6) &&
                     ((dynamicKey.Substring(3, 1) == "-") && (dynamicKey.Substring(8, 1) == "-") &&
                     (dynamicKey.Substring(14, 1) == "-") && (dynamicKey.Substring(17, 1) == "-") &&
                     (dynamicKey.Substring(23, 1) == "-") && dynamicKey.Substring(28, 1) == "-") &&
                     (dynamicKey.Substring(15, 2) == "AA"))
                {
                    isValid = true;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return isValid;
        }

        #endregion validate Dynamic key

        #region Reverse a string

        private static string Reverse(string strParam)
        {
            if (strParam.Length == 1)
            {
                return strParam;
            }
            else
            {
                return Reverse(strParam.Substring(1)) + strParam.Substring(0, 1);
            }
        }

        #endregion Reverse a string

        #region Char count

        private static int CharCount(string strSource, string strToCount)
        {
            int count = 0;
            int position = strSource.IndexOf(strToCount, StringComparison.CurrentCulture);
            while (position != -1)
            {
                count++;
                strSource = strSource.Substring(position + 1);
                position = strSource.IndexOf(strToCount, StringComparison.CurrentCulture);
            }

            return count;
        }

        #endregion Char count
    }
}