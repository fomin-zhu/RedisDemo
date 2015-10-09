using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbBase.Redis;
using BaseModel;
using System.Threading;

namespace WebApp
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //HashOperator hash = new HashOperator();
            //var user = hash.List_GetList<User>("users");
            //if (user.Count > 0)
            //{
            //    hash.List_RemoveAll<User>("users");
            //}

            //var qiujialong = new User
            //{
            //    Id = 1,
            //    Name = "qiujialong",
            //    Job = new Job { Position = ".NET" }
            //};
            //var chenxingxing = new User
            //{
            //    Id = 2,
            //    Name = "chenxingxing",
            //    Job = new Job { Position = ".NET" }
            //};
            //var luwei = new User
            //{
            //    Id = 3,
            //    Name = "luwei",
            //    Job = new Job { Position = ".NET" }
            //};
            //var zhourui = new User
            //{
            //    Id = 4,
            //    Name = "zhourui",
            //    Job = new Job { Position = "Java" }
            //};
            //var userToStore = new List<User> { qiujialong, chenxingxing, luwei, zhourui };
            //hash.List_Add("users", qiujialong);

            //Thread.Sleep(3000);
            //List<User> list = hash.List_Get<User>("users");
            //list[0].Id.ToString();
            //lblShow.Text = "目前共有：" + list[0].Id.ToString() +"人！";
        }

        protected void btnOpenDB_Click(object sender, EventArgs e)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var user = redisClient.As<User>();

                if (user.GetAll().Count > 0)
                    user.DeleteAll();

                var qiujialong = new User
                {
                    Id = user.GetNextSequence(),
                    Name = "qiujialong",
                    Job = new Job { Position = ".NET" }
                };
                var chenxingxing = new User
                {
                    Id = user.GetNextSequence(),
                    Name = "chenxingxing",
                    Job = new Job { Position = ".NET" }
                };
                var luwei = new User
                {
                    Id = user.GetNextSequence(),
                    Name = "luwei",
                    Job = new Job { Position = ".NET" }
                };
                var zhourui = new User
                {
                    Id = user.GetNextSequence(),
                    Name = "zhourui",
                    Job = new Job { Position = "Java" }
                };

                var userToStore = new List<User> { qiujialong, chenxingxing, luwei, zhourui };
                user.StoreAll(userToStore);

                Thread.Sleep(3000);

                lblShow.Text = "目前共有：" + user.GetAll().Count.ToString() + "人！";
            }
        }

        protected void btnSetValue_Click(object sender, EventArgs e)
        {
            using (var redisClient = RedisManager.GetClient())
            {
                var user = redisClient.As<User>();
                if (user.GetAll().Count > 0)
                {
                    var htmlStr = string.Empty;
                    foreach (var u in user.GetAll())
                    {
                        htmlStr += "<li>ID=" + u.Id + "&nbsp;&nbsp;姓名：" + u.Name + "&nbsp;&nbsp;所在部门：" + u.Job.Position + "</li>";
                    }
                    lblPeople.Text = htmlStr;
                }
                lblShow.Text = "目前共有：" + user.GetAll().Count.ToString() + "人！";
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtPosition.Text))
            {
                using (var redisClient = RedisManager.GetClient())
                {
                    var user = redisClient.As<User>();

                    var newUser = new User
                    {
                        Id = user.GetNextSequence(),
                        Name = txtName.Text,
                        Job = new Job { Position = txtPosition.Text }
                    };
                    var userList = new List<User> { newUser };
                    user.StoreAll(userList);

                    if (user.GetAll().Count > 0)
                    {
                        var htmlStr = string.Empty;
                        foreach (var u in user.GetAll())
                        {
                            htmlStr += "<li>ID=" + u.Id + "&nbsp;&nbsp;姓名：" + u.Name + "&nbsp;&nbsp;所在部门：" + u.Job.Position + "</li>";
                        }
                        lblPeople.Text = htmlStr;
                    }
                    lblShow.Text = "目前共有：" + user.GetAll().Count.ToString() + "人！";
                }
            }
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRedisId.Text))
            {
                using (var redisClient = RedisManager.GetClient())
                {
                    var user = redisClient.As<User>();
                    user.DeleteById(txtRedisId.Text);

                    if (user.GetAll().Count > 0)
                    {
                        var htmlStr = string.Empty;
                        foreach (var u in user.GetAll())
                        {
                            htmlStr += "<li>ID=" + u.Id + "&nbsp;&nbsp;姓名：" + u.Name + "&nbsp;&nbsp;所在部门：" + u.Job.Position + "</li>";
                        }
                        lblPeople.Text = htmlStr;
                    }
                    lblShow.Text = "目前共有：" + user.GetAll().Count.ToString() + "人！";
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtScreenPosition.Text))
            {
                using (var redisClient = RedisManager.GetClient())
                {
                    var user = redisClient.As<User>();
                    var userList = user.GetAll().Where(x => x.Job.Position.Contains(txtScreenPosition.Text)).ToList();

                    if (userList.Count > 0)
                    {
                        var htmlStr = string.Empty;
                        foreach (var u in userList)
                        {
                            htmlStr += "<li>ID=" + u.Id + "&nbsp;&nbsp;姓名：" + u.Name + "&nbsp;&nbsp;所在部门：" + u.Job.Position + "</li>";
                        }
                        lblPeople.Text = htmlStr;
                    }
                    lblShow.Text = "筛选后共有：" + userList.Count.ToString() + "人！";
                }
            }
        }
    }
}