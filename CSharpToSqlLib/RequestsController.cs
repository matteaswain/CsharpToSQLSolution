using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpToSqlLib
{
    public class RequestsController
    {
        private static Connection connection { get; set; }

        public RequestsController(Connection connection)
        {
            RequestsController.connection = connection;
        }

        private Request FillRequestForSqlRow(SqlDataReader sqldatareader)
        {
            var request = new Request()
            {
                Id = Convert.ToInt32(sqldatareader["Id"]),
                Description = Convert.ToString(sqldatareader["Description"]),
                Justification = Convert.ToString(sqldatareader["Justification"]),
                RejectionReason = Convert.ToString(sqldatareader["RejectionReason"]),
                DeliveryMode = Convert.ToString(sqldatareader["DeliveryMode"]),
                Status = Convert.ToString(sqldatareader["Status"]),
                Total = Convert.ToDecimal(sqldatareader["Total"]),
                UserId = Convert.ToInt32(sqldatareader["UserId"])
            };
            return request;

        }
        private void FillParameterForSql(SqlCommand cmd, Request request)
        {
            cmd.Parameters.AddWithValue("@Description", request.Description);
            cmd.Parameters.AddWithValue("@Justification", request.Justification);
            cmd.Parameters.AddWithValue("@RejectionReason", (object)request.RejectionReason ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@DeliveryMode", request.DeliveryMode);
            cmd.Parameters.AddWithValue("@Status", request.Status);
            cmd.Parameters.AddWithValue("@Total", request.Total);
            cmd.Parameters.AddWithValue("@UserId", request.UserId);
        }

        public List<Request> GetAll()
        {
            var sql = "SELECT * from Requests ;";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            var sqldatareader = cmd.ExecuteReader();
            var requests = new List<Request>();

            while (sqldatareader.Read())
            {
                var request = FillRequestForSqlRow(sqldatareader);
                requests.Add(request);
            }
            sqldatareader.Close();
            foreach (var request in requests)
            {
                GetUserForRequest(request);
            }
            return requests;
        }

        private void GetUserForRequest(Request request)
        {
            var userCtrl = new UsersController(connection);
            request.user = userCtrl.GetByPK(request.UserId);
        }

        public Request GetByPK(int id)
        {
            var sql = $" Select * from Requests Where Id = {id}; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);
            cmd.Parameters.AddWithValue("@Id", id);
            var sqldatareader = cmd.ExecuteReader();

            if (!sqldatareader.HasRows)
            {
                sqldatareader.Close();
                return null;
            }
            sqldatareader.Read();
            var request = FillRequestForSqlRow(sqldatareader);
            sqldatareader.Close();
            return request;

        }

        public bool Create(Request request)
        {
            var sql = $" Insert into Requests " +
                " (Description, Justification, RejectionReason, DeliveryMode, Status, Total, UserId) " +
                " VALUES " +
                "  (@Description, @Justification, @RejectionReason, @DeliveryMode, @Status, @Total, @UserId) ; ";

            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, request);
            var rowsAffected = cmd.ExecuteNonQuery();

            return (rowsAffected == 1);

        }

        public bool Change(Request request)
        {
            var sql = " UPDATE Requests set " +
                " Description = @Description, " +
                " Justification = @Justification, " +
                " RejectionReason = RejectionReason, " +
                " DeliveryMode = @DeliveryMode, " +
                " Status = @ Status, " +
                " Total = @Total, " +
                " UserId = @UserId, " +
                " Where Id = @Id; ";


            var cmd = new SqlCommand(sql, connection.sqlconn);
            FillParameterForSql(cmd, request);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }


        public bool Delete(Request request)
        {
            var sql = " DELETE from Requests where Id = @Id; ";
            var cmd = new SqlCommand(sql, connection.sqlconn);

            cmd.Parameters.AddWithValue("@Id", request.Id);
            var rowsAffected = cmd.ExecuteNonQuery();
            return (rowsAffected == 1);
        }
    }

}
