using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TwitterClone.BusinessLayer;
using TwitterClone.UI.Models;
using TwitterClone.DataLayer.Models;

namespace TwitterClone.UI.Controllers
{
    public class HomeController : Controller
    {
        UserBL ubl = new UserBL();
        TweetBL tbl = new TweetBL();
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
                return View();
            else
                return RedirectToAction("Login", "User");
        }

        [HttpGet]
        public ActionResult Index(string search)
        {
            string userId = Session["UserId"].ToString();
            string searchVal = Request.QueryString["search"];

            TweetVM tweetData = new TweetVM();
            Search searchResult = new Search();
            if (string.IsNullOrEmpty(searchVal))
            {
                if (Session["UserName"] != null)
                {
                    tweetData.Tweets = tbl.GetTweets(userId);
                    tweetData.Following = tbl.GetUserFollowingCount(userId);
                    tweetData.Followers = tbl.GetUserFollowersCount(userId);
                    tweetData.NoOfTweets = tbl.GetUserTweetCount(userId);
                    return View(tweetData);
                }
                else
                    return RedirectToAction("Login", "User");
            }
            else
            {
                Person person = ubl.SearchUser(searchVal);
                if (person != null)
                    searchResult.showDialog = true;
                return PartialView("_PartialSearchDialog", person);
            }
        }

        [HttpPost]
        public JsonResult ManageTweet(int tweetId, String message)
        {
            Tweet twt = new Tweet();
            twt.UserId = Session["UserId"].ToString();
            twt.Message = message;
            twt.TweetId = tweetId;
            twt.CreatedDate = DateTime.Now;
            tbl.SaveTweet(twt);

            var result = "success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteTweet(int tweetId)
        {
            tbl.DeleteTweet(tweetId);

            var result = "success";
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}