﻿using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Route.Session3.PL.Helpers
{
	public static class DocumentSettings
	{
		public static async Task<string> UploadFile(IFormFile file, string folderName)
		{

			string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName);

			if (!Directory.Exists(folderPath))
				Directory.CreateDirectory(folderPath);

			string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

			string filePath = Path.Combine(folderPath, fileName);

			using var fileStream = new FileStream(filePath, FileMode.Create);

			await file.CopyToAsync(fileStream);

			return fileName;
		}

		public static void DeleteFile(string fileName, string folderName)
		{

			string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files", folderName, fileName);

			if (File.Exists(filePath))
				File.Delete(filePath);
		}
	}
}
