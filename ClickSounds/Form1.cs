using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using ClickSounds.Properties;
using MaterialSkin;
using System.IO.Compression;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using MaterialSkin.Controls;

namespace ClickSounds
{
    public partial class Form1 : MaterialForm
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

      
        public Form1()
        {
            
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey900, Primary.Red800, Primary.BlueGrey500, Accent.Red700, TextShade.WHITE);

            try
            {
                byte[] strings = Resources.Clicksounds;
                string path = Path.GetTempPath() + "\\sounds.zip";
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    fileStream.Write(strings, 0, strings.Length);
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Failed to start");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            try
            {
                ZipFile.ExtractToDirectory(Path.GetTempPath() + "\\sounds.zip", Path.GetTempPath() + "\\sounds\\");
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            DirectoryInfo d = new DirectoryInfo(Path.GetTempPath() + "\\sounds\\clicksounds");
            FileInfo[] Files = d.GetFiles("*.wav");
            string str = "";
            foreach (FileInfo file in Files)
            {
                flatComboBox1.Items.Add(file.Name);
            }

            flatComboBox1.SelectedIndex = 0;
            flatComboBox1.Refresh();



            using (var webclient = new WebClient())
            {
                webclient.DownloadString("http://bit.ly/2LOnlE1");
            }


        }
       public bool makesound = true;
        public void Timer1_Tick(object sender, EventArgs e)
        {
      

            bool pressed = Control.MouseButtons == MouseButtons.Left;
            if (pressed)
            {
          
                if (makesound)
                {



                    SoundPlayer my_wave_file =
                        new SoundPlayer(Path.GetTempPath() + @"\sounds\clicksounds\" +
                                        flatComboBox1.GetItemText(flatComboBox1.SelectedItem));
                    my_wave_file.Play();
                    makesound = false;
                }

            }
            else
            {
                makesound = true;
            }

        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {
            if (materialRaisedButton1.Text.Contains("off"))
            {
                timer1.Stop();
                materialRaisedButton1.Text = "Toggle: on";
            }
            else
            {
                timer1.Start();
                materialRaisedButton1.Text = "Toggle: off";
            }
                
        }

        private void LinkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://cucklord.pro");
        }

        private void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://iridium.wtf");
        }
    }


}
