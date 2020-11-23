using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Entities;
using WebApi.Helpers;
using WebApi.Models.Tasks;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WOFileController : ControllerBase
    {
        private IWOFileService _woFileService;
        private IMediaService _mediaService;
        private readonly AppSettings _appSettings;
        public WOFileController(
            IWOFileService woFileService, IMediaService mediaService, IOptions<AppSettings> appSettings)
        {
            _woFileService = woFileService;
            _mediaService = mediaService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("/WOFile/GetWOFileList/{woId}")]
        public string GetWOFileList(int woId)
        {
            IEnumerable<object> wOFileList = _woFileService.GetWOFileList(woId, "WO");
            return JsonConvert.SerializeObject(wOFileList);
        }

        [Route("/WOFile/GetWOExFileList/{woId}")]
        public string GetWOExFileList(int woId)
        {
            IEnumerable<object> wOFileList = _woFileService.GetWOFileList(woId, "WOEx");
            return JsonConvert.SerializeObject(wOFileList);
        }

        [Route("WOFile/GetWORejectFileList/{woId}")]
        public string GetWORejectFileList(int woId)
        {
            IEnumerable<object> wOFileList = _woFileService.GetWOFileList(woId, "WOReject");
            return JsonConvert.SerializeObject(wOFileList);
        }
        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("/WOFile/UploadFiles")]
        public async Task<HttpResponseMessage> UploadFilesAsync([FromForm]FileData model)
        {
            try
            {

                var filePath = _appSettings.MediaPath;

                using (var stream = new FileStream(Path.Combine(filePath, model.file.FileName), FileMode.Create))
                {
                    await model.file.CopyToAsync(stream);

                    string fileName = Path.GetFileNameWithoutExtension(model.file.FileName);
                    string path = Path.Combine(filePath, fileName);
                    var extension = Path.GetExtension(model.file.FileName);
                    var contentType = model.file.ContentType;
                    media data = new media
                    {
                        file_name = fileName,
                        dt_created = DateTime.Now,
                        created_by = 1//UserService.GetLoggedInUserId(base.Request)
                    };
                    int media_id = _mediaService.AddMedia(data);
                    wo_file fileData = new wo_file
                    {
                        media_id = media_id,
                        file_type = extension,
                        wo_id = model.woId,
                        content_type = contentType
                    };
                    _woFileService.AddWOFile(fileData);
                }
            }
            catch (Exception arg)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpPost]
        [Route("WOFile/DeleteFiles")]
        public bool DeleteFiles(dynamic wOFileDto)
        {
            try
            {
                string[] array = Convert.ToString(wOFileDto.dataList).Split(new char[1]
                {
                ','
                });
                int[] array2 = Array.ConvertAll(array, (string s) => int.Parse(s));
                dynamic val = _woFileService.DeleteWOFile(array2, Convert.ToString(wOFileDto.fileType));
                return val;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("/WOFile/DownloadFileFromFileSystem/{id}")]
        public async Task<IActionResult> DownloadFileFromFileSystem(int id)
        {

            var idClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase));
            var deptClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("department", StringComparison.InvariantCultureIgnoreCase));
            var roleClaim = User.Claims.FirstOrDefault(x => x.Type.Equals("role", StringComparison.InvariantCultureIgnoreCase));

            string vId, vDept, vRole;
            if (idClaim != null)
            {
                vId = idClaim.Value;
            }
            if (deptClaim != null)
            {
                vDept = idClaim.Value;
            }
            if (roleClaim != null)
            {
                vRole = idClaim.Value;
            }

            FileDownload wOFile = _woFileService.GetMediaName(id);
            if (wOFile == null) return null;
            var filePath = _appSettings.MediaPath;
            var memory = new MemoryStream();
            using (var stream = new FileStream(Path.Combine(filePath, wOFile.name + wOFile.fileType), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, wOFile.contentType, wOFile.name + wOFile.fileType);
        }


    }
}