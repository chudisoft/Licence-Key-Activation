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
    public partial class frmActivate : Form
    {
        public bool ShowInfo = false;
        public frmActivate()
        {
            InitializeComponent();
            if (ShowInfo)
            {
                txtProductId.Enabled = false;
                txtProductKey.Enabled = false;
                btnActivate.Visible = false;
            }
        }
        const int ProductCode = 1;
        private void btnActivate_Click(object sender, EventArgs e)
        {
            KeyManager keyManager = new KeyManager(txtProductId.Text);
            string ProductKey = txtProductKey.Text;
            if (keyManager.ValidKey(ref ProductKey))
            {
                KeyValuesClass keyValues = new KeyValuesClass();
                if (keyManager.DisassembleKey(ProductKey, ref keyValues))
                {
                    LicenseInfo licenseInfo = new LicenseInfo()
                    {
                        ProductKey = ProductKey,
                        FullName = "HCF Well Application"
                    };
                    if (keyValues.Type == LicenseType.TRIAL)
                    {
                        licenseInfo.Day = keyValues.Expiration.Day;
                        licenseInfo.Month = keyValues.Expiration.Month;
                        licenseInfo.Year = keyValues.Expiration.Year;
                    }
                    keyManager.SaveSuretyFile(String.Format(@"{0}\Key.lic", Application.StartupPath), licenseInfo);
                    MessageBox.Show("Application registered successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
                else
                {
                    MessageBox.Show("Unable to register Application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }else
                MessageBox.Show("Invalid Registration Key.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtProductId.Text = ComputerInfo.GetComputerId();
        }
    }
}
