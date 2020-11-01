using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WebApi.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using WebApi.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
	[EnableCors()]
	public class EquipmentFileController : ControllerBase
    {
		private IEquipmentFileService _equipmentFileService;

		public EquipmentFileController(
			IEquipmentFileService equipmentFileService)
		{
			_equipmentFileService = equipmentFileService;
		}
		[HttpPost]
		[HttpGet]
		[Route("EquipmentFile/GetEquipmentFileList/{equipmentId}")]
		public string GetEquipmentFileList(int equipmentId)
		{
			IEnumerable<object> equipmentFileList = _equipmentFileService.GetEquipmentFileList(equipmentId, "Equipment");
			return JsonConvert.SerializeObject(equipmentFileList);
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