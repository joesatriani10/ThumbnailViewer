namespace ThumbnailViewer
{
    public partial class ImageViewer : Form
    {
        public ImageViewer(string folderPath)
        {
            InitializeComponent();
            this.folderPath = folderPath;
        }

        private string folderPath = "";

        private void ImageViewerLoad(object sender, EventArgs e)
        {            
            loadImages();
        }

        private void loadImages()
        {
            DirectoryInfo dInfo = new DirectoryInfo(folderPath);

            FileInfo[] files = dInfo.GetFiles("*.tif");// Or any other images in folder
           
            var imageList = new ImageList();
            imageList.ImageSize = new Size(240, 320);

            foreach (FileInfo file in files)
            {
                imageList.Images.Add(file.Name, Image.FromFile(file.FullName));
            }

            // tell your ListView to use the new image list
            listView.LargeImageList = imageList;
            var n = 0;
            foreach (FileInfo file in files)
            {
                listView.Items.Add(file.Name, n++);
            }

        }

        private void mouseClickPreview(object sender, MouseEventArgs e)
        {            
            String selected = Path.Join(folderPath, listView.SelectedItems[0].Text);

            Form formView = new Form();

            formView.FormBorderStyle = FormBorderStyle.FixedSingle;
            formView.MaximizeBox = false;
            formView.MinimizeBox = false;
            formView.Width = 576;
            formView.Height = 768;

            var pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            pictureBox.Image = Image.FromFile(selected);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

            formView.Controls.Add(pictureBox);
            formView.ShowDialog();
            
            pictureBox.Dispose();
            formView.Dispose();            
        }        
    }
}