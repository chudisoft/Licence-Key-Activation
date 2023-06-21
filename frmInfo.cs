using FoxLearn.License;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LicenceKey_Activation
{
    public partial class frmInfo : Form
    {
        public frmInfo()
        {
            InitializeComponent();
        }
        const int ProductCode = 1;

        private void Form1_Load(object sender, EventArgs e)
        {
            txtProductId.Text = ComputerInfo.GetComputerId();
            KeyManager keyManager = new KeyManager(txtProductId.Text);
            LicenseInfo licenseInfo = new LicenseInfo();
            int val = keyManager.LoadSuretyFile(
                String.Format(@"{0}\Key.lic", Application.StartupPath), ref licenseInfo);
            string ProductKey = licenseInfo.ProductKey;
            if (keyManager.ValidKey(ref ProductKey))
            {
                KeyValuesClass keyValues = new KeyValuesClass();
                if (keyManager.DisassembleKey(ProductKey, ref keyValues))
                {
                    if (keyValues.Type == LicenseType.TRIAL)
                    {
                        string DaysRemaining = String.Format(@"{0} days",
                            keyValues.Expiration.Date - DateTime.Now.Date);
                    }
                    else
                    {
                        string DaysRemaining = "Unlimited Plan";
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Registration Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show("Invalid Registration Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
