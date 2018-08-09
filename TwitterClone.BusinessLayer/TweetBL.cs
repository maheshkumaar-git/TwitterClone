using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.DataLayer.Models;

namespace TwitterClone.BusinessLayer
{
    public class TweetBL
    {
        public List<Tweet> GetTweets(string userId)
        {
            List<Tweet> userTweet = new List<Tweet>();
            List<Following> userFollower = new List<Following>();
            List<Tweet> userFollowedTweet = new List<Tweet>();

            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                userTweet = (from a in db.Tweets where a.UserId == userId select a).OrderBy(b => b.CreatedDate).ToList();
                userFollower = (from a in db.Followings where a.UserId == userId select a).ToList();

                foreach (var item in userFollower)
                {
                    userFollowedTweet = (from a in db.Tweets where a.UserId == item.FollowingId select a).OrderBy(b => b.CreatedDate).ToList();

                    foreach (var tweet in userFollowedTweet)
                    {
                        userTweet.Add(tweet);
                    }
                }
            }
            return userTweet;
        }

        public void SaveTweet(Tweet tweet)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                if (tweet.TweetId == 0)
                    db.Tweets.Add(tweet);
                else
                {
                    Tweet twt;
                    twt = GetTweetById(tweet.TweetId);
                    twt.Message = tweet.Message;
                    db.Tweets.Attach(twt);
                    db.Entry(twt).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }

        public void DeleteTweet(int tweetId)
        {
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                Tweet twt;
                twt = GetTweetById(tweetId);
                db.Entry(twt).State = EntityState.Deleted;
                db.SaveChanges();
            }
        }
        public int GetUserTweetCount(string id)
        {
            int count = 0;
            List<Tweet> twt = new List<Tweet>();           
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                twt = (from a in db.Tweets where a.UserId == id select a).OrderBy(x => x.CreatedDate).ToList();

                if (twt != null && twt.Count > 0)
                    count = twt.Count;
            }
            return count;
        }

        public int GetUserFollowingCount(string userId)
        {
            int count = 0;        
            List<Following> userFollowing = new List<Following>();
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                userFollowing = (from a in db.Followings where a.UserId == userId select a).ToList();

                if (userFollowing != null && userFollowing.Count > 0)
                    count = userFollowing.Count;
            }
            return count;
        }

        public int GetUserFollowersCount(string userId)
        {
            int count = 0;
            List<Following> userFollower = new List<Following>();
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                userFollower = (from a in db.Followings where a.FollowingId == userId select a).ToList();

                if (userFollower != null && userFollower.Count > 0)
                    count = userFollower.Count;
            }
            return count;
        }

        public Tweet GetTweetById(int Id)
        {
            Tweet twt = new Tweet();
            using (TwitterCloneEntities db = new TwitterCloneEntities())
            {
                twt = (from a in db.Tweets where a.TweetId == Id select a).SingleOrDefault();
            }
            return twt;
        }
    }
}
