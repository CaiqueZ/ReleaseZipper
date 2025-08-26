using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReleaseZipper
{
    public partial class ReleaseZipper : Form
    {
        // Lista para armazenar o caminho completo de cada arquivo que o usu�rio seleciona.
        // Usamos uma lista separada para manter os dados, independente da interface gr�fica.
        private List<string> arquivosSelecionados = new List<string>();

        // --- In�cio: C�digo para mover a janela sem bordas ---
        // Este bloco de c�digo permite que o usu�rio clique e arraste a janela
        // a partir de um controle (como um Panel ou PictureBox), simulando uma barra de t�tulo.

        // Constantes da API do Windows que definem mensagens do mouse.
        private const int WM_NCLBUTTONDOWN = 0xA1; // Mensagem: bot�o esquerdo do mouse pressionado em uma �rea n�o-cliente.
        private const int HT_CAPTION = 0x2;       // Posi��o: na barra de t�tulo.

        // Importa a fun��o 'ReleaseCapture' da user32.dll.
        // Ela libera a captura do mouse de uma janela.
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        // Importa a fun��o 'SendMessage' da user32.dll.
        // Ela envia uma mensagem espec�fica para uma janela.
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        // --- Fim: C�digo para mover a janela sem bordas ---

        /// <summary>
        /// Construtor do formul�rio. � executado quando a janela � criada.
        /// </summary>
        public ReleaseZipper()
        {
            // M�todo gerado automaticamente que inicializa todos os componentes visuais do formul�rio.
            InitializeComponent();
            // Define o texto do r�tulo de vers�o para a vers�o atual do aplicativo.
            string version = Assembly
        .GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion ?? "dev";

            lblVer.Text = $"Vers�o {version}";

        }

        /// <summary>
        /// Evento disparado quando o bot�o "Selecionar Arquivos" � clicado.
        /// </summary>
        private void btnSelecionarArquivos_Click(object sender, EventArgs e)
        {
            // O 'using' garante que o objeto 'openFileDialog' ser� descartado corretamente da mem�ria.
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Permite que o usu�rio selecione m�ltiplos arquivos de uma vez.
                openFileDialog.Multiselect = true;
                // Define o t�tulo que aparecer� na janela de sele��o de arquivos.
                openFileDialog.Title = "Selecione os arquivos para compactar";
                // Define o filtro de arquivos (neste caso, mostra todos os tipos).
                openFileDialog.Filter = "Todos os arquivos (*.*)|*.*";

                // Exibe a janela de di�logo e verifica se o usu�rio clicou em "OK".
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Adiciona os caminhos dos arquivos selecionados � nossa lista de dados.
                    arquivosSelecionados.AddRange(openFileDialog.FileNames);
                    // Chama o m�todo para atualizar a interface gr�fica com os novos arquivos.
                    AtualizarListViewDeArquivos();
                }
            }
        }

        /// <summary>
        /// Evento disparado quando o bot�o "Compactar" � clicado.
        /// </summary>
        private void btnCompactar_Click(object sender, EventArgs e)
        {
            // Valida��o: verifica se algum arquivo foi selecionado antes de prosseguir.
            if (arquivosSelecionados.Count == 0)
            {
                MessageBox.Show("Por favor, selecione ao menos um arquivo para compactar.", "Nenhum arquivo selecionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Encerra a execu��o do m�todo se nenhum arquivo foi selecionado.
            }

            // Sugest�o de melhoria: Usar um SaveFileDialog aqui!
            // Por enquanto, o caminho do arquivo ZIP est� fixo na �rea de Trabalho.
            string zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "release.zip");

            // O bloco 'try...catch' � usado para capturar poss�veis erros durante a compacta��o.
            try
            {
                // Se o arquivo ZIP j� existir, ele ser� exclu�do para criar um novo.
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }

                // Abre (cria) o arquivo ZIP no modo de cria��o.
                using (var newZipFile = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    // Percorre cada caminho de arquivo na nossa lista de dados.
                    foreach (var file in arquivosSelecionados)
                    {
                        // Verifica se o arquivo realmente existe no caminho especificado.
                        if (File.Exists(file))
                        {
                            // Adiciona o arquivo ao ZIP. O segundo par�metro � o nome que ele ter� dentro do ZIP.
                            newZipFile.CreateEntryFromFile(file, Path.GetFileName(file));
                        }
                    }
                }

                // Exibe uma mensagem de sucesso para o usu�rio.
                MessageBox.Show($"Arquivos compactados com sucesso em:\n{zipFilePath}", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa a sele��o ap�s a compacta��o bem-sucedida.
                LimparSelecao();
            }
            catch (Exception ex)
            {
                // Se um erro ocorrer, exibe uma mensagem detalhando o erro.
                MessageBox.Show($"Ocorreu um erro ao compactar os arquivos:\n{ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Atualiza o componente ListView com os arquivos da lista 'arquivosSelecionados'.
        /// </summary>
        private void AtualizarListViewDeArquivos()
        {
            // Limpa todos os itens existentes no ListView para evitar duplicatas.
            listViewArquivos.Items.Clear();

            // Percorre cada caminho de arquivo na nossa lista de dados.
            foreach (var caminhoCompleto in arquivosSelecionados)
            {
                // Cria um objeto FileInfo para obter detalhes do arquivo (como tamanho).
                FileInfo info = new FileInfo(caminhoCompleto);

                // Cria um novo item para o ListView com o nome do arquivo.
                ListViewItem item = new ListViewItem(info.Name);

                // Adiciona os "sub-itens" que correspondem �s outras colunas.
                item.SubItems.Add(FormatBytes(info.Length)); // Coluna "Tamanho", formatado.
                item.SubItems.Add(info.DirectoryName);      // Coluna "Caminho".

                // Adiciona o item rec�m-criado (a linha inteira) ao ListView.
                listViewArquivos.Items.Add(item);
            }
        }

        /// <summary>
        /// Limpa a lista de dados e a interface gr�fica.
        /// </summary>
        private void LimparSelecao()
        {
            // Limpa a lista de dados que armazena os caminhos dos arquivos.
            arquivosSelecionados.Clear();
            // Chama o m�todo que atualiza a interface, que por sua vez limpar� o ListView.
            AtualizarListViewDeArquivos();
        }

        /// <summary>
        /// Evento que permite mover a janela ao clicar e arrastar um controle (ex: um Panel).
        /// </summary>
        private void tabTop_MouseDown(object sender, MouseEventArgs e)
        {
            // Verifica se o bot�o esquerdo do mouse foi pressionado.
            if (e.Button == MouseButtons.Left)
            {
                // Libera a captura do mouse.
                ReleaseCapture();
                // Envia uma mensagem ao Windows para "engan�-lo", dizendo que o usu�rio
                // est� clicando na barra de t�tulo, permitindo que a janela seja movida.
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// Fun��o auxiliar para formatar um tamanho em bytes para KB, MB, GB, etc.
        /// </summary>
        /// <param name="bytes">O n�mero de bytes a ser formatado.</param>
        /// <returns>Uma string representando o tamanho formatado.</returns>
        private static string FormatBytes(long bytes)
        {
            // Array com os sufixos de tamanho.
            string[] sufixos = { "B", "KB", "MB", "GB", "TB" };
            int contador = 0;
            double numero = (double)bytes;

            // Divide o n�mero por 1024 at� que ele seja menor que 1024.
            while (numero >= 1024 && contador < sufixos.Length - 1)
            {
                numero /= 1024;
                contador++;
            }
            // Retorna o n�mero formatado com uma casa decimal e o sufixo apropriado.
            return string.Format("{0:0.0} {1}", numero, sufixos[contador]);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}