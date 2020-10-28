using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebSpeechAPI.Controllers
{
    public class SpeechToTextAPIController : ApiController
    {
        [HttpPost]
        [ActionName("Upload_AudioFile")]
        public HttpResponseMessage Upload_AudioFile()
        {
            //Create the Directory.
            string path = HttpContext.Current.Server.MapPath("~/UploadedAudioFiles/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //Fetch the File.
            HttpPostedFile postedFile = HttpContext.Current.Request.Files[0];

            //Fetch the File Name.
            string fileName = HttpContext.Current.Request.Form["fileName"];
            if (fileName.Contains(".mp3"))
            {
                fileName = fileName.Replace(" ", "_").Replace(".mp3", "");
                fileName = fileName + "$" + DateTime.Now.ToString("yyyyMMdd") + DateTime.Now.ToString("HHmmss") + ".mp3";
            }
            //Save the File.
            postedFile.SaveAs(path + fileName);

            //Send OK Response to Client.
            return Request.CreateResponse(HttpStatusCode.OK, fileName);
        }
    }
}
