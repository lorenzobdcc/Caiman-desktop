using System;
using System.IO;
using System.Net;

namespace Caiman.models
{
    public class Game
    {
        private const string DEFAULT_GAME_NAME = "defaultGame";
        private const string DEFAULT_DESCRIPTION_NAME = "defaultDescription";
        private const string DEFAULT_IMAGE_NAME = "defaultImageName";
        private const string DEFAULT_PATH = "defaultpath.jpg";
        private const string PATH_IMG_CAIMAN = @"Caiman\img\";
        private const string URL_IMAGES_CAIMAN = "http://caiman.cfpt.info/img/games/";
        public int id;
        public string name;
        public string description;
        public string imageName;
        public int idConsole;
        public int idFile;
        public string imgPath;

        public Game(int idp, string namep, string descriptionp, string imageNamep, int idConsolep, int idFilep) : base()
        {

            imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            id = idp;
            name = namep;
            description = descriptionp;
            imageName = imageNamep;
            idConsole = idConsolep;
            idFile = idFilep;
            if (imageName != DEFAULT_IMAGE_NAME)
            {
                DownloadImage();
            }
        }
        public Game()
        {
            imgPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), PATH_IMG_CAIMAN);
            id = 0;
            name = DEFAULT_GAME_NAME;
            description = DEFAULT_DESCRIPTION_NAME;
            imageName = DEFAULT_IMAGE_NAME;
            idConsole = 0;
            idFile = 0;
        }


        private void DownloadImage()
        {

            if (!File.Exists(imgPath + imageName))
            {
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(new Uri(URL_IMAGES_CAIMAN + imageName), (imgPath + imageName));
                }
            }
        }
    }
}
