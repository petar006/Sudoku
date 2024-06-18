using System;
using System.Drawing;
using System.Windows.Forms;

namespace SudokuApp
{
    public partial class Form1 : Form
    {
        private Engine engine;
        private TextBox[,] textBoxes;

        public Form1()
        {
            InitializeComponent();
            engine = new Engine();
            textBoxes = new TextBox[9, 9];
            InitializeSudokuGrid();
            LoadSudokuGrid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Ostaviti prazno ili dodati logiku koja se treba izvršiti pri učitavanju forme
        }

        private void InitializeSudokuGrid()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    TextBox textBox = new TextBox
                    {
                        Multiline = true,
                        Width = 40,
                        Height = 40,
                        Location = new Point(50 + col * 45, 50 + row * 45),
                        TextAlign = HorizontalAlignment.Center,
                        Font = new Font(FontFamily.GenericSansSerif, 20)
                    };

                    textBoxes[row, col] = textBox;
                    this.Controls.Add(textBox);
                }
            }

            Button checkButton = new Button
            {
                Text = "Proveri rešenje",
                Location = new Point(150, 500),
                Size = new Size(100, 30)
            };
            checkButton.Click += new EventHandler(CheckSolution);
            this.Controls.Add(checkButton);

            Button revealButton = new Button
            {
                Text = "Otkrij polje",
                Location = new Point(250, 500),
                Size = new Size(100, 30)
            };
            revealButton.Click += new EventHandler(RevealField);
            this.Controls.Add(revealButton);
        }

        private void LoadSudokuGrid()
        {
            int[,] prikaz = engine.VratiPrikaz();
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (prikaz[row, col] != 0)
                    {
                        textBoxes[row, col].Text = prikaz[row, col].ToString();
                        textBoxes[row, col].ReadOnly = true;
                        textBoxes[row, col].BackColor = Color.LightGray;
                    }
                    else
                    {
                        textBoxes[row, col].Text = "";
                        textBoxes[row, col].ReadOnly = false;
                        textBoxes[row, col].BackColor = Color.White;
                    }
                }
            }
        }

        private void CheckSolution(object sender, EventArgs e)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (!textBoxes[row, col].ReadOnly)
                    {
                        if (int.TryParse(textBoxes[row, col].Text, out int value))
                        {
                            engine.UmetniVrednost(row, col, value);
                        }
                        else
                        {
                            engine.UmetniVrednost(row, col, 0);
                        }
                    }
                }
            }

            if (engine.ProveriKrajIgre())
            {
                MessageBox.Show("Čestitamo, rešili ste Sudoku!", "Pobeda");
            }
            else
            {
                MessageBox.Show("Rešenje nije tačno, pokušajte ponovo.", "Greška");
            }
        }

        private void RevealField(object sender, EventArgs e)
        {
            engine.OtkrijPolje();
            LoadSudokuGrid();
        }

    }
}
