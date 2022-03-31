using System;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace EncryptDecryptValue
{
    public partial class Encrypt_Decrypt : Form
    {
        public Encrypt_Decrypt()
        {
            InitializeComponent();
        }
       
        string hash = "#djm@";

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtValue.Text))
            {
                try
                {
                    byte[] data = UTF8Encoding.UTF8.GetBytes(txtValue.Text);
                    using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                    {
                        byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                        using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                        {
                            ICryptoTransform transfrom = tripDes.CreateEncryptor();
                            byte[] results = transfrom.TransformFinalBlock(data, 0, data.Length);
                            txtEncrypt.Text = Convert.ToBase64String(results, 0, results.Length);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Encrypt: \n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Value shouldn't be empty");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            try
            {

            
            byte[] data = Convert.FromBase64String(txtEncrypt.Text);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transfrom = tripDes.CreateDecryptor();
                    byte[] results = transfrom.TransformFinalBlock(data, 0, data.Length);
                    txtDecrypt.Text = UTF8Encoding.UTF8.GetString(results);
                }

            }
            }catch (Exception ex)
            {
                MessageBox.Show("Decrypt: \n" + ex.Message);
            }  
        }
    }
}
