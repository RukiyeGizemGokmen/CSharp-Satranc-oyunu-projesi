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

namespace ChessGameWithThemes
{

    public partial class Form2 : Form
    {

        private string secilenkarakter;
        private string secilentahta;
        private List<string> yakalananParçalarOyuncu1 = new List<string>();
        private List<string> yakalananParçalarOyuncu2 = new List<string>();
        private List<(int row, int col)> vurgulananHareketler = new List<(int, int)>();
        private Color[,] originalColors = new Color[8, 8];
        private List<(int row, int col)> vurgulananHücreler = new List<(int row, int col)>();
        private string _mode;
        private Button[,] satranctahtasi;
        private Dictionary<string, Image> pieceImages;
        private string[,] boardState;
        private bool isPlayer1Turn = true; // Oyuncu sırası
        private int Hamlesuresi = 30; // Hamle süresi
        private SoundPlayer musicPlayer;
        private (int, int)? secilentaspozisyonu = null;// secilen tasın pozısyonu
        private Panel panelyenilentasoyuncu1;
        private Panel panelyenilentasoyuncu2;
       



        public Form2(string character, string board, string mode)
        {
            InitializeComponent();

            secilenkarakter = character;
            secilentahta = board;
            _mode = mode;


            this.Load += (sender, e) => CenterPanel();
            this.Resize += (sender, e) => CenterPanel();
            this.SuspendLayout();
            InitializeControls();
            TaşResimleriniYükle();
            this.ResumeLayout(false);


        }

        private void CenterPanel()
        {
            if (pnlBoard != null)
            {
                int x = (this.ClientSize.Width - pnlBoard.Width) / 3;
                int y = (this.ClientSize.Height - pnlBoard.Height) / 2;

                pnlBoard.Location = new Point(Math.Max(0, x), Math.Max(0, y));
            }
            if (pnlInfo != null && pnlBoard != null)
            {
                int xInfo = pnlBoard.Right + 10; // Tahtanın sağından 10 piksel boşluk
                int yInfo = pnlBoard.Top;        // Tahtanın üst hizasında
                pnlInfo.Location = new Point(xInfo, yInfo);
            }
        }
        private void InitializeControls()
        {

            pnlInfo.Visible = false;
            // Bilgi Paneli
            pnlInfo = new Panel
            {

                BackColor = Color.Gray,

                Size = new Size(200, 800)
            };
            pnlInfo.Location = new Point(500, 10);  // Bilgi panelinin sağda olması



            // Oyuncu Sırası Etiketi
            lblTurn = new Label
            {
                Text = $"Sıra: {(isPlayer1Turn ? "Oyuncu 1" : "Oyuncu 2")}",
                Location = new Point(10, 20),
                Size = new Size(180, 20)
            };

            // Zamanlayıcı Etiketi
            lblTimer = new Label
            {
                Text = $"Süre: {Hamlesuresi}s",
                Location = new Point(10, lblTurn.Bottom + 10),
                Size = new Size(180, 20)
            };

          

            // Müziği Kapat Butonu
            btnStopMusic = new Button
            {
                Text = "Müziği Kapat",
                Location = new Point(10, lblTimer.Bottom + 20),
                Size = new Size(180, 40)
            };
            btnStopMusic.Click += btnStopMusic_Click;

            btnExitGame = new Button
            {
                Text = "Oyunu Kapat",
                Location = new Point(10, btnStopMusic.Bottom + 20),
                Size = new Size(180, 40)
            };
            btnExitGame.Click += btnExitGame_Click;

            // Oyuncu 1 Taşları labeli
            Label lblPlayer1 = new Label
            {
                Text = "Oyuncu 1 Taşları",
                Location = new Point(10, btnExitGame.Bottom + 20),
                Size = new Size(180, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlInfo.Controls.Add(lblPlayer1);

            //Oyuncu 1 Taşları Paneli
            panelyenilentasoyuncu1 = new Panel
            {
                BackColor = Color.DarkGray,
                Size = new Size(180, 200),
                Location = new Point(10, lblPlayer1.Bottom + 5),
                BorderStyle = BorderStyle.FixedSingle
            };
            pnlInfo.Controls.Add(panelyenilentasoyuncu1);

            // Oyuncu 2 Taşları labeli
            Label lblPlayer2 = new Label
            {
                Text = "Oyuncu 2 Taşları",
                Location = new Point(10, panelyenilentasoyuncu1.Bottom + 20),
                Size = new Size(180, 20),
                TextAlign = ContentAlignment.MiddleCenter
            };
            pnlInfo.Controls.Add(lblPlayer2);

            // Oyuncu 2 Taşları paneli
            panelyenilentasoyuncu2 = new Panel
            {
                BackColor = Color.DarkGray,
                Size = new Size(180, 200),
                Location = new Point(10, lblPlayer2.Bottom + 5),
                BorderStyle = BorderStyle.FixedSingle
            };
         
            // Panel Elemanlarını Ekle
            pnlInfo.Controls.Add(lblTurn);
            pnlInfo.Controls.Add(lblTimer);
            pnlInfo.Controls.Add(btnStopMusic);
            pnlInfo.Controls.Add(btnExitGame);
            // Bilgi Paneline Kontrolleri Ekle
            pnlInfo.Controls.Add(lblPlayer1);
            pnlInfo.Controls.Add(panelyenilentasoyuncu1);
            pnlInfo.Controls.Add(lblPlayer2);
            pnlInfo.Controls.Add(panelyenilentasoyuncu2);

            Controls.Add(pnlInfo);


            pnlBoard.Location = new Point(10, 10);  // Tahtanın sol üst köşede olması
            Controls.Add(pnlBoard);

            // Timer
            timerGame = new Timer
            {
                Interval = 1000
            };
            timerGame.Tick += timerGame_Tick;

            // Form Ayarları
            ClientSize = new Size(720, 500);
            Text = "Oyun Ekranı";
            Load += Form2_Load;
        }

        private void YakalananTaşlarPaneliniGüncelle()
        {
            // Oyuncu 1 panelini temizle ve güncelle
            panelyenilentasoyuncu1.Controls.Clear();
            int x = 5, y = 5;
            foreach (var piece in yakalananParçalarOyuncu1)
            {
                PictureBox pic = new PictureBox
                {
                    Size = new Size(30, 30),
                    Image = pieceImages[piece],
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(x, y)
                };
                panelyenilentasoyuncu1.Controls.Add(pic);
                x += 35;
                if (x > panelyenilentasoyuncu1.Width - 35)
                {
                    x = 5;
                    y += 35;
                }
            }

            // Oyuncu 2 panelini temizle ve güncelle
            panelyenilentasoyuncu2.Controls.Clear();
            x = 5; y = 5;
            foreach (var piece in yakalananParçalarOyuncu2)
            {
                PictureBox pic = new PictureBox
                {
                    Size = new Size(30, 30),
                    Image = pieceImages[piece],
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Location = new Point(x, y)
                };
                panelyenilentasoyuncu2.Controls.Add(pic);
                x += 35;
                if (x > panelyenilentasoyuncu2.Width - 35)
                {
                    x = 5;
                    y += 35;
                }
            }
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            // Formun boyutunu burada ayarlıyoruz
            ClientSize = new Size(750, 500);  // Yeterli genişlik ve yükseklik
                                             
            // Karaktere özel müziği başlat
            MüziğiBaşlat();

            // Tahtayı oluştur ve ayarları yap
            TahtayıBaşlat();

            // Tahtayı oluştur
            SatrançTahtasıOluştur();

            // Timer başlat
            timerGame.Start();
            
           
        }


        private void TahtayıBaşlat()
        {
            boardState = new string[8, 8]
            {
                { "r", "n", "b", "q", "k", "b", "n", "r" },
                { "p", "p", "p", "p", "p", "p", "p", "p" },
                { "", "", "", "", "", "", "", "" },
                { "", "", "", "", "", "", "", "" },
                { "", "", "", "", "", "", "", "" },
                { "", "", "", "", "", "", "", "" },
                { "P", "P", "P", "P", "P", "P", "P", "P" },
                { "R", "N", "B", "Q", "K", "B", "N", "R" }
            };
            pnlBoard.Controls.Clear();
            SatrançTahtasıOluştur();

        }


        private void SatrançTahtasıOluştur()
        {

            int tileSize = 100;
            int boardSize = tileSize * 8;

            pnlBoard.Size = new Size(boardSize, boardSize);
            pnlBoard.Controls.Clear();

            satranctahtasi = new Button[8, 8];
            ToolTip toolTip = new ToolTip(); // Tek bir ToolTip nesnesi tüm taşlar için kullanılacak.
            Color color1, color2;

            switch (secilentahta)
            {
                case "KIRMIZI-SİYAH":
                    color1 = Color.IndianRed;
                    color2 = Color.Black;
                    break;
                case "SARI-SİYAH":
                    color1 = Color.Gold;
                    color2 = Color.Black;
                    break;
                case "BEYAZ-GRİ":
                    color1 = Color.White;
                    color2 = Color.Gray;
                    break;
                case "PEMBE-SİYAH":
                    color1 = Color.Pink;
                    color2 = Color.Black;
                    break;

                case "YEŞİL-SARI":
                    color1 = Color.Green;
                    color2 = Color.Gold;
                    break;

                case "TURUNCU-KOYU GRİ":
                    color1 = Color.Orange;
                    color2 = Color.DarkGray;
                    break;
                case "SİYAH-BEYAZ":
                    color1 = Color.Black;
                    color2 = Color.White;
                    break;
                     case "KAHVERENGİ-BEYAZ":
                    color1 = Color.Brown;
                    color2 = Color.White;
                    break;
                case "ÇAMGÖBEĞİ-GRİ":
                    color1 = Color.Cyan;
                    color2 = Color.Gray;
                    break;

                case "TURKUAZ-SARI":
                    color1 = Color.Turquoise;
                    color2 = Color.Gold;
                    break;

                case "YEŞİL-SİYAH":
                    color1 = Color.Green;
                    color2 = Color.Black;
                    break;
                case "GRİ-KIRMIZI":
                    color1 = Color.Gray;
                    color2 = Color.Red;
                    break;
                default:
                    color1 = Color.White;
                    color2 = Color.Black;
                    break;
            }

            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Button tile = new Button
                    {
                        Width = tileSize,
                        Height = tileSize,
                        Location = new Point(col * tileSize, row * tileSize),
                        BackColor = (row + col) % 2 == 0 ? color1 : color2,
                        Tag = $"{row},{col}"
                    };
                    originalColors[row, col] = tile.BackColor;
                    string piece = boardState[row, col];
                    if (!string.IsNullOrEmpty(piece))
                    {
                        tile.BackgroundImage = pieceImages[piece];
                        tile.BackgroundImageLayout = ImageLayout.Stretch;

                        
                        string pieceName = TaşAdınıAl(piece);
                        string characterName = TaşAdınıAl(piece);
                        toolTip.SetToolTip(tile, $"{characterName}");
                    }

                    tile.Click += KareyeTıkla;
                    satranctahtasi[row, col] = tile;
                    pnlBoard.Controls.Add(tile);
                }
            }
        }
       

        private void TaşResimleriniYükle()
        {
            pieceImages = new Dictionary<string, Image>();

            // Resimlerin bulunduğu klasörün yolu
            string piecesPath = Path.Combine(Application.StartupPath, @"..\..\Pieces");

            // Dosya varlığını kontrol et
            if (!Directory.Exists(piecesPath))
            {
                MessageBox.Show($"Pieces klasörü bulunamadı: {piecesPath}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (secilenkarakter)
            {
                case "Harry Potter":

                    pieceImages = new Dictionary<string, Image>
               {
                { "p", Image.FromFile(Path.Combine(piecesPath,"black_pawn.png")) },
                { "r", Image.FromFile(Path.Combine(piecesPath,"black_hermione.png")) },
                { "n", Image.FromFile(Path.Combine(piecesPath,"black_ron.png") )},
                { "b", Image.FromFile(Path.Combine(piecesPath, "black_snape.png")) },
                { "q", Image.FromFile(Path.Combine(piecesPath, "black_voldemort.png") )},
                { "k", Image.FromFile(Path.Combine(piecesPath, "black_harry.png")) },
                { "P", Image.FromFile(Path.Combine(piecesPath, "white_pawn.png")) },
                { "R", Image.FromFile(Path.Combine(piecesPath, "white_hermione.png")) },
                { "N", Image.FromFile(Path.Combine(piecesPath, "white_ron.png")) },
                { "B", Image.FromFile(Path.Combine(piecesPath,"white_snape.png")) },
                { "Q", Image.FromFile(Path.Combine(piecesPath, "white_voldemort.png")) },
                { "K", Image.FromFile(Path.Combine(piecesPath, "white_harry.png") )}
                };
                    break;
                case "Tom ve Jerry":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_mouse.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_cat.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_dog.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_ordek.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_jerry.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_tom.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_mouse.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_cat.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_dog.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_ordek.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_jerry.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_tom.png") )}
                    };
                    break;
                case "Şirinler":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_azman.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_bilge_sirin.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_guclusirin.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_sirine.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_gargamel.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_sirinbaba.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_azman.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_bilge_sirin.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_guclusirin.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_sirine.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_gargamel.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_sirinbaba.png") )}
                    };
                    break;
                case "Scooby Doo":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_araba.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_velma.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_shaggy.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_daphne.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_fred.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_scoobydoo.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_araba.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_velma.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_shaggy.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_daphne.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_fred.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_scoobydoo.png") )}
                    };
                    break;
                case "Kral Şakir vs Peter Pan":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_mirket.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_canan.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_kadriye.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_necati.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_kral_remzi.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_sakir.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_tinkerbell.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_Michael_Darling.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_Jhon_darling.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_Wendy_Darling.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_kaptan_kanca.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_peter_pan.png") )}
                    };
                    break;
                case "Yunan Tanrıları vs Roma Tanrıları":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_artemis.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_athena.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_hermes.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_poseidon.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_ares.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_zeus.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_diana.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_mercury.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_neptune.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_venus.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_mars.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_jupiter.png") )}
                    };
                    break;
                case "Klasik":
                    pieceImages = new Dictionary<string, Image>
                    {
                        { "p", Image.FromFile(Path.Combine(piecesPath,"black_piyon.png")) },
                        { "r", Image.FromFile(Path.Combine(piecesPath,"black_rook.png")) },
                        { "n", Image.FromFile(Path.Combine(piecesPath,"black_knight.png") )},
                        { "b", Image.FromFile(Path.Combine(piecesPath, "black_bishop.png")) },
                        { "q", Image.FromFile(Path.Combine(piecesPath, "black_queen.png") )},
                        { "k", Image.FromFile(Path.Combine(piecesPath, "black_king.png")) },
                        { "P", Image.FromFile(Path.Combine(piecesPath, "white_piyon.png")) },
                        { "R", Image.FromFile(Path.Combine(piecesPath, "white_rook.png")) },
                        { "N", Image.FromFile(Path.Combine(piecesPath, "white_knight.png")) },
                        { "B", Image.FromFile(Path.Combine(piecesPath,"white_bishop.png")) },
                        { "Q", Image.FromFile(Path.Combine(piecesPath, "white_queen.png")) },
                        { "K", Image.FromFile(Path.Combine(piecesPath, "white_king.png") )}
                    };
                    break;
                default:
                    break;
            }

        }
        private void VurgularıSıfırla
()
        {
            foreach (var (row, col) in vurgulananHücreler)
            {
                satranctahtasi[row, col].BackColor = originalColors[row, col]; // Orijinal renge döndür
            }

            vurgulananHücreler.Clear();
        }
        private void GeçerliHamleleriVurgula(int row, int col)
        {

            VurgularıSıfırla(); // Önceki vurguları kaldır

            if (row == -1 || col == -1)
                return;

            for (int r = 0; r < 8; r++)
            {
                for (int c = 0; c < 8; c++)
                {
                    if (GeçerliHamleMi(row, col, r, c))
                    {
                        satranctahtasi[r, c].BackColor = Color.Red; // Geçerli hamle vurgusu
                        vurgulananHücreler.Add((r, c)); // Vurgulanan hücreleri listeye ekle
                    }

                }
            }
        }

        private void KareyeTıkla(object sender, EventArgs e)
        {
            Button clickedTile = sender as Button;
            string[] position = clickedTile?.Tag?.ToString()?.Split(',');
            if (position == null || position.Length != 2) return;

            int row = int.Parse(position[0]);
            int col = int.Parse(position[1]);

            if (secilentaspozisyonu == null)
            {
                // İlk seçim
                if (!string.IsNullOrEmpty(boardState[row, col]) && OyuncuTaşıMı(row, col))
                {
                    secilentaspozisyonu = (row, col);
                    // Taş açıklamasını göster

                    GeçerliHamleleriVurgula(row, col); // Olası hamleleri göster

                }
            }
            else
            {
                VurgularıSıfırla();
            
                // Hamle yapmayı dene
                (int startRow, int startCol) = secilentaspozisyonu.Value;
                if (GeçerliHamleMi(startRow, startCol, row, col))
                {
                    TaşıHareketEttir(startRow, startCol, row, col);
                }

                // Tüm karelerin rengini sıfırla
                GeçerliHamleleriVurgula(-1, -1);
                secilentaspozisyonu = null;

            }
        }

        
        private bool OyuncuTaşıMı(int row, int col)
        {
            string piece = boardState[row, col];
            return isPlayer1Turn ? char.IsUpper(piece[0]) : char.IsLower(piece[0]);
        }

        private bool GeçerliHamleMi(int startRow, int startCol, int endRow, int endCol)
        {
            // Sınır kontrolleri
            if (startRow < 0 || startRow >= 8 || startCol < 0 || startCol >= 8 ||
                endRow < 0 || endRow >= 8 || endCol < 0 || endCol >= 8)
            {
                    return false; // Kendi taşını yeme kontrolü
             
            }
            string piece = boardState[startRow, startCol];
            string targetPiece = boardState[endRow, endCol];

            // Hedef karede kendi taşımız varsa geçersiz
            if (!string.IsNullOrEmpty(targetPiece) &&
                (isPlayer1Turn ? char.IsUpper(targetPiece[0]) : char.IsLower(targetPiece[0])))
            {
                return false;
            }

            
            int rowDiff = Math.Abs(endRow - startRow);
            int colDiff = Math.Abs(endCol - startCol);

            switch (char.ToLower(piece[0]))
            {
                case 'p':
                    // Piyon hareketi (örnek olarak ilerleme)
                    int direction = isPlayer1Turn ? -1 : 1;
                    if (startCol == endCol)
                    {
                        if (boardState[endRow, endCol] == "" && endRow - startRow == direction)
                            return true; // Tek kare ileri

                        if (boardState[endRow, endCol] == "" && endRow - startRow == 2 * direction &&
                            (isPlayer1Turn && startRow == 6 || !isPlayer1Turn && startRow == 1) &&
                            boardState[startRow + direction, startCol] == "")
                            return true; // İlk hamlede iki kare ileri
                    }
                    else if (colDiff == 1 && endRow - startRow == direction && boardState[endRow, endCol] != "")
                    {
                        return true; // Çapraz yemeler
                    }
                    break;
                case 'r': // Kale hareketi
                    if (rowDiff == 0 || colDiff == 0)
                        return YolTemizMi(startRow, startCol, endRow, endCol);
                    break;
                case 'n': // At hareketi
                    if (rowDiff * colDiff == 2)
                        return true;
                    break;
                case 'b': // Fil hareketi
                    if (rowDiff == colDiff)
                        return YolTemizMi(startRow, startCol, endRow, endCol);
                    break;
                case 'q': // Vezir hareketi
                    if (rowDiff == colDiff || rowDiff == 0 || colDiff == 0)
                        return YolTemizMi(startRow, startCol, endRow, endCol);
                    break;
                case 'k': // Şah hareketi
                    if (rowDiff <= 1 && colDiff <= 1)
                        return true;
                    break;
                default:
                     return false;
            }

            return false;
        }

        private bool YolTemizMi(int startRow, int startCol, int endRow, int endCol)
        {
            int rowStep = Math.Sign(endRow - startRow);
            int colStep = Math.Sign(endCol - startCol);
            int currentRow = startRow + rowStep;
            int currentCol = startCol + colStep;

            while (currentRow != endRow || currentCol != endCol)
            {
                if (!string.IsNullOrEmpty(boardState[currentRow, currentCol]))
                    return false;
                currentRow += rowStep;
                currentCol += colStep;
            }

            return true;
        }
       
       
        private void TaşıHareketEttir(int startRow, int startCol, int endRow, int endCol)
        {
            // Eğer hedefte bir taş varsa, yenilen taşlara ekle
            if (!string.IsNullOrEmpty(boardState[endRow, endCol]))
            {
                if (isPlayer1Turn)
                {
                    yakalananParçalarOyuncu2.Add(boardState[endRow, endCol]);
                    
                }
            else
            {
                    yakalananParçalarOyuncu1.Add(boardState[endRow, endCol]);
                     
                }
                YakalananTaşlarPaneliniGüncelle(); // Panelleri güncelle
            }
            if (boardState[endRow, endCol] == "k" || boardState[endRow, endCol] == "K")
            {
                OyunuBitir(true);
                return;
            }

            boardState[endRow, endCol] = boardState[startRow, startCol];
            boardState[startRow, startCol] = "";
            // Tahtanın renklerini sıfırla


            SatrançTahtasıOluştur();
            PiyonTerfisiniKontrolEt(endRow, endCol);

            if (ŞahMatMı())
            {
                OyunuBitir(false);
            }
            SırayıDeğiştir();
          
        }
        
        private bool ŞahMatMı()
        {
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    string piece = boardState[row, col];
                    if (!string.IsNullOrEmpty(piece) && OyuncuTaşıMı(row, col))
                    {
                        for (int r = 0; r < 8; r++)
                        {
                            for (int c = 0; c < 8; c++)
                            {
                                if (GeçerliHamleMi(row, col, r, c))
                                    return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        private void PiyonTerfisiniKontrolEt(int row, int col)
        {
            string piece = boardState[row, col];

            if ((piece == "P" && row == 0) || (piece == "p" && row == 7))
            {
                List<string> capturedPieces = isPlayer1Turn ? yakalananParçalarOyuncu1 : yakalananParçalarOyuncu2;
                string choice = TaşSeçimiSor(capturedPieces);

                boardState[row, col] = choice;
                SatrançTahtasıOluştur();
                
            }
        }
        private string TaşAdınıAl(string piece)
        {
            if (secilenkarakter == "Harry Potter")
            {
                switch (piece)
                {
                    case "p": return "Dobby:Piyon(oyuncu 2)";
                    case "r": return "Hermione Granger:Kale(oyuncu 2)";
                    case "n": return "Ron Weasley:At(oyuncu 2)";
                    case "b": return "Severus Snape:.Fil(oyuncu 2)";
                    case "q": return "Lord Voldemort:Vezir(oyuncu 2)";
                    case "k": return "Harry Potter:Şah(oyuncu 2)";
                    case "P": return "Dobby:Piyon(oyuncu 1)";
                    case "R": return "Hermione Granger:Kale(oyuncu 1)";
                    case "N": return "Ron Weasley:At(oyuncu 1)";
                    case "B": return "Severus Snape:Fil(oyuncu 1)";
                    case "Q": return "Lord Voldemort:Vezir(oyuncu 1)";
                    case "K": return "Harry Potter:Şah(oyuncu 1)";
                    default: return "Bilinmeyen Taş";
                }
            }
            else if (secilenkarakter == "Şirinler")
            {
                switch (piece)
                {
                    case "p": return "Azman:Piyon(oyuncu 2)";
                    case "r": return "Bilge Şirin:Kale(oyuncu 2)";
                    case "n": return "Güçlü Şirin:At(oyuncu 2)";
                    case "b": return "Şirine:Fil(oyuncu 2)";
                    case "q": return "Gargamel:Vezir(oyuncu 2)";
                    case "k": return "Şirin Baba:Şah(oyuncu 2)";
                    case "P": return "Azman:Piyon(oyuncu 1)";
                    case "R": return "Bilge Şirin:Kale(oyuncu 1)";
                    case "N": return "Güçlü Şirin:At(oyuncu 1)";
                    case "B": return "Şirine:Fil(oyuncu 1)";
                    case "Q": return "Gargamel:Vezir(oyuncu 1)";
                    case "K": return "Şirin Baba:Şah(oyuncu 1)";
                    default: return "Bilinmeyen Taş";
                }
            }
            else if (secilenkarakter == "Tom ve Jerry")
            {
                switch (piece)
                {
                    case "p": return "Fare:Piyon(oyuncu 2)";
                    case "r": return "Kedi:Kale(oyuncu 2)";
                    case "n": return "Köpek:At(oyuncu 2)";
                    case "b": return "Ördek:Fil(oyuncu 2)";
                    case "q": return "Jerry:Vezir(oyuncu 2)";
                    case "k": return "Tom:Şah(oyuncu 2)";
                    case "P": return "Fare:Piyon(oyuncu 1)";
                    case "R": return "Kedi:Kale(oyuncu 1)";
                    case "N": return "Köpek:At(oyuncu 1)";
                    case "B": return "Ördek:Fil(oyuncu 1)";
                    case "Q": return "Jerry:Vezir(oyuncu 1)";
                    case "K": return "Tom:Şah(oyuncu 1)";
                    default: return "Bilinmeyen Taş";
                }
            }
            else if (secilenkarakter == "Scooby Doo") {
                switch (piece)
                {
                    case "p": return "Piyon: Araba(Oyuncu 2)";
                    case "r": return "Kale: Velma(Oyuncu 2)";
                    case "n": return "At: Shaggy(Oyuncu 2)";
                    case "b": return "Fil: Daphne(Oyuncu 2)";
                    case "q": return "Vezir: Fred(Oyuncu 2)";
                    case "k": return "Şah: Scooby Doo(Oyuncu 2)";
                    case "P": return "Piyon: Araba(Oyuncu 1)";
                    case "R": return "Kale: Velma(Oyuncu 1)";
                    case "N": return "At: Shaggy(Oyuncu 1)";
                    case "B": return "Fil: Daphne(Oyuncu 1)";
                    case "Q": return "Vezir: Fred(Oyuncu 1)";
                    case "K": return "Şah: Scooby Doo(Oyuncu 1)";
                    default: return "Bilinmeyen";
                }
            }
            else if (secilenkarakter == "Kral Şakir vs Peter Pan") {
                switch (piece)
                {
                    case "p": return "Piyon: Mirket(Oyuncu 2)";
                    case "r": return "Kale: Canan(Oyuncu 2)";
                    case "n": return "At: Kadriye(Oyuncu 2)";
                    case "b": return "Fil: Necati(Oyuncu 2)";
                    case "q": return "Vezir: Kral Remzi(Oyuncu 2)";
                    case "k": return "Şah: Şakir(Oyuncu 2)";
                    case "P": return "Piyon: Tinkerbell(Oyuncu 1)";
                    case "R": return "Kale: Michael Darling(Oyuncu 1)";
                    case "N": return "At: John Darling(Oyuncu 1)";
                    case "B": return "Fil: Wendy Darling(Oyuncu 1)";
                    case "Q": return "Vezir: Kaptan Kanca(Oyuncu 1)";
                    case "K": return "Şah: Peter Pan(Oyuncu 1)";
                    default: return "Bilinmeyen";
                }
            }
            else if (secilenkarakter == "Yunan Tanrıları vs Roma Tanrıları") {
                switch (piece)
                {
                    case "p": return "Piyon: Artemis(Oyuncu 2)";
                    case "r": return "Kale: Athena(Oyuncu 2)";
                    case "n": return "At: Hermes(Oyuncu 2)";
                    case "b": return "Fil: Poseidon(Oyuncu 2)";
                    case "q": return "Vezir: Ares(Oyuncu 2)";
                    case "k": return "Şah: Zeus(Oyuncu 2)";
                    case "P": return "Piyon: Diana(Oyuncu 1)";
                    case "R": return "Kale: Mercury(Oyuncu 1)";
                    case "N": return "At: Neptune(Oyuncu 1)";
                    case "B": return "Fil: Venus(Oyuncu 1)";
                    case "Q": return "Vezir: Mars(Oyuncu 1)";
                    case "K": return "Şah: Jupiter(Oyuncu 1)";
                    default: return "Bilinmeyen";
                }
            }
        
            else
            {
                // Klasik tema
                switch (piece)
                {
                    case "p": return "Piyon(Siyah)";
                    case "r": return "Kale(Siyah)";
                    case "n": return "At(Siyah)";
                    case "b": return "Fil(Siyah)";
                    case "q": return "Vezir(Siyah)";
                    case "k": return "Şah(Siyah)";
                    case "P": return "Piyon (Beyaz)";
                    case "R": return "Kale (Beyaz)";
                    case "N": return "At (Beyaz)";
                    case "B": return "Fil (Beyaz)";
                    case "Q": return "Vezir (Beyaz)";
                    case "K": return "Şah (Beyaz)";
                    default: return "Bilinmeyen Taş";
                }
            }
        

    }
        private string TaşSeçimiSor(List<string> capturedPieces)
        {
            if (capturedPieces.Count == 0)
            {
                MessageBox.Show("Yenilmiş bir taş yok! Varsayılan olarak Vezir seçilecek.");
                return "Q"; // Varsayılan olarak Vezir
            }

            using (Form form = new Form())
            {
                form.Text = "Piyon Terfisi";
                Label label = new Label
                {
                    Text = "Yenilen taşlardan biriyle değiştirmek istiyorsunuz:",
                    Dock = DockStyle.Top,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                ComboBox comboBox = new ComboBox
                {
                   
                    Dock = DockStyle.Top,
                    DropDownStyle = ComboBoxStyle.DropDownList
                };
                // Görüntülenen isimleri güncelle
                Dictionary<string, string> pieceMapping = new Dictionary<string, string>();
                foreach (var piece in capturedPieces)
                {
                    string displayName = TaşAdınıAl(piece); // Görüntüleme adı
                    pieceMapping[displayName] = piece; // Harita oluştur (görünen -> gerçek isim)
                    comboBox.Items.Add(displayName);
                }
                Button okButton = new Button
                {
                    Text = "Tamam",
                    DialogResult = DialogResult.OK,
                    Dock = DockStyle.Bottom
                };

                form.Controls.Add(label);
                form.Controls.Add(comboBox);
                form.Controls.Add(okButton);

                form.AcceptButton = okButton;
                form.StartPosition = FormStartPosition.CenterParent;
                form.ClientSize = new Size(300, 150);

                if (form.ShowDialog() == DialogResult.OK)
                {
                    string selectedDisplayName = comboBox.SelectedItem.ToString();
                    return pieceMapping[selectedDisplayName]; // Gerçek ismi döndür
                }
                return capturedPieces[0]; // Varsayılan olarak ilk taş
            }
        }

        private void OyunuBitir(bool isKingCaptured)
        {
            timerGame.Stop();
            string winner = isPlayer1Turn ? "Oyuncu 1" : "Oyuncu 2";

            if (isKingCaptured)
            {
                MessageBox.Show($"{winner} şahı yedi ve kazandı!", "Oyun Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show($"{winner} şah mat yaptı ve kazandı!", "Oyun Bitti", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            Application.Exit();
        }




      



        private void MüziğiBaşlat()
        {
            if (musicPlayer == null)
                musicPlayer = new SoundPlayer(); // musicPlayer nesnesini oluştur

            string soundPath = Path.Combine(Application.StartupPath, "Sounds");

            switch (secilenkarakter)
            {
                case "Harry Potter":
                    musicPlayer.SoundLocation = Path.Combine(soundPath, "harry_potter_theme.wav");
                    break;
                case "Tom ve Jerry":
                    musicPlayer.SoundLocation = Path.Combine(soundPath, "tom_and_jerry_theme.wav");
                    break;
                default:
                    musicPlayer.SoundLocation = Path.Combine(soundPath, "slow_muzik_theme.wav");
                    break;

            }
            if (File.Exists(musicPlayer.SoundLocation))
                musicPlayer.PlayLooping();
            else
                MessageBox.Show("Şarkı dosyası bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void SırayıDeğiştir()
        {
            isPlayer1Turn = !isPlayer1Turn;
            lblTurn.Text = $"Sıra: {(isPlayer1Turn ? "Oyuncu 1" : "Oyuncu 2")}";
            Hamlesuresi = 30; // Zamanlayıcıyı sıfırla

        }
        private void timerGame_Tick(object sender, EventArgs e)
        {

            Hamlesuresi--;
            lblTimer.Text = $"Süre: {Hamlesuresi}s";

            if (Hamlesuresi <= 0)
            {
                SırayıDeğiştir();
            }
        }


        private void btnStopMusic_Click(object sender, EventArgs e)
        {



            musicPlayer?.Stop();

        }


        private void btnExitGame_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
    }
}