
using Microsoft.AspNetCore.Http;

namespace Repositories.ResponseModel.GroupModel
{
    public class PutGroupModel
    {
        public string? Name { get; set; }
        public int? Size { get; set; }
        public int? Type { get; set; }
        public IFormFile? Wallpaper { get; set; }
    }
}
