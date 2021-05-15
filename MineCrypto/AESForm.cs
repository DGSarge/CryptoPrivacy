﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Threading;
using System.Drawing;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoPrivacy;
using System.Windows.Forms;
using System.IO;

namespace MineCrypto
{
    public partial class AES : Form
    {
        public AES()
        {
            InitializeComponent();
        }

        public static string StringToBinary(string data)
        {
            StringBuilder sb = new StringBuilder();

            foreach (char c in data.ToCharArray())
            {
                sb.Append(Convert.ToString(c, 2).PadLeft(8, '0'));
            }
            return sb.ToString();
        }

        public static string BinaryToString(string data)
        {
            List<Byte> byteList = new List<Byte>();

            for (int i = 0; i < data.Length; i += 8)
            {
                byteList.Add(Convert.ToByte(data.Substring(i, 8), 2));
            }
            return Encoding.ASCII.GetString(byteList.ToArray());
        }

        string Hash = "Un77bo3aJi3K2eWGPeDgY3YaMOdXJP2yYjqLOz3EdnhgBUOpLp";
        string AnotherEncryptionLayer = "m9COKoLqYTqWnu9gG8R1xn0iESjIIZqMva1CzWROSaUL6T1PX3";

        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (checkBox2.Checked == true)
                {
                    var binaryString = StringToBinary(textBox1.Text);
                    textBox1.Text = binaryString;
                    checkBox2.Enabled = false;
                }
                byte[] data = UTF8Encoding.UTF8.GetBytes(textBox1.Text);
                using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
                {
                    byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                    using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                    {
                        string initVector = "#N0w5VbjLUSrn8!B";
                        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                        AES.IV = initVectorBytes;
                        ICryptoTransform transform = AES.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        var AnotherLayer = Convert.ToBase64String(results, 0, results.Length);
                        byte[] dataa = UTF8Encoding.UTF8.GetBytes(AnotherLayer);
                        using (SHA256CryptoServiceProvider SHA2566 = new SHA256CryptoServiceProvider())
                        {
                            byte[] keyss = SHA2566.ComputeHash(UTF8Encoding.UTF8.GetBytes(AnotherEncryptionLayer));
                            using (AesCryptoServiceProvider AESS = new AesCryptoServiceProvider() { Key = keyss, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                            {
                                string iniitVector = "#N0w5VbjLUSrn8!B";
                                byte[] iniitVectorBytes = Encoding.ASCII.GetBytes(iniitVector);
                                AESS.IV = initVectorBytes;
                                ICryptoTransform transformm = AESS.CreateEncryptor();
                                byte[] resultss = transformm.TransformFinalBlock(dataa, 0, dataa.Length);
                                textBox1.Clear();
                                textBox3.Text = Convert.ToBase64String(resultss, 0, resultss.Length);
                                checkBox1.Enabled = false;
                            }
                        }
                    }
                }
            }
            else
            {
                if (checkBox2.Checked == true)
                {
                    var binaryString = StringToBinary(textBox1.Text);
                    textBox1.Text = binaryString;
                    textBox1.Clear();
                    checkBox2.Enabled = false;
                }
                byte[] data = UTF8Encoding.UTF8.GetBytes(textBox1.Text);
                using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
                {
                    string initVector = "#N0w5VbjLUSrn8!B";
                    byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                    byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                    using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                    {
                        AES.IV = initVectorBytes;
                        ICryptoTransform transform = AES.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        textBox3.Text = Convert.ToBase64String(results, 0, results.Length);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked == false)
                {
                    byte[] data = Convert.FromBase64String(textBox2.Text);
                    using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
                    {
                        byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                        using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                        {
                            string initVector = "#N0w5VbjLUSrn8!B";
                            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                            AES.IV = initVectorBytes;
                            ICryptoTransform transform = AES.CreateDecryptor();
                            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                            textBox4.Text = UTF8Encoding.UTF8.GetString(results);
                            textBox1.Clear();
                            if (checkBox2.Checked == true)
                            {
                                var binaryString = StringToBinary(textBox2.Text);
                                textBox2.Text = binaryString;
                                checkBox2.Enabled = true;
                            }
                        }
                    }
                }
                else if (checkBox1.Checked == true)
                {
                    byte[] data = Convert.FromBase64String(textBox2.Text);
                    using (SHA256CryptoServiceProvider SHA256 = new SHA256CryptoServiceProvider())
                    {
                        byte[] keys = SHA256.ComputeHash(UTF8Encoding.UTF8.GetBytes(AnotherEncryptionLayer));
                        using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider() { Key = keys, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                        {
                            string initVector = "#N0w5VbjLUSrn8!B";
                            byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                            AES.IV = initVectorBytes;
                            ICryptoTransform transform = AES.CreateDecryptor();
                            byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                            textBox2.Text = UTF8Encoding.UTF8.GetString(results);
                            checkBox1.Checked = false;
                            if (checkBox1.Checked == false)
                            {
                                byte[] dataa = Convert.FromBase64String(textBox2.Text);
                                using (SHA256CryptoServiceProvider SHA2566 = new SHA256CryptoServiceProvider())
                                {
                                    byte[] keyss = SHA2566.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                                    using (AesCryptoServiceProvider AESS = new AesCryptoServiceProvider() { Key = keyss, Mode = CipherMode.CBC, Padding = PaddingMode.PKCS7 })
                                    {
                                        string iniitVector = "#N0w5VbjLUSrn8!B";
                                        byte[] iniitVectorBytes = Encoding.ASCII.GetBytes(iniitVector);
                                        AESS.IV = iniitVectorBytes;
                                        ICryptoTransform transformm = AESS.CreateDecryptor();
                                        byte[] resultss = transformm.TransformFinalBlock(dataa, 0, dataa.Length);
                                        textBox2.Clear();
                                        textBox4.Text = UTF8Encoding.UTF8.GetString(resultss);
                                        if (checkBox2.Checked == true)
                                        {
                                            textBox4.Text = BinaryToString(textBox4.Text);
                                            checkBox1.Enabled = false;
                                            checkBox2.Enabled = true;
                                        }
                                        if (checkBox1.Enabled == false)
                                        {
                                            checkBox1.Enabled = true;
                                        }
                                        checkBox1.Checked = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + ", Please report that error to my github page to get help (im really happy to provide help)", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Please of you checked the checkbox (Add Another Layer of Encryption) if you want to decrypt it choose that checkbox again to decrypt it probarly.", "IMPORTANT", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show("that will encrypts your text with a key and encrypts it again with different key making it harder to decrypt", "Another Layer", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }

        private void AES_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked == true)
            {
                MessageBox.Show("WARNING: that will make the encrypted text more bigger", "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new AESFileEncryptForm().Show();
        }
    }
}
