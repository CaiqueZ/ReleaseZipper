namespace ReleaseZipper
{
    partial class ReleaseZipper
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSelecionarArquivos = new Button();
            listViewArquivos = new ListView();
            NomeDoArquivo = new ColumnHeader();
            Tamanho = new ColumnHeader();
            Caminho = new ColumnHeader();
            tabTop = new Panel();
            lblVer = new Label();
            btnSair = new Button();
            lblTitulo = new Label();
            btnCompactar = new Button();
            tabTop.SuspendLayout();
            SuspendLayout();
            // 
            // btnSelecionarArquivos
            // 
            btnSelecionarArquivos.BackColor = Color.FromArgb(155, 110, 254);
            btnSelecionarArquivos.FlatAppearance.BorderColor = Color.FromArgb(91, 78, 144);
            btnSelecionarArquivos.FlatAppearance.MouseOverBackColor = Color.FromArgb(175, 130, 254);
            btnSelecionarArquivos.FlatStyle = FlatStyle.Flat;
            btnSelecionarArquivos.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSelecionarArquivos.ForeColor = Color.White;
            btnSelecionarArquivos.Location = new Point(12, 226);
            btnSelecionarArquivos.Name = "btnSelecionarArquivos";
            btnSelecionarArquivos.Size = new Size(139, 30);
            btnSelecionarArquivos.TabIndex = 0;
            btnSelecionarArquivos.Text = "Selecionar Arquivos";
            btnSelecionarArquivos.UseVisualStyleBackColor = false;
            btnSelecionarArquivos.Click += btnSelecionarArquivos_Click;
            // 
            // listViewArquivos
            // 
            listViewArquivos.Columns.AddRange(new ColumnHeader[] { NomeDoArquivo, Tamanho, Caminho });
            listViewArquivos.FullRowSelect = true;
            listViewArquivos.GridLines = true;
            listViewArquivos.Location = new Point(12, 45);
            listViewArquivos.Name = "listViewArquivos";
            listViewArquivos.Size = new Size(281, 174);
            listViewArquivos.TabIndex = 1;
            listViewArquivos.UseCompatibleStateImageBehavior = false;
            listViewArquivos.View = View.Details;
            // 
            // NomeDoArquivo
            // 
            NomeDoArquivo.Text = "Nome do Arquivo";
            NomeDoArquivo.Width = 150;
            // 
            // Tamanho
            // 
            Tamanho.Text = "Tamanho";
            Tamanho.Width = 62;
            // 
            // Caminho
            // 
            Caminho.Text = "Local";
            Caminho.Width = 40;
            // 
            // tabTop
            // 
            tabTop.BackColor = Color.FromArgb(71, 58, 144);
            tabTop.Controls.Add(lblVer);
            tabTop.Controls.Add(btnSair);
            tabTop.Controls.Add(lblTitulo);
            tabTop.Dock = DockStyle.Top;
            tabTop.Location = new Point(0, 0);
            tabTop.Name = "tabTop";
            tabTop.Size = new Size(305, 30);
            tabTop.TabIndex = 2;
            tabTop.MouseDown += tabTop_MouseDown;
            // 
            // lblVer
            // 
            lblVer.AutoSize = true;
            lblVer.Dock = DockStyle.Right;
            lblVer.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblVer.ForeColor = Color.White;
            lblVer.Location = new Point(257, 0);
            lblVer.Name = "lblVer";
            lblVer.Size = new Size(17, 17);
            lblVer.TabIndex = 2;
            lblVer.Text = "...";
            lblVer.TextAlign = ContentAlignment.MiddleCenter;
            lblVer.MouseDown += tabTop_MouseDown;
            // 
            // btnSair
            // 
            btnSair.BackColor = Color.FromArgb(155, 110, 254);
            btnSair.Dock = DockStyle.Right;
            btnSair.FlatAppearance.BorderColor = Color.FromArgb(91, 78, 144);
            btnSair.FlatAppearance.MouseOverBackColor = Color.FromArgb(175, 130, 254);
            btnSair.FlatStyle = FlatStyle.Flat;
            btnSair.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSair.ForeColor = Color.WhiteSmoke;
            btnSair.Location = new Point(274, 0);
            btnSair.Name = "btnSair";
            btnSair.Size = new Size(31, 30);
            btnSair.TabIndex = 1;
            btnSair.Text = "X";
            btnSair.UseVisualStyleBackColor = false;
            btnSair.Click += btnSair_Click;
            // 
            // lblTitulo
            // 
            lblTitulo.Dock = DockStyle.Left;
            lblTitulo.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(0, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(100, 30);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "ReleaseZipper";
            lblTitulo.TextAlign = ContentAlignment.MiddleCenter;
            lblTitulo.MouseDown += tabTop_MouseDown;
            // 
            // btnCompactar
            // 
            btnCompactar.BackColor = Color.FromArgb(155, 110, 254);
            btnCompactar.FlatAppearance.BorderColor = Color.FromArgb(91, 78, 144);
            btnCompactar.FlatAppearance.MouseOverBackColor = Color.FromArgb(175, 130, 254);
            btnCompactar.FlatStyle = FlatStyle.Flat;
            btnCompactar.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCompactar.ForeColor = Color.White;
            btnCompactar.Location = new Point(154, 225);
            btnCompactar.Name = "btnCompactar";
            btnCompactar.Size = new Size(139, 30);
            btnCompactar.TabIndex = 3;
            btnCompactar.Text = "Compactar";
            btnCompactar.UseVisualStyleBackColor = false;
            btnCompactar.Click += btnCompactar_Click;
            // 
            // ReleaseZipper
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(28, 30, 42);
            ClientSize = new Size(305, 268);
            Controls.Add(btnCompactar);
            Controls.Add(tabTop);
            Controls.Add(listViewArquivos);
            Controls.Add(btnSelecionarArquivos);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ReleaseZipper";
            StartPosition = FormStartPosition.CenterScreen;
            tabTop.ResumeLayout(false);
            tabTop.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSelecionarArquivos;
        private ListView listViewArquivos;
        private Panel tabTop;
        private Button btnCompactar;
        private ColumnHeader NomeDoArquivo;
        private ColumnHeader Tamanho;
        private ColumnHeader Caminho;
        private Label lblTitulo;
        private Button btnSair;
        private Label lblVer;
    }
}
