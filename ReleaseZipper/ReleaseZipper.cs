using System.IO.Compression;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReleaseZipper
{
    public partial class ReleaseZipper : Form
    {
        // Lista para armazenar o caminho completo de cada arquivo que o usuário seleciona.
        // Usamos uma lista separada para manter os dados, independente da interface gráfica.
        private List<string> arquivosSelecionados = new List<string>();

        // --- Início: Código para mover a janela sem bordas ---
        // Este bloco de código permite que o usuário clique e arraste a janela
        // a partir de um controle (como um Panel ou PictureBox), simulando uma barra de título.

        // Constantes da API do Windows que definem mensagens do mouse.
        private const int WM_NCLBUTTONDOWN = 0xA1; // Mensagem: botão esquerdo do mouse pressionado em uma área não-cliente.
        private const int HT_CAPTION = 0x2;       // Posição: na barra de título.

        // Importa a função 'ReleaseCapture' da user32.dll.
        // Ela libera a captura do mouse de uma janela.
        [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        // Importa a função 'SendMessage' da user32.dll.
        // Ela envia uma mensagem específica para uma janela.
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        // --- Fim: Código para mover a janela sem bordas ---

        /// <summary>
        /// Construtor do formulário. É executado quando a janela é criada.
        /// </summary>
        public ReleaseZipper()
        {
            // Método gerado automaticamente que inicializa todos os componentes visuais do formulário.
            InitializeComponent();
            // Define o texto do rótulo de versão para a versão atual do aplicativo.
            string version = Assembly
        .GetExecutingAssembly()
        .GetCustomAttribute<AssemblyInformationalVersionAttribute>()?
        .InformationalVersion ?? "dev";

            lblVer.Text = $"Versão {version}";

        }

        /// <summary>
        /// Evento disparado quando o botão "Selecionar Arquivos" é clicado.
        /// </summary>
        private void btnSelecionarArquivos_Click(object sender, EventArgs e)
        {
            // O 'using' garante que o objeto 'openFileDialog' será descartado corretamente da memória.
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // Permite que o usuário selecione múltiplos arquivos de uma vez.
                openFileDialog.Multiselect = true;
                // Define o título que aparecerá na janela de seleção de arquivos.
                openFileDialog.Title = "Selecione os arquivos para compactar";
                // Define o filtro de arquivos (neste caso, mostra todos os tipos).
                openFileDialog.Filter = "Todos os arquivos (*.*)|*.*";

                // Exibe a janela de diálogo e verifica se o usuário clicou em "OK".
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Adiciona os caminhos dos arquivos selecionados à nossa lista de dados.
                    arquivosSelecionados.AddRange(openFileDialog.FileNames);
                    // Chama o método para atualizar a interface gráfica com os novos arquivos.
                    AtualizarListViewDeArquivos();
                }
            }
        }

        /// <summary>
        /// Evento disparado quando o botão "Compactar" é clicado.
        /// </summary>
        private void btnCompactar_Click(object sender, EventArgs e)
        {
            // Validação: verifica se algum arquivo foi selecionado antes de prosseguir.
            if (arquivosSelecionados.Count == 0)
            {
                MessageBox.Show("Por favor, selecione ao menos um arquivo para compactar.", "Nenhum arquivo selecionado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Encerra a execução do método se nenhum arquivo foi selecionado.
            }

            // Sugestão de melhoria: Usar um SaveFileDialog aqui!
            // Por enquanto, o caminho do arquivo ZIP está fixo na Área de Trabalho.
            string zipFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "release.zip");

            // O bloco 'try...catch' é usado para capturar possíveis erros durante a compactação.
            try
            {
                // Se o arquivo ZIP já existir, ele será excluído para criar um novo.
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }

                // Abre (cria) o arquivo ZIP no modo de criação.
                using (var newZipFile = ZipFile.Open(zipFilePath, ZipArchiveMode.Create))
                {
                    // Percorre cada caminho de arquivo na nossa lista de dados.
                    foreach (var file in arquivosSelecionados)
                    {
                        // Verifica se o arquivo realmente existe no caminho especificado.
                        if (File.Exists(file))
                        {
                            // Adiciona o arquivo ao ZIP. O segundo parâmetro é o nome que ele terá dentro do ZIP.
                            newZipFile.CreateEntryFromFile(file, Path.GetFileName(file));
                        }
                    }
                }

                // Exibe uma mensagem de sucesso para o usuário.
                MessageBox.Show($"Arquivos compactados com sucesso em:\n{zipFilePath}", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Limpa a seleção após a compactação bem-sucedida.
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

                // Adiciona os "sub-itens" que correspondem às outras colunas.
                item.SubItems.Add(FormatBytes(info.Length)); // Coluna "Tamanho", formatado.
                item.SubItems.Add(info.DirectoryName);      // Coluna "Caminho".

                // Adiciona o item recém-criado (a linha inteira) ao ListView.
                listViewArquivos.Items.Add(item);
            }
        }

        /// <summary>
        /// Limpa a lista de dados e a interface gráfica.
        /// </summary>
        private void LimparSelecao()
        {
            // Limpa a lista de dados que armazena os caminhos dos arquivos.
            arquivosSelecionados.Clear();
            // Chama o método que atualiza a interface, que por sua vez limpará o ListView.
            AtualizarListViewDeArquivos();
        }

        /// <summary>
        /// Evento que permite mover a janela ao clicar e arrastar um controle (ex: um Panel).
        /// </summary>
        private void tabTop_MouseDown(object sender, MouseEventArgs e)
        {
            // Verifica se o botão esquerdo do mouse foi pressionado.
            if (e.Button == MouseButtons.Left)
            {
                // Libera a captura do mouse.
                ReleaseCapture();
                // Envia uma mensagem ao Windows para "enganá-lo", dizendo que o usuário
                // está clicando na barra de título, permitindo que a janela seja movida.
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        /// <summary>
        /// Função auxiliar para formatar um tamanho em bytes para KB, MB, GB, etc.
        /// </summary>
        /// <param name="bytes">O número de bytes a ser formatado.</param>
        /// <returns>Uma string representando o tamanho formatado.</returns>
        private static string FormatBytes(long bytes)
        {
            // Array com os sufixos de tamanho.
            string[] sufixos = { "B", "KB", "MB", "GB", "TB" };
            int contador = 0;
            double numero = (double)bytes;

            // Divide o número por 1024 até que ele seja menor que 1024.
            while (numero >= 1024 && contador < sufixos.Length - 1)
            {
                numero /= 1024;
                contador++;
            }
            // Retorna o número formatado com uma casa decimal e o sufixo apropriado.
            return string.Format("{0:0.0} {1}", numero, sufixos[contador]);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}