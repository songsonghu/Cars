using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace CarsManagePlatform
{
    public partial class SearchCar : System.Web.UI.Page
    {
        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserName"] == null)
                {
                    Response.Redirect("~/Default.aspx");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoomID.Text))
            {
                lbResult.Text = "请输入房间号！";
                return;
            }
            MySqlConnection con = new MySqlConnection(connectionString);
            string roomID = this.txtRoomID.Text;
            MySqlDataAdapter adapter = new MySqlDataAdapter("Select * from tb_cars where RoomID='"+ roomID +"' order by ID desc", con);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            this.GridView1.DataSource = ds.Tables[0];
            this.GridView1.DataBind();
            con.Close();

            if (ds.Tables[0].Rows.Count == 0)
            {
                lbResult.Text = "没有记录！";
            }
        }
    }
}
