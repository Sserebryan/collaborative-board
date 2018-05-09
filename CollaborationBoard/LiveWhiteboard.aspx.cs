using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CollaborationBoard
{
    public partial class LiveWhiteboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //if (!IsPostBack)
            //{
            //    String p = Request.QueryString["p"];
            //    if (string.IsNullOrWhiteSpace(p))
            //    {
            //        Guid g = Guid.NewGuid();
            //        p = Convert.ToBase64String(g.ToByteArray());
            //        p = p.Replace("=", "");
            //        p = p.Replace("+", "");
            //        //ViewData["IsNewGroup"] = true;
            //       // ViewData["url"] = Request.Url.AbsoluteUri.ToString() + "?p=" + p;
                    
            //    }
            //    else
            //    {
            //        ViewData["url"] = Request.Url.AbsoluteUri.ToString();
            //    }

            //    ViewData["GroupName"] = p;
            //    ViewBag.GroupName = p;
            //}
            
        }
    }
}