using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace ChessGameWithThemes
{
    public partial class Form1 : Form
    {
        private SoundPlayer musicPlayer;
        public Form1()
        {
            InitializeComponent();

            // Paneli merkeze hizala
            this.Load += (sender, e) => CenterPanel();
            this.Resize += (sender, e) => CenterPanel();
            

        }
       

        private void Form1_Load(object sender, EventArgs e)
        {



            // Karakter seçeneklerini yükle
            cmbCharacter.Items.AddRange(new string[] { "Harry Potter", "Tom ve Jerry", "Şirinler", "Scooby Doo" ,"Kral Şakir vs Peter Pan","Yunan Tanrıları vs Roma Tanrıları", "Klasik" });
            cmbCharacter.SelectedIndex = 0; // Varsayılan seçim

            // Masa seçeneklerini yükle
            cmbBoard.Items.AddRange(new string[] { "KIRMIZI-SİYAH", "SARI-SİYAH", "BEYAZ-GRİ", "PEMBE-SİYAH","TURUNCU-KOYU GRİ","YEŞİL-SARI","SİYAH-BEYAZ","KAHVERENGİ-BEYAZ","ÇAMGÖBEĞİ-GRİ","TURKUAZ-SARI","YEŞİL-SİYAH","GRİ-KIRMIZI" });
            cmbBoard.SelectedIndex = 0; // Varsayılan seçim

            
            
            musicPlayer = new SoundPlayer();

        }
       

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Seçimleri kontrol et
            string selectedCharacter = cmbCharacter.SelectedItem.ToString();
            string selectedBoard = cmbBoard.SelectedItem.ToString();
           
            

            // Form2'yi başlat ve seçimleri aktar
            Form2 gameForm = new Form2(selectedCharacter, selectedBoard,"ÇİFT KİŞİLİK");
            this.Hide(); // Form1'i gizle
            gameForm.Show();
        }
        private void StartMusic(string character)
        {
            // Seçilen karaktere göre müzik
            if (character == "Harry Potter")
                musicPlayer.SoundLocation = "harry_potter_theme.wav";
            else if (character == "Tom ve Jerry")
                musicPlayer.SoundLocation = "tom_and_jerry_theme.wav";
            else
                musicPlayer.SoundLocation = "slow_muzik_theme.wav";

            // musicPlayer.PlayLooping(); // Müzik döngü halinde çalar
        }
        private void buttonExit_Click(object sender, EventArgs e)
        {
            // Çıkış butonu
            Application.Exit();
        }
        
        private void CenterPanel()
        {
            if (panel != null) // Eğer panel tanımlandıysa
            {
                int x = (this.ClientSize.Width - panel.Width) / 2;
                int y = (this.ClientSize.Height - panel.Height) / 2;
                panel.Location = new Point(x, y);
                // Arka plan resmini form boyutuna göre ayarla
                this.BackgroundImageLayout = ImageLayout.Stretch; 
            }
        }


    }
}
