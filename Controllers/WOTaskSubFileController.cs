using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using WebApi.Entities;
using Newtonsoft.Json;
using WebApi.Models.Equipment;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading.Tasks;
using WebApi.Models.Tasks;
using System.IO;
using System.Linq;
using System.Net;

namespace WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WOTaskSubFileController : ControllerBase
    {
        private IWOTaskSubFileService _woTaskSubFileService;
        private IMediaService _mediaService;
        private readonly AppSettings _appSettings;

        public WOTaskSubFileController(
            IWOTaskSubFileService woTaskSubFileService, IMediaService mediaService, IOptions<AppSettings> appSettings)
        {
            _woTaskSubFileService = woTaskSubFileService;
            _mediaService = mediaService;
            _appSettings = appSettings.Value;
        }

        [HttpGet]
        [Route("/WOTaskSubFile/GetWOTaskSubFileList/{woid}/{task_sub_id}/{upload_type}")]
        public string GetWOTaskSubFileList(int woId, int task_sub_id, int upload_type)
        {
            IEnumerable<object> wOTaskSubFileList = _woTaskSubFileService.GetWOTaskSubFileList(woId, task_sub_id, upload_type);
            return JsonConvert.SerializeObject(wOTaskSubFileList);
        }

        [DisableRequestSizeLimit]
        [HttpPost]
        [Route("/WOTaskSubFile/UploadFiles/{task_sub_id}/{upload_type}")]
        public async Task<HttpResponseMessage> UploadFilesAsync(int task_sub_id, int upload_type, [FromForm]FileData model)
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
                    int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);

                    media data = new media
                    {
                        file_name = fileName,
                        dt_created = DateTime.Now,
                        created_by = idClaim
                    };
                    int media_id = _mediaService.AddMedia(data);
                    wo_task_sub_file fileData = new wo_task_sub_file
                    {
                        media_id = media_id,
                        file_type = extension,
                        wo_id = model.id,
                        content_type = contentType,
                        task_sub_id = task_sub_id,
                        upload_type = upload_type
                    };
                    _woTaskSubFileService.AddWOtasksubFile(fileData);
                }
            }
            catch (Exception arg)
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpGet]
        [Route("/WOTaskSubFile/DownloadFileFromFileSystem/{id}")]
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

            FileDownload wOFile = _woTaskSubFileService.GetMediaName(id);
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