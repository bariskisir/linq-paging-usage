using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace WebApplication1
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 numberOfItem = 5;
            Int32 pageNumber = 1;
            String strPageNumber = Request.QueryString["page"];
            if (strPageNumber != null)
            {
                pageNumber = Int32.Parse(strPageNumber);
            }
            MakeLinksDynamic(pageNumber);
            List<RootObject> data = MakePaging(numberOfItem, pageNumber, "http://jsonplaceholder.typicode.com/posts");
            gv1.DataSource = data;
            gv1.DataBind();
        }
        private List<RootObject> GetJsonFromUrl(String url)
        {
            using (WebClient wc = new WebClient())
            {
                String json = wc.DownloadString(url);
                List<RootObject> rootObject = Newtonsoft.Json.JsonConvert.DeserializeObject<List<RootObject>>(json);
                return rootObject;
            }
        }
        private List<RootObject> MakePaging(Int32 numberOfItem, Int32 pageNumber, String url)
        {
            List<RootObject> result = GetJsonFromUrl(url)
             .Skip(numberOfItem * (pageNumber - 1))
             .Take(numberOfItem).ToList();
            return result;
        }
        private void MakeLinksDynamic(Int32 page)
        {
            // i dont recommend this method :) You can make it more dynamic, its for just testing
            a1.HRef = "~/default.aspx?page=" + page.ToString();
            a2.HRef = "~/default.aspx?page=" + (page + 1).ToString();
            a3.HRef = "~/default.aspx?page=" + (page + 2).ToString();
            a4.HRef = "~/default.aspx?page=" + (page + 3).ToString();
            a5.HRef = "~/default.aspx?page=" + (page + 4).ToString();
            a1.InnerText = page.ToString();
            a2.InnerText = (page + 1).ToString();
            a3.InnerText = (page + 2).ToString();
            a4.InnerText = (page + 3).ToString();
            a5.InnerText = (page + 4).ToString();
        }
    }
}