using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace OFX.Models
{
    public class Upload
    {
        private readonly IHostingEnvironment _hostingEnv;

        public Upload(IHostingEnvironment hostingEnv)
        {
            _hostingEnv = hostingEnv;
        }

        public List<string> UploadFile(IEnumerable<IFormFile> files)
        {
            List<string> listPath = new List<string>();

            foreach (var file in files)
            {
                string nameFile = file.FileName + "_" + DateTime.Now.Month.ToString()
                                                + "_" + DateTime.Now.Day.ToString()
                                                + "_" + DateTime.Now.Year.ToString()
                                                + "_" + DateTime.Now.Hour.ToString()
                                                + "_" + DateTime.Now.Minute.ToString()
                                                + "_" + DateTime.Now.Second.ToString()
                                                + "_" + DateTime.Now.Millisecond.ToString();
                string path = _hostingEnv.WebRootPath + "\\Files" + "\\Upload" + "\\" + nameFile;

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyToAsync(stream);
                    listPath.Add(path);
                }
            }

            return listPath;
        }
    }
}
