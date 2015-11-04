using Nettbutikk.Model;
using System.Collections.Generic;

namespace Nettbutikk.DataAccess
{
    public class ImageRepoStub : IImageRepo
    {
        public bool AddImage(int productId, string imageUrl)
        {
            return productId != -1;
        }

        public bool AddOldImage(Image image, Admin changer)
        {
            return image.Product.Id != -1;
        }

        public bool DeleteImage(int imageId)
        {
            return imageId != -1;
        }

        public List<Image> GetAllImages()
        {
            var allImages = new List<Image> {
                new Image { ImageId = 1, Product = new Product { Id = 1 }, ImageUrl = "test1"},
                new Image { ImageId = 2, Product = new Product { Id = 2 }, ImageUrl = "test2"},
                new Image { ImageId = 3, Product = new Product { Id = 3 }, ImageUrl = "test3"},
                new Image { ImageId = 4, Product = new Product { Id = 4 }, ImageUrl = "test4"}
            };

            return allImages;
        }


        public Image GetImage(int imageId)
        {
            
            return imageId == -1 ? null : new Image { ImageId = imageId, Product = new Product { Id = 1 }, ImageUrl = "test"};
        }


        public bool UpdateImage(int imageId, int productId, string imageUrl)
        {

            return imageId != -1 && productId != -1; 
           
        }

    }
}
