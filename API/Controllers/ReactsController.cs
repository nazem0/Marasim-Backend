﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Repository;
using System.Security.Claims;
using System.Text;
using ViewModels.ReactViewModels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReactController : ControllerBase
    {
        private readonly ReactManager ReactManager;
        public ReactController
            (ReactManager _ReactManager)
        {
            ReactManager = _ReactManager;
        }

        [HttpGet("GetReactsByPostId/{PostId}")]
        public IActionResult GetReactsByPostId(int PostId)
        {
            var Data = ReactManager.GetByPostId(PostId)
                         .Select(r => r.ToViewModel(r.User));

            return new JsonResult(Data);
        }

        [HttpGet("GetIsLiked/{PostId}")]
        public IActionResult GetIsLiked(int PostId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (ReactManager.GetByPostId(PostId).Any(r => r.UserId == UserId))
            {
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }
        }

        [Authorize]
        [HttpPost("Add")]
        public IActionResult Add([FromForm] AddReactViewModel AddReact)
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

            if (!ReactManager.IsLiked(UserId!, AddReact.PostId))
            {
                ReactManager.Add(AddReact.ToModel(UserId!));
                ReactManager.Save();
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

        [Authorize]
        [HttpDelete("Delete/{PostId}")]
        public IActionResult Delete(int PostId)
        {
            var UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var React = ReactManager.GetByPostId(PostId).Where(r => r.UserId == UserId).FirstOrDefault();
            if (React is not null && React.UserId == UserId)
            {
                ReactManager.Delete(React);
                ReactManager.Save();
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
