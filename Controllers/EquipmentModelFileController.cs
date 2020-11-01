using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using WebApi.Helpers;
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

		public EquipmentModelFileController(
			IEquipmentModelFileService equipmentModelFileService)
		{
			_equipmentModelFileService = equipmentModelFileService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentModelFile/GetEquipmentModelFileList/{equipmentModelId}")]
		public string GetEquipmentModelFileList(int equipmentModelId)
		{
			IEnumerable<object> equipmentModelFileList = _equipmentModelFileService.GetEquipmentModelFileList(equipmentModelId, "EquipmentModel");
			return JsonConvert.SerializeObject(equipmentModelFileList);
		}
		//[HttpPost("UploadFiles")]
		//[Route("EquipmentFile/UploadFiles")]
		//public async Task<IActionResult> UploadFiles(List<IFormFile> files)
		//{
		//	long size = files.Sum(f => f.Length);

		//	// full path to file in temp location
		//	var filePath = Path.GetTempFileName();

		//	foreach (var formFile in files)
		//	{
		//		if (formFile.Length > 0)
		//		{
		//			using (var stream = new FileStream(filePath, FileMode.Create))
		//			{
		//				await formFile.CopyToAsync(stream);
		//			}
		//		}
		//	}

		//	// process uploaded files
		//	// Don't rely on or trust the FileName property without validation.

		//	return Ok(new { count = files.Count, size, filePath });
		//}
		//[HttpPost]
		//[Route("EquipmentModelFile/UploadFiles")]
		//public JsonResult<string> UploadFiles(int equipmentModelId, string fileType)
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
		//				equipment_model_file fileData = new equipment_model_file
		//				{
		//					media_id = media_id,
		//					file_type = fileType,
		//					equipment_model_id = equipmentModelId
		//				};
		//				EquipmentModelFileService equipmentModelFileService = new EquipmentModelFileService();
		//				equipmentModelFileService.AddEquipmentModelFile(fileData);
		//			}
		//		}
		//	}
		//	catch (Exception arg)
		//	{
		//		return Json("Upload failed " + arg);
		//	}
		//	return Json("File uploaded successfully");
		//}

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