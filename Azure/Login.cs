// Decompiled with JetBrains decompiler
// Type: KeyAuth.Login
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
using System.Net;
using System.Windows.Forms;


namespace KeyAuth
{
  public class Login : Form
  {
    public static api KeyAuthApp = new api("Azure", "fEVDe3NIC0", "39ebe670408bf662a265e4b36a23c446d67339555337eb311a5ceedb59ea9fea", "1.1.5");
    private System.Windows.Forms.Button btnChangeLanguage;
    private IContainer components;
    private SiticoneDragControl siticoneDragControl1;
    private SiticoneControlBox siticoneControlBox1;
    private SiticoneControlBox siticoneControlBox2;
    private Label label1;
    private SiticoneShadowForm siticoneShadowForm;
    private SiticoneRoundedTextBox password;
    private System.Windows.Forms.PictureBox pictureBox1;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private SiticoneRoundedTextBox username;
    private System.Windows.Forms.PictureBox pictureBox2;
    private Label label8;
    private Label label3;
    private System.Windows.Forms.Button button1;
    private SiticoneRoundedButton LoginBtn;
    private CheckBox checkBox1;
    private Label label9;
    private Label label10;
    private Label label11;
    private ImageList imageList1;
    private Label label12;
    private SiticoneLabel siticoneLabel1;
    private Label label13;
    private SiticoneLabel siticoneLabel23;
    private SiticoneRoundedButton siticoneRoundedButton3;
    private SiticoneLabel status;
    private Label label2;
    private SiticoneLabel siticoneLabel11;
    private SiticoneLabel siticoneLabel12;
    private SiticoneLabel siticoneLabel13;
    private SiticoneLabel siticoneLabel14;
    private SiticoneLabel siticoneLabel15;
    private SiticoneLabel siticoneLabel16;
    private SiticoneLabel siticoneLabel17;
    private SiticoneLabel siticoneLabel24;
    private SiticoneLabel siticoneLabel25;
    private SiticoneLabel siticoneLabel26;
    private SiticoneLabel siticoneLabel27;
    private SiticoneLabel siticoneLabel2;
    private SiticoneLabel siticoneLabel3;
    private SiticoneLabel siticoneLabel4;
    private SiticoneLabel siticoneLabel5;
    private SiticoneLabel siticoneLabel6;
    private SiticoneLabel siticoneLabel7;

    private void ShowResponse(string type)
    {
      int num = (int) MessageBox.Show(string.Format("It took {0} msg to {1}", (object) api.responseTime, (object) type));
    }

    public Login()
    {
      this.InitializeComponent();
      this.RedondearBordes((Form) this, 10);
      this.username.Enter += new EventHandler(this.username_Enter);
      this.username.Leave += new EventHandler(this.username_Leave);
      this.password.Enter += new EventHandler(this.password_Enter);
      this.password.Leave += new EventHandler(this.password_Leave);
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

    private void siticoneControlBox1_Click(object sender, EventArgs e) => Environment.Exit(0);

    public static bool SubExist(string name)
    {
      return Login.KeyAuthApp.user_data.subscriptions.Exists((Predicate<api.Data>) (x => x.subscription == name));
    }

    private void Login_Load(object sender, EventArgs e)
    {
      Login.KeyAuthApp.init();
      if (Login.KeyAuthApp.response.message == "invalidver")
      {
        if (!string.IsNullOrEmpty(Login.KeyAuthApp.app_data.downloadLink))
        {
          switch (MessageBox.Show("Dale a SI y descarga la nueva actualización\n", "NUEVA ACTUALIZACIÓN", MessageBoxButtons.YesNo))
          {
            case DialogResult.Yes:
              Process.Start(Login.KeyAuthApp.app_data.downloadLink);
              Environment.Exit(0);
              break;
            case DialogResult.No:
              WebClient webClient = new WebClient();
              string fileName1 = Application.ExecutablePath.Replace(".exe", "-" + Login.random_string() + ".exe");
              string downloadLink = Login.KeyAuthApp.app_data.downloadLink;
              string fileName2 = fileName1;
              webClient.DownloadFile(downloadLink, fileName2);
              Process.Start(fileName1);
              Process.Start(new ProcessStartInfo()
              {
                Arguments = "/C choice /C Y /N /D Y /T 3 & Del \"" + Application.ExecutablePath + "\"",
                WindowStyle = ProcessWindowStyle.Hidden,
                CreateNoWindow = true,
                FileName = "cmd.exe"
              });
              Environment.Exit(0);
              break;
            default:
              int num1 = (int) MessageBox.Show("Invalid option");
              Environment.Exit(0);
              break;
          }
        }
        int num2 = (int) MessageBox.Show("La versión de este programa no coincide con la que está en línea. Además, el enlace de descarga en línea no está configurado. Deberá obtener manualmente el enlace de descarga del desarrollador.");
        Environment.Exit(0);
      }
      if (Login.KeyAuthApp.response.success)
        return;
      int num = (int) MessageBox.Show(Login.KeyAuthApp.response.message);
      Environment.Exit(0);
    }

    private static string random_string()
    {
      string str = (string) null;
      Random random = new Random();
      for (int index = 0; index < 5; ++index)
        str += Convert.ToChar(Convert.ToInt32(Math.Floor(26.0 * random.NextDouble() + 65.0))).ToString();
      return str;
    }

    private void UpgradeBtn_Click(object sender, EventArgs e)
    {
      this.status.Text = Login.KeyAuthApp.response.message ?? "";
    }

    private void LoginBtn_Click(object sender, EventArgs e)
    {
      Login.KeyAuthApp.login(this.username.Text, this.password.Text);
      if (Login.KeyAuthApp.response.success)
      {
        new Main().Show();
        this.Hide();
      }
      else
        this.status.Text = Login.KeyAuthApp.response.message ?? "";
    }

    private void username_Enter(object sender, EventArgs e)
    {
      if (!(this.username.Text == "Username") && !(this.username.Text == "Nombre de usuario"))
        return;
      this.username.Text = "";
      this.username.ForeColor = Color.White;
    }

    private void username_Leave(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(this.username.Text))
        return;
      this.username.Text = this.btnChangeLanguage.Text == "Español" ? "Nombre de usuario" : "Username";
      this.username.ForeColor = Color.Gray;
    }

    private void status_Click(object sender, EventArgs e)
    {
    }

    private void email_TextChanged(object sender, EventArgs e)
    {
    }

    private void label3_Click(object sender, EventArgs e)
    {
    }

    private void siticoneRoundedButton2_Click(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
    }

    private void pictureBox2_Click(object sender, EventArgs e)
    {
      Process.Start("https://discord.gg/robmodzz");
    }

    private void pictureBox3_Click(object sender, EventArgs e)
    {
      Process.Start("https://azure-menu.com");
    }

    private void username_TextChanged(object sender, EventArgs e)
    {
    }

    private void siticoneRoundedButton2_Click_1(object sender, EventArgs e)
    {
      string address = "https://github.com/GitHubR0B/LauncherMinus/blob/main/README.md";
      string version2 = "1.0.5";
      try
      {
        using (WebClient webClient = new WebClient())
        {
          string str1 = webClient.DownloadString(address);
          string str2 = (string) null;
          char[] chArray = new char[1]{ '\n' };
          foreach (string str3 in str1.Split(chArray))
          {
            if (str3.Contains("href") && str3.Contains(".exe"))
            {
              str2 = str3.Split('"')[1];
              break;
            }
          }
          string[] strArray = str2.Split('/');
          if (this.CompareVersions(strArray[strArray.Length - 1].Split('-')[1], version2) > 0)
          {
            int num1 = (int) MessageBox.Show("¡Hay una nueva versión disponible!");
          }
          else
          {
            int num2 = (int) MessageBox.Show("No hay una nueva versión disponible.");
          }
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Error: " + ex.Message);
      }
    }

    private int CompareVersions(string version1, string version2)
    {
      string[] strArray1 = version1.Split('.');
      string[] strArray2 = version2.Split('.');
      for (int index = 0; index < Math.Min(strArray1.Length, strArray2.Length); ++index)
      {
        int num1 = int.Parse(strArray1[index]);
        int num2 = int.Parse(strArray2[index]);
        if (num1 < num2)
          return -1;
        if (num1 > num2)
          return 1;
      }
      if (strArray1.Length < strArray2.Length)
        return -1;
      return strArray1.Length > strArray2.Length ? 1 : 0;
    }

    private void label4_Click(object sender, EventArgs e)
    {
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void btnChangeLanguage_Click(object sender, EventArgs e)
    {
      if (this.siticoneRoundedButton3.Text == "ES")
        this.siticoneRoundedButton3.Text = "EN";
      else if (this.siticoneRoundedButton3.Text == "EN")
        this.siticoneRoundedButton3.Text = "ES";
      if (this.btnChangeLanguage.Text == "")
      {
        this.username.Text = "Nombre de usuario";
        this.password.Text = "Contraseña";
        this.LoginBtn.Text = "Iniciar sesión";
        this.btnChangeLanguage.Text = "Inglés";
        this.status.Text = "Idioma cambiado a español.";
        this.label4.Text = "INICIO DE SESIÓN";
        this.CentrarTexto(this.label4);
      }
      else
      {
        this.username.Text = "Username";
        this.password.Text = "Password";
        this.LoginBtn.Text = "Sign in";
        this.btnChangeLanguage.Text = "";
        this.status.Text = "Language changed to English.";
        this.label4.Text = "SIGN IN";
        this.CentrarTexto(this.label4);
      }
    }

    private void password_Enter(object sender, EventArgs e)
    {
      if (!(this.password.Text == "Password") && !(this.password.Text == "Contraseña"))
        return;
      this.password.Text = "";
      this.password.UseSystemPasswordChar = true;
      this.password.ForeColor = Color.White;
    }

    private void password_Leave(object sender, EventArgs e)
    {
      if (!string.IsNullOrWhiteSpace(this.password.Text))
        return;
      this.password.Text = this.btnChangeLanguage.Text == "Español" ? "Contraseña" : "Password";
      this.password.UseSystemPasswordChar = false;
      this.password.ForeColor = Color.Gray;
    }

    private void CentrarTexto(Label label)
    {
      int x = (this.ClientSize.Width - label.Width) / 2;
      label.Location = new Point(x, label.Location.Y);
    }

    private void label4_Click_1(object sender, EventArgs e)
    {
    }

    private void siticoneRoundedButton5_Click(object sender, EventArgs e)
    {
    }

    private void siticoneRoundedButton1_Click(object sender, EventArgs e)
    {
    }

    private void siticoneRoundedButton4_Click(object sender, EventArgs e)
    {
    }

    private void button1_Click(object sender, EventArgs e)
    {
    }

    private void label5_Click(object sender, EventArgs e)
    {
    }

    private void label6_Click(object sender, EventArgs e)
    {
    }

    private void label7_Click(object sender, EventArgs e)
    {
    }

    private void label8_Click(object sender, EventArgs e)
    {
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      //Login.KeyAuthApp.login(this.username.Text, this.password.Text);
      //if (Login.KeyAuthApp.response.success)
      //{
        new Main().Show();
        this.Hide();
      //}
      //else
      //  this.status.Text = Login.KeyAuthApp.response.message ?? "";
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
    }

    private void label11_Click(object sender, EventArgs e)
    {
    }

    private void label12_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel2_Click(object sender, EventArgs e)
    {
    }

    private void label13_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel23_Click(object sender, EventArgs e)
    {
    }

    private void label3_Click_1(object sender, EventArgs e)
    {
    }

    private void siticoneLabel12_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel13_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel14_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel15_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel16_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel17_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel24_Click(object sender, EventArgs e)
    {
    }

    private void siticoneLabel11_Click(object sender, EventArgs e)
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (Login));
      this.siticoneDragControl1 = new SiticoneDragControl(this.components);
      this.siticoneControlBox1 = new SiticoneControlBox();
      this.siticoneControlBox2 = new SiticoneControlBox();
      this.label1 = new Label();
      this.username = new SiticoneRoundedTextBox();
      this.password = new SiticoneRoundedTextBox();
      this.status = new SiticoneLabel();
      this.siticoneShadowForm = new SiticoneShadowForm(this.components);
      this.pictureBox1 = new System.Windows.Forms.PictureBox();
      this.label4 = new Label();
      this.label5 = new Label();
      this.label6 = new Label();
      this.label7 = new Label();
      this.pictureBox2 = new System.Windows.Forms.PictureBox();
      this.label8 = new Label();
      this.label3 = new Label();
      this.button1 = new System.Windows.Forms.Button();
      this.LoginBtn = new SiticoneRoundedButton();
      this.checkBox1 = new CheckBox();
      this.label9 = new Label();
      this.label10 = new Label();
      this.label11 = new Label();
      this.imageList1 = new ImageList(this.components);
      this.label12 = new Label();
      this.siticoneLabel1 = new SiticoneLabel();
      this.label13 = new Label();
      this.siticoneLabel23 = new SiticoneLabel();
      this.siticoneRoundedButton3 = new SiticoneRoundedButton();
      this.label2 = new Label();
      this.siticoneLabel11 = new SiticoneLabel();
      this.siticoneLabel12 = new SiticoneLabel();
      this.siticoneLabel13 = new SiticoneLabel();
      this.siticoneLabel14 = new SiticoneLabel();
      this.siticoneLabel15 = new SiticoneLabel();
      this.siticoneLabel16 = new SiticoneLabel();
      this.siticoneLabel17 = new SiticoneLabel();
      this.siticoneLabel24 = new SiticoneLabel();
      this.siticoneLabel25 = new SiticoneLabel();
      this.siticoneLabel26 = new SiticoneLabel();
      this.siticoneLabel27 = new SiticoneLabel();
      this.siticoneLabel2 = new SiticoneLabel();
      this.siticoneLabel3 = new SiticoneLabel();
      this.siticoneLabel4 = new SiticoneLabel();
      this.siticoneLabel5 = new SiticoneLabel();
      this.siticoneLabel6 = new SiticoneLabel();
      this.siticoneLabel7 = new SiticoneLabel();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      ((ISupportInitialize) this.pictureBox2).BeginInit();
      this.SuspendLayout();
      this.siticoneDragControl1.TargetControl = (Control) this;
      this.siticoneControlBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
      this.siticoneControlBox1.BackColor = Color.Transparent;
      this.siticoneControlBox1.FillColor = Color.Transparent;
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
      this.siticoneControlBox2.BackColor = Color.Transparent;
      this.siticoneControlBox2.ControlBoxType = ControlBoxType.MinimizeBox;
      this.siticoneControlBox2.FillColor = Color.Transparent;
      this.siticoneControlBox2.HoveredState.Parent = (Siticone.UI.WinForms.Suite.ControlBox) this.siticoneControlBox2;
      this.siticoneControlBox2.IconColor = Color.White;
      this.siticoneControlBox2.Location = new Point(622, 4);
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
      this.username.AllowDrop = true;
      this.username.BackColor = Color.Transparent;
      this.username.BorderColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.username.BorderThickness = 0;
      this.username.Cursor = Cursors.IBeam;
      this.username.DefaultText = "Username";
      this.username.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.username.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.username.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.username.DisabledState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.username;
      this.username.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.username.FillColor = Color.FromArgb(64, 64, 64);
      this.username.FocusedState.BorderColor = Color.White;
      this.username.FocusedState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.username;
      this.username.ForeColor = Color.LightGray;
      this.username.HoveredState.BorderColor = Color.White;
      this.username.HoveredState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.username;
      this.username.Location = new Point(44, 189);
      this.username.Margin = new Padding(4);
      this.username.Name = "username";
      this.username.PasswordChar = char.MinValue;
      this.username.PlaceholderText = "";
      this.username.SelectedText = "";
      this.username.ShadowDecoration.Parent = (Control) this.username;
      this.username.Size = new Size(236, 41);
      this.username.TabIndex = 33;
      this.username.TextAlign = HorizontalAlignment.Center;
      this.username.TextChanged += new EventHandler(this.username_TextChanged);
      this.password.AllowDrop = true;
      this.password.BackColor = Color.Transparent;
      this.password.BorderColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.password.BorderThickness = 0;
      this.password.Cursor = Cursors.IBeam;
      this.password.DefaultText = "Password";
      this.password.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
      this.password.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
      this.password.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
      this.password.DisabledState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.password;
      this.password.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
      this.password.FillColor = Color.FromArgb(64, 64, 64);
      this.password.FocusedState.BorderColor = Color.White;
      this.password.FocusedState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.password;
      this.password.ForeColor = Color.LightGray;
      this.password.HoveredState.BorderColor = Color.White;
      this.password.HoveredState.Parent = (Siticone.UI.WinForms.Suite.TextBox) this.password;
      this.password.Location = new Point(44, 238);
      this.password.Margin = new Padding(4);
      this.password.Name = "password";
      this.password.PasswordChar = char.MinValue;
      this.password.PlaceholderText = "";
      this.password.SelectedText = "";
      this.password.ShadowDecoration.Parent = (Control) this.password;
      this.password.Size = new Size(236, 37);
      this.password.TabIndex = 34;
      this.password.TextAlign = HorizontalAlignment.Center;
      this.status.AutoSize = false;
      this.status.BackColor = Color.Transparent;
      this.status.Dock = DockStyle.Bottom;
      this.status.Font = new Font("Segoe UI", 9f, FontStyle.Bold);
      this.status.ForeColor = SystemColors.ButtonHighlight;
      this.status.Location = new Point(0, 371);
      this.status.Margin = new Padding(50, 0, 50, 0);
      this.status.Name = "status";
      this.status.Size = new Size(717, 53);
      this.status.TabIndex = 38;
      this.status.Text = (string) null;
      this.status.TextAlignment = ContentAlignment.TopCenter;
      this.status.Click += new EventHandler(this.status_Click);
      this.pictureBox1.BackColor = Color.Transparent;
      this.pictureBox1.Image = (Image) componentResourceManager.GetObject("pictureBox1.Image");
      this.pictureBox1.Location = new Point(77, -19);
      this.pictureBox1.MaximumSize = new Size(170, 180);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(170, 100);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 43;
      this.pictureBox1.TabStop = false;
      this.pictureBox1.Click += new EventHandler(this.pictureBox1_Click);
      this.label4.AutoSize = true;
      this.label4.BackColor = Color.Transparent;
      this.label4.Font = new Font("Arial Black", 22.25f, FontStyle.Bold);
      this.label4.ForeColor = Color.White;
      this.label4.Location = new Point(86, 73);
      this.label4.Name = "label4";
      this.label4.Size = new Size(102, 42);
      this.label4.TabIndex = 49;
      this.label4.Text = "SIGN";
      this.label4.Click += new EventHandler(this.label4_Click_1);
      this.label5.AutoSize = true;
      this.label5.BackColor = Color.Transparent;
      this.label5.Font = new Font("Calibri", 12.25f);
      this.label5.ForeColor = Color.White;
      this.label5.Location = new Point(54, 142);
      this.label5.Name = "label5";
      this.label5.Size = new Size(74, 21);
      this.label5.TabIndex = 50;
      this.label5.Text = "Welcome";
      this.label5.Click += new EventHandler(this.label5_Click);
      this.label6.AutoSize = true;
      this.label6.BackColor = Color.Transparent;
      this.label6.Font = new Font("Calibri", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label6.ForeColor = Color.White;
      this.label6.Location = new Point(54, 158);
      this.label6.Name = "label6";
      this.label6.Size = new Size(80, 19);
      this.label6.TabIndex = 51;
      this.label6.Text = "Log in and";
      this.label6.Click += new EventHandler(this.label6_Click);
      this.label7.AutoSize = true;
      this.label7.BackColor = Color.Transparent;
      this.label7.Font = new Font("Calibri", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label7.ForeColor = Color.FromArgb(208, 161, 96);
      this.label7.Location = new Point(129, 158);
      this.label7.Name = "label7";
      this.label7.Size = new Size(47, 19);
      this.label7.TabIndex = 52;
      this.label7.Text = "enjoy";
      this.label7.Click += new EventHandler(this.label7_Click);
      this.pictureBox2.BackColor = Color.Transparent;
      this.pictureBox2.Image = (Image) componentResourceManager.GetObject("pictureBox2.Image");
      this.pictureBox2.Location = new Point(668, 397);
      this.pictureBox2.MaximumSize = new Size(170, 180);
      this.pictureBox2.Name = "pictureBox2";
      this.pictureBox2.Size = new Size(62, 24);
      this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox2.TabIndex = 53;
      this.pictureBox2.TabStop = false;
      this.label8.AutoSize = true;
      this.label8.BackColor = Color.Transparent;
      this.label8.Font = new Font("Calibri", 9.25f);
      this.label8.ForeColor = Color.White;
      this.label8.Location = new Point(330, 0);
      this.label8.Name = "label8";
      this.label8.Size = new Size(37, 15);
      this.label8.TabIndex = 54;
      this.label8.Text = "Azure Cracked";
      this.label8.Click += new EventHandler(this.label8_Click);
      this.label3.AutoSize = true;
      this.label3.BackColor = Color.Transparent;
      this.label3.Font = new Font("Calibri", 9.25f);
      this.label3.ForeColor = Color.FromArgb(208, 161, 96);
      this.label3.Location = new Point(363, 0);
      this.label3.Name = "label3";
      this.label3.Size = new Size(34, 15);
      this.label3.TabIndex = 55;
      this.label3.Text = "1.1.3";
      this.label3.Click += new EventHandler(this.label3_Click_1);
      this.button1.BackColor = Color.FromArgb(208, 161, 96);
      this.button1.BackgroundImageLayout = ImageLayout.None;
      this.button1.Cursor = Cursors.Default;
      this.button1.FlatAppearance.BorderColor = Color.FromArgb(208, 161, 96);
      this.button1.FlatAppearance.BorderSize = 5;
      this.button1.FlatAppearance.MouseDownBackColor = Color.FromArgb(208, 161, 96);
      this.button1.FlatAppearance.MouseOverBackColor = Color.FromArgb(208, 161, 96);
      this.button1.Font = new Font("Calibri", 18f, FontStyle.Bold);
      this.button1.ForeColor = Color.White;
      this.button1.Location = new Point(44, 331);
      this.button1.Name = "button1";
      this.button1.RightToLeft = RightToLeft.No;
      this.button1.Size = new Size(236, 42);
      this.button1.TabIndex = 56;
      this.button1.Text = "SIGN IN";
      this.button1.UseVisualStyleBackColor = false;
      this.button1.Click += new EventHandler(this.button1_Click_1);
      this.LoginBtn.BackColor = Color.Transparent;
      this.LoginBtn.BorderColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.LoginBtn.CheckedState.Parent = (CustomButtonBase) this.LoginBtn;
      this.LoginBtn.CustomBorderColor = Color.FromArgb(208, 161, 96);
      this.LoginBtn.CustomImages.Parent = (CustomButtonBase) this.LoginBtn;
      this.LoginBtn.FillColor = Color.FromArgb(208, 161, 96);
      this.LoginBtn.Font = new Font("Calibri", 18f, FontStyle.Bold);
      this.LoginBtn.ForeColor = Color.White;
      this.LoginBtn.HoveredState.BorderColor = Color.FromArgb(213, 218, 223);
      this.LoginBtn.HoveredState.Parent = (CustomButtonBase) this.LoginBtn;
      this.LoginBtn.Location = new Point(44, 331);
      this.LoginBtn.Name = "LoginBtn";
      this.LoginBtn.ShadowDecoration.Parent = (Control) this.LoginBtn;
      this.LoginBtn.Size = new Size(236, 42);
      this.LoginBtn.TabIndex = 26;
      this.LoginBtn.Text = "SIGN IN";
      this.LoginBtn.Visible = false;
      this.LoginBtn.Click += new EventHandler(this.LoginBtn_Click);
      this.checkBox1.Appearance = Appearance.Button;
      this.checkBox1.CheckAlign = ContentAlignment.TopLeft;
      this.checkBox1.Cursor = Cursors.Default;
      this.checkBox1.ForeColor = Color.Transparent;
      this.checkBox1.Location = new Point(47, 295);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(22, 21);
      this.checkBox1.TabIndex = 57;
      this.checkBox1.UseVisualStyleBackColor = true;
      this.checkBox1.CheckedChanged += new EventHandler(this.checkBox1_CheckedChanged);
      this.label9.AutoSize = true;
      this.label9.BackColor = Color.Transparent;
      this.label9.Font = new Font("Calibri", 12.25f, FontStyle.Bold);
      this.label9.ForeColor = Color.White;
      this.label9.Location = new Point(73, 295);
      this.label9.Name = "label9";
      this.label9.Size = new Size(80, 21);
      this.label9.TabIndex = 58;
      this.label9.Text = "Save Data";
      this.label10.AutoSize = true;
      this.label10.BackColor = Color.Transparent;
      this.label10.Font = new Font("Calibri", 12f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label10.ForeColor = Color.FromArgb(208, 161, 96);
      this.label10.Location = new Point(43, 386);
      this.label10.Name = "label10";
      this.label10.Size = new Size(122, 19);
      this.label10.TabIndex = 59;
      this.label10.Text = "Password Reset?";
      this.label11.AutoSize = true;
      this.label11.BackColor = Color.Transparent;
      this.label11.Font = new Font("Arial Black", 22.25f, FontStyle.Bold);
      this.label11.ForeColor = Color.FromArgb(208, 161, 96);
      this.label11.Location = new Point(180, 73);
      this.label11.Name = "label11";
      this.label11.Size = new Size(55, 42);
      this.label11.TabIndex = 60;
      this.label11.Text = "IN";
      this.label11.Click += new EventHandler(this.label11_Click);
      this.imageList1.ColorDepth = ColorDepth.Depth8Bit;
      this.imageList1.ImageSize = new Size(16, 16);
      this.imageList1.TransparentColor = Color.Transparent;
      this.label12.AutoSize = true;
      this.label12.BackColor = Color.Transparent;
      this.label12.Cursor = Cursors.Default;
      this.label12.Font = new Font("Calibri", 12f);
      this.label12.ForeColor = Color.Silver;
      this.label12.Image = (Image) componentResourceManager.GetObject("label12.Image");
      this.label12.Location = new Point(329, 235);
      this.label12.Name = "label12";
      this.label12.Size = new Size(337, 19);
      this.label12.TabIndex = 61;
      this.label12.Text = "_________________________________________";
      this.label12.Click += new EventHandler(this.label12_Click);
      this.siticoneLabel1.BackColor = Color.Transparent;
      this.siticoneLabel1.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel1.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel1.Location = new Point(332, 235);
      this.siticoneLabel1.Margin = new Padding(2);
      this.siticoneLabel1.Name = "siticoneLabel1";
      this.siticoneLabel1.Size = new Size(75, 15);
      this.siticoneLabel1.TabIndex = 62;
      this.siticoneLabel1.Text = "⁞ Version 1.1.2";
      this.label13.AutoSize = true;
      this.label13.BackColor = Color.Transparent;
      this.label13.Cursor = Cursors.Default;
      this.label13.Font = new Font("Calibri", 12f);
      this.label13.ForeColor = Color.Silver;
      this.label13.Location = new Point(329, 38);
      this.label13.Name = "label13";
      this.label13.Size = new Size(337, 19);
      this.label13.TabIndex = 63;
      this.label13.Text = "_________________________________________";
      this.label13.Click += new EventHandler(this.label13_Click);
      this.siticoneLabel23.BackColor = Color.Transparent;
      this.siticoneLabel23.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel23.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel23.Location = new Point(335, 38);
      this.siticoneLabel23.Margin = new Padding(2);
      this.siticoneLabel23.Name = "siticoneLabel23";
      this.siticoneLabel23.Size = new Size(75, 15);
      this.siticoneLabel23.TabIndex = 90;
      this.siticoneLabel23.Text = "⁞ Version 1.1.3";
      this.siticoneLabel23.Click += new EventHandler(this.siticoneLabel23_Click);
      this.siticoneRoundedButton3.BackColor = Color.Transparent;
      this.siticoneRoundedButton3.BorderColor = Color.FromArgb((int) byte.MaxValue, 224, 192);
      this.siticoneRoundedButton3.CheckedState.Parent = (CustomButtonBase) this.siticoneRoundedButton3;
      this.siticoneRoundedButton3.CustomImages.Parent = (CustomButtonBase) this.siticoneRoundedButton3;
      this.siticoneRoundedButton3.Enabled = false;
      this.siticoneRoundedButton3.FillColor = Color.FromArgb(39, 42, 50);
      this.siticoneRoundedButton3.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.siticoneRoundedButton3.ForeColor = Color.White;
      this.siticoneRoundedButton3.HoveredState.BorderColor = Color.FromArgb(213, 218, 223);
      this.siticoneRoundedButton3.HoveredState.Parent = (CustomButtonBase) this.siticoneRoundedButton3;
      this.siticoneRoundedButton3.Location = new Point(204, 19);
      this.siticoneRoundedButton3.Name = "siticoneRoundedButton3";
      this.siticoneRoundedButton3.ShadowDecoration.Parent = (Control) this.siticoneRoundedButton3;
      this.siticoneRoundedButton3.Size = new Size(43, 23);
      this.siticoneRoundedButton3.TabIndex = 48;
      this.siticoneRoundedButton3.Text = "ES";
      this.siticoneRoundedButton3.Visible = false;
      this.siticoneRoundedButton3.Click += new EventHandler(this.btnChangeLanguage_Click);
      this.label2.AutoSize = true;
      this.label2.BackColor = Color.Transparent;
      this.label2.Font = new Font("Calibri", 7.25f);
      this.label2.ForeColor = Color.White;
      this.label2.Location = new Point(639, 407);
      this.label2.Name = "label2";
      this.label2.Size = new Size(50, 13);
      this.label2.TabIndex = 103;
      this.label2.Text = "RobModZz";
      this.siticoneLabel11.BackColor = Color.Transparent;
      this.siticoneLabel11.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel11.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel11.Location = new Point(347, 211);
      this.siticoneLabel11.Margin = new Padding(2);
      this.siticoneLabel11.Name = "siticoneLabel11";
      this.siticoneLabel11.Size = new Size(128, 15);
      this.siticoneLabel11.TabIndex = 158;
      this.siticoneLabel11.Text = "• Heist Editor - Recovery";
      this.siticoneLabel11.Click += new EventHandler(this.siticoneLabel11_Click);
      this.siticoneLabel12.BackColor = Color.Transparent;
      this.siticoneLabel12.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel12.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel12.Location = new Point(346, 181);
      this.siticoneLabel12.Margin = new Padding(2);
      this.siticoneLabel12.Name = "siticoneLabel12";
      this.siticoneLabel12.Size = new Size(218, 15);
      this.siticoneLabel12.TabIndex = 157;
      this.siticoneLabel12.Text = "• LS Car Meet Award - Recovery - Unlocks";
      this.siticoneLabel12.Click += new EventHandler(this.siticoneLabel12_Click);
      this.siticoneLabel13.BackColor = Color.Transparent;
      this.siticoneLabel13.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel13.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel13.Location = new Point(346, 196);
      this.siticoneLabel13.Margin = new Padding(2);
      this.siticoneLabel13.Name = "siticoneLabel13";
      this.siticoneLabel13.Size = new Size(250, 15);
      this.siticoneLabel13.TabIndex = 156;
      this.siticoneLabel13.Text = "• Arms trafficking missions - Recovery - Unlocks";
      this.siticoneLabel13.Click += new EventHandler(this.siticoneLabel13_Click);
      this.siticoneLabel14.BackColor = Color.Transparent;
      this.siticoneLabel14.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel14.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel14.Location = new Point(346, 166);
      this.siticoneLabel14.Margin = new Padding(2);
      this.siticoneLabel14.Name = "siticoneLabel14";
      this.siticoneLabel14.Size = new Size(197, 15);
      this.siticoneLabel14.TabIndex = 155;
      this.siticoneLabel14.Text = "• Unlock Clothes - Recovery - Unlocks";
      this.siticoneLabel14.Click += new EventHandler(this.siticoneLabel14_Click);
      this.siticoneLabel15.BackColor = Color.Transparent;
      this.siticoneLabel15.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel15.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel15.Location = new Point(346, 137);
      this.siticoneLabel15.Margin = new Padding(2);
      this.siticoneLabel15.Name = "siticoneLabel15";
      this.siticoneLabel15.Size = new Size(187, 15);
      this.siticoneLabel15.TabIndex = 154;
      this.siticoneLabel15.Text = "• Clear Reports - Recovery - General";
      this.siticoneLabel15.Click += new EventHandler(this.siticoneLabel15_Click);
      this.siticoneLabel16.BackColor = Color.Transparent;
      this.siticoneLabel16.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel16.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel16.Location = new Point(346, 152);
      this.siticoneLabel16.Margin = new Padding(2);
      this.siticoneLabel16.Name = "siticoneLabel16";
      this.siticoneLabel16.Size = new Size(251, 15);
      this.siticoneLabel16.TabIndex = 153;
      this.siticoneLabel16.Text = "• Change Character Gender - Recovery - General";
      this.siticoneLabel16.Click += new EventHandler(this.siticoneLabel16_Click);
      this.siticoneLabel17.BackColor = Color.Transparent;
      this.siticoneLabel17.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel17.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel17.Location = new Point(346, 122);
      this.siticoneLabel17.Margin = new Padding(2);
      this.siticoneLabel17.Name = "siticoneLabel17";
      this.siticoneLabel17.Size = new Size(198, 15);
      this.siticoneLabel17.TabIndex = 152;
      this.siticoneLabel17.Text = "• Speedometer KM - Vehicle - General";
      this.siticoneLabel17.Click += new EventHandler(this.siticoneLabel17_Click);
      this.siticoneLabel24.BackColor = Color.Transparent;
      this.siticoneLabel24.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel24.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel24.Location = new Point(346, 106);
      this.siticoneLabel24.Margin = new Padding(2);
      this.siticoneLabel24.Name = "siticoneLabel24";
      this.siticoneLabel24.Size = new Size(200, 15);
      this.siticoneLabel24.TabIndex = 150;
      this.siticoneLabel24.Text = "• Bring Personal Vehicle abroad to me";
      this.siticoneLabel24.Click += new EventHandler(this.siticoneLabel24_Click);
      this.siticoneLabel25.BackColor = Color.Transparent;
      this.siticoneLabel25.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel25.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel25.Location = new Point(346, 91);
      this.siticoneLabel25.Margin = new Padding(2);
      this.siticoneLabel25.Name = "siticoneLabel25";
      this.siticoneLabel25.Size = new Size(155, 15);
      this.siticoneLabel25.TabIndex = 149;
      this.siticoneLabel25.Text = "• Parachute - Player - General";
      this.siticoneLabel26.BackColor = Color.Transparent;
      this.siticoneLabel26.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel26.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel26.Location = new Point(346, 76);
      this.siticoneLabel26.Margin = new Padding(2);
      this.siticoneLabel26.Name = "siticoneLabel26";
      this.siticoneLabel26.Size = new Size(197, 15);
      this.siticoneLabel26.TabIndex = 148;
      this.siticoneLabel26.Text = "• Swim in the air - Player - Movement";
      this.siticoneLabel27.BackColor = Color.Transparent;
      this.siticoneLabel27.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel27.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel27.Location = new Point(340, 58);
      this.siticoneLabel27.Margin = new Padding(2);
      this.siticoneLabel27.Name = "siticoneLabel27";
      this.siticoneLabel27.Size = new Size(56, 15);
      this.siticoneLabel27.TabIndex = 147;
      this.siticoneLabel27.Text = "➦ Added:";
      this.siticoneLabel2.BackColor = Color.Transparent;
      this.siticoneLabel2.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel2.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel2.Location = new Point(340, 345);
      this.siticoneLabel2.Margin = new Padding(2);
      this.siticoneLabel2.Name = "siticoneLabel2";
      this.siticoneLabel2.Size = new Size(150, 15);
      this.siticoneLabel2.TabIndex = 165;
      this.siticoneLabel2.Text = "• Infinite charge upon death";
      this.siticoneLabel3.BackColor = Color.Transparent;
      this.siticoneLabel3.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel3.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel3.Location = new Point(340, 330);
      this.siticoneLabel3.Margin = new Padding(2);
      this.siticoneLabel3.Name = "siticoneLabel3";
      this.siticoneLabel3.Size = new Size(132, 15);
      this.siticoneLabel3.TabIndex = 164;
      this.siticoneLabel3.Text = "• Map Error Infinite Load";
      this.siticoneLabel4.BackColor = Color.Transparent;
      this.siticoneLabel4.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel4.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel4.Location = new Point(334, 312);
      this.siticoneLabel4.Margin = new Padding(2);
      this.siticoneLabel4.Name = "siticoneLabel4";
      this.siticoneLabel4.Size = new Size(47, 15);
      this.siticoneLabel4.TabIndex = 163;
      this.siticoneLabel4.Text = "➦ Fixes:";
      this.siticoneLabel5.BackColor = Color.Transparent;
      this.siticoneLabel5.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel5.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel5.Location = new Point(344, 289);
      this.siticoneLabel5.Margin = new Padding(2);
      this.siticoneLabel5.Name = "siticoneLabel5";
      this.siticoneLabel5.Size = new Size(201, 15);
      this.siticoneLabel5.TabIndex = 162;
      this.siticoneLabel5.Text = "• Delete player's vehicle - Players - Evil";
      this.siticoneLabel6.BackColor = Color.Transparent;
      this.siticoneLabel6.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel6.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel6.Location = new Point(344, 274);
      this.siticoneLabel6.Margin = new Padding(2);
      this.siticoneLabel6.Name = "siticoneLabel6";
      this.siticoneLabel6.Size = new Size(176, 15);
      this.siticoneLabel6.TabIndex = 161;
      this.siticoneLabel6.Text = "• Airstrike Multiple - Players - Evil";
      this.siticoneLabel7.BackColor = Color.Transparent;
      this.siticoneLabel7.Font = new Font("Segoe UI", 8f, FontStyle.Bold);
      this.siticoneLabel7.ForeColor = SystemColors.ButtonHighlight;
      this.siticoneLabel7.Location = new Point(338, 256);
      this.siticoneLabel7.Margin = new Padding(2);
      this.siticoneLabel7.Name = "siticoneLabel7";
      this.siticoneLabel7.Size = new Size(56, 15);
      this.siticoneLabel7.TabIndex = 160;
      this.siticoneLabel7.Text = "➦ Added:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.AutoValidate = AutoValidate.Disable;
      this.BackColor = Color.FromArgb(35, 39, 42);
      this.BackgroundImage = (Image) componentResourceManager.GetObject("$this.BackgroundImage");
      this.BackgroundImageLayout = ImageLayout.Center;
      this.ClientSize = new Size(717, 424);
      this.Controls.Add((Control) this.siticoneLabel2);
      this.Controls.Add((Control) this.siticoneLabel3);
      this.Controls.Add((Control) this.siticoneLabel4);
      this.Controls.Add((Control) this.siticoneLabel5);
      this.Controls.Add((Control) this.siticoneLabel6);
      this.Controls.Add((Control) this.siticoneLabel7);
      this.Controls.Add((Control) this.siticoneLabel11);
      this.Controls.Add((Control) this.siticoneLabel12);
      this.Controls.Add((Control) this.siticoneLabel13);
      this.Controls.Add((Control) this.siticoneLabel14);
      this.Controls.Add((Control) this.siticoneLabel15);
      this.Controls.Add((Control) this.siticoneLabel16);
      this.Controls.Add((Control) this.siticoneLabel17);
      this.Controls.Add((Control) this.siticoneLabel24);
      this.Controls.Add((Control) this.siticoneLabel25);
      this.Controls.Add((Control) this.siticoneLabel26);
      this.Controls.Add((Control) this.siticoneLabel27);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.siticoneLabel23);
      this.Controls.Add((Control) this.label13);
      this.Controls.Add((Control) this.siticoneLabel1);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.label10);
      this.Controls.Add((Control) this.label9);
      this.Controls.Add((Control) this.checkBox1);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label8);
      this.Controls.Add((Control) this.pictureBox2);
      this.Controls.Add((Control) this.label7);
      this.Controls.Add((Control) this.label6);
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.pictureBox1);
      this.Controls.Add((Control) this.password);
      this.Controls.Add((Control) this.username);
      this.Controls.Add((Control) this.LoginBtn);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.siticoneControlBox2);
      this.Controls.Add((Control) this.siticoneControlBox1);
      this.Controls.Add((Control) this.label12);
      this.Controls.Add((Control) this.siticoneRoundedButton3);
      this.Controls.Add((Control) this.status);
      this.DoubleBuffered = true;
      this.FormBorderStyle = FormBorderStyle.None;
      this.Icon = (Icon) componentResourceManager.GetObject("$this.Icon");
      this.Name = nameof (Login);
      this.StartPosition = FormStartPosition.CenterScreen;
      this.Text = "Loader";
      this.TransparencyKey = Color.Maroon;
      this.Load += new EventHandler(this.Login_Load);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      ((ISupportInitialize) this.pictureBox2).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public object Properties { get; private set; }
  }
}
