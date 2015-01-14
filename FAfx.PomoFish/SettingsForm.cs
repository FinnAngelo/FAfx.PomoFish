using Common.Logging;
using FAfx.PomoFish.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FAfx.PomoFish
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private readonly ILog _log;
        private readonly ISettings _settings;

        internal ISettings Settings { get { return _settings; } }

        internal SettingsForm(ILog log, ISettings settings)
        {
            _log = log;
            _settings = settings;
            _log.Trace("SettingsForm(ILog log, ISettings settings)");

            InitializeComponent();
            iSettingsBindingSource.DataSource = _settings;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _settings.Save();
            this.Close();
        }
    }
}
