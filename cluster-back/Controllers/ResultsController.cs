using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cluster_back.Services;
using Entities.Models;

namespace cluster_back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ResultsController : ControllerBase
    {
        private IRepositoryWrapper _wrapper;
        UserService userService;
        public ResultsController(IRepositoryWrapper wrapper,
            ILogger<UsersController> logger)
        {
            this.userService = new UserService(wrapper, logger);
            this._wrapper = wrapper;
        }
        //[HttpGet]
        //public async Task<IActionResult> GetAllUsers()
        //{
        //    var currentUser = await _wrapper.User.GetByUsername(User.Identity.Name);
        //    if (currentUser.Role == "Admin")
        //    {
        //        var users = _wrapper.User.FindAll();
        //        return Ok(users);
        //    }
        //    return Forbid("You don't have acces for this resource");
        //}
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            string username = User.Identity.Name;
            User user = await _wrapper.User.FindByCondition(u => u.UserName == username).FirstOrDefaultAsync();
            if (user != null)
            {
                List<ClusteringResult> results = _wrapper.ClusteringResults.FindByCondition(r => r.UserId == user.Id).ToList();
                results = results.OrderBy(r => r.Created).Reverse().ToList();
                return Ok(results);
            }
            return NotFound("Such user does not exists");
        }
        [HttpGet("history/{id}")]
        public async Task<IActionResult> GetHistoryById(Guid id)
        {
            string username = User.Identity.Name;
            User user = await _wrapper.User.FindByCondition(u => u.UserName == username).FirstOrDefaultAsync();
            if (user != null)
            {
                ClusteringResult results = _wrapper.ClusteringResults.FindByCondition(r => r.UserId == user.Id && r.Id == id).FirstOrDefault();
                return Ok(results);
            }
            return NotFound("Such user does not exists");
        }
        [HttpPost("saveData")]
        [AllowAnonymous]
        public async Task<IActionResult> SaveData([FromBody] ClusteringResult result)
        {
            var currentUser = await _wrapper.User.GetByUsername(User.Identity.Name);
            result.User = currentUser;
            await _wrapper.ClusteringResults.Create(result);
            await _wrapper.SaveAsync();
            return Ok(result);
        }
        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<IActionResult> Login([FromBody] UserModel login)
        //{
        //    IActionResult response = Unauthorized();
        //    var token = await this.userService.GetToken(login);
        //    var user = _wrapper.User.FindByCondition(u => u.UserName == login.Username).FirstOrDefault();

        //    if (token != null)
        //    {
        //        response = Ok(new { token, user });
        //    }

        //    return response;
        //}
        //[HttpGet("info")]
        //public async Task<IActionResult> Info()
        //{
        //    string username = User.Identity.Name;
        //    User user = await _wrapper.User.FindByCondition(x => x.UserName == username).FirstOrDefaultAsync();
        //    if (user != null)
        //    {
        //        var userModel = new UserModel() { Username = user.UserName, Password = user.Password };
        //        var token = await this.userService.GetToken(userModel);
        //        return Ok(new { token, user });
        //    }
        //    return NotFound("There is no info");
        //}
    }
}
