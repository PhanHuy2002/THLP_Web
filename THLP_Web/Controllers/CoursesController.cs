﻿using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THLP_Web.Models;
using THLP_Web.ViewModel;

namespace THLP_Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Courses
        [Authorize]
        public ActionResult Create()
        {
            var viewModels = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
            };
            return View(viewModels);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View("Create", viewModel);
            }
            var course = new Course()
            {
                LecturerId = User.Identity.GetUserId(),
                Datetime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place,
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Attending()
        {
            var UserId = User.Identity.GetUserId();
            var Courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == UserId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CourseViewModel
            {
                UpcomingCourses = Courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
    }
}