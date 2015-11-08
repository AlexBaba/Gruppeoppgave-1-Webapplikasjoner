using Nettbutikk.DataAccess;
using Nettbutikk.Model;
using System.Collections.Generic;

namespace Nettbutikk.BusinessLogic
{
    public class ImageBLL : IImageLogic
    {

        private IImageRepo repo;

        public ImageBLL() {
            repo = new ImageRepo();
        }

        public ImageBLL(IImageRepo repo) {
            this.repo = repo;
        }

        public bool AddImage(int productId, string imageUrl)
        {
            return repo.AddImage(productId,imageUrl);
        }

        public bool DeleteImage(int imageId, int adminId)
        {
            Image img = repo.GetImage(imageId);

            if (img == null)
                return false;

            if (!repo.AddOldImage(img.ProductId, img.ImageUrl, adminId))
                return false; 

            return repo.DeleteImage(imageId);
        }

        public List<Image> GetAllImages()
        {
            return repo.GetAllImages();
        }

        public Image GetImage(int imageId)
        {
            return repo.GetImage(imageId);
        }

        public bool UpdateImage(int imageId, int productId, string imageUrl, int adminId)
        {

            Image img = repo.GetImage(imageId);

            if (img == null)
                return false;

            if (!repo.AddOldImage(img.ProductId, img.ImageUrl, adminId))
                return false;

            return repo.UpdateImage(imageId, productId, imageUrl);
        }
    }
}
