using Blob;

namespace BlobHandler
{
    public partial class Form1 : Form
    {
        private string connectionString = "Your connection string";
        private string containerName = "mycontainer";
        private string blobName { get; set; }
        private FileStream? fs { get; set; }
        private List<Blobs> blobs { get; set; }
        HandleBlob objBlob { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            objBlob = new HandleBlob(connectionString, containerName, blobName, fs);
            lblFileName.Text = string.Empty;
            blobs = new List<Blobs>();
            blobGrid.DataSource = await objBlob.LoadAllBlobs();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "All Files|*.*";
                openFileDialog.Title = "Select a File";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    LoadFile(filePath);
                }
            }
        }

        private void LoadFile(string filePath)
        {
            try
            {
                fs = File.OpenRead(filePath);
                blobName = fs.Name;
                lblFileName.Text = blobName;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                objBlob = new HandleBlob(connectionString, containerName, blobName, fs);
                var blob = await objBlob.UploadFileToBlob();
                if (blob != null)
                {
                    blobs.Add(new Blobs() { blobUrl = blob.AbsoluteUri });
                    blobGrid.DataSource = await objBlob.LoadAllBlobs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while uploading the file: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                blobName = string.Empty;
                fs = null;
            }
        }
    }
}
