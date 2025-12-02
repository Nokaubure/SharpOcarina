using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace SharpOcarina
{
    public class AutoCompleteTextBox : TextBox
    {
        private ListBox listBox;
        private Form parentForm;
        private List<string> items = new List<string>();

        public AutoCompleteTextBox()
        {
            listBox = new ListBox();
            listBox.Visible = false;
            listBox.MouseDown += (object s, MouseEventArgs me) => ChooseItem(me.Location);



            this.TextChanged += OnTextChanged;
            this.KeyDown += OnKeyDown;
            this.LostFocus += (s, e) => HideListBox();
        }

        public void SetAutoCompleteItems(IEnumerable<string> source)
        {
            items = source.ToList();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            parentForm = this.FindForm();
            if (parentForm != null)
                parentForm.Controls.Add(listBox);
        }

        private void OnTextChanged(object sender, EventArgs e)
        {
            string text = this.Text;

            if (string.IsNullOrWhiteSpace(text))
            {
                HideListBox();
                return;
            }

            var startsWith = items
                .Where(x => x.StartsWith(text, StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x);

            var contains = items
                .Where(x => !x.StartsWith(text, StringComparison.OrdinalIgnoreCase) &&
                            x.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                .OrderBy(x => x);

            var matches = startsWith.Concat(contains).ToList();

            if (matches.Count == 0)
            {
                HideListBox();
                return;
            }

            ShowListBox(matches);
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (!listBox.Visible) return;

            if (e.KeyCode == Keys.Down)
            {
                if (listBox.SelectedIndex < listBox.Items.Count - 1)
                    listBox.SelectedIndex++;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                if (listBox.SelectedIndex > 0)
                    listBox.SelectedIndex--;
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ChooseItem();
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Escape)
            {
                HideListBox();
                e.Handled = true;
            }
        }

        private void ShowListBox(List<string> matches)
        {
            listBox.BeginUpdate();
            listBox.Items.Clear();
            foreach (var item in matches) listBox.Items.Add(item);
            if (listBox.Items.Count > 0)
                listBox.SelectedIndex = 0;
            listBox.EndUpdate();

            var point = this.Parent.PointToScreen(this.Location);
            point = parentForm.PointToClient(point);

            listBox.Location = new Point(point.X, point.Y + this.Height);
            listBox.Width = this.Width;

            int itemCount = matches.Count;
            int maxVisibleItems = 8;
            int height = Math.Min(itemCount, maxVisibleItems) * listBox.ItemHeight + listBox.ItemHeight;
            if (height < listBox.ItemHeight)
                height = listBox.ItemHeight;

            listBox.Height = height + 2;

            listBox.Visible = true;
            listBox.BringToFront();
        }

        private void HideListBox()
        {
            listBox.Visible = false;
        }

        private void ChooseItem()
        {
            if (listBox.SelectedItem != null)
            {
                this.Text = listBox.SelectedItem.ToString();
                this.SelectionStart = this.Text.Length;
            }
            HideListBox();
        }

        private void ChooseItem(Point clickPoint)
        {
            int idx = listBox.IndexFromPoint(clickPoint);
            if (idx >= 0)
            {
                listBox.SelectedIndex = idx;
                this.Text = listBox.SelectedItem.ToString();
                this.SelectionStart = this.Text.Length;
            }
            HideListBox();
        }
    }
}