using FoxLearn.License;
using LicenceKey_Activation.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenceKey_Activation
{
    public partial class frmGenerate : Form
    {
        public frmGenerate()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;
        private string expiryDays;

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            KeyManager keyManager = new KeyManager(txtProductId.Text);
            KeyValuesClass keyValues;
            string ProductKey = String.Empty;
            keyValues = new KeyValuesClass()
            {
                Header = Convert.ToByte(9),
                Footer = Convert.ToByte(6),
                ProductCode = (byte)ProductCode,
                Edition = Edition.ENTERPRISE,
                Version = 1
            };
            if (cmbLicenceType.SelectedIndex == 0)
            {
                keyValues.Type = LicenseType.FULL;
            }
            else
            {
                keyValues.Type = LicenseType.TRIAL;
                keyValues.Expiration = DateTime.Now.AddDays(
                    Convert.ToInt32(txtExpDays.Text));
            }
            if (!keyManager.GenerateKey(keyValues, ref ProductKey))
            {
                txtProductKey.Text = "Error";
            }
            else
                txtProductKey.Text = ProductKey;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cmbLicenceType.SelectedIndex = 0;
            txtProductId.Text = ComputerInfo.GetComputerId();
        }

        private void cmbLicenceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtExpDays.Text))
                expiryDays = txtExpDays.Text;
            if(cmbLicenceType.SelectedIndex == 0)
            {
                txtExpDays.Enabled = false;
                txtExpDays.Clear();
                btnGenerate.Focus();
            }
            else
            {
                txtExpDays.Enabled = true;
                txtExpDays.Text = expiryDays;
                txtExpDays.Focus();
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(txtProductKey.Text);
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            txtProductId.Text = Clipboard.GetText();
        }
    }
}

