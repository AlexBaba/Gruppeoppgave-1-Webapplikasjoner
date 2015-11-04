using System;
using System.Collections.Generic;
using System.Linq;
using Nettbutikk.Model;
using Nettbutikk.Model.RemovedEntities;
using System.Data.Entity;

namespace Nettbutikk.DataAccess
{
    public class ImageRepo : IImageRepo
    {


        public Image GetImage(int imageId)
        {
            try {
                return new TankshopDbContext().Images.FirstOrDefault(i => i.ImageId == imageId);
            }
            catch (Exception e) {
                //LogHandler.WriteToLog(e);
                return null;
            }

        }

        public List<Image> GetAllImages()
        {

            try {
                return new TankshopDbContext().Images.ToList();
            }
            catch (Exception e) {
                //LogHandler.WriteToLog(e);
                return new List<Image>();
            }
            
        }


        public bool AddImage(int productId, string imageUrl)
        {

            try
            {
                var db = new TankshopDbContext();
                db.Images.Add(new Image() { Product = new Product { Id = productId }, ImageUrl = imageUrl });
                db.SaveChanges();
                return true;
            }
            catch (Exception e) { }//LogHandler.WriteToLog(e); }

            return false;
            
        }

        public bool DeleteImage(int imageId)
        {

            var db = new TankshopDbContext();

            Image img = (from i in db.Images where i.ImageId == imageId select i).FirstOrDefault();

            if (img == null)
            {
                return false;
            }

            try {
                db.Images.Remove(img);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) { }//LogHandler.WriteToLog(e); }

            return false;
        }

        public bool UpdateImage(int imageId, int productId, string imageUrl)
        {

            var db = new TankshopDbContext();

            Image img = (from i in db.Images where i.ImageId == imageId select i).FirstOrDefault();

            if (img == null)
            {
                return false;
            }

            img.Product = new Product { Id = productId };
            img.ImageUrl = imageUrl;

            db.Entry(img).State = EntityState.Modified;

            try {
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
            }

            return false;
        }


        //OldImage
        public bool AddOldImage(Image image, Admin admin)
        {
            var db = new TankshopDbContext();
            OldImage oldImage = new OldImage();

            oldImage.Product = image.Product;
            oldImage.ImageUrl = image.ImageUrl;
            oldImage.Changer = admin;
            oldImage.Changed = DateTime.Now;
            
            try {
                db.SaveChanges();
                return true;
            }
            catch (Exception e) { }//LogHandler.WriteToLog(e); }

            return false;
        }


    }
}
