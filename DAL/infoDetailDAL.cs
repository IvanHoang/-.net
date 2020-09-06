using Command;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DAL
{

    public class infoDetailDAL
    {
        DbHelperSQLP DbHelperSQL = new DbHelperSQLP();
        public bool Exists(string id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from infoDetail");
            strSql.Append(" where ");
            strSql.Append(" jobnumber = @jobnumber and  ");
            strSql.Append(" putrecSeqno = @putrecSeqno and  ");
            strSql.Append(" SKU = @SKU and  ");
            strSql.Append(" GoodsName = @GoodsName and  ");
            strSql.Append(" HSCode = @HSCode and  ");
            strSql.Append(" gdsSpcfModelDesc = @gdsSpcfModelDesc and  ");
            strSql.Append(" dcl_QTY = @dcl_QTY and ");
            strSql.Append(" dclUnitcd = @dclUnitcd  and");
            strSql.Append(" law_QTY = @law_QTY and ");
            strSql.Append(" lawfUnitcd = @lawfUnitcd  and");
            strSql.Append(" Volume = @Volume and ");
            strSql.Append(" grossWt = @grossWt and ");
            strSql.Append(" netWt = @netWt and ");
            strSql.Append(" Origin = @Origin and ");
            strSql.Append(" InDate = @InDate  and");
            strSql.Append(" InPep = @InPep and ");
            strSql.Append(" inDep = @inDep and ");
            strSql.Append(" batch = @batch and ");
            strSql.Append(" LPN = @LPN and ");
            strSql.Append(" productionDate = @productionDate and ");
            strSql.Append(" validDate = @validDate and ");
            strSql.Append(" Location = @Location and ");
            strSql.Append(" unitprice = @unitprice and ");
            strSql.Append(" totalamount = @totalamount and  ");
            strSql.Append(" curr = @curr and  ");
            strSql.Append(" goodsStatus = @goodsStatus   ");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(infoDetail model)
        {


            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into infoDetail(");
            strSql.Append("jobnumber,putrecSeqno,SKU,GoodsName,HSCode,gdsSpcfModelDesc,dcl_QTY,dclUnitcd,law_QTY,lawfUnitcd,Volume,grossWt,netWt,Origin,InDate,InPep,inDep,batch,Location,unitprice,totalamount,curr,goodsStatus,LPN,productionDate,validDate");
            strSql.Append(") values (");
            strSql.Append("@jobnumber,@putrecSeqno,@SKU,@GoodsName,@HSCode,@gdsSpcfModelDesc,@dcl_QTY,@dclUnitcd,@law_QTY,@lawfUnitcd,@Volume,@grossWt,@netWt,@Origin,@InDate,@InPep,@inDep,@batch,@Location,@unitprice,@totalamount,@curr,@goodsStatus,@LPN,@productionDate,@validDate");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@jobnumber",SqlDbType.NVarChar,20),
                        new SqlParameter("@putrecSeqno",SqlDbType.Int),
                        new SqlParameter("@SKU",SqlDbType.NVarChar,50),
                        new SqlParameter("@GoodsName",SqlDbType.NVarChar,100),
                        new SqlParameter("@HSCode",SqlDbType.NVarChar,50),
                        new SqlParameter("@gdsSpcfModelDesc",SqlDbType.NVarChar,100),
                        new SqlParameter("@dcl_QTY",SqlDbType.Decimal,13),
                        new SqlParameter("@dclUnitcd",SqlDbType.NVarChar,50),
                        new SqlParameter("@law_QTY",SqlDbType.Decimal,13),
                        new SqlParameter("@lawfUnitcd",SqlDbType.NVarChar,50),
                        new SqlParameter("@Volume",SqlDbType.Decimal,13),
                        new SqlParameter("@grossWt",SqlDbType.Decimal,13),
                        new SqlParameter("@netWt",SqlDbType.Decimal,13),
                        new SqlParameter("@Origin",SqlDbType.NVarChar,50),
                        new SqlParameter("@InDate",SqlDbType.DateTime),
                        new SqlParameter("@InPep",SqlDbType.NVarChar,50),
                        new SqlParameter("@inDep",SqlDbType.NVarChar,50),
                        new SqlParameter("@batch",SqlDbType.NVarChar,50),
                        new SqlParameter("@Location",SqlDbType.NVarChar,50),
                        new SqlParameter("@unitprice",SqlDbType.Decimal,14),
                        new SqlParameter("@totalamount",SqlDbType.Decimal,16),
                        new SqlParameter("@curr",SqlDbType.NVarChar,10),
                        new SqlParameter("@goodsStatus",SqlDbType.NVarChar,10),
                        new SqlParameter("@LPN",SqlDbType.NVarChar,50),
                        new SqlParameter("@productionDate",SqlDbType.DateTime),
                        new SqlParameter("@validDate",SqlDbType.DateTime),
            };

            parameters[0].Value = model.jobnumber;
            parameters[1].Value = model.putrecSeqno;
            parameters[2].Value = model.SKU;
            parameters[3].Value = model.GoodsName;
            parameters[4].Value = model.HSCode;
            parameters[5].Value = model.gdsSpcfModelDesc;
            parameters[6].Value = model.dcl_QTY;
            parameters[7].Value = model.dclUnitcd;
            parameters[8].Value = model.law_QTY;
            parameters[9].Value = model.lawfUnitcd;
            parameters[10].Value = model.Volume;
            parameters[11].Value = model.grossWt;
            parameters[12].Value = model.netWt;
            parameters[13].Value = model.Origin;
            parameters[14].Value = DateTime.Now;
            parameters[15].Value = PubConstant.YongHu;
            parameters[16].Value = PubConstant.BuMen;
            parameters[17].Value = model.batch;
            parameters[18].Value = model.Location;
            parameters[19].Value = model.unitprice;
            parameters[20].Value = model.totalamount;
            parameters[21].Value = model.curr;
            parameters[22].Value = model.goodsStatus;
            parameters[23].Value = model.LPN;
            parameters[24].Value = model.productionDate;
            parameters[25].Value = model.validDate;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(infoDetail model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update infoDetail set ");
            strSql.Append(" jobnumber = @jobnumber , ");
            strSql.Append(" putrecSeqno = @putrecSeqno , ");
            strSql.Append(" SKU = @SKU , ");
            strSql.Append(" GoodsName = @GoodsName , ");
            strSql.Append(" HSCode = @HSCode ,");
            strSql.Append(" gdsSpcfModelDesc = @gdsSpcfModelDesc, ");
            strSql.Append(" dcl_QTY = @dcl_QTY ,");
            strSql.Append(" dclUnitcd = @dclUnitcd ,");
            strSql.Append(" law_QTY = @law_QTY ,");
            strSql.Append(" lawfUnitcd = @lawfUnitcd ,");
            strSql.Append(" Volume = @Volume ,");
            strSql.Append(" grossWt = @grossWt ,");
            strSql.Append(" netWt = @netWt ,");
            strSql.Append(" Origin = @Origin ,");
            strSql.Append(" InDate = @InDate ,");
            strSql.Append(" InPep = @InPep ,");
            strSql.Append(" inDep = @inDep ,");
            strSql.Append(" batch = @batch ,");
            strSql.Append(" Location = @Location ,");
            strSql.Append(" unitprice = @unitprice ,");
            strSql.Append(" totalamount = @totalamount ,");
            strSql.Append(" curr = @curr, ");
            strSql.Append(" goodsStatus = @goodsStatus , ");
            strSql.Append(" LPN = @LPN , ");
            strSql.Append(" productionDate = @productionDate , ");
            strSql.Append(" validDate = @validDate");
            strSql.Append(" where id=@id ");

            SqlParameter[] parameters = {

                        new SqlParameter("@jobnumber",SqlDbType.NVarChar,20),
                        new SqlParameter("@putrecSeqno",SqlDbType.Int),
                        new SqlParameter("@SKU",SqlDbType.NVarChar,50),
                        new SqlParameter("@GoodsName",SqlDbType.NVarChar,100),
                        new SqlParameter("@HSCode",SqlDbType.NVarChar,50),
                        new SqlParameter("@gdsSpcfModelDesc",SqlDbType.NVarChar,100),
                        new SqlParameter("@dcl_QTY",SqlDbType.Decimal,13),
                        new SqlParameter("@dclUnitcd",SqlDbType.NVarChar,50),
                        new SqlParameter("@law_QTY",SqlDbType.Decimal,13),
                        new SqlParameter("@lawfUnitcd",SqlDbType.NVarChar,50),
                        new SqlParameter("@Volume",SqlDbType.Decimal,13),
                        new SqlParameter("@grossWt",SqlDbType.Decimal,13),
                        new SqlParameter("@netWt",SqlDbType.Decimal,13),
                        new SqlParameter("@Origin",SqlDbType.NVarChar,50),
                        new SqlParameter("@InDate",SqlDbType.DateTime),
                        new SqlParameter("@InPep",SqlDbType.NVarChar,50),
                        new SqlParameter("@inDep",SqlDbType.NVarChar,50),
                        new SqlParameter("@batch",SqlDbType.NVarChar,50),
                        new SqlParameter("@Location",SqlDbType.NVarChar,50),
                        new SqlParameter("@unitprice",SqlDbType.Decimal,14),
                        new SqlParameter("@totalamount",SqlDbType.Decimal,16),
                        new SqlParameter("@curr",SqlDbType.NVarChar,10),
                        new SqlParameter("@goodsStatus",SqlDbType.NVarChar,10),
                        new SqlParameter("@LPN",SqlDbType.NVarChar,50),
                        new SqlParameter("@productionDate",SqlDbType.DateTime),
                        new SqlParameter("@validDate",SqlDbType.DateTime),
                        new SqlParameter("@id", SqlDbType.Int,4)


            };


            parameters[0].Value = model.jobnumber;
            parameters[1].Value = model.putrecSeqno;
            parameters[2].Value = model.SKU;
            parameters[3].Value = model.GoodsName;
            parameters[4].Value = model.HSCode;
            parameters[5].Value = model.gdsSpcfModelDesc;
            parameters[6].Value = model.dcl_QTY;
            parameters[7].Value = model.dclUnitcd;
            parameters[8].Value = model.law_QTY;
            parameters[9].Value = model.lawfUnitcd;
            parameters[10].Value = model.Volume;
            parameters[11].Value = model.grossWt;
            parameters[12].Value = model.netWt;
            parameters[13].Value = model.Origin;
            parameters[14].Value = DateTime.Now;
            parameters[15].Value = PubConstant.YongHu;
            parameters[16].Value = PubConstant.BuMen;
            parameters[17].Value = model.batch;
            parameters[18].Value = model.Location;
            parameters[19].Value = model.unitprice;
            parameters[20].Value = model.totalamount;
            parameters[21].Value = model.curr;
            parameters[22].Value = model.goodsStatus;
            parameters[23].Value = model.LPN;
            parameters[24].Value = model.productionDate;
            parameters[25].Value = model.validDate;
            parameters[26].Value = model.id;
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int NextId()
        {
            string sql = "select ISNULL(MAX(id), 0) + 1 as id from infoDetail";
            return Convert.ToInt32(DbHelperSQL.GetSingle(sql));
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from infoDetail ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from infoDetail ");
            strSql.Append(" where ID in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public infoDetail GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,jobnumber,putrecSeqno,SKU,GoodsName,HSCode,gdsSpcfModelDesc,dcl_QTY,dclUnitcd,law_QTY,lawfUnitcd,Volume,grossWt,netWt,Origin,InDate,InPep,inDep,batch,Location,unitprice,totalamount,curr,goodsStatus,LPN,productionDate,validDate  ");
            strSql.Append("  from infoDetail ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
                    new SqlParameter("@id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;


            infoDetail model = new infoDetail();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.jobnumber = ds.Tables[0].Rows[0]["jobnumber"].ToString();
                if (ds.Tables[0].Rows[0]["putrecSeqno"].ToString() != "")
                {
                    model.putrecSeqno = int.Parse(ds.Tables[0].Rows[0]["putrecSeqno"].ToString());
                }
                model.SKU = ds.Tables[0].Rows[0]["SKU"].ToString();
                model.GoodsName = ds.Tables[0].Rows[0]["GoodsName"].ToString();
                model.HSCode = ds.Tables[0].Rows[0]["HSCode"].ToString();
                model.gdsSpcfModelDesc = ds.Tables[0].Rows[0]["gdsSpcfModelDesc"].ToString();
                if (ds.Tables[0].Rows[0]["dcl_QTY"].ToString() != "")
                {
                    model.dcl_QTY = decimal.Parse(ds.Tables[0].Rows[0]["dcl_QTY"].ToString());
                }
                model.dclUnitcd = ds.Tables[0].Rows[0]["dclUnitcd"].ToString();
                if (ds.Tables[0].Rows[0]["law_QTY"].ToString() != "")
                {
                    model.law_QTY = decimal.Parse(ds.Tables[0].Rows[0]["law_QTY"].ToString());
                }
                model.lawfUnitcd = ds.Tables[0].Rows[0]["lawfUnitcd"].ToString();
                if (ds.Tables[0].Rows[0]["grossWt"].ToString() != "")
                {
                    model.grossWt = decimal.Parse(ds.Tables[0].Rows[0]["grossWt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["netWt"].ToString() != "")
                {
                    model.netWt = decimal.Parse(ds.Tables[0].Rows[0]["netWt"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Volume"].ToString() != "")
                {
                    model.Volume = decimal.Parse(ds.Tables[0].Rows[0]["Volume"].ToString());
                }
                model.Origin = ds.Tables[0].Rows[0]["Origin"].ToString();
                if (ds.Tables[0].Rows[0]["InDate"].ToString() != "")
                {
                    model.InDate = DateTime.Parse(ds.Tables[0].Rows[0]["InDate"].ToString());
                }
                model.InPep = ds.Tables[0].Rows[0]["InPep"].ToString();
                model.inDep = ds.Tables[0].Rows[0]["inDep"].ToString();
                model.batch = ds.Tables[0].Rows[0]["batch"].ToString();
                model.Location = ds.Tables[0].Rows[0]["Location"].ToString();
                if (ds.Tables[0].Rows[0]["unitprice"].ToString() != "")
                {
                    model.unitprice = decimal.Parse(ds.Tables[0].Rows[0]["unitprice"].ToString());
                }
                if (ds.Tables[0].Rows[0]["totalamount"].ToString() != "")
                {
                    model.totalamount = decimal.Parse(ds.Tables[0].Rows[0]["totalamount"].ToString());
                }
                model.curr = ds.Tables[0].Rows[0]["curr"].ToString();
                model.goodsStatus = ds.Tables[0].Rows[0]["goodsStatus"].ToString();
                model.LPN=ds.Tables[0].Rows[0]["LPN"].ToString();
                if (ds.Tables[0].Rows[0]["productionDate"].ToString() != "")
                {
                    model.productionDate = DateTime.Parse(ds.Tables[0].Rows[0]["productionDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["validDate"].ToString() != "")
                {
                    model.validDate = DateTime.Parse(ds.Tables[0].Rows[0]["validDate"].ToString());
                }

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from infoDetail T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM infoDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM infoDetail ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
    }
}
