namespace windowsController
{
    public partial class Form1 : Form
    {
        private bool isEnabled;
        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Icon = System.Drawing.SystemIcons.Application;
            notifyIcon1.Visible = true;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated)
            {
                CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void enableControlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;
            menuItem.Checked = !menuItem.Checked;
            isEnabled = menuItem.Checked;
            if (isEnabled)
            {

            }
        }


    }
}