using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;
using System.Net.Http;
using WebApi.Models.Tasks;
using System.IO;
using WebApi.Entities;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Linq;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentFileController : ControllerBase
    {
		private IEquipmentFileService _equipmentFileService;
		private IMediaService _mediaService;
		private readonly AppSettings _appSettings;

		public EquipmentFileController(
			IEquipmentFileService equipmentFileService, IMediaService mediaService, IOptions<AppSettings> appSettings)
		{
			_equipmentFileService = equipmentFileService;
			_mediaService = mediaService;
			_appSettings = appSettings.Value;
		}

		[HttpGet]
		[Route("/EquipmentFile/GetFileList/{equipmentId}")]
		public string GetFileList(int equipmentId)
		{
			IEnumerable<object> equipmentFileList = _equipmentFileService.GetFileList(equipmentId);
			return JsonConvert.SerializeObject(equipmentFileList);
		}

		[DisableRequestSizeLimit]
		[HttpPost]
		[Route("/EquipmentFile/UploadFiles")]
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
					int idClaim = Convert.ToInt32(User.Claims.FirstOrDefault(x => x.Type.Equals("assigned_User_Id", StringComparison.InvariantCultureIgnoreCase)).Value);

					media data = new media
					{
						file_name = fileName,
						dt_created = DateTime.Now,
						created_by = idClaim
					};
					int media_id = _mediaService.AddMedia(data);
					equipment_file fileData = new equipment_file
					{
						media_id = media_id,
						file_type = extension,
						equipment_id = model.id,
						content_type = contentType
					};
					_equipmentFileService.AddEquipmentFile(fileData);
				}
			}
			catch (Exception arg)
			{
				return new HttpResponseMessage(HttpStatusCode.Unauthorized);
			}
			return new HttpResponseMessage(HttpStatusCode.OK);
		}

		[HttpGet]
		[Route("/EquipmentFile/DownloadFileFromFileSystem/{id}")]
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

			FileDownload equipmentFile = _equipmentFileService.GetMediaName(id);
			if (equipmentFile == null) return null;
			var filePath = _appSettings.MediaPath;
			var memory = new MemoryStream();
			using (var stream = new FileStream(Path.Combine(filePath, equipmentFile.name + equipmentFile.fileType), FileMode.Open))
			{
				await stream.CopyToAsync(memory);
			}
			memory.Position = 0;
			return File(memory, equipmentFile.contentType, equipmentFile.name + equipmentFile.fileType);
		}
	}
}