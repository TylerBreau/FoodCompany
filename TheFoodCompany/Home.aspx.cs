using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TheFoodCompany
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FillCategoryList();

            int category;
            if (Int32.TryParse((Request.QueryString["category"] == null ? "-1" : Request.QueryString["category"]), out category))
            {
                FillProductList(category);
            }
            else
            {
                //URL Parameter "category" was not null, and not a valid Integer. Now what?
            }
        }

        /// <summary>
        /// Gets and places the Product List on the web page.
        /// </summary>
        private void FillCategoryList()
        {
            rptCategories.DataSource = DataAccess.SQLAccess.GetData("spGetCategories", DataAccess.SQLAccess.SqlCommandType.StoredProcedure);
            rptCategories.DataBind();
        }

        /// <summary>
        /// Gets and places the Product List on the web page. Pass in -1 to get Featured Products.
        /// </summary>
        /// <param name="category">Product List's Category ID</param>
        private void FillProductList(int category)
        {
            if (category == -1) //Display featured products
            {
                rptProducts.DataSource = DataAccess.SQLAccess.GetData("spGetFeaturedProducts", DataAccess.SQLAccess.SqlCommandType.StoredProcedure);
            }
            else
            {
                List<DataAccess.Parameter> parameters = new List<DataAccess.Parameter>() {
                    new DataAccess.Parameter("@categoryId", category)
                };

                rptProducts.DataSource = DataAccess.SQLAccess.GetData("spGetProductsByCategoryId", DataAccess.SQLAccess.SqlCommandType.StoredProcedure, parameters);
            }

            rptProducts.DataBind();
        }
    }
}