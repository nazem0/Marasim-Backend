using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using ViewModels.ReactsViewModel;

namespace API.Controllers
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class ReactsController : ControllerBase
    {
        private readonly PostManager PostManager;
        private readonly ReactsManager ReactsManager;
        private readonly VendorManager VendorManager;
        public ReactsController
            (PostManager _PostManager,
            ReactsManager _ReactsManager,
            VendorManager _VendorManager)
        {
            PostManager = _PostManager;
            ReactsManager = _ReactsManager;
            VendorManager = _VendorManager;
        }

        public IActionResult GetReactsByPostID(int PostID)
        {
            var Data = ReactsManager.GetByPostID(PostID);
            return new JsonResult(Data);
        }

        public IActionResult Add(AddReactViewModel AddReact)
        {
            if (!ModelState.IsValid)
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);
            }
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ReactsManager.Get(AddReact.ToModel(UserId!).ID) is null)
            {
                ReactsManager.Add(AddReact.ToModel(UserId!));
                ReactsManager.Save();
                return Ok();
            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);
            }
        }

        public IActionResult Delete(int ReactID)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var React = ReactsManager.Get(ReactID).FirstOrDefault();
            if (React is not null && React.UserID == UserId)
            {
                ReactsManager.Delete(React);
                return Ok();
            }
            else
            {
                var str = new StringBuilder();
                foreach (var item in ModelState.Values)
                {
                    foreach (var item1 in item.Errors)
                    {
                        str.Append(item1.ErrorMessage);
                    }
                }
                return BadRequest(str);
            }
        }
    }
}
