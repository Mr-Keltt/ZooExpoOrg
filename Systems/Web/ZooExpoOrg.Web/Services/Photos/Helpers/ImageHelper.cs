using Microsoft.AspNetCore.Components.Forms;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace ZooExpoOrg.Web.Services.Photos;

public static class ImageHelper
{
	public static async Task<byte[]> ConvertToByteArray(IBrowserFile file)
	{
        using (var memoryStream = new MemoryStream())
		{
			await file.OpenReadStream().CopyToAsync(memoryStream);
			memoryStream.Seek(0, SeekOrigin.Begin);

			return memoryStream.ToArray();
		}
	}
}
