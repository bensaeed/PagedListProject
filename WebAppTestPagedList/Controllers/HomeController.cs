﻿using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppTestPagedList.Models;
using WebAppTestPagedList.Database;
using WebAppTestPagedList.Attributes;

namespace WebAppTestPagedList.Controllers
{
    
    public class HomeController : Controller
    {
        //Learn Link From :
        //https://www.dotnettips.info/post/2501/%D9%BE%DB%8C%D8%A7%D8%AF%D9%87-%D8%B3%D8%A7%D8%B2%DB%8C-%DA%A9%D8%AA%D8%A7%D8%A8%D8%AE%D8%A7%D9%86%D9%87-pagedlist-mvc-%D8%A8%D8%B1%D8%A7%DB%8C-%D8%B5%D9%81%D8%AD%D9%87-%D8%A8%D9%86%D8%AF%DB%8C-%D8%A7%D8%B7%D9%84%D8%A7%D8%B9%D8%A7%D8%AA-%D8%AF%D8%B1-asp-net-mvc
        // GET: Home

        db_TestEntities _db = new db_TestEntities();


        [VisiteLog]
        [My]
        public ActionResult Index(int? page)
        {
            //addData();
            var pageNumber = page ?? 1;
            var posts = PostService.GetAll();
            var result = posts.ToPagedList(pageNumber, 10);
            ViewBag.posts = result;

            var info = _db.tbl_Action.ToList().LastOrDefault();
            string LicationInfo =  
                info.countryCode==null ? "":info.countryCode +
                " : " + info.Country==null ? "":info.Country + " : " +
                info.region == null ? "" : info.region+ " : " +
                info.regionName == null ? "" :info.regionName  +" : " + 
                info.city  == null ? "": info.city + " : ";
            ViewBag.MyInfo =LicationInfo ;
            return View();
        }
        public ActionResult IndexExample2(int? page)
        {
            //addData();
            var pageIndex = (page ?? 1) - 1;
            var pageSize = 10;
            int totalPostCount;
            var posts = PostService.GetAll(pageIndex, pageSize, out totalPostCount);
            var result = new StaticPagedList<Post>(posts, pageIndex + 1, pageSize, totalPostCount);
            ViewBag.posts = result;
            return View(result);
        }
        public void addData()
        {
            for (int i = 1; i <= 100; i++)
            {
                var newItem = new tbl_Post
                {
                    Title = "Mohammad",
                    Body = "bensaeed"
                };
                _db.tbl_Post.Add(newItem);
            }
            _db.SaveChanges();
        }


        public ActionResult DownWeb()
        {

            return View();
        }
    }
}