using System.Collections.Generic;
using AutoMapper;
using Blog.Models.Article;
using Blog.Models.User;
using Blog.Services.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Blog.Pages.Admin.Users
{
    public class UsersModel : PageModel
    {
        [BindProperty]
        public DisplayManyUsersModel Model { get; set; }

        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UsersModel(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

            Model = new DisplayManyUsersModel {Users = new List<DisplayUserModel>()};
        }

        public void OnGet()
        {
            GetRegisteredUsers();
            Page();
        }

        private void GetRegisteredUsers()
        {
            var users = _userService.GetAllUsers();

            foreach (var user in users)
            {
                Model.Users.Add(new DisplayUserModel
                {
                    Id = user.UserId,
                    DisplayName = user.DisplayName
                });
            }
        }
    }
}