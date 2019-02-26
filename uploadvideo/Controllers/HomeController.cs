using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace uploadvideo.Controllers
{
    public class HomeController : Controller
    {
        private static readonly string _awsAccessKey =
            ConfigurationManager.AppSettings["AWSAccessKey"];

        private static readonly string _awsSecretKey =
            ConfigurationManager.AppSettings["AWSSecretKey"];

        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast2;

        private static IAmazonS3 client = new AmazonS3Client(_awsAccessKey, _awsSecretKey, bucketRegion);
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Uploadvideo()
        {
            try
            {
                var keyDate = DateTime.Now.ToString("MM-dd-yyyy");
                var keyTime = DateTime.Now.ToString("HH:mm:ss");
                string keyname = keyDate + "_" + keyTime + ".mp4";
                HttpPostedFileBase blobData = Request.Files[0];
                var putRequest1 = new PutObjectRequest
                {
                    BucketName = "python-test-osmosis",
                    Key = keyname,
                    ContentType = "video/mp4",
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = blobData.InputStream
                };

                PutObjectResponse response1 = await client.PutObjectAsync(putRequest1);
            }
            catch(Exception ex)
            {

            }
            return Json(new { });
        }
    }
}