using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace lensLook.Pl.Helper
{
	public static class DocumentSetting
	{

		public static async Task<string> UploadFillesAsync(IFormFile File, string FolderName)
		{
			//GetLocated Foled Path
			string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/files", FolderName);
			//2-GetFillesName And Make It Unique
			string FileName = $"{Guid.NewGuid()}{File.FileName}";
			//2-Make File Path
			string FilePath = Path.Combine(FolderPath, FileName);

			//Save File In Server As Streams Shot Per Time 
			using (var Fs = new FileStream(FilePath, FileMode.Create))
			{
				await File.CopyToAsync(Fs);   //ToCreate File
				return FileName;
			}


			//IF File Is Excite It make Override This Use IT in Update 
		}

		public static void DeleteFile(string Filename, string folderName)
		{
			if (Filename is not null && folderName is not null)
			{

				String FilePATH = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Filles", folderName, Filename);
				if (File.Exists(FilePATH))
				{
					File.Delete(FilePATH);
				}
			}

		}


	}
}
