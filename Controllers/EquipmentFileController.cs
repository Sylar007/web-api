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
					media data = new media
					{
						file_name = fileName,
						dt_created = DateTime.Now,
						created_by = 1//UserService.GetLoggedInUserId(base.Request)
					};
					int media_id = _mediaService.AddMedia(data);
					equipment_file fileData = new equipment_file
					{
						media_id = media_id,
						file_type = extension,
						equipment_id = model.id
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

		//[HttpPost]
		//[Route("EquipmentFile/UploadFiles")]
		//public JsonResult<string> UploadFiles(int equipmentId, string fileType)
		//{
		//	try
		//	{
		//		foreach (string file in HttpContext.Current.Request.Files)
		//		{
		//			HttpPostedFile httpPostedFile = HttpContext.Current.Request.Files[file];
		//			if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
		//			{
		//				Stream inputStream = httpPostedFile.InputStream;
		//				string fileName = httpPostedFile.FileName;
		//				string text = WebConfigurationManager.AppSettings["MediaPath"] + fileType;
		//				if (!Directory.Exists(text))
		//				{
		//					Directory.CreateDirectory(text);
		//				}
		//				string path = Path.Combine(text, fileName);
		//				using (FileStream destination = File.Create(path))
		//				{
		//					inputStream.CopyTo(destination);
		//				}
		//				medium data = new medium
		//				{
		//					file_name = fileName,
		//					dt_created = DateTime.Now,
		//					created_by = 1
		//				};
		//				MediaService mediaService = new MediaService();
		//				int media_id = mediaService.AddMedia(data);
		//				equipment_file fileData = new equipment_file
		//				{
		//					media_id = media_id,
		//					file_type = fileType,
		//					equipment_id = equipmentId
		//				};
		//				_equipmentFileService.AddEquipmentFile(fileData);
		//			}
		//		}
		//	}
		//	catch (Exception arg)
		//	{
		//		return Json("Upload failed " + arg);
		//	}
		//	return Json("File uploaded successfully");
		//}

		//[HttpPost]
		//[Route("EquipmentFile/DeleteFiles")]
		//public bool DeleteFiles(dynamic equipmentFileDto)
		//{
		//	try
		//	{
		//		string[] array = Convert.ToString(equipmentFileDto.dataList).Split(new char[1]
		//		{
		//		','
		//		});
		//		int[] equipmentFileIds = Array.ConvertAll(array, (string s) => int.Parse(s));
		//		EquipmentFileService equipmentFileService = new EquipmentFileService();
		//		return equipmentFileService.DeleteEquipmentFile(equipmentFileIds);
		//	}
		//	catch (Exception ex)
		//	{
		//		throw ex;
		//	}
		//}
	}
}