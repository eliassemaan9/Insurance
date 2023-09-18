using Dapper;
using InsuranceCore.Interface;
using InsuranceCore.Models;
using InsuranceCore.Models.View;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceCore.Repositories
{

 
    public class ClaimCenterRepository : IClaimCenterRepository
    {
        private readonly IConfiguration configuration;

        private readonly InsuranceContext Icontext;
        public ClaimCenterRepository(InsuranceContext context, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.Icontext = context;

        }
        public async Task<IEnumerable<ClaimCenterView>> GetAllClaimCenterData(DateTime? fromDate,DateTime? toDate,string cardNumber,int? hospitalId)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(configuration.GetValue<string>("ConnectionStrings:Default")))
                {
                    Connection.Open();

                    var Params = new DynamicParameters();
                    Params.Add("@HospitalId", hospitalId);
                    Params.Add("@CardNumber", cardNumber);
                    Params.Add("@FromDate", fromDate);
                    Params.Add("@ToDate", toDate);
                    var Results = await Connection.QueryAsync<ClaimCenterView>("GetClaimCenter", Params, commandType: CommandType.StoredProcedure);
                    Connection.Close();
                    return Results.ToList();
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
        public async Task<Tuple<bool, string>> SaveClaimCenter(int id, string remarks, string medicalCase, DateTime? admissionDate, decimal? estimatedCost, int? insuredId
           , int? hospitalId, int? treatingId, int? statusId, bool? isDeleted)
        {
            try
            {
                using (SqlConnection Connection = new SqlConnection(configuration.GetValue<string>("ConnectionStrings:Default")))
                {

                 
                    Connection.Open();
                    var Params = new DynamicParameters();
                    Params.Add("@id", id);
                    Params.Add("@AdmissionDate", admissionDate);
                    Params.Add("@MedicalCase", medicalCase);
                    Params.Add("@EstimatedCost", estimatedCost);
                    Params.Add("@Remarks", remarks);
                    Params.Add("@InsuredId", insuredId);
                    Params.Add("@HospitalId", hospitalId);
                    Params.Add("@TreatingId", treatingId);
                    Params.Add("@StatusId", statusId);
                    Params.Add("@isDeleted", isDeleted);
                    var Result = await Connection.QueryAsync<bool>("SaveClaimCenter", Params, commandType: CommandType.StoredProcedure);
                    Connection.Close();
                    return new Tuple<bool, string>(true,"claim Center");
                }
            }
            catch (System.Exception ex)
            {
                return new Tuple<bool, string>(false, null);
            }

        }
        public async Task<ClaimCenter> GetClaimCenterById(int id)
        {
            return await Icontext.ClaimCenters.Include(m => m.Insured).Include(m => m.Hospital).Include(m => m.Treating).Include(m => m.Status).Where(m => m.Id == id).SingleOrDefaultAsync();
        }

    }
}
