using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharpOcarina
{
    public partial class SettingsForm : Form
    {
        // To add a setting, simply add a checkbox to the form and set its internal name to the name of the field in the Settings class.
        Settings EditedSettings;

        public SettingsForm(Settings settings)
        {
            InitializeComponent();
            EditedSettings = settings;
            ApplyCheckboxSettingsToForm();
        }

        public void ApplyCheckboxSettingsToForm()
        {
            foreach (MemberInfo propertyInfo in EditedSettings.GetType().GetMembers())
            {
                try
                {
                    Control c = GetControl(this, propertyInfo.Name);

                    if (c is CheckBox)
                        (c as CheckBox).Checked = (bool)(propertyInfo as FieldInfo).GetValue(EditedSettings);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Settings -> controls transfer error with control {propertyInfo.Name}: {ex.Message}");
                }
            }
        }

        private void Settings_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                (EditedSettings.GetType().GetMembers().First(x => x.Name == (sender as CheckBox).Name) as FieldInfo).SetValue(EditedSettings, (sender as CheckBox).Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Control -> settings error with control {(sender as Control).Name}: {ex.Message}");
            }
        }

        public void SaveSettings()
        {
            IO.Export<Settings>(EditedSettings, "Settings.xml");
        }

        public Control GetControl(Control Parent, string Name)
        {
            if (Parent.Name == Name)
                return Parent;

            foreach (Control c in Parent.Controls)
            {
                if (c.Name == Name)
                    return c;

                if (c.HasChildren)
                {
                    Control cc = GetControl(c, Name);

                    if (cc != null)
                        return cc;
                }
            }

            return null;
        }
    }
}
