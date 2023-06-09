﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using THLP_Web.DTOs;
using THLP_Web.Models;

namespace THLP_Web.Controllers
{
    public class FollowingController : ApiController
    {
        private readonly ApplicationDbContext _dbContext;
        public FollowingController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Follow(FollowingDto followingDto)
        {
            var UserId = User.Identity.GetUserId();
            if (_dbContext.Followings.Any(f => f.FollowerId == UserId && f.FolloweeId == followingDto.FolloweeId))
                return BadRequest("Following already exist!");
            var following = new Following()
            {
                FollowerId = UserId,
                FolloweeId = followingDto.FolloweeId
            };
            _dbContext.Followings.Add(following);
            _dbContext.SaveChanges();
            return Ok();
        }
    }
}
