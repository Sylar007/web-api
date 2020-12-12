using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
	[Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentModelFileController : ControllerBase
    {
		private IEquipmentModelFileService _equipmentModelFileService;
		private IMediaService _mediaService;
		private readonly AppSettings _appSettings;

		public EquipmentModelFileController(
			IEquipmentModelFileService equipmentModelFileService, IMediaService mediaService, IOptions<AppSettings> appSettings)
		{
			_equipmentModelFileService = equipmentModelFileService;
			_mediaService = mediaService;
			_appSettings = appSettings.Value;
		}
		[HttpGet]
		[Route("/EquipmentModelFile/GetFileList/{equipmentModelId}")]
		public string GetFileList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelFileList = _equipmentModelFileService.GetFileList(equipmentModelId);
			return JsonConvert.SerializeObject(equipmentModelFileList);
		}

		[DisableRequestSizeLimit]
		[HttpPost]
		[Route("/EquipmentModelFile/UploadFiles")]
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
					equipment_model_file fileData = new equipment_model_file
					{
						media_id = media_id,
						file_type = extension,
						equipment_model_id = model.id,
						content_type = contentType
					};
					_equipmentModelFileService.AddEquipmentModelFile(fileData);
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

			FileDownload equipmentFile = _equipmentModelFileService.GetMediaName(id);
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

		[HttpPost]
		[Route("EquipmentModelFile/DeleteFiles")]
		public bool DeleteFiles(dynamic equipmentModelFileDto)
		{
			try
			{
				string[] array = Convert.ToString(equipmentModelFileDto.dataList).Split(new char[1]
				{
				','
				});
				int[] equipmentModelFileIds = Array.ConvertAll(array, (string s) => int.Parse(s));
				return _equipmentModelFileService.DeleteEquipmentModelFile(equipmentModelFileIds);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}
}